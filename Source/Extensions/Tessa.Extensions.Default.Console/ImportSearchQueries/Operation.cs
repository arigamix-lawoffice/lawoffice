using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Views.SearchQueries;

namespace Tessa.Extensions.Default.Console.ImportSearchQueries
{
    /// <summary>
    /// Операция импорта рабочих мест.
    /// </summary>
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        private readonly SearchQueryFilePersistent searchQueryFilePersistent;

        private readonly ISearchQueryService searchQueryService;

        /// <inheritdoc />
        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            SearchQueryFilePersistent searchQueryFilePersistent,
            ISearchQueryService searchQueryService)
            : base(logger, sessionManager)
        {
            if (searchQueryFilePersistent == null)
            {
                throw new ArgumentNullException(nameof(searchQueryFilePersistent));
            }

            if (searchQueryService == null)
            {
                throw new ArgumentNullException(nameof(searchQueryService));
            }

            this.searchQueryService = searchQueryService;
            this.searchQueryFilePersistent = searchQueryFilePersistent;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            var files = DefaultConsoleHelper.GetSourceFiles(context.Source, "*.query;*.jquery", throwIfNotFound: false);
            await this.Logger.InfoAsync("Reading search queries from: \"{0}\"", context.Source);
            var models = (await this.searchQueryFilePersistent.ReadAsync(
                files,
                cancellationToken)).ToArray();
            if (!models.Any())
            {
                await this.Logger.InfoAsync("No files in \"{0}\" to import.", context.Source);
                return 0;
            }
            await this.Logger.InfoAsync("Found search queries ({0})", models.Length);
            try
            {
                await this.searchQueryService.ImportAsync(models);
                await this.Logger.InfoAsync("Search queries are imported successfully");
                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing search queries", e);
                return -1;
            }
        }
    }
}