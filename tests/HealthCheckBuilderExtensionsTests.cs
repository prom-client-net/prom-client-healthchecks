using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client.Collectors;
using Xunit;

namespace Prometheus.Client.HealthChecks.Tests;

public class HealthCheckBuilderExtensionsTests
{
    [Fact]
    public void WriteToPrometheus()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ICollectorRegistry, CollectorRegistry>();
        services.AddHealthChecks().WriteToPrometheus();

        var sp = services.BuildServiceProvider();
        var publisher = sp.GetService<IHealthCheckPublisher>();
        Assert.NotNull(publisher);
    }
}
