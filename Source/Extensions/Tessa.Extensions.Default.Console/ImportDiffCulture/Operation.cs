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

namespace Tessa.Extensions.Default.Console.ImportDiffCulture
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
            try
            {
                await this.Logger.InfoAsync(
                    "Combining localization between \"{0}\" and \"{1}\" for {2} language",
                    context.Source,
                    context.Output,
                    context.TargetCulture.TwoLetterISOLanguageName);
                
                bool targetPathIsDirectory = (File.GetAttributes(context.Output) & FileAttributes.Directory) == FileAttributes.Directory;
                bool sourcePathIsDirectory = (File.GetAttributes(context.Source) & FileAttributes.Directory) ==
                    FileAttributes.Directory;
                if (sourcePathIsDirectory && !targetPathIsDirectory)
                {
                    await this.Logger.ErrorAsync(
                        "Invalid command. Source path is a directory but target path \"{0}\" is a file. Potential multiple overrides.",
                        context.Output);
                    return -2;
                }

                var outputLibraries = new Dictionary<string, JsonFileLocalizationService>(StringComparer.OrdinalIgnoreCase);

                foreach (string outputFilePath in DefaultConsoleHelper
                                 .GetSourceFiles(
                                     context.Output,
                                     JsonFileLocalizationService.FileSearchPattern,
                                     throwIfNotFound: false))
                {
                    var localizationService = new JsonFileLocalizationService(new[] { outputFilePath });
                    LocalizationLibrary library =
                        (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                        .First();
                    if (outputLibraries.TryGetValue(library.Name, out var lib))
                    {
                        await this.Logger.ErrorAsync(
                            "Found duplicated localization library \"{1}\".{0}First occurrence \"{2}\".{0}Second occurence \"{3}\".",
                            Environment.NewLine, library.Name, lib.FileNames?.First() ?? string.Empty, outputFilePath);
                        return -3;
                    }
                    outputLibraries.Add(library.Name, localizationService);
                }

                foreach (string sourceFilePath in DefaultConsoleHelper
                                 .GetSourceFiles(
                                     context.Source,
                                     "*.xlsx",
                                     throwIfNotFound: false))
                {
                    string libraryName = !sourcePathIsDirectory && !targetPathIsDirectory 
                    ? outputLibraries.FirstOrDefault(static x => true).Key 
                    : Path.GetFileNameWithoutExtension(sourceFilePath);
                    
                    if (!outputLibraries.TryGetValue(libraryName, out var localizationService))
                    {
                        await this.Logger.InfoAsync("Library \"{0}\" is not found, skipping", libraryName);
                        continue;
                    }

                    LocalizationLibrary library =
                        (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                        .First();

                    Import(sourceFilePath, library, context.TargetCulture);
                    await localizationService.SaveLibraryAsync(library, cancellationToken);
                }

                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error combining localization", e);
                return -1;
            }
        }

        #endregion

        #region Private Methods

        private static void Import(string filePath, LocalizationLibrary library, CultureInfo culture)
        {
            using FileStream stream = FileHelper.OpenRead(filePath, synchronousOnly: true);
            using SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false);
            WorkbookPart workbookPart = document.WorkbookPart;

            if (workbookPart?.Workbook is null)
            {
                return;
            }

            SharedStringTablePart sharedStringPart = workbookPart
                .GetPartsOfType<SharedStringTablePart>()
                .FirstOrDefault();

            foreach (Sheet sheet in workbookPart.Workbook.Descendants<Sheet>())
            {
                if (sheet.Id is null)
                {
                    continue;
                }

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                ImportSheet(worksheetPart.Worksheet, sharedStringPart?.SharedStringTable, library, culture);
            }
        }

        private static void ImportSheet(
            Worksheet worksheet, 
            SharedStringTable sharedStringTable, 
            LocalizationLibrary library,
            CultureInfo culture)
        {
            if (worksheet is null)
            {
                return;
            }

            SheetData sheetData;
            Row rowHeader;

            if ((sheetData = worksheet.GetFirstChild<SheetData>()) is null
                || (rowHeader = sheetData.GetFirstChild<Row>()) is null
                || (rowHeader.RowIndex != 1))
            {
                return;
            }

            CultureInfo[] cultures = null;
            int cultureCount = 0;
            int cellIndex = 0;

            foreach (Cell cell in rowHeader.Descendants<Cell>())
            {
                cellIndex = GetCellIndex(cell, cellIndex);

                if (cellIndex < 3)
                {
                    continue;
                }

                // Trim possible spaces from culture's name
                string text = GetCellText(cell, sharedStringTable)?.Trim();

                if (text is null)
                {
                    continue;
                }

                if (cultures is null)
                {
                    cultures = new CultureInfo[4];
                }
                else if (cultures.Length == cultureCount)
                {
                    Array.Resize(ref cultures, cultureCount * 2);
                }

                try
                {
                    cultures[cultureCount] = CultureInfo.GetCultureInfo(text.Trim());
                    cultureCount++;
                }
                catch
                {
                    // ignored
                }
            }

            if (cultureCount == 0)
            {
                return;
            }

            foreach (Row row in sheetData.Descendants<Row>().Skip(1))
            {
                LocalizationEntry entry = null;
                cellIndex = 0;

                foreach (Cell cell in row.Descendants<Cell>())
                {
                    cellIndex = GetCellIndex(cell, cellIndex);
                    string text = GetCellText(cell, sharedStringTable);

                    if (entry is null)
                    {
                        if (text is null || cellIndex != 1 || !library.Entries.TryGetItem(text, out entry))
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (cellIndex > 2 && cultureCount > cellIndex - 3)
                        {
                            CultureInfo cellCulture = cultures[cellIndex - 3];
                            if (Equals(cellCulture, culture))
                            {
                                entry.Strings[cellCulture] = new LocalizationString(cellCulture, text);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static int GetCellIndex(Cell cell, int previousCellIndex)
        {
            StringValue cellReference = cell.CellReference;

            if (cellReference is null || cellReference.Value is null)
            {
                return ++previousCellIndex;
            }

            int position = 0;
            string reference = cellReference.Value;

            if (!TryGetColumnIndex(reference, ref position, out int actualColumnIndex))
            {
                return ++previousCellIndex;
            }

            return actualColumnIndex;
        }

        private static string GetCellText(Cell cell, SharedStringTable sharedStringTable) =>
            sharedStringTable is not null
            && cell.DataType is { Value: CellValues.SharedString }
            && int.TryParse(cell.InnerText, out int sharedStringIndex)
                ? sharedStringTable.ElementAt(sharedStringIndex).InnerText
                : cell.InnerText;

        private static bool TryGetColumnIndex(string address, ref int position, out int value)
        {
            value = 0;

            if (address[position] < 'A'
                || address[position] > 'Z')
            {
                return false;
            }

            value = address[position++] - 'A' + 1;

            while (position < address.Length
                   && address[position] >= 'A'
                   && address[position] <= 'Z')
            {
                value = value * 26 + address[position++] - 'A';
            }

            return true;
        }

        #endregion
    }
}
