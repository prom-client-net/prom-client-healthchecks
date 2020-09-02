using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Prometheus.Client.HealthChecks
{
    /// <summary>
    /// 
    /// </summary>
    public static class HealthCheckBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IHealthChecksBuilder WriteToPrometheus(this IHealthChecksBuilder builder, PrometheusHealthCheckPublisherOptions? options = null)
        {
            options ??= new PrometheusHealthCheckPublisherOptions();
            options.CollectorRegistry ??= Metrics.DefaultCollectorRegistry; // todo: add DI

            builder.Services.AddSingleton<IHealthCheckPublisher, PrometheusHealthCheckPublisher>(provider => new PrometheusHealthCheckPublisher(options));

            return builder;
        }
    }
}
