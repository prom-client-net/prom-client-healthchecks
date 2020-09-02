using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Abstractions;

namespace Prometheus.Client.HealthChecks
{
    /// <summary>
    ///     Publish Health Check statuses as Prometheus metrics.
    /// </summary>
    public class PrometheusHealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly IMetricFamily<IGauge, ValueTuple<string>> _status;
        private readonly IMetricFamily<IGauge, ValueTuple<string>> _duration;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options">Options for PrometheusHealthCheckPublisher</param>
        public PrometheusHealthCheckPublisher(PrometheusHealthCheckPublisherOptions options)
        {
            var metricFactory = new MetricFactory(options.CollectorRegistry);

            _status = metricFactory.CreateGauge(options.StatusMetricName,
                "Shows raw health check status (0 = Unhealthy, 1 = Degraded, 2 = Healthy)", "name");
            _duration = metricFactory.CreateGauge(options.DurationMetricName,
                "Shows duration of the health check execution in seconds", "name");
        }

        /// <inheritdoc />
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            foreach (var entry in report.Entries)
            {
                _status.WithLabels(entry.Key).Set((double)entry.Value.Status);
                _duration.WithLabels(entry.Key).Set(entry.Value.Duration.TotalSeconds);
            }

            return Task.CompletedTask;
        }
    }
}
