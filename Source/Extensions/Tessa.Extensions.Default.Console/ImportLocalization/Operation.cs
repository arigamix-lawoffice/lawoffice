using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ImportLocalization
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

            await this.Logger.InfoAsync("Reading localization from: \"{0}\"", context.Source);

            try
            {
                // сначала читаем все библиотеки локализации из файлов, чтобы убедиться, что там нет ошибок
                var libraries = new List<LocalizationLibrary>();
                var fileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                List<string> jsonFileNames = DefaultConsoleHelper.GetSourceFiles(context.Source, "*.jlocalization", throwIfNotFound: false);
                for (int i = jsonFileNames.Count - 1; i >= 0; i--)
                {
                    string fileName = jsonFileNames[i];
                    if (string.Equals(Path.GetExtension(fileName), ".tll", StringComparison.OrdinalIgnoreCase))
                    {
                        // если в context.Source указан не путь к папке, а путь к файлам, то здесь мы можем получить файлы в старом формате
                        jsonFileNames.RemoveAt(i);
                    }
                    else
                    {
                        fileNames.Add(fileName);
                    }
                }

                var jsonFileLocalizationService = new JsonFileLocalizationService(jsonFileNames);
                libraries.AddRange(await jsonFileLocalizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken));

                List<string> xmlFileNames = DefaultConsoleHelper.GetSourceFiles(context.Source, "*.tll", throwIfNotFound: false);
                for (int i = xmlFileNames.Count - 1; i >= 0; i--)
                {
                    if (fileNames.Contains(xmlFileNames[i]))
                    {
                        xmlFileNames.RemoveAt(i);
                    }
                }

                var xmlFileLocalizationService = new FileLocalizationService(xmlFileNames);
                libraries.AddRange(await xmlFileLocalizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken));

                if (libraries.Count == 0)
                {
                    throw new FileNotFoundException($"Couldn't locate *.jlocalization or *.tll files in \"{context.Source}\"", context.Source);
                }

                // если надо сначала удалить библиотеки в базе, то это можно сделать
                if (context.ClearLibraries)
                {
                    await this.Logger.InfoAsync("Removing all existent libraries");

                    foreach (LocalizationLibrary library in (await this.localizationService
                            .GetLibrariesAsync(cancellationToken: cancellationToken))
                        .ToArray())
                    {
                        await this.localizationService.RemoveLibraryAsync(library, cancellationToken);
                        await this.Logger.InfoAsync("Library is deleted: {0}", library.Name);
                    }
                }

                // сохраняем все библиотеки через сервис
                foreach (LocalizationLibrary library in libraries)
                {
                    await this.Logger.InfoAsync("Importing library \"{0}\"", library.Name);
                    await this.localizationService.SaveLibraryAsync(library, cancellationToken);
                    await this.Logger.InfoAsync("Library is saved: {0}", library.Name);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing localization", e);
                return -1;
            }

            await this.Logger.InfoAsync("Localization is imported successfully");
            return 0;
        }

        #endregion
    }
}