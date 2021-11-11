# Prometheus.Client.HealthChecks

[![NuGet](https://img.shields.io/nuget/v/Prometheus.Client.HealthChecks.svg)](https://www.nuget.org/packages/Prometheus.Client.HealthChecks)
[![NuGet](https://img.shields.io/nuget/dt/Prometheus.Client.HealthChecks.svg)](https://www.nuget.org/packages/Prometheus.Client.HealthChecks)
[![Gitter](https://img.shields.io/gitter/room/PrometheusClientNet/community.svg)](https://gitter.im/PrometheusClientNet/community)
[![CI](https://github.com/PrometheusClientNet/Prometheus.Client.HealthChecks/workflows/CI/badge.svg)](https://github.com/PrometheusClientNet/Prometheus.Client.HealthChecks/actions?query=workflow%3ACI)
[![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) 

## Installation
```shell
dotnet add package Prometheus.Client.HealthChecks
```

## Quick start

```c#
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddHealthChecks()
        .WriteToPrometheus(); // just add this line
}
```

Example [here](https://github.com/PrometheusClientNet/Prometheus.Client.Examples/tree/master/HealthChecks/HealthChecks_3.1)

## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/PrometheusClientNet/Prometheus.Client.HealthChecks/issues).
* You can grab the source code at the package's [git repository](https://github.com/PrometheusClientNet/Prometheus.Client.HealthChecks).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
