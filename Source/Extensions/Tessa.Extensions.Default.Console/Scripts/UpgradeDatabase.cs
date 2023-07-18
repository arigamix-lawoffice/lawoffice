using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(UpgradeDatabase))]
    public sealed class UpgradeDatabase
        : ServerConsoleScriptBase
    {
        #region Private Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            var dbScope = this.Container.Resolve<IDbScope>();
            await using var _ = dbScope.Create();
            var schemeService = new DatabaseSchemeService(dbScope, new ServerConfigurationVersionProvider(dbScope));
            var dbms = await dbScope.GetDbmsAsync(cancellationToken);
            var schemeDbmsVersion = await schemeService.ReadSchemeDbmsVersionAsync(cancellationToken);

            var currentDbmsVersion = await dbScope.Db.TryGetDatabaseVersionAsync(cancellationToken);
            if (currentDbmsVersion is null)
            {
                await this.Logger.WriteLineAsync("Unable to determine database version");
                return;
            }
            
            try
            {
                if (dbms == Dbms.PostgreSql && currentDbmsVersion.Major >= 11 && schemeDbmsVersion.Major < 11)
                {
                    // PostgreSQL 11 добавил поддержку Include колонок в индексах
                    await this.Logger.WriteLineAsync("Rebuilding indices with included columns");
                    await schemeService.RebuildIndicesAsync(index => index.IncludedColumns.Count > 0,
                        cancellationToken);
                }

                await this.Logger.WriteLineAsync("All necessary migrations are applied");
            }
            catch
            {
                // Если получили исключение (например, команда была отменена)
                // То выставляем прошлую версию, чтобы при следующем запуске мы снова применили
                // Все необходимые миграции
                try
                {
                    // Не передаём токен отмены на случай, если команда была отменена
                    await schemeService.UpdateSchemeDbmsVersionAsync(schemeDbmsVersion, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    logger.LogException(ex, LogLevel.Warn);
                }
                throw;
            }
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Executes necessary scheme migrations after a database upgrade.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(UpgradeDatabase)} -cs:default");
        }

        #endregion
    }
}
