using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <inheritdoc/>
    public sealed class ExcelDocumentParser : IExcelDocumentParser
    {
        #region IExcelDocumentParser Implemetation

        /// <inheritdoc/>
        public (ValidationResult, HashSet<string, WorksheetElement>) ParseDocument(SpreadsheetDocument document)
        {
            var worksheetHash = this.InitializeWorksheets(document);
            var result = this.InitializeTableGroups(document, worksheetHash);

            return result.IsSuccessful
                ? (result, worksheetHash)
                : (result, null);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Метод производит инициализацию табличных групп.
        /// </summary>
        private ValidationResult InitializeTableGroups(SpreadsheetDocument document, HashSet<string, WorksheetElement> worksheetHash)
        {
            ValidationResultBuilder validationResult = new ValidationResultBuilder();
            DefinedNames defNames = document.WorkbookPart.Workbook.DefinedNames;

            if (defNames != null && defNames.ChildElements.Count > 0)
            {
                List<TableGroup> allTableGroups = new List<TableGroup>();
                foreach (DefinedName defName in defNames.Elements<DefinedName>())
                {
                    TableGroupType type;
                    string prefix = defName.Name.Value.Length > 1 ? defName.Name.Value.Substring(0, 2) : string.Empty;
                    switch (prefix)
                    {
                        case "r_":
                            type = TableGroupType.Row;
                            break;

                        case "j_":
                            type = TableGroupType.Jump;
                            break;

                        case "g_":
                            type = TableGroupType.Group;
                            break;

                        case "t_":
                            type = TableGroupType.Table;
                            break;

                        default:
                            continue;
                    }

                    TableGroup newTableGroup = new TableGroup(defName, type);
                    newTableGroup.ParseElement();
                    if (!newTableGroup.IsValid)
                    {
                        validationResult.AddError(this, newTableGroup.ErrorText);
                        break;
                    }

                    allTableGroups.Add(newTableGroup);
                }

                foreach (var newTableGroup in allTableGroups
                    .OrderBy(x => x.Bottom)
                    .ThenByDescending(x => x.Top)
                    .ThenBy(x => x.Left)
                    .ThenByDescending(x => x.Right)
                    .ThenByDescending(x => x.Type))
                {
                    if (!worksheetHash.TryGetItem(newTableGroup.WorksheetName, out var worksheetElement))
                    {
                        // Если по каким то причинам для группы невозможно найти страницу, то пишем ошибку
                        validationResult.AddError(this,
                            "$FileTemplate_Excel_ErrorFindingPageInGroup",
                            newTableGroup.Name, newTableGroup.WorksheetName);

                        break;
                    }

                    newTableGroup.Initialize(worksheetElement);

                    if (!newTableGroup.IsValid)
                    {
                        validationResult.AddError(this, newTableGroup.ErrorText);
                        break;
                    }
                }
            }

            return validationResult.Build();
        }

        /// <summary>
        /// Метод производит инициализацию объектов Worksheets
        /// </summary>
        private HashSet<string, WorksheetElement> InitializeWorksheets(SpreadsheetDocument document)
        {
            var worksheetHash = new HashSet<string, WorksheetElement>(x => x.Name);

            WorkbookPart workboookPart = document.WorkbookPart;
            IEnumerable<WorksheetPart> worksheetParts = workboookPart.WorksheetParts;
            Sheet[] sheetCollection = document.WorkbookPart.Workbook.Sheets.Elements<Sheet>().ToArray();

            foreach (WorksheetPart worksheetPart in worksheetParts)
            {
                string relID = workboookPart.GetIdOfPart(worksheetPart);
                Sheet sheet = sheetCollection.FirstOrDefault(x => x.Id == relID);

                if (sheet != null)
                {
                    WorksheetElement worksheet = new WorksheetElement(worksheetPart.Worksheet, sheet.Name, worksheetHash);
                    worksheetHash.Add(worksheet);
                }
            }

            foreach (var worksheet in worksheetHash)
            {
                worksheet.ParseElement();
            }

            return worksheetHash;
        }

        #endregion
    }
}
