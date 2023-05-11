using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Collectors;

namespace Prometheus.Client.HealthChecks;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Write data to Prometheus
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="options">PrometheusHealthCheckPublisherOptions</param>
    public static IServiceCollection AddPrometheusHealthCheckPublisher(this IServiceCollection services, PrometheusHealthCheckPublisherOptions? options = null)
    {
        options ??= new PrometheusHealthCheckPublisherOptions();

        services.AddSingleton<IHealthCheckPublisher, PrometheusHealthCheckPublisher>(provider =>
        {
            options.CollectorRegistry ??= provider.GetService<ICollectorRegistry>() ?? Metrics.DefaultCollectorRegistry;
            return new PrometheusHealthCheckPublisher(options);
        });

        return services;
    }
}
