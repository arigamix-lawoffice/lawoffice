using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект для парсинга документа Word на группы.
    /// </summary>
    public interface IWordDocumentTableGroupParser
    {
        /// <summary>
        /// Метод для поиска табличных групп в документе <paramref name="wordDocument"/>. Возвращает сквозной список групп.
        /// </summary>
        /// <param name="wordDocument">Документ Word, в котором идет поиск табличных групп.</param>
        List<WordDocumentTableGroup> FindTableGroups(WordprocessingDocument wordDocument);

        /// <summary>
        /// Метод для заполнения табличных групп документа Word.
        /// </summary>
        /// <param name="tableGroups">Список табличных групп, для которых нужно заполнить актуальные данные по документу.</param>
        void FillTableGroups(WordprocessingDocument wordDocument, List<WordDocumentTableGroup> tableGroups);

        /// <summary>
        /// Метод для формирования связей между таблицами. Таблицы разных типов могут быть вложены друг в друга.
        /// </summary>
        /// <param name="newTables">Добавляемые таблицы.</param>
        /// <param name="tables">Уже существующий список таблиц, если он уже есть</param>
        /// <returns>Возвращает список таблиц со связями</returns>
        List<WordDocumentTableGroup> FillTableRelationships(
            IEnumerable<WordDocumentTableGroup> newTables,
            List<WordDocumentTableGroup> tables = null);
    }
}