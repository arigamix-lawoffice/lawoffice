#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    public interface IOnlyOfficeSettings
    {
        string? ConverterUrl { get; }

        string? DocumentBuilderPath { get; }

        string? WebApiBasePath { get; }

        int TokenLifetimePeriod { get; }

        TimeSpan LoadTimeout { get; }
    }
}
