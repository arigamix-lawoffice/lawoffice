using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Views;
using Tessa.Views.Parser.Serialization;
using Tessa.Views.Workplaces;

namespace Tessa.Extensions.Default.Console.ImportWorkplaces
{
    /// <summary>
    ///     Операция импорта рабочих мест
    /// </summary>
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        private readonly WorkplaceFilePersistent workplaceFilePersistent;

        private readonly ITessaWorkplaceService workplaceService;

        /// <inheritdoc />
        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            WorkplaceFilePersistent workplaceFilePersistent,
            ITessaWorkplaceService workplaceService)
            : base(logger, sessionManager)
        {
            if (workplaceFilePersistent == null)
            {
                throw new ArgumentNullException("workplaceFilePersistent");
            }

            if (workplaceService == null)
            {
                throw new ArgumentNullException("workplaceService");
            }

            this.workplaceFilePersistent = workplaceFilePersistent;
            this.workplaceService = workplaceService;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            await this.Logger.InfoAsync("Reading workplaces from: \"{0}\"", context.Source);

            TessaExchangeWorkplaceModel[] models = (await this.workplaceFilePersistent.ReadAsync(
                context.Source,
                async (fileName, e, ct) =>
                {
                    await this.Logger.LogExceptionAsync(string.Format("Error reading file: \"{0}\"", fileName), e);
                    return true;
                },
                cancellationToken)).ToArray();

            if (!models.Any())
            {
                await this.Logger.InfoAsync("No files in \"{0}\" to import.", context.Source);
                return 0;
            }

            await this.Logger.InfoAsync("Found workplaces ({0})", models.Length);
            var request = new ImportWorkplaceModelRequest
            {
                Models = models,
                ImportViews = context.ImportViews,
                ImportRoles = context.ImportRoles,
                ImportSearchQueries = context.ImportSearchQueries,
                NeedClear = context.ClearWorkplaces
            };

            try
            {
                await this.workplaceService.ImportWorkplacesAsync(request, cancellationToken);
                await this.Logger.InfoAsync("Workplaces are imported successfully");
                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing workplaces", e);
                return -1;
            }
        }
    }
}