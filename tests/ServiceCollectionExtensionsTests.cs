using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Collectors;
using Xunit;

namespace Prometheus.Client.HealthChecks.Tests;

public class ServiceCollectionExtensionsTests
{
    private const string _defaultStatusMetricName = "healthcheck_status";
    private const string _defaultDurationMetricName = "healthcheck_duration_seconds";

    [Fact]
    public void AddPrometheusHealthCheckPublisher()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ICollectorRegistry, CollectorRegistry>();
        services.AddPrometheusHealthCheckPublisher();
        var sp = services.BuildServiceProvider();
        var publisher = sp.GetService<IHealthCheckPublisher>();
        Assert.NotNull(publisher);
    }

    [Fact]
    public void AddPrometheusHealthCheckPublisher_With_Default_CollectorRegistry()
    {
        var services = new ServiceCollection();
        services.AddPrometheusHealthCheckPublisher();

        var sp = services.BuildServiceProvider();
        var publisher = sp.GetService<IHealthCheckPublisher>();
        Assert.NotNull(publisher);

        Assert.True(Metrics.DefaultCollectorRegistry.TryGet(_defaultStatusMetricName, out var statusCollector));
        Assert.True(Metrics.DefaultCollectorRegistry.TryGet(_defaultDurationMetricName, out var durationCollector));

        // Cleanup
        Metrics.DefaultCollectorRegistry?.Remove(statusCollector);
        Metrics.DefaultCollectorRegistry?.Remove(durationCollector);
    }

    [Fact]
    public void AddPrometheusHealthCheckPublisher_With_Custom_CollectorRegistry()
    {
        var registry = new CollectorRegistry();

        var services = new ServiceCollection();
        services.AddPrometheusHealthCheckPublisher(new PrometheusHealthCheckPublisherOptions
        {
            CollectorRegistry = registry
        });

        var sp = services.BuildServiceProvider();
        var publisher = sp.GetService<IHealthCheckPublisher>();
        Assert.NotNull(publisher);

        Assert.True(registry.TryGet(_defaultStatusMetricName, out _));
        Assert.True(registry.TryGet("healthcheck_duration_seconds", out _));

        Assert.False(Metrics.DefaultCollectorRegistry.TryGet(_defaultStatusMetricName, out _));
        Assert.False(Metrics.DefaultCollectorRegistry.TryGet(_defaultDurationMetricName, out _));
    }

    [Fact]
    public void AddPrometheusHealthCheckPublisher_With_DI_CollectorRegistry()
    {
        var registry = new CollectorRegistry();

        var services = new ServiceCollection();
        services.AddSingleton<ICollectorRegistry>(registry);
        services.AddPrometheusHealthCheckPublisher();

        var sp = services.BuildServiceProvider();
        var publisher = sp.GetService<IHealthCheckPublisher>();
        Assert.NotNull(publisher);

        Assert.True(registry.TryGet(_defaultStatusMetricName, out _));
        Assert.True(registry.TryGet(_defaultDurationMetricName, out _));

        Assert.False(Metrics.DefaultCollectorRegistry.TryGet(_defaultStatusMetricName, out _));
        Assert.False(Metrics.DefaultCollectorRegistry.TryGet(_defaultDurationMetricName, out _));
    }
}
