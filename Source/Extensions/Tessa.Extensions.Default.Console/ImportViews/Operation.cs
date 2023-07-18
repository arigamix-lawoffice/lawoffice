using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Views;
using Tessa.Views.Parser.Serialization;

namespace Tessa.Extensions.Default.Console.ImportViews
{
    /// <summary>
    ///     Операция импорта представлений
    /// </summary>
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        private readonly ViewFilePersistent viewFilePersistent;

        private readonly ITessaViewService viewService;

        /// <inheritdoc />
        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            ITessaViewService viewService,
            ViewFilePersistent viewFilePersistent)
            : base(logger, sessionManager)
        {
            if (viewService == null)
            {
                throw new ArgumentNullException("viewService");
            }

            if (viewFilePersistent == null)
            {
                throw new ArgumentNullException("viewFilePersistent");
            }

            this.viewService = viewService;
            this.viewFilePersistent = viewFilePersistent;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            await this.Logger.InfoAsync(
                "Importing views from \"{0}\" with options: clear views = {1}, import roles = {2}",
                context.Source,
                context.ClearViews,
                context.ImportRoles);

            TessaViewModel[] files = (await this.viewFilePersistent.ReadAsync(
                context.Source,
                async (fileName, e, ct) =>
                    {
                        await this.Logger.LogExceptionAsync(string.Format("Error reading file: \"{0}\"", fileName), e);
                        return true;
                    },
                cancellationToken)).ToArray();

            if (!files.Any())
            {
                await this.Logger.InfoAsync("No files in \"{0}\" to import.", context.Source);
                return 0;
            }

            await this.Logger.InfoAsync("Found views ({0})", files.Length);
            var request = new ImportTessaViewRequest { ImportRoles = context.ImportRoles, NeedClear = context.ClearViews, Models = files };

            try
            {
                await this.viewService.ImportViewsAsync(request, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing views", e);
                return -1;
            }

            await this.Logger.InfoAsync("Views are imported successfully");
            return 0;
        }
    }
}