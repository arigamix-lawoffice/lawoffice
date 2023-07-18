using DocumentFormat.OpenXml.Packaging;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Парсер документов Excel.
    /// </summary>
    public interface IExcelDocumentParser
    {
        /// <summary>
        /// Метод для парсинга документа Excel на хеш элементов-страниц по имени страницы.
        /// </summary>
        /// <param name="document">Документ Excel.</param>
        /// <returns>Результат валидации парсинга и хеш со списокм страниц документа Excel по имени страницы, если парсинг был успешен.</returns>
        (ValidationResult, HashSet<string, WorksheetElement>) ParseDocument(SpreadsheetDocument document);
    }
}
