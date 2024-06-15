using System;

namespace Prometheus.Client.HealthChecks.Tests;

public static class Extensions
{
    private const string _unix = "\n";
    private const string _nonUnix = "\r\n";

    public static string ToUnixLineEndings(this string s)
    {
        return Environment.NewLine == _unix ? s : s.Replace(_nonUnix, _unix);
    }
}
