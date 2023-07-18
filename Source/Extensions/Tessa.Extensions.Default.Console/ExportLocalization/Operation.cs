using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ExportLocalization
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            [Dependency(nameof(LocalizationServiceClient))]
            ILocalizationService localizationService)
            : base(logger, sessionManager)
        {
            this.localizationService = localizationService;
        }

        #endregion

        #region Fields

        private readonly ILocalizationService localizationService;

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
                    await this.Logger.InfoAsync("Removing existent libraries from output folder \"{0}\"", exportPath);

                    foreach (string filePath in Directory.EnumerateFiles(exportPath, "*.jlocalization", SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(filePath);
                    }

                    foreach (string filePath in Directory.EnumerateFiles(exportPath, "*.tll", SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(filePath);
                    }
                }

                var fileLocalizationService = new JsonFileLocalizationService(exportPath);

                if (context.LibraryNames is null || context.LibraryNames.Count == 0)
                {
                    await this.Logger.InfoAsync("Exporting all libraries to folder \"{0}\"", exportPath);

                    await this.Logger.InfoAsync("Loading from service...");

                    foreach (LocalizationLibrary library in await this.localizationService
                        .GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                    {
                        await this.Logger.InfoAsync("Saving library \"{0}\"", library.Name);
                        await fileLocalizationService.SaveLibraryAsync(library, cancellationToken);

                        exportedCount++;
                    }
                }
                else
                {
                    await this.Logger.InfoAsync(
                        "Exporting libraries to folder \"{0}\": {1}",
                        exportPath,
                        string.Join(", ", context.LibraryNames.Select(name => "\"" + name + "\"")));

                    foreach (string libraryName in context.LibraryNames)
                    {
                        await this.Logger.InfoAsync("Loading library from service \"{0}\"", libraryName);

                        LocalizationLibrary library = await this.localizationService.GetLibraryAsync(
                            libraryName, cancellationToken: cancellationToken);

                        if (library is null)
                        {
                            await this.Logger.ErrorAsync("Library \"{0}\" isn't found", libraryName);
                            notFoundCount++;
                        }
                        else
                        {
                            await this.Logger.InfoAsync("Saving library \"{0}\"", library.Name);
                            await fileLocalizationService.SaveLibraryAsync(library, cancellationToken);
                            exportedCount++;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error exporting libraries", e);
                return -1;
            }

            // количество экспортированных выводим как при наличии, так и при отсутствии ошибок
            if (exportedCount > 0)
            {
                await this.Logger.InfoAsync("Libraries ({0}) are exported successfully", exportedCount);
            }

            if (notFoundCount != 0)
            {
                await this.Logger.ErrorAsync("Libraries ({0}) aren't found by provided names", notFoundCount);
            }
            else if (exportedCount == 0)
            {
                await this.Logger.InfoAsync("No libraries to export");
            }

            return 0;
        }

        #endregion
    }
}