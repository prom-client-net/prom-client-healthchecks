using Prometheus.Client.Collectors;

namespace Prometheus.Client.HealthChecks;

/// <summary>
///     Options for PrometheusHealthCheckPublisher
/// </summary>
public class PrometheusHealthCheckPublisherOptions
{
    /// <summary>
    ///     Metric name for Status
    /// </summary>
    public string StatusMetricName { get; set; }

    /// <summary>
    ///     Metric name for Duration
    /// </summary>
    public string DurationMetricName { get; set; }

    /// <summary>
    ///     Collector Registry
    /// </summary>
    public ICollectorRegistry? CollectorRegistry { get; set; }

    /// <summary>
    ///     Constructor
    /// </summary>
    public PrometheusHealthCheckPublisherOptions()
    {
        StatusMetricName = "healthcheck_status";
        DurationMetricName = "healthcheck_duration_seconds";
    }
}
