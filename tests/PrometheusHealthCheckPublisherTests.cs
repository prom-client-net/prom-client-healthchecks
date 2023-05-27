using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Collectors;
using Xunit;

namespace Prometheus.Client.HealthChecks.Tests;

public class PrometheusHealthCheckPublisherTests
{
    private readonly ICollectorRegistry _registry;
    private readonly PrometheusHealthCheckPublisherOptions _options;

    public PrometheusHealthCheckPublisherTests()
    {
        _registry = new CollectorRegistry();
        _options = new PrometheusHealthCheckPublisherOptions
        {
            CollectorRegistry = _registry
        };
    }

    [Fact]
    public void Publisher_Register_Metrics()
    {
        var publisher = new PrometheusHealthCheckPublisher(_options);
        Assert.True(_registry.TryGet(PrometheusHealthCheckPublisherOptions.DefaultStatusMetricName, out _));
        Assert.True(_registry.TryGet(PrometheusHealthCheckPublisherOptions.DefaultDurationMetricName, out _));
    }

    [Theory]
    [InlineData("sm1", "dm1")]
    [InlineData("sm2", "dm3")]
    [InlineData("sm4", "dm5")]
    public void Publisher_Register_Metrics_With_CustomNames(string statusMetricName, string durationMetricName)
    {
        _options.StatusMetricName = statusMetricName;
        _options.DurationMetricName = durationMetricName;

        var publisher = new PrometheusHealthCheckPublisher(_options);
        Assert.True(_registry.TryGet(statusMetricName, out _));
        Assert.True(_registry.TryGet(durationMetricName, out _));
    }

    [Theory]
    [InlineData("key1", HealthStatus.Healthy, 1)]
    [InlineData("key2", HealthStatus.Healthy, 2)]
    [InlineData("k4", HealthStatus.Unhealthy, 3)]
    [InlineData("key", HealthStatus.Degraded, 1)]
    public async Task Publisher_Publish_Correct_Result(string key, HealthStatus status, int durationSec)
    {
        const string tmpl = @"# HELP [[durationMetricName]] Shows duration of the health check execution in seconds
# TYPE [[durationMetricName]] gauge
[[durationMetricName]]{name=""[[key]]""} [[duration]]
# HELP [[statusMetricName]] Shows raw health check status (0 = Unhealthy, 1 = Degraded, 2 = Healthy)
# TYPE [[statusMetricName]] gauge
[[statusMetricName]]{name=""[[key]]""} [[status]]
";
        var expected = tmpl
            .Replace("[[durationMetricName]]", PrometheusHealthCheckPublisherOptions.DefaultDurationMetricName)
            .Replace("[[statusMetricName]]", PrometheusHealthCheckPublisherOptions.DefaultStatusMetricName)
            .Replace("[[key]]", key)
            .Replace("[[status]]", ((int)status).ToString())
            .Replace("[[duration]]", durationSec.ToString());

        var publisher = new PrometheusHealthCheckPublisher(_options);

        var entries = new Dictionary<string, HealthReportEntry>
        {
            { key, new HealthReportEntry(status, string.Empty, TimeSpan.FromSeconds(durationSec), null, null) }
        };
        var report = new HealthReport(entries, TimeSpan.FromSeconds(1));
        await publisher.PublishAsync(report, CancellationToken.None);

        using var stream = new MemoryStream();
        await ScrapeHandler.ProcessAsync(_registry, stream);

        var actual = Encoding.UTF8.GetString(stream.ToArray());
        Assert.Equal(expected, actual);
    }
}
