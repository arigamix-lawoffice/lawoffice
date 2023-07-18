#nullable enable

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.Maintenance
{
    public sealed class WebbiWebProxy :
        WebProxy
    {
        #region Constructor

        public WebbiWebProxy() :
            base(string.Empty, string.Empty)
        {}

        #endregion
        
        #region Public Methods

        public async Task SwitchModeAsync(
            bool maintenance,
            IDictionary<string, string>? messages = null,
            IDictionary<string, string>? localization = null,
            CancellationToken cancellationToken = default)
        {
            var data = new Dictionary<string, object?>
            {
                { "maintenance", maintenance ? "on" : "off" }
            };
            
            if (messages is not null)
            {
                data["messages"] = messages;
            }

            if (localization is not null)
            {
                data["localization"] = localization;
            }
            
            var result = await this.SendAsync<string>(
                HttpMethod.Post,
                "switch",
                RequestFlags.OmitInstanceInUri | RequestFlags.Json,
                cancellationToken,
                parameters: new object[]
                {
                    data
                });
            if (!string.Equals(result?.Trim(), "ok", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Couldn't switch mode. Reason: {result}");
            }
        }

        public async Task CheckCanSwitchModeAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.SendAsync<string>(
                HttpMethod.Get,
                "check",
                RequestFlags.OmitInstanceInUri,
                cancellationToken);
            if (!string.Equals(result?.Trim(), "ok", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Couldn't switch mode. Reason: {result}");
            }
        }

        public Task<string?> HealthCheckAsync(CancellationToken cancellationToken = default) =>
            this.SendAsync<string>(
                HttpMethod.Get,
                "hcheck",
                RequestFlags.OmitInstanceInUri,
                cancellationToken);

        public async Task<bool> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.SendAsync<IDictionary<string, object?>>(
                HttpMethod.Get,
                "mode",
                RequestFlags.OmitInstanceInUri,
                cancellationToken);
            return result is not null && result.TryGet<bool>("maintenance");
        }
        
        #endregion
    }
}
