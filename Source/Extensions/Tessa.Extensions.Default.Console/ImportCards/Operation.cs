using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Applications.Package;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.SourceProviders;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.ImportCards
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region FailedImportingResult Private Class

        #endregion

        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardLibraryManager cardLibraryManager)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardLibraryManager = cardLibraryManager;
        }

        #endregion

        #region Fields

        private readonly ICardLibraryManager cardLibraryManager;

        #endregion

        #region Private Methods

        private async Task<int> ImportFilesAsync(
            string filePath,
            CardLibraryImportGlobalSettings settings,
            bool ignoreExistentCards,
            bool ignoreRepairMessages,
            CancellationToken cancellationToken)
        {
            IValidationResultBuilder validationResult = new ValidationResultBuilder();
            var successfulCardNames = new List<string>();

            try
            {
                var listner = new CardLibraryConsoleImportListener(
                    this.Logger,
                    validationResult,
                    successfulCardNames,
                    ignoreExistentCards,
                    ignoreRepairMessages);

                await this.cardLibraryManager.ImportAsync(
                    new CardLibraryImportItem
                    {
                        CardOrLibraryProvider = new FileSourceContentProvider(Path.GetFullPath(filePath), true)
                    },
                    settings,
                    listner,
                    cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                validationResult.AddException(this, ex);
            }

            if (successfulCardNames.Count != 0)
            {
                validationResult = GetImportingResultWithPreamble(validationResult, successfulCardNames);
            }

            ValidationResult totalResult = validationResult.Build();
            await this.Logger.LogResultAsync(totalResult);

            return totalResult.IsSuccessful ? successfulCardNames.Count : -1;
        }

        private static IValidationResultBuilder GetImportingResultWithPreamble(
            IValidationResultBuilder validationResult,
            ICollection<string> successfulCardNames)
        {
            if (successfulCardNames.Count == 0)
            {
                return validationResult;
            }

            string infoText =
                DefaultConsoleHelper.GetQuotedItemsText(
                        new StringBuilder(),
                        "UI_Cards_CardImported",
                        "UI_Cards_MultipleCardsImported",
                        successfulCardNames)
                    .ToString();

            return new ValidationResultBuilder()
                .AddInfo(typeof(Operation), infoText)
                .Add(validationResult);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context,
            CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            (_, ConfigurationConnection configurationConnection) =
                (await ConfigurationManager.GetDefaultAsync(cancellationToken))
                .Configuration
                .GetConfigurationDataProvider();

            DbProviderFactory factory = ConfigurationManager
                .GetConfigurationDataProviderFromType(configurationConnection.DataProvider)
                .GetDbProviderFactory();

            Dbms dbms = factory.GetDbms();

            var anyCardImported = false;
            try
            {
                // если указана папка, то находим первый файл с подходящим расширением
                HashSet<int> libraries = new HashSet<int>();
                var sources = context.Sources.SelectMany((x, i) =>
                {
                    var sourceFiles = DefaultConsoleHelper.GetSourceFiles(
                        x,
                        "*.cardlib;*.jcardlib",
                        throwIfNotFound: false,
                        checkPatternMatch: true);
                    if (sourceFiles.Count > 0)
                    {
                        libraries.Add(i);
                    }

                    return sourceFiles;
                }).ToList();

                sources.AddRange(context.Sources
                    .Where((x, i) => !libraries.Contains(i))
                    .SelectMany(x => DefaultConsoleHelper.GetSourceFiles(x, "*.jcard", throwIfNotFound: false, checkPatternMatch: true)));
                sources.AddRange(context.Sources
                    .Where((x, i) => !libraries.Contains(i))
                    .SelectMany(x => DefaultConsoleHelper.GetSourceFiles(x, "*.card", throwIfNotFound: false, checkPatternMatch: true)));

                var settings = new CardLibraryImportGlobalSettings
                {
                    Dbms = dbms,
                    FileWithIgnoreProvider = !string.IsNullOrEmpty(context.IgnoredFilesPath)
                            ? new FileSourceContentProvider(Path.GetFullPath(context.IgnoredFilesPath), true)
                            : null,
                    IgnoredFilesProvider = new FileSystemIgnoredFilesProvider(),
                    IgnoreExistentCards = context.IgnoreExistentCards,
                    GeneralMergeOptionsProvider = !string.IsNullOrEmpty(context.MergeOptionsPath)
                            ? new FileSourceContentProvider(Path.GetFullPath(context.MergeOptionsPath), true)
                            : null
                };

                foreach (string source in sources)
                {
                    var extension = Path.GetExtension(source);
                    if (extension.Equals(".jcardlib", StringComparison.OrdinalIgnoreCase)
                        || extension.Equals(".cardlib", StringComparison.OrdinalIgnoreCase))
                    {
                        await this.Logger.InfoAsync("Reading card library from: \"{0}\"", source);
                    }
                    else
                    {
                        await this.Logger.InfoAsync("Importing card from: \"{0}\"", source);
                    }

                    var importResult = await this.ImportFilesAsync(
                        source,
                        settings,
                        context.IgnoreExistentCards,
                        context.IgnoreRepairMessages,
                        cancellationToken);

                    if (importResult < 0)
                    {
                        return -1;
                    }
                    anyCardImported = importResult > 0;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing cards", e);
                return -1;
            }

            if (anyCardImported)
            {
                await this.Logger.InfoAsync("Cards are imported successfully");
            }
            else
            {
                await this.Logger.InfoAsync("Cards for import aren't found.");
            }
            return 0;
        }

        #endregion
    }
}
