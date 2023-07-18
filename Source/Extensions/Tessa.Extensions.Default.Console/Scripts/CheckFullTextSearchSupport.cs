using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(CheckFullTextSearchSupport))]
    public sealed class CheckFullTextSearchSupport :
        ServerConsoleScriptBase
    {
        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            var dbScope = this.Container.Resolve<IDbScope>();
            await using (dbScope.Create())
            {
                DbManager db = dbScope.Db;
                IQueryBuilderFactory builderFactory = dbScope.BuilderFactory;
                var isFullTextSearchEnabled = await db
                    .SetCommand(
                        builderFactory
                            .Select()
                            .IsFullTextSearchEnabled()
                            .Build())
                    .LogCommand()
                    .ExecuteAsync<int>(cancellationToken);

                if (isFullTextSearchEnabled != 1)
                {
                    await this.Logger.ErrorAsync("Full-Text Search is not installed, or a full-text component can't be loaded for your database.");
                    this.Result = -1;
                }
            }
        }


        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Checks whether full text search component is enabled for the database." +
                " Return zero error code if its enabled, or non-zero, if its disabled or other error occured.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(CheckFullTextSearchSupport)}");
        }

        #endregion
    }
}