using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.CheckCommand
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            bool checkHealth,
            string address,
            string instanceName,
            int seconds,
            bool queit)
        {
            if (address?.Length > 0)
            {
                address = address.Trim();

                if (address.Length > 0)
                {
                    if (address.EndsWith('/'))
                    {
                        address = address[..^1];
                    }

                    if (!address.Contains("://", StringComparison.Ordinal))
                    {
                        address = "https://" + address;
                    }
                }
                else
                {
                    // используем значение из конфига
                    address = null;
                }
            }

            if (string.IsNullOrEmpty(instanceName))
            {
                // используем значение из конфига
                instanceName = null;
            }

            var connectionSettings = ConnectionSettings.ParseFromConfigurationSettings(ConfigurationManager.Settings, instanceName, address);

            // отрицательное значение seconds использует таймаут по умолчанию в соответствии с конфигурационным файлом
            if (seconds >= 0)
            {
                TimeSpan timeoutSpan = seconds == 0 ? Timeout.InfiniteTimeSpan : TimeSpan.FromSeconds(seconds);

                connectionSettings.OpenTimeout = timeoutSpan;
                connectionSettings.SendTimeout = timeoutSpan;
                connectionSettings.CloseTimeout = timeoutSpan;
            }

            await logger.InfoAsync("Retrieving {0} data from {1}", checkHealth ? "health" : "check", connectionSettings.BaseAddress);
            string result;

            try
            {
                await using var httpClientPool = new HttpClientPool(connectionSettings);
                await using var proxies = new WebProxyFactory(connectionSettings, httpClientPool: httpClientPool);
                await using var proxy = await proxies.UseProxyAsync<CheckWebProxy>();

                result = await proxy.GetAsync(checkHealth);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync($"Failed to retrieve {(checkHealth ? "health" : "check")} data from {connectionSettings.BaseAddress}", ex);
                return -1;
            }

            await logger.WriteAsync(result);
            return 0;
        }
    }
}
