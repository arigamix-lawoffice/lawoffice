using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Console.ExportDiffCulture
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger)
            : base(logger, sessionManager)
        {
        }

        #endregion

        #region Base Overrides

        public override async Task<int> ExecuteAsync(
           OperationContext context,
           CancellationToken cancellationToken = default)
        {
            if (context.Sources.Count == 0)
            {
                await this.Logger.ErrorAsync("No sources are specified");
                return -2;
            }

            if (context.BaseCulture.Equals(context.TargetCulture))
            {
                await this.Logger.ErrorAsync("Given cultures are equal");
                return -3;
            }

            var sourceLibraryPath = context.Sources[0];
            var compareLibraryPaths = context.Sources.Skip(1);

            try
            {
                await this.Logger.InfoAsync(
                    "Comparing localization between \"{0}\" and \"{1}\" for {2} language",
                    sourceLibraryPath,
                    string.Join(", ", compareLibraryPaths),
                    context.TargetCulture.TwoLetterISOLanguageName);

                var sourceLibraries = new Dictionary<string, (LocalizationLibrary Library, string Path)>();

                foreach (string sourceFilePath in DefaultConsoleHelper
                                 .GetSourceFiles(
                                     sourceLibraryPath,
                                     JsonFileLocalizationService.FileSearchPattern,
                                     throwIfNotFound: false))
                {
                    var localizationService = new JsonFileLocalizationService(new[] { sourceFilePath });

                    LocalizationLibrary library =
                        (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                        .First();
                    if (sourceLibraries.TryGetValue(library.Name, out var lib))
                    {
                        await this.Logger.ErrorAsync(
                            "There are duplicates of localization library \"{1}\".{0}First occurrence \"{2}\".{0}Second occurence \"{3}\".",
                            Environment.NewLine, library.Name, lib.Path, sourceFilePath);
                        return -4;
                    }
                    sourceLibraries.Add(library.Name, (library, sourceFilePath));
                }

                if (sourceLibraries.Count > 0)
                {
                    FileHelper.CreateDirectoryIfNotExists(context.Output, true);
                }
                var compareLibraries = new Dictionary<string, List<LocalizationLibrary>>();

                foreach (var compareLibraryPath in compareLibraryPaths)
                {
                    foreach (string compareFilePath in DefaultConsoleHelper
                                 .GetSourceFiles(
                                     compareLibraryPath,
                                     JsonFileLocalizationService.FileSearchPattern,
                                     throwIfNotFound: false))
                    {
                        var localizationService = new JsonFileLocalizationService(new[] { compareFilePath });

                        LocalizationLibrary library =
                            (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                            .First();
                        if (compareLibraries.TryGetValue(library.Name, out var compareLibraryList))
                        {
                            compareLibraryList.Add(library);
                        }
                        else
                        {
                            compareLibraries.Add(library.Name, new List<LocalizationLibrary>(1) { library });
                        }
                    }
                }

                foreach (var (libraryName, (sourceLibrary, _)) in sourceLibraries)
                {
                    LocalizationLibrary diffLibrary;

                    if (compareLibraries.TryGetValue(libraryName, out var compareLibraryList))
                    {
                        diffLibrary = new LocalizationLibrary(sourceLibrary.ID, sourceLibrary.Name, sourceLibrary.Priority);
                        foreach (var sourceEntry in sourceLibrary.Entries)
                        {
                            var found = false;

                            foreach (var compareLibrary in compareLibraryList)
                            {
                                if (!compareLibrary.Entries.TryGetItem(sourceEntry.Name, out var compareEntry))
                                {
                                    continue;
                                }

                                found = true;

                                var sourceLocalizationString = sourceEntry.Strings[context.BaseCulture];
                                var compareLocalizationString = compareEntry.Strings[context.BaseCulture];

                                if (!string.Equals(sourceLocalizationString.Value, compareLocalizationString.Value, StringComparison.Ordinal))
                                {
                                    diffLibrary.Entries.Add(sourceEntry);
                                }

                                break;
                            }

                            if (!found)
                            {
                                // В основной библиотеке есть строка локализации, которой нет в сравниваемой.
                                diffLibrary.Entries.Add(sourceEntry);
                            }
                        }
                    }
                    else
                    {
                        // Раньше такой библиотеки не было: указываем ее всю как требуемую локализации
                        diffLibrary = sourceLibrary;
                    }

                    if (diffLibrary.Entries.Count == 0)
                    {
                        await this.Logger.InfoAsync("Library \"{0}\" is skipped due to no changes", libraryName);
                        continue;
                    }

                    var exportPath = Path.Combine(context.Output, libraryName + ".xlsx");
                    await this.Logger.InfoAsync("Exporing library \"{0}\" diff: \"{1}\"", libraryName, exportPath);
                    Export(exportPath, diffLibrary, new[] { context.BaseCulture, context.TargetCulture });
                }

                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error comparing localization", e);
                return -1;
            }
        }

        #endregion

        #region Private Methods

        private static void Export(string filePath, LocalizationLibrary library, CultureInfo[] cultures)
        {
            using FileStream stream = FileHelper.Create(filePath, synchronousOnly: true);
            using SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet();

            SharedStringTablePart sharedStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
            sharedStringPart.SharedStringTable = new SharedStringTable();

            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
            Sheet sheet = sheets.AppendChild(new Sheet());

            sheet.Id = workbookPart.GetIdOfPart(worksheetPart);
            sheet.SheetId = 1;
            sheet.Name = LocalizationManager.GetString("Localization_Translations");

            SharedStringTable sharedStringTable = sharedStringPart.SharedStringTable;
            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

            uint rowIndex = 1;
            Row rowHeaders = sheetData.AppendChild(new Row { RowIndex = rowIndex++ });

            AppendCell(rowHeaders, sharedStringTable, LocalizationManager.GetString("UI_Common_Name"));
            AppendCell(rowHeaders, sharedStringTable, LocalizationManager.GetString("UI_Common_Comment"));

            foreach (CultureInfo culture in cultures)
            {
                AppendCell(rowHeaders, sharedStringTable, culture.Name);
            }

            foreach (LocalizationEntry entry in library.Entries)
            {
                Row row = sheetData.AppendChild(new Row { RowIndex = rowIndex++ });

                AppendCell(row, sharedStringTable, entry.Name);
                AppendCell(row, sharedStringTable, entry.Comment);

                foreach (CultureInfo culture in cultures)
                {
                    if (entry.Strings.TryGetItem(culture, out LocalizationString str))
                    {
                        AppendCell(row, sharedStringTable, str.Value);
                    }
                }
            }
        }

        private static void AppendCell(Row row, SharedStringTable sharedStringTable, string text)
        {
            Cell cell = row.AppendChild(new Cell());

            cell.CellValue = new CellValue(AppendSharedStringItem(sharedStringTable, text));
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
        }

        private static string AppendSharedStringItem(SharedStringTable sharedStringTable, string text)
        {
            int index = 0;

            foreach (SharedStringItem item in sharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return index.ToString();
                }

                index++;
            }

            sharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
            return index.ToString();
        }

        #endregion
    }
}
