using Prometheus.Client.Collectors;

namespace Prometheus.Client.HealthChecks;

/// <summary>
///     Options for PrometheusHealthCheckPublisher
/// </summary>
public class PrometheusHealthCheckPublisherOptions
{
    public const string DefaultStatusMetricName = "healthcheck_status";
    public const string DefaultDurationMetricName = "healthcheck_duration_seconds";

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
        StatusMetricName = DefaultStatusMetricName;
        DurationMetricName = DefaultDurationMetricName;
    }
}
