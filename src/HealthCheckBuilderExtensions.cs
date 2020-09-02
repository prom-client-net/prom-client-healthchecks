using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Collectors.Abstractions;

namespace Prometheus.Client.HealthChecks
{
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
        public static IHealthChecksBuilder WriteToPrometheus(this IHealthChecksBuilder builder, PrometheusHealthCheckPublisherOptions? options = null)
        {
            options ??= new PrometheusHealthCheckPublisherOptions();

            builder.Services.AddSingleton<IHealthCheckPublisher, PrometheusHealthCheckPublisher>(provider =>
            {
                options.CollectorRegistry
                    ??= provider.GetService<ICollectorRegistry>()
                        ?? Metrics.DefaultCollectorRegistry;
                return new PrometheusHealthCheckPublisher(options);
            });

            return builder;
        }
    }
}
