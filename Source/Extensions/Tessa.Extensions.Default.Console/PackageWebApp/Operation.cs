using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Applications.Package;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.PackageWebApp
{
    public static class Operation
    {
        #region Private Methods

        private static string GetDefaultAppFileNameWithoutExtension(WebApplication app) =>
            app.Name + (string.IsNullOrEmpty(app.LanguageCode) ? null : "." + app.LanguageCode);

        private static async Task<bool> LoadInfoAsync(IConsoleLogger logger, WebApplication app, string exePath, string languageCode, bool readAllBytes)
        {
            await logger.InfoAsync("Packaging web application: {0}", exePath);

            if (string.IsNullOrWhiteSpace(app.Name))
            {
                app.Name = Path.GetFileNameWithoutExtension(exePath) ?? "Application";
            }

            // эти свойства точно не null, но могут содержать оконечные пробелы
            app.Name = app.Name.Trim();
            app.ExeFileName = Path.GetFileName(exePath);
            app.Description = app.Description?.Replace("\\r", "\r", StringComparison.Ordinal).Replace("\\n", "\n", StringComparison.Ordinal).Replace("\\t", "\t", StringComparison.Ordinal).Trim();

            string appFolder = Path.GetDirectoryName(Path.GetFullPath(exePath));
            if (string.IsNullOrEmpty(appFolder))
            {
                appFolder = Directory.GetCurrentDirectory();
            }

            string ignoredVersionFilePath = null;
            if (string.IsNullOrWhiteSpace(app.Version))
            {
                (bool success, string version, string versionFilePath) = await TryLoadVersionFromFileAsync(logger, appFolder);
                if (!success)
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(version))
                {
                    app.Version = version.Trim();
                    ignoredVersionFilePath = versionFilePath;
                }
            }

            switch (languageCode?.ToLowerInvariant())
            {
                case "en":
                    app.LanguageCode = "en";
                    app.LanguageID = 0;
                    app.LanguageCaption = "English";
                    break;

                case "ru":
                    app.LanguageCode = "ru";
                    app.LanguageID = 1;
                    app.LanguageCaption = "Русский";
                    break;

                default:
                    string[] languageParts = languageCode?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                    if (languageParts?.Length > 0)
                    {
                        if (languageParts.Length != 3)
                        {
                            await logger.ErrorAsync("Invalid language parameter: {0}", languageCode);
                            return false;
                        }

                        if (!int.TryParse(languageParts[1], out int languageID))
                        {
                            await logger.ErrorAsync("Can't parse language identifier: {0}", languageCode[1]);
                            return false;
                        }

                        app.LanguageCode = languageParts[0];
                        app.LanguageID = languageID;
                        app.LanguageCaption = languageParts[2];
                    }
                    else
                    {
                        // если 0 частей, то это многоязычное приложение
                        app.LanguageCode = null;
                        app.LanguageID = null;
                        app.LanguageCaption = null;
                    }

                    break;
            }

            await logger.InfoAsync("Packaging from folder: {0}", appFolder);
            await logger.InfoAsync("ExeFileName = {0}", app.ExeFileName);
            await logger.InfoAsync("Name = {0}", app.Name);
            await logger.InfoAsync("Version = {0}", app.Version);
            await logger.InfoAsync("Description = {0}", string.IsNullOrEmpty(app.Description) ? "(empty)" : app.Description);

            if (!app.LanguageID.HasValue)
            {
                await logger.InfoAsync("LanguageCode = (multilingual)");
            }
            else
            {
                await logger.InfoAsync("LanguageCode = {0}", app.LanguageCode);
                await logger.InfoAsync("LanguageID = {0}", app.LanguageID);
                await logger.InfoAsync("LanguageCaption = {0}", app.LanguageCaption);
            }

            var ignoredProvider = new FileSystemIgnoredFilesProvider();
            var ignoredPathList = new HashSet<string>(ignoredProvider.GetIgnoredFileNames(appFolder));

            if (ignoredVersionFilePath != null)
            {
                ignoredPathList.Add(ignoredVersionFilePath);
            }

            foreach (string path in ignoredPathList)
            {
                await logger.InfoAsync("Ignored: {0}", path);
            }

            foreach (string filePath in Directory.EnumerateFiles(appFolder, "*.*", SearchOption.AllDirectories))
            {
                if (!ignoredPathList.Contains(filePath))
                {
                    var file = new WebApplicationFile(filePath, appFolder);
                    if (readAllBytes)
                    {
                        await file.ReadAllBytesAsync();
                    }
                    else
                    {
                        file.ReadFileSize();
                    }

                    app.Files.Add(file);

                    if (string.IsNullOrEmpty(file.Category))
                    {
                        await logger.InfoAsync("Added: {0}", file.Name);
                    }
                    else
                    {
                        await logger.InfoAsync("Added: {0}", Path.Combine(file.Category.NormalizePathOnCurrentPlatform(), file.Name));
                    }
                }
            }

            return true;
        }

        private static async ValueTask<(bool success, string version, string versionFilePath)> TryLoadVersionFromFileAsync(
            IConsoleLogger logger,
            string appFolder)
        {
            string versionFilePath = Path.Combine(appFolder, "version.txt");
            if (!File.Exists(versionFilePath))
            {
                return (true, null, null);
            }

            string text = await File.ReadAllTextAsync(versionFilePath);
            if (string.IsNullOrEmpty(text))
            {
                return (true, null, null);
            }

            Match match = Regex.Match(text, @"^\s*DESKI_VERSION\s*=\s*(.+)\s*$", RegexOptions.Multiline);
            if (match.Success)
            {
                string version = match.Groups[1].Captures[0].Value;
                return (true, version, versionFilePath);
            }

            await logger.ErrorAsync("Invalid version information in file: {0}", versionFilePath);
            return (false, null, null);
        }

        #endregion

        #region Methods

        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string exePath,
            string outputPath,
            string name,
            string version,
            string description,
            string languageCode,
            string osName,
            bool client64Bit,
            bool binaryMode)
        {
            if (string.IsNullOrEmpty(exePath))
            {
                await logger.ErrorAsync("Can't package app: no source executable specified.");
                return -1;
            }

            try
            {
                // если File.Exists упадёт (например, невалидные символы в пути), то его перехватит catch
                exePath = Path.GetFullPath(exePath.NormalizePathOnCurrentPlatform());
                if (!File.Exists(exePath))
                {
                    await logger.ErrorAsync("Can't find source executable \"{0}\". Please, check if file exists and application has access to it.", exePath);
                    return -2;
                }

                var app = new WebApplication { Name = name, Version = version, Description = description, OSName = osName, Client64Bit = client64Bit };
                bool success = await LoadInfoAsync(logger, app, exePath, languageCode, readAllBytes: !binaryMode);
                if (!success)
                {
                    return -3;
                }

                DateTime utcNow = DateTime.UtcNow;

                var card = new Card
                {
                    ID = Guid.NewGuid(),
                    TypeID = CardHelper.WebApplicationTypeID,
                    TypeName = CardHelper.WebApplicationTypeName,
                    TypeCaption = CardHelper.WebApplicationTypeCaption,
                    Created = utcNow,
                    CreatedByID = Session.SystemID,
                    CreatedByName = Session.SystemName,
                    Modified = utcNow,
                    ModifiedByID = Session.SystemID,
                    ModifiedByName = Session.SystemName,
                };

                StringDictionaryStorage<CardSection> sections = card.Sections;

                Dictionary<string, object> fields = sections.GetOrAddEntry("WebApplications").RawFields;
                fields["Name"] = app.Name;
                fields["AppVersion"] = app.Version;
                fields["PlatformVersion"] = BuildInfo.Version;
                fields["ExecutableFileName"] = app.ExeFileName;
                fields["Description"] = app.Description;
                fields["LanguageID"] = app.LanguageID;
                fields["LanguageCode"] = app.LanguageCode;
                fields["LanguageCaption"] = app.LanguageCaption;
                fields["OSName"] = app.OSName?.Trim();
                fields["Client64Bit"] = BooleanBoxes.Box(app.Client64Bit);

                var header = binaryMode ? new CardHeader() : null;
                var fileStreams = binaryMode ? new Dictionary<Guid?, Func<Stream>>() : null;

                if (app.Files.Count > 0)
                {
                    ListStorage<CardFile> cardFiles = card.Files;
                    using HashAlgorithm hashAlgorithm = HashSignatureProvider.Files.CreateAlgorithm();

                    foreach (WebApplicationFile file in app.Files)
                    {
                        Guid versionRowID = Guid.NewGuid();

                        CardFile cardFile = cardFiles.Add();
                        cardFile.RowID = file.RowID;
                        cardFile.TypeID = CardHelper.FileTypeID;
                        cardFile.TypeName = CardHelper.FileTypeName;
                        cardFile.TypeCaption = CardHelper.FileTypeCaption;
                        cardFile.VersionRowID = versionRowID;
                        cardFile.Name = file.Name;
                        cardFile.CategoryCaption = file.Category;
                        cardFile.Size = file.Size;

                        if (!binaryMode)
                        {
                            // содержимое файла требуется в виде массива байт для сохранения в json, поэтому файл уже есть весь в памяти
                            cardFile.Hash = hashAlgorithm.ComputeHash(file.Content);

                            // в бинарной форме .card не рассчитываем хеш-сумму, чтобы не открывать файл лишний раз; хеш-сумма будет рассчитана автоматически сервером;
                            // тестирование показало, что суммарно времени на создание карточки и импорт уходит на 20% меньше, если считать хеш-сумму одновременно с импортом
                        }

                        cardFile.State = CardFileState.Inserted;

                        Card fileCard = cardFile.Card;
                        fileCard.ID = file.RowID;
                        fileCard.TypeID = CardHelper.FileTypeID;
                        fileCard.TypeName = CardHelper.FileTypeName;
                        fileCard.TypeCaption = CardHelper.FileTypeCaption;
                        fileCard.Created = utcNow;
                        fileCard.CreatedByID = Session.SystemID;
                        fileCard.CreatedByName = Session.SystemName;
                        fileCard.Modified = utcNow;
                        fileCard.ModifiedByID = Session.SystemID;
                        fileCard.ModifiedByName = Session.SystemName;

                        if (binaryMode)
                        {
                            CardHeaderFile headerFile = header.Files.Add(file.RowID);
                            headerFile.Size = file.Size;
                            headerFile.Order = header.Files.Count - 1;

                            fileStreams.Add(file.RowID, () => (Stream) FileHelper.OpenRead(file.FilePath));
                        }
                    }
                }

                var request = new CardStoreRequest { Card = card, Method = CardStoreMethod.Import };
                request.SetImportVersion(1);

                var container = new List<object> { request.GetStorage() };

                foreach (WebApplicationFile file in app.Files)
                {
                    container.Add(new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { CardComponentHelper.ContentFileIDKey, file.RowID },
                        { CardComponentHelper.ContentFileSizeKey, file.Size },
                        { CardComponentHelper.ContentFileDataKey, file.Content },
                    });
                }

                string outputExtension = binaryMode ? ".card" : ".jcard";
                if (string.IsNullOrWhiteSpace(outputPath) || outputPath == ".")
                {
                    outputPath = GetDefaultAppFileNameWithoutExtension(app) + outputExtension;
                }
                else if (outputPath.EndsWith("/", StringComparison.Ordinal)
                    || outputPath.EndsWith("\\", StringComparison.Ordinal))
                {
                    outputPath = Path.Combine(
                        outputPath[..^1],
                        GetDefaultAppFileNameWithoutExtension(app) + outputExtension);
                }
                else if (!outputPath.EndsWith(outputExtension, StringComparison.OrdinalIgnoreCase)
                    && Directory.Exists(outputPath))
                {
                    outputPath = Path.Combine(
                        outputPath,
                        GetDefaultAppFileNameWithoutExtension(app) + outputExtension);
                }

                await logger.InfoAsync("Writing package to: {0}", outputPath);

                string outputFolder = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputFolder) && !Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                if (binaryMode)
                {
                    await using FileStream targetStream = FileHelper.Create(outputPath);
                    var writer = new CardWriter(targetStream, SerializableObjectFormat.Binary);
                    await writer.WriteAsync(header);
                    await writer.WriteAsync(request);

                    foreach (CardHeaderFile headerFile in header.GetOrderedFiles())
                    {
                        await writer.WriteAsync(fileStreams[headerFile.ID]());
                    }
                }
                else
                {
                    await using FileStream targetStream = FileHelper.Create(outputPath, synchronousOnly: true);
                    await using var writer = new StreamWriter(targetStream, Encoding.UTF8, FileHelper.DefaultFileBufferSize, leaveOpen: true) { NewLine = "\n" };
                    using var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented };
                    TessaSerializer.JsonTyped.Serialize(jsonWriter, container);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync("Error packaging application", ex);
                return -1;
            }

            await logger.InfoAsync("Packaging is completed");
            return 0;
        }

        #endregion
    }
}