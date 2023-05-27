# Prometheus.Client.HealthChecks

[![ci](https://img.shields.io/github/actions/workflow/status/prom-client-net/prom-client-healthchecks/ci.yml?branch=main&label=ci&logo=github&style=flat-square)](https://github.com/prom-client-net/prom-client-healthchecks/actions/workflows/ci.yml)
[![nuget](https://img.shields.io/nuget/v/Prometheus.Client.HealthChecks?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.HealthChecks)
[![nuget](https://img.shields.io/nuget/dt/Prometheus.Client.HealthChecks?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.HealthChecks)
[![codecov](https://img.shields.io/codecov/c/github/prom-client-net/prom-client-healthchecks?logo=codecov&style=flat-square)](https://app.codecov.io/gh/prom-client-net/prom-client-healthchecks)
[![codefactor](https://img.shields.io/codefactor/grade/github/prom-client-net/prom-client-healthchecks?logo=codefactor&style=flat-square)](https://www.codefactor.io/repository/github/prom-client-net/prom-client-healthchecks)
[![license](https://img.shields.io/github/license/prom-client-net/prom-client-healthchecks?style=flat-square)](https://github.com/prom-client-net/prom-client-healthchecks/blob/main/LICENSE)

## Installation

```sh
dotnet add package Prometheus.Client.HealthChecks
```

## Quick start

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks();
    services.AddPrometheusHealthCheckPublisher();
}
```

Example [here](https://github.com/prom-client-net/prom-examples)

## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/prom-client-net/prom-client-healthchecks/issues).
* You can grab the source code at the package's [git repository](https://github.com/prom-client-net/prom-client-healthchecks).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
