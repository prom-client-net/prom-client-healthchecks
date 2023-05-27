using System;
using Microsoft.Extensions.DependencyInjection;

namespace Prometheus.Client.HealthChecks;

/// <summary>
///     Extensions for IHealthChecksBuilder
/// </summary>
public static class HealthCheckBuilderExtensions
{
    /// <summary>
    ///     Write data to Prometheus
    /// </summary>
    /// <param name="builder">IHealthChecksBuilder</param>
    /// <param name="options">PrometheusHealthCheckPublisherOptions</param>
    [Obsolete("Use AddPrometheusHealthCheckPublisher(this IServiceCollection services, PrometheusHealthCheckPublisherOptions? options = null) instead.")]
    public static IHealthChecksBuilder WriteToPrometheus(this IHealthChecksBuilder builder, PrometheusHealthCheckPublisherOptions? options = null)
    {
        builder.Services.AddPrometheusHealthCheckPublisher(options);
        return builder;
    }
}
