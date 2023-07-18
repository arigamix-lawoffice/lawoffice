using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Applications;
using Tessa.Applications.Package;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.PackageApp
{
    public static class Operation
    {
        #region Private Methods

        private static string GetDefaultAppFileNameWithoutExtension(Application app) =>
            app.Alias + (app.Client64Bit ? "64" : null);

        private static async Task LoadInfoAsync(
            IConsoleLogger logger,
            Application app,
            string exePath,
            string icoPath,
            CancellationToken cancellationToken = default)
        {
            await logger.InfoAsync("Packaging application: {0}", exePath);

            RuntimeHelper.GetApplicationInfoForDefaultApps(
                exePath,
                out string name,
                out string alias,
                out Version version,
                out bool knownApp);

            if (!knownApp)
            {
                await logger.InfoAsync("Application is unknown, trying to load its assembly");

                try
                {
                    Assembly appAssembly = Assembly.LoadFrom(exePath);

                    RuntimeHelper.GetApplicationInfo(
                        appAssembly,
                        out string newName,
                        out string newAlias,
                        out Version newVersion);

                    name = newName;
                    alias = newAlias;
                    version = newVersion;
                }
                catch (Exception ex)
                {
                    await logger.LogExceptionAsync("Failed to load assembly, falling back to default alias and name", ex, warning: true);
                }
            }

            if (string.IsNullOrWhiteSpace(app.Alias))
            {
                if (string.IsNullOrWhiteSpace(alias))
                {
                    alias = RuntimeHelper.ApplicationDefaultAlias;
                }

                app.Alias = alias;
            }

            if (string.IsNullOrWhiteSpace(app.Name))
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Application";
                }

                app.Name = name;
            }

            if (string.IsNullOrWhiteSpace(app.Group))
            {
                app.Group = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(app.Version))
            {
                // если версия в файле не указана, то по умолчанию версия платформы преобразуется в объект Version,
                // который затем преобразуется в строку с четырьмя числами, т.е. к строковому представлению версии платформы
                // может быть добавлено один или два нуля с точками
                app.Version = (version ?? BuildInfo.VersionObject).ToString();
            }

            // эти свойства точно не null, но могут содержать оконечные пробелы
            app.Alias = app.Alias.Trim();
            app.Name = app.Name.Trim();
            app.Group = app.Group.Trim();
            app.Version = app.Version.Trim();
            app.ExeFileName = Path.GetFileName(exePath);

            // сборка, из которого фактически загружались атрибуты, это может быть .dll, когда запускаемый файл - .exe
            app.AssemblyFileName = app.ExeFileName;

            string appFolder = Path.GetDirectoryName(Path.GetFullPath(exePath));
            if (string.IsNullOrEmpty(appFolder))
            {
                appFolder = Directory.GetCurrentDirectory();
            }

            // если запускаемый файл .dll, то наверняка это .NET Core, у которого рядом лежит .exe
            if (string.Equals(Path.GetExtension(app.ExeFileName), ".dll", StringComparison.OrdinalIgnoreCase))
            {
                string exeFileName = Path.GetFileNameWithoutExtension(app.ExeFileName) + ".exe";
                if (File.Exists(Path.Combine(appFolder, exeFileName)))
                {
                    app.ExeFileName = exeFileName;
                }
            }

            await logger.InfoAsync("Packaging from folder: {0}", appFolder);
            await logger.InfoAsync("ExeFileName = {0}", app.ExeFileName);
            await logger.InfoAsync("AssemblyFileName = {0}", app.AssemblyFileName);
            await logger.InfoAsync("Version = {0}", app.Version);
            await logger.InfoAsync("Name = {0}", app.Name);
            await logger.InfoAsync("Group = {0}", string.IsNullOrEmpty(app.Group) ? "(empty)" : app.Group);
            await logger.InfoAsync("Alias = {0}", app.Alias);
            await logger.InfoAsync("Admin = {0}", app.Admin);
            await logger.InfoAsync("64bit = {0}", app.Client64Bit);
            await logger.InfoAsync("API v2 = {0}", app.AppManagerApiV2);
            await logger.InfoAsync("Hidden = {0}", app.Hidden);

            if (string.IsNullOrWhiteSpace(icoPath))
            {
                string iconFileNameWithoutExtension = Path.GetFileNameWithoutExtension(app.ExeFileName);
                icoPath = Path.Combine(appFolder, iconFileNameWithoutExtension + ".ico");

                if (iconFileNameWithoutExtension?.EndsWith("32", StringComparison.Ordinal) == true
                    && !File.Exists(icoPath))
                {
                    // для TessaClient32.exe - ищем иконку в TessaClient.ico, если не нашли TessaClient32.ico
                    icoPath = Path.Combine(appFolder, iconFileNameWithoutExtension[..^2] + ".ico");
                }
            }
            else if (!Path.IsPathRooted(icoPath))
            {
                icoPath = Path.Combine(appFolder, icoPath.NormalizePathOnCurrentPlatform()).NormalizePathOnCurrentPlatform();
            }

            await logger.InfoAsync("Loading icon from: {0}", icoPath);

            if (File.Exists(icoPath))
            {
                try
                {
                    app.Icon = await RuntimeHelper.TryGetRecommendedIconDataFromIcoFileAsync(icoPath, cancellationToken);

                    if (app.Icon is null)
                    {
                        await logger.InfoAsync("No suitable icon was found in file, packaging without it");
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    await logger.LogExceptionAsync("Failed to load icon, packaging without it", ex);
                }
            }
            else
            {
                await logger.InfoAsync("No icon found, packaging without it");
            }

            var ignoredProvider = new FileSystemIgnoredFilesProvider();
            var ignoredPathList = new HashSet<string>(ignoredProvider.GetIgnoredFileNames(appFolder));

            foreach (string path in ignoredPathList)
            {
                await logger.InfoAsync("Ignored: {0}", path);
            }

            foreach (string filePath in Directory.EnumerateFiles(appFolder, "*.*", SearchOption.AllDirectories))
            {
                if (!ignoredPathList.Contains(filePath))
                {
                    var file = new ApplicationFile(filePath, appFolder);
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
        }

        #endregion

        #region Methods

        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string exePath,
            string outputPath,
            string icoPath,
            string alias,
            string name,
            string group,
            string version,
            bool admin,
            bool client64Bit,
            bool appManagerApiV2,
            bool hidden,
            bool binaryMode,
            CancellationToken cancellationToken = default)
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

                var app = new Application 
                { 
                    Alias = alias, 
                    Name = name, 
                    Group = group, 
                    Version = version, 
                    Admin = admin, 
                    Client64Bit = client64Bit, 
                    AppManagerApiV2 = appManagerApiV2,
                    Hidden = hidden,
                };
                await LoadInfoAsync(logger, app, exePath, icoPath, cancellationToken: cancellationToken);

                DateTime utcNow = DateTime.UtcNow;

                var card = new Card
                {
                    ID = Guid.NewGuid(),
                    TypeID = CardHelper.ApplicationTypeID,
                    TypeName = CardHelper.ApplicationTypeName,
                    TypeCaption = CardHelper.ApplicationTypeCaption,
                    Created = utcNow,
                    CreatedByID = Session.SystemID,
                    CreatedByName = Session.SystemName,
                    Modified = utcNow,
                    ModifiedByID = Session.SystemID,
                    ModifiedByName = Session.SystemName,
                };

                StringDictionaryStorage<CardSection> sections = card.Sections;
                sections.GetOrAddTable("ApplicationRoles");

                Dictionary<string, object> fields = sections.GetOrAddEntry(ApplicationCardConstants.ApplicationSection.SectionName).RawFields;
                fields[ApplicationCardConstants.ApplicationSection.AliasField] = app.Alias;
                fields[ApplicationCardConstants.ApplicationSection.AppVersionField] = app.Version;
                fields[ApplicationCardConstants.ApplicationSection.ExecutableFileNameField] = app.ExeFileName;
                fields[ApplicationCardConstants.ApplicationSection.ForAdminField] = BooleanBoxes.Box(app.Admin);
                fields[ApplicationCardConstants.ApplicationSection.GroupNameField] = app.Group;
                fields[ApplicationCardConstants.ApplicationSection.IconField] = app.Icon;
                fields[ApplicationCardConstants.ApplicationSection.NameField] = app.Name;
                fields[ApplicationCardConstants.ApplicationSection.PlatformVersionField] = BuildInfo.Version;
                fields[ApplicationCardConstants.ApplicationSection.Client64BitField] = BooleanBoxes.Box(app.Client64Bit);
                fields[ApplicationCardConstants.ApplicationSection.AppManagerApiV2Field] = BooleanBoxes.Box(app.AppManagerApiV2);
                fields[ApplicationCardConstants.ApplicationSection.HiddenField] = BooleanBoxes.Box(app.Hidden);

                var header = binaryMode ? new CardHeader() : null;
                var fileStreams = binaryMode ? new Dictionary<Guid?, Func<ValueTask<Stream>>>() : null;

                if (app.Files.Count > 0)
                {
                    ListStorage<CardFile> cardFiles = card.Files;
                    using HashAlgorithm hashAlgorithm = HashSignatureProvider.Files.CreateAlgorithm();
                    foreach (ApplicationFile file in app.Files)
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

                        // Считаем хеш и длину
                        await using (var stream = await file.FileContentProvider.CreateStreamReadAsync(cancellationToken))
                        {
                            cardFile.Hash = await hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
                            file.Size = stream.Length;
                        }

                        cardFile.Size = file.Size;
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

                            fileStreams.Add(file.RowID, async () => await file.FileContentProvider.CreateStreamReadAsync(cancellationToken));
                        }
                    }
                }

                var request = new CardStoreRequest { Card = card, Method = CardStoreMethod.Import };
                request.SetImportVersion(1);

                var container = new List<object> { request.GetStorage() };

                string outputExtension = binaryMode ? ".card" : ".jcard";

                var sourceDirectoryName = Path.GetDirectoryName(exePath) ?? string.Empty;
                foreach (ApplicationFile file in app.Files)
                {
                    var fileName = Path.GetRelativePath(sourceDirectoryName, file.FileContentProvider.GetFullName());
                    fileName = fileName.Replace("\\", "/", StringComparison.Ordinal);

                    container.Add(new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { CardComponentHelper.ContentFileIDKey, file.RowID },
                        { CardComponentHelper.ContentFileSizeKey, file.Size },
                        { CardComponentHelper.ContentFileReferenceKey, fileName },
                    });
                }

                if (string.IsNullOrWhiteSpace(outputPath) || outputPath == ".")
                {
                    outputPath = GetDefaultAppFileNameWithoutExtension(app) + outputExtension;
                }
                else if (outputPath.EndsWith("/", StringComparison.Ordinal)
                    || outputPath.EndsWith("\\", StringComparison.Ordinal))
                {
                    outputPath = Path.Combine(outputPath[..^1], GetDefaultAppFileNameWithoutExtension(app) + outputExtension);
                }
                else if (!outputPath.EndsWith(outputExtension, StringComparison.OrdinalIgnoreCase)
                    && Directory.Exists(outputPath))
                {
                    outputPath = Path.Combine(outputPath, GetDefaultAppFileNameWithoutExtension(app) + outputExtension);
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
                    await writer.WriteAsync(header, cancellationToken);
                    await writer.WriteAsync(request, cancellationToken);

                    foreach (CardHeaderFile headerFile in header.GetOrderedFiles())
                    {
                        await writer.WriteAsync(await fileStreams[headerFile.ID](), cancellationToken);
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
