#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Json;
using Tessa.Platform.Maintenance;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace Tessa.Extensions.Default.Console.Maintenance
{
    public sealed class Operation:
        ConsoleOperation<OperationContext>
    {
        #region Fields

        private const string LocalizationPreferencesPrefix = "Localization.";
        private const string DefaultLanguage = nameof(DefaultLanguage);
        
        private readonly IMaintenanceLocalizationStrategy localizationStrategy;
        private readonly IConfigurationManager configurationManager;
        
        #endregion
        
        #region Constructor

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IMaintenanceLocalizationStrategy localizationStrategy,
            IConfigurationManager configurationManager) :
            base(logger, sessionManager)
        {
            this.localizationStrategy = NotNullOrThrow(localizationStrategy);
            this.configurationManager = NotNullOrThrow(configurationManager);
        }

        #endregion
        
        #region Base Overrides

        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(context.Address))
            {
                await this.Logger.ErrorAsync("Incorrect \"wa\" argument value. It must be provided and not empty.");
                return -2;
            }

            if (context.Timeout < 0)
            {
                await this.Logger.ErrorAsync("Incorrect \"wtimeout\" argument value. It must be non negative.");
                return -3;
            }
            
            return context.Command switch
            {
                SupportedCommand.SwitchOn => await this.HandleSwitchOnAsync(context, cancellationToken),
                SupportedCommand.SwitchOff => await this.HandleSwitchOffAsync(context, cancellationToken),
                SupportedCommand.Check => await this.HandleCheckAsync(context, cancellationToken),
                SupportedCommand.Status => await this.HandleStatusAsync(context, cancellationToken),
                SupportedCommand.HealthCheck => await this.HandleHealthCheckAsync(context, cancellationToken),
                _ => -1
            };
        }

        #endregion

        #region Private Methods

        #region Command handlers

        private async Task<int> HandleSwitchOnAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            // getting maintenance settings.
            var maintenanceSettings = this.configurationManager.Configuration.Settings.TryGet<IDictionary<string, object?>>("Maintenance");
            
            var messages = new Dictionary<string, string>();
            GetConfigMessages(messages, maintenanceSettings);

            // user message override default one.
            try
            {
                GetUserMessages(messages, context.RawMessages);
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Invalid \"m\" argument value.", e);
                return -4;
            }
            // load localization library from config
            var localizationLibrary = new Dictionary<string, string>();
            // get needed localization entries
            var neededEntries = new HashSet<string>();
            GetNeededLocalizationEntries(neededEntries, messages);

            var preferences = GetConfigLocalizationPreferences(maintenanceSettings);
            
            try
            {
                await this.LoadNeededLocalizationEntriesAsync(neededEntries, preferences,
                    localizationLibrary, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error while fetching localization entries.", e);
                return -5;
            }
            
            // final check
            if (neededEntries.Count > 0)
            {
                await this.Logger.ErrorAsync("Needed localization entries missed. Couldn't find them in registered sources.");
                return -5;
            }

            // switch rewrite rules to webbi.
            await this.Logger.InfoAsync("Try to switch rewrite rules.");
            try
            {
                // logging command
                await this.Logger.InfoAsync("Sending data");
                await this.Logger.InfoAsync("Messages:{0}{1}", Environment.NewLine, SerializeJson(messages));
                await this.Logger.InfoAsync("Localization library:{0}{1}", Environment.NewLine, SerializeJson(localizationLibrary));
                // request
                await using var proxyFactory = this.GetWebProxyFactory(context.Address, context.Timeout);
                await using var proxy = await proxyFactory.UseProxyAsync<WebbiWebProxy>(cancellationToken: cancellationToken);
                await proxy.SwitchModeAsync(true, messages, localizationLibrary, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Rewrite rules switching error.", e);
                return -6;
            }
            
            await this.Logger.InfoAsync("Rewrite rules successfully switched.");
            
            return 0;
        }

        private async Task<int> HandleSwitchOffAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            // switch rewrite rules to webbi.
            await this.Logger.InfoAsync("Try to switch rewrite rules.");
            try
            {
                await using var proxyFactory = this.GetWebProxyFactory(context.Address, context.Timeout);
                await using var proxy = await proxyFactory.UseProxyAsync<WebbiWebProxy>(cancellationToken: cancellationToken);
                await proxy.SwitchModeAsync(false, cancellationToken: cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Rewrite rules switching error.", e);
                return -6;
            }
            
            await this.Logger.InfoAsync("Rewrite rules successfully switched.");
            
            return 0;
        }

        private async Task<int> HandleCheckAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            // check that we can switch rewrite rules.
            await this.Logger.InfoAsync("Check availability of rewrite rules switching.");
            try
            {
                await using var proxyFactory = this.GetWebProxyFactory(context.Address, context.Timeout);
                await using var proxy = await proxyFactory.UseProxyAsync<WebbiWebProxy>(cancellationToken: cancellationToken);
                await proxy.CheckCanSwitchModeAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Check availability of rewrite rules error.", e);
                return -7;
            }
            await this.Logger.InfoAsync("Rules switching is available.");
            await this.Logger.WriteLineAsync("ok");
            
            return 0;
        }
        
        private async Task<int> HandleStatusAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            // get maintenance state from webbi.
            await this.Logger.InfoAsync("Checking current maintenance status of paired service.");
            bool maintenance;
            try
            {
                await using var proxyFactory = this.GetWebProxyFactory(context.Address, context.Timeout);
                await using var proxy = await proxyFactory.UseProxyAsync<WebbiWebProxy>(cancellationToken: cancellationToken);
                maintenance = await proxy.GetStatusAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Checking current maintenance status error.", e);
                return -8;
            }
            await this.Logger.WriteLineAsync(maintenance ? "on" : "off");
            
            return 0;
        }
        
        private async Task<int> HandleHealthCheckAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            // webbi health check.
            await this.Logger.InfoAsync("Checking health of paired service.");
            string? healthState;
            try
            {
                await using var proxyFactory = this.GetWebProxyFactory(context.Address, context.Timeout);
                await using var proxy = await proxyFactory.UseProxyAsync<WebbiWebProxy>(cancellationToken: cancellationToken);
                healthState = await proxy.HealthCheckAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Checking health of paired service error.", e);
                return -9;
            }
            await this.Logger.WriteLineAsync(healthState);
            
            return 0;
        }
        
        #endregion
        
        private static void GetUserMessages(IDictionary<string, string> messages, IList<string>? rawMessages)
        {
            if (rawMessages is null)
            {
                return;
            }
            foreach (var rawMessage in rawMessages)
            {
                int idx = rawMessage.IndexOf("=", StringComparison.Ordinal);
                if (idx < 1)
                {
                    throw new FormatException(
                        $"Invalid format of a given message \"{rawMessage}\". Required format <messageName>=<messageValue>.");
                }
                messages[rawMessage[..idx]] = rawMessage[(idx+1)..].Replace("\\n","\n", StringComparison.OrdinalIgnoreCase);
            }
        }

        private static void GetConfigMessages(IDictionary<string, string> messages, IDictionary<string, object?>? settings)
        {
            var data = settings?.TryGet<IDictionary<string, object?>>("Messages");
            if (data is null)
            {
                return;
            }
            foreach (var pair in data)
            {
                if (pair.Value is string value)
                {
                    messages[pair.Key] = value;
                }
            }
        }
        
        private static IDictionary<string, object?> GetConfigLocalizationPreferences(IDictionary<string, object?>? settings)
        {
            var preferences = new Dictionary<string, object?>();
            if (settings is null)
            {
                return preferences;
            }

            int idx = LocalizationPreferencesPrefix.Length;
            foreach (var pair in settings)
            {
                if (pair.Key.StartsWith(LocalizationPreferencesPrefix))
                {
                    preferences[pair.Key[idx..]] = pair.Value;
                }
            }

            return preferences;
        }
        
        private static void GetNeededLocalizationEntries(
            ISet<string> neededEntries, IDictionary<string, string> messages)
        {
            foreach (var pair in messages)
            {
                var message = pair.Value;
                if (string.IsNullOrWhiteSpace(message))
                {
                    continue;
                }

                var names = LocalizationManager.TryGetNames(message);
                foreach (var name in names)
                {
                    neededEntries.Add(name);
                }
            }
        }

        private async Task LoadNeededLocalizationEntriesAsync(
            HashSet<string> neededEntries,
            IDictionary<string, object?> preferences,
            IDictionary<string, string> localizationLibrary,
            CancellationToken cancellationToken = default)
        {
            var defaultLanguage = preferences.TryGet<string>(DefaultLanguage) ?? "en";
            var entries = await this.localizationStrategy.GetEntriesAsync(neededEntries, preferences, cancellationToken);
            foreach (var entry in entries)
            {
                foreach (var entryString in entry.Strings)
                {
                    var lang = entryString.Culture.TwoLetterISOLanguageName;
                    var name = GetLocalizationLibraryEntryName(lang, entry.Name, defaultLanguage);
                    localizationLibrary[name] = entryString.Value ?? string.Empty;
                }
                // exclude fetched entry
                neededEntries.Remove(entry.Name);
            }
        }

        private static string GetLocalizationLibraryEntryName(string lang, string name, string defaultLang) =>
            lang == defaultLang || lang == "iv" ? name : $"{lang}:{name}";
        
        private IWebProxyFactory GetWebProxyFactory(string? address, int timeout)
        {
            var connectionSettings = ConnectionSettings.ParseFromConfigurationSettings(this.configurationManager.Configuration.Settings, string.Empty, address);

            if (timeout >= 0)
            {
                TimeSpan timeoutSpan = timeout == 0 ? Timeout.InfiniteTimeSpan : TimeSpan.FromSeconds(timeout);

                connectionSettings.OpenTimeout = timeoutSpan;
                connectionSettings.SendTimeout = timeoutSpan;
                connectionSettings.CloseTimeout = timeoutSpan;
            }
            
            var httpClientPool = new HttpClientPool(connectionSettings);
            return new WebProxyFactory(connectionSettings, httpClientPool: httpClientPool);
        }

        private static string SerializeJson(object? value, bool indented = true)
        {
            var serializer = indented
                ? TessaSerializer.Create(c => c.Formatting = JsonFormatting.Indented)
                : TessaSerializer.Json;

            using var writer = new StringWriter(CultureInfo.InvariantCulture);
            serializer.Serialize(writer, value);
            return writer.ToString();
        }
        
        #endregion
    }
}
