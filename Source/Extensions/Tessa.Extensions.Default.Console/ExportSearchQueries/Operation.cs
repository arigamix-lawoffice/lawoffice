using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Views.SearchQueries;

namespace Tessa.Extensions.Default.Console.ExportSearchQueries
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            SearchQueryFilePersistent searchQueryFilePersistent,
            ISearchQueryService searchQueryService)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.searchQueryFilePersistent = searchQueryFilePersistent ?? throw new ArgumentNullException(nameof(searchQueryFilePersistent));
            this.searchQueryService = searchQueryService ?? throw new ArgumentNullException(nameof(searchQueryService));
        }

        #endregion

        #region Fields

        private readonly ISearchQueryService searchQueryService;

        private readonly SearchQueryFilePersistent searchQueryFilePersistent;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            int exportedCount = 0;
            int notFoundCount = 0;

            try
            {
                string exportPath = DefaultConsoleHelper.NormalizeFolderAndCreateIfNotExists(context.OutputFolder);
                if (string.IsNullOrEmpty(exportPath))
                {
                    exportPath = Directory.GetCurrentDirectory();
                }

                if (context.ClearOutputFolder)
                {
                    await this.Logger.InfoAsync("Removing existent search queries from output folder \"{0}\".", exportPath);
                    FileHelper.DeleteFilesByPatterns(exportPath, "*.jquery", "*.query");
                }

                await this.Logger.InfoAsync("Loading search queries from service.");

                var availableSearchQueries = context.PublicQueriesOnly ?
                    await this.searchQueryService.GetPublicAsync(cancellationToken) :
                    await this.searchQueryService.GetUserAvailableAsync(cancellationToken);

                var publicLabel = context.PublicQueriesOnly ? "public " : string.Empty;

                if (context.SearchQueryNamesOrIdentifiers is null || !context.SearchQueryNamesOrIdentifiers.Any())
                {
                    await this.Logger.InfoAsync($"Exporting all ${publicLabel}search queries to folder \"${exportPath}\"");
                    await this.searchQueryFilePersistent.WriteAsync(availableSearchQueries, exportPath);
                    exportedCount = availableSearchQueries.Count;
                }
                else
                {
                    var searchQueryList = string.Join(", ", context.SearchQueryNamesOrIdentifiers.Select(name => $"\"{name}\""));
                    await this.Logger.InfoAsync($"Exporting search queries to folder \"{exportPath}\": {searchQueryList}.");
                    var namesOrIdsSet = new HashSet<string>(context.SearchQueryNamesOrIdentifiers, StringComparer.OrdinalIgnoreCase);

                    var searchQueriesToExport = availableSearchQueries.
                        Where(x => namesOrIdsSet.Contains(x.Alias) || namesOrIdsSet.Contains(x.ID.ToString())).ToList();
                    exportedCount = searchQueriesToExport.Count;

                    var searchQueriesToExportSet = searchQueriesToExport.SelectMany(x => new[] { x.Alias, x.ID.ToString() })
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);
                    notFoundCount = namesOrIdsSet.Count(x => !searchQueriesToExportSet.Contains(x));

                    await this.searchQueryFilePersistent.WriteAsync
                        (searchQueriesToExport, exportPath);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error exporting search queries.", e);
                return -1;
            }

            // количество экспортированных выводим как при наличии, так и при отсутствии ошибок
            if (exportedCount > 0)
            {
                await this.Logger.InfoAsync($"Search queries ({exportedCount}) are exported successfully.");
            }

            if (notFoundCount != 0)
            {
                await this.Logger.ErrorAsync($"Search queries ({notFoundCount}) aren't found by provided names or identifiers.");
            }
            else if (exportedCount == 0)
            {
                await this.Logger.InfoAsync("No search queries to export.");
            }

            return 0;
        }

        #endregion
    }
}