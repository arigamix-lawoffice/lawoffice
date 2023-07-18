#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Metadata;
using Tessa.Views.Parser;
using Tessa.Views.Parser.Serialization;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;
using Tessa.Views.Parser.SyntaxTree.ViewMetadata;
using Tessa.Views.SearchQueries;
using Tessa.Views.Workplaces;
using Tessa.Views.Workplaces.Json;
using Tessa.Views.Workplaces.Json.Converters;
using Tessa.Views.Workplaces.Json.Metadata;

namespace Tessa.Extensions.Default.Console.ConvertConfiguration
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ViewFilePersistent viewFilePersistent,
            IJsonViewModelExporter jsonViewModelExporter,
            WorkplaceFilePersistent workplaceFilePersistent,
            IConverter<IJsonSearchQueryMetadata, ISearchQueryMetadata> searchQueryConverter,
            IJsonViewModelConverter viewModelAdapter,
            IViewServiceImplementer viewServiceImplementer,
            ViewMetadataEvaluationContextFactory evaluationContextFactory,
            IViewMetadataInterpreter viewMetadataInterpreter,
            IExchangeFormatInterpreter exchangeFormatInterpreter,
            IIndentationStrategy indentationStrategy,
            IJsonViewModelUpgrader jsonViewModelUpgrader)
            : base(logger, sessionManager)
        {
            this.viewFilePersistent = viewFilePersistent;
            this.jsonViewModelExporter = jsonViewModelExporter;
            this.viewModelAdapter = viewModelAdapter;
            this.workplaceFilePersistent = workplaceFilePersistent;
            this.searchQueryConverter = searchQueryConverter;
            this.viewServiceImplementer = viewServiceImplementer;
            this.evaluationContextFactory = evaluationContextFactory;
            this.viewMetadataInterpreter = viewMetadataInterpreter;
            this.exchangeFormatInterpreter = exchangeFormatInterpreter;
            this.indentationStrategy = indentationStrategy;
            this.jsonViewModelUpgrader = jsonViewModelUpgrader;
        }

        #endregion

        #region Private Fields

        private readonly ViewFilePersistent viewFilePersistent;

        private readonly IJsonViewModelExporter jsonViewModelExporter;
        private readonly IJsonViewModelUpgrader jsonViewModelUpgrader;

        private readonly IJsonViewModelConverter viewModelAdapter;

        private readonly WorkplaceFilePersistent workplaceFilePersistent;

        private readonly IConverter<IJsonSearchQueryMetadata, ISearchQueryMetadata> searchQueryConverter;

        private readonly IViewServiceImplementer viewServiceImplementer;

        private readonly ViewMetadataEvaluationContextFactory evaluationContextFactory;

        private readonly IViewMetadataInterpreter viewMetadataInterpreter;

        private readonly IExchangeFormatInterpreter exchangeFormatInterpreter;

        private readonly IIndentationStrategy indentationStrategy;

        #endregion

        #region Private Methods

        private async ValueTask<string> ConvertItemsAsync(
            string sourcePath,
            string targetPath,
            bool doNotDelete,
            ConversionMode conversionMode,
            CancellationToken cancellationToken = default)
        {
            // Подменяем источных данных в поставщике представлений, чтоб он не пытался вытягивать их через сессию.
            if (viewServiceImplementer is IViewServiceInitializer viewServiceInitializer)
            {
                viewServiceInitializer.Initialize(new List<IViewMetadata>());
            }

            var items = new List<ConversionItem>();

            FileAttributes attr = File.GetAttributes(sourcePath);
            bool sourcePathIsDirectory = (attr & FileAttributes.Directory) == FileAttributes.Directory;
            foreach (string sourceFilePath in DefaultConsoleHelper.GetSourceFiles(sourcePath, "*.*", false))
            {
                // Имя директории может быть с точкой, тогда Path.GetDirectoryName() даст ошибочный результат, отбросив
                // часть пути, на самом деле являющегося директорией, поэтому выше проверяется через файловую систему
                // является ли sourcePath директорией.
                string relativeFilePath =
                    sourcePathIsDirectory
                        ? Path.GetRelativePath(sourcePath, sourceFilePath)
                        : Path.GetRelativePath(Path.GetDirectoryName(sourcePath) ?? string.Empty, sourceFilePath);

                string targetFilePath = Path.Combine(targetPath, relativeFilePath);
                string extension = Path.GetExtension(sourceFilePath).ToLowerInvariant();
                ConversionItem? item;

                try
                {
                    switch (conversionMode)
                    {
                        case ConversionMode.Upgrade:
                            item = await this.ReadForConvertAsync(extension, sourceFilePath, targetFilePath,
                                cancellationToken);
                            break;

                        case ConversionMode.Downgrade:
                            item = await this.ReadForDowngradeAsync(extension, sourceFilePath, targetFilePath,
                                cancellationToken);
                            break;

                        case ConversionMode.LF:
                        case ConversionMode.CRLF:
                            switch (extension)
                            {
                                case ".cardlib": // библиотека карточек в xml
                                case ".jcardlib": // библиотека карточек в json
                                case ".jcard": // карточка в json
                                case ".json": // произвольный текстовый json, например, app.json
                                case ".jlocalization": // библиотека локализации в json
                                case ".jtype": // тип карточки в json
                                case ".jview": // представление в json
                                case ".jworkplace": // рабочее место в json
                                case ".jquery": // поисковый запрос в json
                                case ".sql": // sql-скрипт с процедурой, функцией или миграцией
                                case ".tct": // тип карточки в xml
                                case ".tll": // библиотека локализации в xml
                                case ".tpf": // функция схемы в xml
                                case ".tpm": // миграция схемы в xml
                                case ".tpp": // процедура схемы в xml
                                case ".tsd": // база данных схемы в xml
                                case ".tsp": // библиотека схемы в xml
                                case ".tst": // таблица схемы в xml
                                case ".txt": // текстовые файлы вида readme.txt
                                case ".view": // представление в exchange format
                                case ".workplace": // рабочее место в exchange format
                                case ".query": // поисковый в exchange format
                                case ".xml": // произвольный текстовый xml, например, extensions.xml
                                    // только наши текстовые файлы, не трогаем бинарные .card, и другие файлы (например, файлы реестра .reg)
                                    item = new ConversionItem(sourceFilePath, targetFilePath, null);
                                    break;

                                default:
                                    item = null;
                                    break;
                            }

                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(ConversionMode), conversionMode, null);
                    }
                }
                catch (Exception ex)
                {
                    await this.Logger.LogExceptionAsync($"Error when loading file \"{sourceFilePath}\"", ex);
                    item = null;
                }

                if (item != null)
                {
                    items.Add(item);
                }
            }

            var errorCount = 0;

            if (items.Count > 0)
            {
                await this.Logger.InfoAsync("Converting configuration files ({0})", items.Count);

                foreach (ConversionItem item in items)
                {
                    var itemWasConverted = true;
                    string? targetDirectoryName = Path.GetDirectoryName(item.NewPath);
                    if (string.IsNullOrEmpty(targetDirectoryName))
                    {
                        itemWasConverted = false;
                        errorCount++;
                        await this.Logger.ErrorAsync("Can't get target directory name from: \"{0}\"", item.NewPath);
                    }
                    else
                    {
                        try
                        {
                            // Подготовить директорию для target
                            FileHelper.CreateDirectoryIfNotExists(targetDirectoryName, true);

                            switch (item.Object)
                            {
                                case null: // преобразование переводов строк
                                    string text =
                                        await File.ReadAllTextAsync(item.OldPath, Encoding.UTF8, cancellationToken);
                                    string newText = conversionMode == ConversionMode.LF
                                        ? text.NormalizeLineEndingsUnixStyle()
                                        : text.NormalizeLineEndingsWindowsStyle();

                                    if (!string.Equals(text, newText, StringComparison.Ordinal))
                                    {
                                        await File.WriteAllTextAsync(item.NewPath, newText, Encoding.UTF8,
                                            cancellationToken);
                                        await this.Logger.InfoAsync("Line endings are converted: \"{0}\"", item.NewPath);
                                    }

                                    break;

                                case CardType cardType:
                                    string typeText = await cardType.SerializeToJsonAsync(indented: true, cancellationToken);
                                    await File.WriteAllTextAsync(item.NewPath, typeText, Encoding.UTF8, cancellationToken);
                                    await this.Logger.InfoAsync("Type is converted: \"{0}\"", item.NewPath);
                                    break;

                                case LocalizationLibrary localizationLibrary:
                                    if (conversionMode == ConversionMode.Downgrade)
                                    {
                                        var localizationService =
                                            new FileLocalizationService(targetDirectoryName);
                                        await localizationService.SaveLibraryAsync(localizationLibrary, item.NewPath,
                                            cancellationToken);
                                    }
                                    else
                                    {
                                        var localizationService =
                                            new JsonFileLocalizationService(targetDirectoryName);
                                        await localizationService.SaveLibraryAsync(localizationLibrary, item.NewPath,
                                            cancellationToken);
                                    }

                                    await this.Logger.InfoAsync("Localization library is converted: \"{0}\"", item.NewPath);
                                    break;

                                case TessaViewModel:
                                    var viewFileName =
                                        await this.ConvertViewToJsonAsync(item.OldPath, item.NewPath, cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(viewFileName))
                                    {
                                        await this.Logger.InfoAsync("View is converted: \"{0}\"", viewFileName);
                                    }
                                    else
                                    {
                                        itemWasConverted = false;
                                        errorCount++;
                                        await this.Logger.InfoAsync("Cannot convert view: \"{0}\"", item.OldPath);
                                    }

                                    break;

                                case WorkplaceModel:
                                    var workplaceFileName =
                                        await this.ConvertWorkplaceToJsonAsync(item.OldPath, item.NewPath,
                                            cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(workplaceFileName))
                                    {
                                        await this.Logger.InfoAsync("Workplace is converted: \"{0}\"", workplaceFileName);
                                    }
                                    else
                                    {
                                        itemWasConverted = false;
                                        errorCount++;
                                        await this.Logger.InfoAsync("Cannot convert workplace: \"{0}\"", item.OldPath);
                                    }

                                    break;

                                case JsonWorkplace:
                                    var jWorkplaceFileName = await this.ConvertJWorkplaceAsync(item.OldPath, item.NewPath, cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(jWorkplaceFileName))
                                    {
                                        await this.Logger.InfoAsync("Json workplace is converted: \"{0}\"", jWorkplaceFileName);
                                    }
                                    else
                                    {
                                        itemWasConverted = false;
                                        errorCount++;
                                        await this.Logger.InfoAsync("Cannot convert json workplace: \"{0}\"", item.OldPath);
                                    }

                                    break;

                                case JsonViewModel:
                                    var jViewFileName = await this.ConvertJViewAsync(item.OldPath, item.NewPath, cancellationToken);
                                    if (jViewFileName is null)
                                    {
                                        itemWasConverted = false;
                                        errorCount++;
                                        await this.Logger.InfoAsync("Cannot convert json view: \"{0}\"", item.OldPath);
                                    }
                                    else if (string.IsNullOrEmpty(jViewFileName))
                                    {
                                        await this.Logger.InfoAsync(
                                            "Skipped. Json view: \"{0}\" is no need to convert and output path equals source.",
                                            item.OldPath);
                                    }
                                    else
                                    {
                                        await this.Logger.InfoAsync("Json view is converted: \"{0}\"", jViewFileName);
                                    }

                                    break;

                                case SearchQueryMetadata:
                                    var searchQueryName =
                                        await this.ConvertSearchQueryToJsonAsync(item.OldPath, item.NewPath,
                                            cancellationToken);
                                    if (!string.IsNullOrWhiteSpace(searchQueryName))
                                    {
                                        await this.Logger.InfoAsync("Search query is converted: \"{0}\"", searchQueryName);
                                    }
                                    else
                                    {
                                        itemWasConverted = false;
                                        errorCount++;
                                        await this.Logger.InfoAsync("Cannot convert search query: \"{0}\"", item.OldPath);
                                    }

                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            itemWasConverted = false;
                            errorCount++;
                            await this.Logger.LogExceptionAsync($"Error when converting file \"{item.OldPath}\"", ex);

                            // Удалить созданную target directory если в ней ничего нет.
                            if (Directory.Exists(targetDirectoryName)
                                && !Directory.EnumerateFileSystemEntries(targetDirectoryName).Any())
                            {
                                Directory.Delete(targetDirectoryName);
                            }
                        }
                    }

                    // Удалить файл-источник если: нет флага "Не удалять", объект успешно сконвертирован и путь назначения отличается от источника. 
                    if (!doNotDelete
                        && itemWasConverted
                        && item.OldPath != item.NewPath)
                    {
                        FileHelper.DeleteFileSafe(item.OldPath);
                    }
                }
            }

            return $"{items.Count - errorCount} of {items.Count} files were converted successfully.";
        }

        private async ValueTask<string?> ConvertJViewAsync(string itemOldPath, string itemNewPath, CancellationToken cancellationToken)
        {
            IJsonViewModel? jsonViewModel;
            await using (var readStream = FileHelper.OpenRead(itemOldPath))
            {
                var context = new TessaJsonSerializationContext();
                await using var scope = TessaJsonSerializationContext.Create(context);

                var reader = new TextPartReader(readStream);
                var text = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);

                jsonViewModel = text.FromJsonString<JsonViewModel>();

                if (jsonViewModel is null or { JsonMetadataSource: null })
                {
                    await this.Logger.ErrorAsync("Can't create JsonViewModel from json string.");
                    return null;
                }

                var upgraded = await this.jsonViewModelUpgrader.UpgradeAsync(jsonViewModel, cancellationToken);

                // Если изменений нет и путь назначения равен источнику.
                if (!upgraded && itemOldPath == itemNewPath)
                {
                    return string.Empty;
                }
            }

            await this.Logger.InfoAsync($"Writing JSON format metadata for {Path.GetFileName(itemNewPath)}.");
            await using var writeStream = FileHelper.Create(itemNewPath);
            await this.jsonViewModelExporter.ExportAsync(jsonViewModel, writeStream, cancellationToken);

            return itemNewPath;
        }

        private async ValueTask<string?> ConvertJWorkplaceAsync(string oldPath, string newPath, CancellationToken cancellationToken)
        {
            JsonWorkplaceModel? jsonWorkplaceModel;
            var context = new TessaJsonSerializationContext();
            await using (var _ = TessaJsonSerializationContext.Create(context))
            {
                await using (var readStream = FileHelper.OpenRead(oldPath))
                {
                    var reader = new TextPartReader(readStream);
                    var text = await reader.ReadAsync(cancellationToken);

                    jsonWorkplaceModel = text.FromJsonString<JsonWorkplaceModel>();

                    if (jsonWorkplaceModel is null)
                    {
                        return null;
                    }

                    var jsonWorkplace = jsonWorkplaceModel.Content;
                    if (jsonWorkplace is null || jsonWorkplace.Metadata is null || jsonWorkplace.Metadata.FormatVersion >= 2)
                    {
                        return oldPath;
                    }

                    await this.Logger.WriteLineAsync($"Upgrading JSON format metadata for {Path.GetFileName(oldPath)}.");
                    jsonWorkplace.Order = jsonWorkplace.Metadata.OrderPos;
                }
            }

            await using (var _ = TessaJsonSerializationContext.Create(context))
            {
                await using (var writeStream = FileHelper.Create(newPath))
                {
                    var jsonString = jsonWorkplaceModel.ToJsonString();
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    await writeStream.WriteAsync(bytes, cancellationToken);
                    var textPartWriter = new TextPartWriter(writeStream);
                    await textPartWriter.WriteAsync(cancellationToken);
                }

                return newPath;
            }
        }

        private async ValueTask<string?> ConvertViewToJsonAsync(string oldPath, string newPath,
            CancellationToken cancellationToken = default)
        {
            var viewModels = await this.viewFilePersistent.ReadAsync(oldPath, async (fileName, exception, ct) =>
            {
                await this.Logger.LogExceptionAsync($"Error while reading {fileName}", exception);
                return true;
            }, cancellationToken);

            var viewModel = viewModels.FirstOrDefault();
            if (viewModel is null)
            {
                return null;
            }

            var jsonViewModel = this.viewModelAdapter.ConvertToJsonViewModel(viewModel);
            var context = this.evaluationContextFactory(Dbms.Unknown, jsonViewModel.Alias, jsonViewModel.Caption);

            jsonViewModel.JsonMetadataSource =
                (await this.viewMetadataInterpreter.EvaluateAsync(viewModel.MetadataSource, context, cancellationToken))
                .ToJsonString();
            jsonViewModel.MetadataSource = null;

            var fullName = Path.Combine(newPath, $"{jsonViewModel.Alias}.jview");

            await using var file = FileHelper.Create(fullName);
            await this.jsonViewModelExporter.ExportAsync(jsonViewModel, file, CancellationToken.None);

            return fullName;
        }

        private async ValueTask<string?> ConvertWorkplaceToJsonAsync(string oldPath, string newPath,
            CancellationToken cancellationToken = default)
        {
            var workplaceModels = await this.workplaceFilePersistent.ReadAsync(oldPath,
                async (fileName, exception, ct) =>
                {
                    await this.Logger.LogExceptionAsync($"Error while reading {fileName}", exception);
                    return true;
                }, cancellationToken);

            var model = workplaceModels.FirstOrDefault();
            if (model is null)
            {
                return null;
            }

            var context = new TessaJsonSerializationContext();
            await using var scope = TessaJsonSerializationContext.Create(context);

            var workplace = model.Workplace;

            var jsonWorkplace = new JsonWorkplace()
            {
                Metadata = workplace.Metadata.FromJsonString<JsonWorkplaceMetadata>(),
                Order = workplace.Order
            };
            if (workplace.Roles != null)
            {
                jsonWorkplace.Roles.AddRange(workplace.Roles);
            }

            var jsonWorkplaceModel = new JsonWorkplaceModel()
            {
                Content = jsonWorkplace
            };

            var fileName = await LocalizationManager.LocalizeOrGetNameAsync(
                workplace.Name,
                LocalizationManager.EnglishCultureInfo,
                cancellationToken);

            var fullName = Path.Combine(newPath, $"{fileName}.jworkplace");

            await using var file = FileHelper.Create(fullName);

            var jsonString = jsonWorkplaceModel.ToJsonString();

            var bytes = Encoding.UTF8.GetBytes(jsonString);
            await file.WriteAsync(bytes.AsMemory(0, bytes.Length), cancellationToken);
            var textpartWriter = new TextPartWriter(file);
            await textpartWriter.WriteAsync(cancellationToken);

            return fullName;
        }

        private async ValueTask<string?> ConvertSearchQueryToJsonAsync(string oldPath, string newPath,
            CancellationToken cancellationToken = default)
        {
            await using var stream = FileHelper.OpenRead(oldPath);
            var context = await this.exchangeFormatInterpreter.InterpretAsync(stream, this.indentationStrategy,
                cancellationToken: cancellationToken);
            var searchQueries = context.GetSearchQueries();

            var searchQuery = searchQueries.FirstOrDefault();
            if (searchQuery is null)
            {
                return null;
            }

            var alias = await LocalizationManager.LocalizeOrGetNameAsync(searchQuery.Alias, cancellationToken);
            var fullName = GetSearchQueryConvertedFileName(alias ?? string.Empty, newPath);

            await using var file = FileHelper.Create(fullName);

            var jsonMetadata = await this.searchQueryConverter.ConvertBackAsync(searchQuery);
            var jsonString = jsonMetadata.ToJsonString();
            byte[] bytes = Encoding.UTF8.GetBytesWithPreamble(jsonString);
            await file.WriteAsync(bytes, cancellationToken);

            return fullName;
        }


        // Метод определяет имя файла для сконвертированного поискового запроса. По умолчанию это <alias>.jquery, но если такой файл уже существует,
        // то метод вернет <alias>_1.jquery. Если существует и <alias>_1.jquery, то возвращено будет <alias>_2.jquery. И т.д.
        private static string GetSearchQueryConvertedFileName(string alias, string path)
        {
            var fileName = Path.Combine(path, $"{alias}.jquery");
            var counter = 0;
            while (File.Exists(fileName))
            {
                fileName = Path.Combine(path, $"{alias}_{++counter}.jquery");
            }

            return fileName;
        }

        private async Task<ConversionItem?> ReadForConvertAsync(string extension, string sourceFilePath,
            string targetFilePath, CancellationToken cancellationToken = default)
        {
            switch (extension)
            {
                case ".tct":
                    await this.Logger.InfoAsync("Reading type from: \"{0}\"", sourceFilePath);

                    var cardTypeFromXml = new CardType();
                    await using (FileStream fileStream = FileHelper.OpenRead(sourceFilePath, synchronousOnly: true))
                    {
                        await cardTypeFromXml.DeserializeFromXmlAsync(fileStream, cancellationToken);
                    }

                    string newTypeFromXmlFilePath = DefaultConsoleHelper.ChangeExtension(targetFilePath, ".tct", ".jtype");
                    return new ConversionItem(sourceFilePath, newTypeFromXmlFilePath, cardTypeFromXml);

                case ".jtype":
                    await this.Logger.InfoAsync("Reading type from: \"{0}\"", sourceFilePath);

                    Dictionary<string, object?>? storage;
                    await using (FileStream fileStream = FileHelper.OpenRead(sourceFilePath, synchronousOnly: true))
                    {
                        using var sr = new StreamReader(fileStream);
                        string typeJson = await sr.ReadToEndAsync(cancellationToken);
                        storage = StorageHelper.DeserializeFromTypedJson(typeJson);
                    }

                    if (storage is null)
                    {
                        return null;
                    }

                    // Если тип карточки имеет текущий формат версии и перезаписывается по такому же пути что и источник, пропускаем.
                    if (sourceFilePath == targetFilePath
                        && storage.TryGet<int>(CardSerializableObject.FormatVersionKey) == CardType.CurrentFormatVersion)
                    {
                        await this.Logger.InfoAsync(
                            "Skipping type from: \"{0}\". Output path equals source path and format version equals current version.",
                            sourceFilePath);
                        return null;
                    }

                    var cardType = await CardSerializableObject.DeserializeFromStorageAsync<CardType>(storage, cancellationToken);

                    return new ConversionItem(sourceFilePath, targetFilePath, cardType);

                case ".tll":
                {
                    await this.Logger.InfoAsync("Reading localization library from: \"{0}\"", sourceFilePath);

                    var localizationService = new FileLocalizationService(new[] { sourceFilePath });
                    var localizationLibrary =
                        (await localizationService.GetLibrariesAsync(returnComments: true,
                            cancellationToken: cancellationToken)).First();

                    string newLibraryFilePath =
                        DefaultConsoleHelper.ChangeExtension(targetFilePath, ".tll", ".jlocalization");
                    return new ConversionItem(sourceFilePath, newLibraryFilePath, localizationLibrary);
                }

                case ".jlocalization":
                {
                    await this.Logger.InfoAsync("Reading localization library from: \"{0}\"", sourceFilePath);

                    var localizationService = new JsonFileLocalizationService(new[] { sourceFilePath });
                    var localizationLibrary =
                        (await localizationService.GetLibrariesAsync(returnComments: true,
                            cancellationToken: cancellationToken)).First();

                    return new ConversionItem(sourceFilePath, targetFilePath, localizationLibrary);
                }

                case ".view":
                    await this.Logger.InfoAsync("View is pending to convert: \"{0}\"", sourceFilePath);
                    // string newViewTypePath = ChangeExtension(filePath, ".view", ".jview");
                    return new ConversionItem(sourceFilePath, Path.GetDirectoryName(targetFilePath),
                        new TessaViewModel());

                case ".workplace":
                    await this.Logger.InfoAsync("Workplace is pending to convert: \"{0}\"", sourceFilePath);
                    return new ConversionItem(sourceFilePath, Path.GetDirectoryName(targetFilePath),
                        new WorkplaceModel());

                case ".query":
                    await this.Logger.InfoAsync("Search query is pending to convert: \"{0}\"", sourceFilePath);
                    return new ConversionItem(sourceFilePath, Path.GetDirectoryName(targetFilePath),
                        new SearchQueryMetadata());

                case ".jworkplace":
                    await this.Logger.InfoAsync("Json workplace is pending to convert: \"{0}\"", sourceFilePath);
                    return new ConversionItem(sourceFilePath, targetFilePath, new JsonWorkplace());

                case ".jview":
                    await this.Logger.InfoAsync("Json view is pending to convert: \"{0}\"", sourceFilePath);
                    return new ConversionItem(sourceFilePath, targetFilePath, new JsonViewModel());

                default:
                    return null;
            }
        }

        private async Task<ConversionItem?> ReadForDowngradeAsync(string extension, string sourceFilePath,
            string targetFilePath, CancellationToken cancellationToken = default)
        {
            switch (extension)
            {
                case ".jlocalization":
                    await this.Logger.InfoAsync("Reading localization library from: \"{0}\"", sourceFilePath);

                    var localizationService = new JsonFileLocalizationService(new[] { sourceFilePath });
                    var localizationLibrary =
                        (await localizationService.GetLibrariesAsync(returnComments: true,
                            cancellationToken: cancellationToken)).First();

                    string newLibraryFilePath =
                        DefaultConsoleHelper.ChangeExtension(targetFilePath, ".jlocalization", ".tll");
                    return new ConversionItem(sourceFilePath, newLibraryFilePath, localizationLibrary);

                default:
                    return null;
            }
        }

        #endregion

        #region Base Overrides

        public override async Task<int> ExecuteAsync(
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await this.Logger.InfoAsync("Converting configuration from: \"{0}\"", context.Source);
                var resultMessage = await this.ConvertItemsAsync(
                    context.Source,
                    context.Target,
                    context.DoNotDelete,
                    context.ConversionMode,
                    cancellationToken);
                await this.Logger.InfoAsync(resultMessage);
                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error converting configuration", e);
                return -1;
            }
        }

        #endregion
    }
}
