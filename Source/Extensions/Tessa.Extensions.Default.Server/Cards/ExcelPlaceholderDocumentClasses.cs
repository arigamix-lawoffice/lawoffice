using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Placeholders;
using A = DocumentFormat.OpenXml.Drawing;
using Hyperlink = DocumentFormat.OpenXml.Spreadsheet.Hyperlink;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;


namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Класс с вспомогательными методами для обработки Excel
    /// </summary>
    public static class ExcelHelper
    {
        #region Private Fields

        /// <summary>
        /// Regex шаблон для строк
        /// </summary>
        private static readonly Regex rowPattern = new Regex(@"[^\d+]");

        /// <summary>
        /// Regex шаблон для столбцов
        /// </summary>
        private static readonly Regex columnPattern = new Regex(@"[^A-Z]+");

        #endregion

        #region Public Fields and Constants

        /// <summary>
        /// Пустой список плейсхолдеров
        /// </summary>
        public static readonly IList<IPlaceholder> EmptyPlaceholders = EmptyHolder<IPlaceholder>.Collection;

        /// <summary>
        /// Настройки форматирования при переводе значения double в строку, обозначающюю дату/время в Excel
        /// </summary>
        public static readonly NumberFormatInfo DoubleExcelFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

        /// <summary>
        /// Имя первой колонки в Excel
        /// </summary>
        public const string FirstColumn = "A";

        /// <summary>
        /// Имя последней колонки в Excel
        /// </summary>
        public const string LastColumn = "XFD";

        #endregion

        #region Static Methods

        /// <summary>
        /// Производит действие при отсутствии доступа к операции
        /// </summary>
        /// <param name="operation">Название операции или метода</param>
        /// <param name="objectType">Тип объекта</param>
        public static void NotSupported(string operation, string objectType)
        {
            throw new NotSupportedException(
                string.Format(LocalizationManager.GetString("KrMessages_ExcelTemplate_OperationNotSupported"),
                    operation,
                    objectType));
        }

        /// <summary>
        /// Производит сравнение значений колонок Excel между собой
        /// </summary>
        /// <param name="x">Имя первой колонки</param>
        /// <param name="y">Имя второй колонки</param>
        /// <returns>Возвращает положительное число, если x > y, отрицаительное, если y > x и 0, если они равны</returns>
        public static int Compare(string x, string y)
        {
            if (x.Length > y.Length)
            {
                return 1;
            }
            if (x.Length < y.Length)
            {
                return -1;
            }
            return string.CompareOrdinal(x, y);
        }

        /// <summary>
        /// Метод для получения номера строки из ссылки ячейки
        /// </summary>
        /// <param name="reference">Ссылка ячейки</param>
        /// <returns>Возвращает номер строки переданной ссылки</returns>
        public static int GetRowIndex(string reference)
        {
            return int.Parse(rowPattern.Replace(reference, string.Empty));
        }

        /// <summary>
        /// Метод для получения имени колонки из ссылки ячейки
        /// </summary>
        /// <param name="reference">Ссылка ячейки</param>
        /// <returns>Возвращает имя колонки переданной ссылки</returns>
        public static string GetColumnIndex(string reference)
        {
            return columnPattern.Replace(reference, string.Empty);
        }

        /// <summary>
        /// Возвращает имя колонки в Excel по отсчитываемому от нуля индексу.
        /// Например: <c>0 = A, 1 = B, 2 = C, ..., 26 = AA, 27 = AB, ...</c>
        /// </summary>
        /// <param name="index">Отсчитываемый от нуля индекс колонки.</param>
        /// <returns>Строка с именем колонки в Excel, соответствующая заданному индексу.</returns>
        public static string GetColumnName(int index)
        {
            const byte alphabetLength = 'Z' - 'A' + 1; // 26

            string name = string.Empty;

            while (index >= 0)
            {
                name = Convert.ToChar('A' + index % alphabetLength) + name;
                index = index / alphabetLength - 1;
            }

            return name;
        }

        /// <summary>
        /// Возвращает номер колонки по ее имени. Номер колонки отсчитывается от нуля.
        /// </summary>
        /// <param name="columnName">Имя колонки. Например A = 0, B = 1,...,Z = 25, AA = 26</param>
        /// <returns>Номер колонки.</returns>
        public static int ColumnNameToInt(string columnName)
        {
            const byte alphabetLength = 'Z' - 'A' + 1; // 26
            const byte charValueOffset = 'A' - 1;

            int result = 0;
            foreach (char c in columnName)
            {
                result = result * alphabetLength + c - charValueOffset;
            }

            return result - 1;
        }

        #endregion
    }

    /// <summary>
    /// Класс, описывающий общие свойства хранилищ элементов Excel
    /// </summary>
    /// <typeparam name="TElement">Тип хранимого элемента Excel</typeparam>
    public abstract class ElementBase<TElement> where TElement : OpenXmlElement
    {
        #region Constructors

        protected ElementBase(TElement element)
        {
            this.Element = element;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Хранимый элемент Excel
        /// </summary>
        public TElement Element { get; }

        #endregion

        #region Abstract And Virtual Methods

        /// <summary>
        /// Метод для клонирования хранимого элемента.
        /// </summary>
        /// <returns>Возвращает новый объект хранилища с клонируемым элементом Excel</returns>
        public abstract ElementBase<TElement> Clone();

        /// <summary>
        /// Метод для вставки элемента.
        /// </summary>
        public abstract void Insert();

        /// <summary>
        /// Метод для получения основных данных из хранимого элемента
        /// </summary>
        public abstract void ParseElement();

        /// <summary>
        /// Метод для удаления объекта и его элемента
        /// </summary>
        public virtual void Remove()
        {
            this.Element.SafeRemove();
        }

        /// <summary>
        /// Метод для обновления позиции элемента в документе Excel
        /// </summary>
        public abstract void Update();

        #endregion

        #region Protected Methods

        /// <summary>
        /// Метод для клонирования хранимого элемента
        /// </summary>
        /// <returns>Возвращает полный клон хранимого элемента</returns>
        protected TElement CloneElement()
        {
            return (TElement)this.Element.CloneNode(true);
        }

        #endregion
    }

    /// <summary>
    /// Класс-хранилище для упрощенной работы с элементои типа Worksheet
    /// </summary>
    public sealed class WorksheetElement : ElementBase<Worksheet>
    {
        #region Constructors

        public WorksheetElement(Worksheet worksheet, string name, HashSet<string, WorksheetElement> worksheetHash)
            : base(worksheet)
        {
            this.Name = name;
            this.WorksheetHash = worksheetHash;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Имя элемента
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Список объектов смерженных ячеек, хранимых в данном элементе
        /// </summary>
        public List<MergeCellGroup> MergeCells { get; private set; }

        /// <summary>
        /// Список объектов гиперссылок, хранимых в данном элементе
        /// </summary>
        public List<HyperlinkCellGroup> Hyperlinks { get; private set; }

        /// <summary>
        /// Список объектов строк, хранимых в данном элементе
        /// </summary>
        public List<RowCellGroup> Rows { get; private set; }

        /// <summary>
        /// Список объектов-якорей, используемых для привязки надписей и картинок к данному элементу
        /// </summary>
        public List<AnchorCellGroup> Anchors { get; private set; }

        /// <summary>
        /// Список всех таблиц верхнего  уровня в текущем элементе, отсортированных сверху вниз.
        /// </summary>
        public List<TableGroup> Tables { get; private set; }

        /// <summary>
        /// Хеш страниц Excel по имени.
        /// </summary>
        public HashSet<string, WorksheetElement> WorksheetHash { get; }

        /// <summary>
        /// Флаг определяет, есть ли в данном листе формула или источник данных для формулы.
        /// </summary>
        public bool HasFormulas { get; set; }

        private Dictionary<uint, FormulaElement> sharedFormulas;

        /// <summary>
        /// Расшаренные формулы.
        /// </summary>
        public Dictionary<uint, FormulaElement> SharedFormulas => this.sharedFormulas ??= new Dictionary<uint, FormulaElement>();

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных из элемента Worksheet
        /// </summary>
        public override void ParseElement()
        {
            this.InitializeHyperlinks();
            this.InitializeMergeCells();
            this.InitializeRows();
            this.InitializeAnchors();
            this.InitializeTables();
        }

        /// <summary>
        /// Клонирование элемента типа Worksheet недоступно
        /// </summary>
        public override ElementBase<Worksheet> Clone()
        {
            ExcelHelper.NotSupported(nameof(Clone), nameof(WorksheetElement));
            return null;
        }

        /// <summary>
        /// Данный метод недоступен для текущего типа
        /// </summary>
        public override void Insert()
        {
            ExcelHelper.NotSupported(nameof(Insert), nameof(TableGroup));
        }

        /// <summary>
        /// Удаление элемента типа Worksheet недоступно
        /// </summary>
        public override void Remove()
        {
            ExcelHelper.NotSupported(nameof(Remove), nameof(WorksheetElement));
        }

        /// <summary>
        /// Производит обновление позиций всех строк, объединенных ячеек и гиперссылок в текущем Worksheet
        /// </summary>
        public override void Update()
        {
            this.Rows.ForEach(x => x.Update());
            this.MergeCells.ForEach(x => x.Update());
            this.Hyperlinks.ForEach(x => x.Update());
            this.Anchors.ForEach(x => x.Update());

            // Только строки добавляем в конце, для остальных элементов порядок не важен
            SheetData rowData = this.Element.Elements<SheetData>().FirstOrDefault();
            rowData.RemoveAllChildren();

            foreach (var row in this.Rows.OrderBy(x => x.Top))
            {
                rowData.AppendChild(row.Element);
            }

            MergeCells mergeCells = this.Element.Elements<MergeCells>().FirstOrDefault();
            if (mergeCells != null
                && mergeCells.ChildElements.Count == 0)
            {
                mergeCells.Remove();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Производит инициализацию объектов гиперссылок
        /// </summary>
        private void InitializeHyperlinks()
        {
            this.Hyperlinks = new List<HyperlinkCellGroup>();
            Hyperlinks hyperlinks = this.Element.Elements<Hyperlinks>().FirstOrDefault();
            if (hyperlinks == null)
            {
                return;
            }
            foreach (var element in hyperlinks)
            {
                var hyperlink = (Hyperlink)element;

                HyperlinkCellGroup group = new HyperlinkCellGroup(hyperlink, this);
                group.ParseElement();
                this.Hyperlinks.Add(group);
            }
        }

        /// <summary>
        /// Производит инициализацию объектов смерженных ячеек
        /// </summary>
        private void InitializeMergeCells()
        {
            this.MergeCells = new List<MergeCellGroup>();
            MergeCells mergeCells = this.Element.Elements<MergeCells>().FirstOrDefault();
            if (mergeCells == null)
            {
                return;
            }
            foreach (var element in mergeCells)
            {
                var mergeCell = (MergeCell)element;

                MergeCellGroup group = new MergeCellGroup(mergeCell, this);
                group.ParseElement();
                this.MergeCells.Add(group);
            }
        }

        /// <summary>
        /// Производит инициализацию объектов строк
        /// </summary>
        private void InitializeRows()
        {
            this.Rows = new List<RowCellGroup>();
            SheetData rowData = this.Element.Elements<SheetData>().FirstOrDefault();
            if (rowData == null)
            {
                return;
            }
            foreach (Row row in rowData.Elements<Row>())
            {
                RowCellGroup rowElement = new RowCellGroup(row, this);
                rowElement.ParseElement();
                this.Rows.Add(rowElement);
            }
        }

        /// <summary>
        /// Производит инициализацию объектов якорей
        /// </summary>
        private void InitializeAnchors()
        {
            this.Anchors = new List<AnchorCellGroup>();

            var anchors = this.Element.WorksheetPart.DrawingsPart?.WorksheetDrawing?.Elements<Xdr.TwoCellAnchor>();
            if (anchors != null)
            {
                foreach (Xdr.TwoCellAnchor anchor in anchors)
                {
                    var group = new AnchorCellGroup(anchor, this);
                    group.ParseElement();
                    this.Anchors.Add(group);
                }
            }
        }

        /// <summary>
        /// Производит инициализацию списка таблиц
        /// </summary>
        private void InitializeTables()
        {
            this.Tables = new List<TableGroup>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Производит перемещение всех элементов данного Worksheet, начиная с moveFrom строки (не включая ее) на moveBy
        /// </summary>
        /// <param name="moveBy">Число, на которое необходимо переместить элементы</param>
        /// <param name="moveFrom">Номер строки, начиная с которой производится перемещение элементов</param>
        public void Move(int moveBy, int moveFrom)
        {
            if (moveBy == 0)
            {
                return;
            }

            foreach (RowCellGroup row in this.Rows.Where(x => x.Bottom > moveFrom))
            {
                row.Move(moveBy);
            }
            foreach (HyperlinkCellGroup hyp in this.Hyperlinks.Where(x => x.Bottom > moveFrom))
            {
                hyp.Move(moveBy);
            }
            foreach (MergeCellGroup mc in this.MergeCells.Where(x => x.Bottom > moveFrom))
            {
                mc.Move(moveBy);
            }
            foreach (AnchorCellGroup anchor in this.Anchors.Where(x => x.Bottom > moveFrom))
            {
                anchor.Move(moveBy);
            }
        }

        /// <summary>
        /// Метод для получения строки по ее номеру
        /// </summary>
        /// <param name="rowIndex">Номер строки</param>
        /// <returns>Возвращает первую найденную строку, или null, если таких строк нет</returns>
        public RowCellGroup GetRow(int rowIndex)
        {
            return this.Rows.FirstOrDefault(x => x.Top == rowIndex);
        }

        #endregion
    }

    /// <summary>
    /// Класс, определяющий общие свойства объектов, хранимых на базе Worksheet
    /// </summary>
    /// <typeparam name="TElement">Тип хранимого элемента Excel</typeparam>
    public abstract class WorksheetBase<TElement> : ElementBase<TElement> where TElement : OpenXmlElement
    {
        #region Constructors

        protected WorksheetBase(TElement element)
            : base(element)
        {
        }

        protected WorksheetBase(TElement element, WorksheetElement worksheet)
            : base(element)
        {
            this.Worksheet = worksheet;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Объект Worksheet, на базе которого хранится текущий объект
        /// </summary>
        public WorksheetElement Worksheet { get; protected set; }

        #endregion
    }

    /// <summary>
    /// Интерфейс для объектов, являющихся хранилищем для одной или нескольких ячеек
    /// </summary>
    public interface ICellsGroup
    {
        /// <summary>
        /// Свойство, определяющее, состоит ли объект из одной ячейки
        /// </summary>
        bool IsSingleCell { get; }

        /// <summary>
        /// Определяет верхнюю границу диапозона
        /// </summary>
        int Top { get; }

        /// <summary>
        /// Определяет нижнюю границу диапозона
        /// </summary>
        int Bottom { get; }

        /// <summary>
        /// Определяет левую границу диапозона
        /// </summary>
        string Left { get; }

        /// <summary>
        /// Определяет правую границу диапозона
        /// </summary>
        string Right { get; }

        /// <summary>
        /// Определяет высоту (количество строк) диапозона
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Возвращает строковый вариант диапозона (например 'B2' для одной ячейки или 'B2:D4' для группы ячеек)
        /// </summary>
        string Reference { get; }
    }

    /// <summary>
    /// Класс для объектов, являющихся хранилищем для одной или нескольких ячеек, включающий в себя общие методы для работы с группой ячеек
    /// </summary>
    public abstract class CellsGroup<TElement> : WorksheetBase<TElement>, ICellsGroup where TElement : OpenXmlElement
    {
        #region Constructors

        protected CellsGroup(TElement element)
            : base(element)
        {
        }

        protected CellsGroup(TElement element, WorksheetElement worksheet)
            : base(element, worksheet)
        {
        }

        #endregion

        #region Properties

        #region Fields

        /// <summary>
        /// Параметр, определяющий на какое число нужно переместить элемент при обновлении
        /// </summary>
        public int MoveBy { get; protected set; }

        #endregion
        /// <summary>
        /// Определяет верхнюю границу диапозона
        /// </summary>
        public int Top { get; protected set; }

        /// <summary>
        /// Определяет нижнюю границу диапозона
        /// </summary>
        public int Bottom { get; protected set; }

        /// <summary>
        /// Определяет левую границу диапозона
        /// </summary>
        public string Left { get; protected set; }

        /// <summary>
        /// Определяет правую границу диапозона
        /// </summary>
        public string Right { get; protected set; }

        /// <summary>
        /// Определяет высоту (количество строк) диапозона
        /// </summary>
        public int Height => this.Top - this.Bottom + 1;

        /// <summary>
        /// Свойство, определяющее, состоит ли объект из одной ячейки
        /// </summary>
        public bool IsSingleCell => this.Left == this.Right && this.Top == this.Bottom;

        /// <summary>
        /// Возвращает строковый вариант диапозона (например 'B2' для одной ячейки или 'B2:D4' для группы ячеек)
        /// </summary>
        public string Reference =>
            this.IsSingleCell
                ? this.Left + this.Bottom
                : this.Left + this.Bottom + ":" + this.Right + this.Top;

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Метод для получения отображаемого значения элемента. Обычно это <see cref="Reference"/>
        /// </summary>
        /// <returns></returns>
        public virtual string GetDisplayString() => this.Reference;

        /// <summary>
        /// Метод для перемещения элемента внутри документа Excel на заданное число. Фактическое обновление позиции элемента в документе производится методом Update
        /// </summary>
        /// <param name="moveBy">Число, на которое требуется переместить хранимый элемент</param>
        public virtual void Move(int moveBy)
        {
            this.MoveBy += moveBy;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает признак того, что среди наследников текущего элемента присутствует указанный элемент.
        /// </summary>
        /// <typeparam name="T">Тип искомого элемента. Объекты других типов игнорируются.</typeparam>
        /// <param name="element">Искомый элемент.</param>
        /// <returns>
        /// <c>true</c>, если среди наследников текущего элемента присутствует указанный элемент;
        /// <c>false</c> в противном случае.
        /// </returns>
        public bool HasChildElement<T>(T element)
            where T : OpenXmlElement
        {
            return this.Element.Descendants<T>().Contains(element);
        }

        /// <summary>
        /// Проверяет, включает ли данная группа в себя передаваемую ссылку.
        /// </summary>
        /// <param name="reference">Ссылка на ячейку или диапозон ячеек</param>
        /// <returns>Возвращает true, если текущая группа ячеек включает в себя группу ячеек по передаваемой ссылке</returns>
        public bool IsInclude(string reference)
        {
            if (reference.Contains(":", StringComparison.Ordinal))
            {
                return this.IsIncludeGroup(reference);
            }
            else
            {
                string column = ExcelHelper.GetColumnIndex(reference);
                int row = ExcelHelper.GetRowIndex(reference);
                return this.IsIncludeCell(column, row);
            }
        }

        /// <summary>
        /// Проверяет, включает ли данная группа в себя передаваемую группу
        /// </summary>
        /// <param name="group">Передаваемая группа</param>
        /// <returns>Возвращает true, если текущая группа ячеек включает в себя передаваемую группу</returns>
        public bool IsInclude(ICellsGroup group)
        {
            if (group.IsSingleCell)
            {
                return this.IsIncludeCell(group.Left, group.Bottom);
            }
            else
            {
                return this.IsIncludeGroup(group.Left, group.Right, group.Bottom, group.Top);
            }
        }

        /// <summary>
        /// Метод проверяет, входит ли передаваеммая группа ячеек в текущую группу
        /// </summary>
        /// <param name="reference">Ссылка на группу ячеек</param>
        /// <returns>Возвращает true, если передаваеммая группа ячеек входит в текущую группу</returns>
        public bool IsIncludeGroup(string reference)
        {
            string[] refs = reference.Split(':');
            if (refs.Length != 2)
            {
                throw new ArgumentException("Wrong parameter '" + reference + "'", "reference");
            }
            string leftR = ExcelHelper.GetColumnIndex(refs[0]),
                rightR = ExcelHelper.GetColumnIndex(refs[1]);
            int bottomR = ExcelHelper.GetRowIndex(refs[0]),
                topR = ExcelHelper.GetRowIndex(refs[1]);

            return this.IsIncludeGroup(leftR, rightR, bottomR, topR);
        }

        /// <summary>
        /// Метод проверяет, входит ли передаваеммая группа ячеек в текущую группу
        /// </summary>
        /// <param name="leftR">Левая граница передаваемой группы</param>
        /// <param name="rightR">Правая граница передаваемой группы</param>
        /// <param name="bottomR">Нижняя граница передаваемой группы</param>
        /// <param name="topR">Верхняя граница передаваемой группы</param>
        /// <returns>Возвращает true, если передаваеммая группа ячеек входит в текущую группу</returns>
        public bool IsIncludeGroup(string leftR, string rightR, int bottomR, int topR)
        {
            return ExcelHelper.Compare(rightR, this.Right) <= 0
                && ExcelHelper.Compare(leftR, this.Left) >= 0
                && bottomR >= this.Bottom
                && topR <= this.Top;
        }

        /// <summary>
        /// Метод проверяет, входит ли передаваеммая ячейка в текущую группу
        /// </summary>
        /// <param name="column">Имя колонки передаваемой ячейки</param>
        /// <param name="row">Номер строки передаваемой ячейки</param>
        /// <returns>Возвращает true, если передаваеммая ячейка входит в текущую группу</returns>
        public bool IsIncludeCell(string column, int row)
        {
            return ExcelHelper.Compare(column, this.Right) <= 0
                && ExcelHelper.Compare(column, this.Left) >= 0
                && row >= this.Bottom
                && row <= this.Top;
            //column <= right && column >= left && row >= bottom && row <= top;
        }

        /// <summary>
        /// Метод проверяет, есть ли пересечения между текущей и передаваемой группами
        /// </summary>
        /// <param name="group">Группа ячеек</param>
        /// <returns>Возвращает true, если есть пересечение</returns>
        public bool IsCrossed(ICellsGroup group)
        {
            return this.IsCrossed(group.Left, group.Right, group.Bottom, group.Top);
        }

        /// <summary>
        /// Метод проверяет, есть ли пересечения между текущей и передаваемой группами
        /// </summary>
        /// <param name="leftR">Левая граница передаваемой группы</param>
        /// <param name="rightR">Правая граница передаваемой группы</param>
        /// <param name="bottomR">Нижняя граница передаваемой группы</param>
        /// <param name="topR">Верхняя граница передаваемой группы</param>
        /// <returns>Возвращает true, если есть пересечение</returns>
        public bool IsCrossed(string leftR, string rightR, int bottomR, int topR)
        {
            return !(ExcelHelper.Compare(leftR, this.Right) > 0
                || ExcelHelper.Compare(rightR, this.Left) < 0
                || topR < this.Bottom
                || bottomR > this.Top);
        }

        #endregion
    }

    /// <summary>
    /// Класс для работы с элементом строки Excel
    /// </summary>
    public sealed class RowCellGroup : CellsGroup<Row>
    {
        #region Fields

        private IList<FormulaElement> formulasForClone;
        private IList<FormulaSourceGroup> formulaSources;

        #endregion

        #region Constructors

        public RowCellGroup(Row row, WorksheetElement worksheet)
            : base(row, worksheet)
        {
        }

        private RowCellGroup(Row row, WorksheetElement worksheet, IList<FormulaElement> formulasForClone)
            : this(row, worksheet)
        {
            this.formulasForClone = formulasForClone;
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных из элемента строки
        /// </summary>
        public override void ParseElement()
        {
            this.Top = this.Bottom = Convert.ToInt32(this.Element.RowIndex.Value);
            this.Left = ExcelHelper.FirstColumn;
            this.Right = ExcelHelper.LastColumn;

            int index = 0;
            foreach (var cell in this.Element.Elements<Cell>())
            {
                if (cell.CellFormula != null)
                {
                    this.Formulas ??= new List<FormulaElement>();

                    var formula = new FormulaElement(cell, this.Worksheet, this.formulasForClone?[index++]);
                    formula.ParseElement();
                    this.Formulas.Add(formula);
                    this.Worksheet.HasFormulas = true;
                }
            }

            this.formulasForClone = null;
        }

        /// <summary>
        /// Метод для клонирования хранимого элемента и вставки его перед передаваемым элементом, или перед клонируемым, если параметр отсутствует
        /// </summary>
        /// <returns></returns>
        public override ElementBase<Row> Clone()
        {
            RowCellGroup newRow = new RowCellGroup(this.CloneElement(), this.Worksheet, this.Formulas);
            newRow.ParseElement();
            newRow.MoveBy = this.MoveBy;
            return newRow;
        }

        /// <summary>
        /// Метод для вставки элемента.
        /// </summary>
        public override void Insert()
        {
            this.Worksheet.Rows.Add(this);
        }

        /// <summary>
        /// Метод для удаления объекта и его элемента
        /// </summary>
        public override void Remove()
        {
            base.Remove();
            this.Worksheet.Rows.Remove(this);
        }

        /// <summary>
        /// Метод для обновления позиции элемента в документе Excel
        /// </summary>
        public override void Update()
        {
            if (this.MoveBy != 0)
            {
                int oldIndex = this.Top;
                int newIndex = oldIndex + this.MoveBy;
                string newIndexS = newIndex.ToString(), oldIndexS = oldIndex.ToString();

                foreach (Cell cell in this.Element.Elements<Cell>())
                {
                    cell.CellReference.Value = cell.CellReference.Value.Replace(oldIndexS, newIndexS, StringComparison.Ordinal);
                }
                this.Element.RowIndex.Value = Convert.ToUInt32(newIndex);
                this.Top = newIndex;
                this.Bottom = newIndex;

                this.MoveBy = 0;
            }

            // Формулы апдейтим всегда
            if (this.Formulas != null)
            {
                foreach (var formula in this.Formulas)
                {
                    formula.Update();
                }
            }
        }

        public override void Move(int moveBy)
        {
            base.Move(moveBy);
            if (this.formulaSources != null)
            {
                foreach (var source in this.formulaSources)
                {
                    source.Move(moveBy);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Набор формул, которые соответствуют текущей строке.
        /// </summary>
        public IList<FormulaElement> Formulas { get; private set; }

        /// <summary>
        /// Набор ссылок из формул на текущую строку. Для области ссылки формируются только на первую строку.
        /// </summary>
        public IList<FormulaSourceGroup> FormulaSources => this.formulaSources ??= new List<FormulaSourceGroup>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Метод производит установку новой ячейки внутри строки. Если ячейка уже существуют, то он заменит ее на новую.
        /// </summary>
        /// <param name="newCell">Новый элемент ячейки</param>
        public void SetCell(Cell newCell)
        {
            string newCellColumn = ExcelHelper.GetColumnIndex(newCell.CellReference.Value);
            string newCellReference = newCellColumn + this.Top.ToString();
            newCell.CellReference = newCellReference;
            Cell oldCell;
            if ((oldCell = this.Element.Elements<Cell>().FirstOrDefault(x => x.CellReference.Value == newCellReference)) != null)
            {
                this.Element.ReplaceChild(newCell, oldCell);
            }
            else
            {
                Cell beforeCell = this.Element.Elements<Cell>().FirstOrDefault(x => ExcelHelper.Compare(newCellColumn, ExcelHelper.GetColumnIndex(x.CellReference.Value)) < 0);

                if (beforeCell != null)
                {
                    beforeCell.InsertBeforeSelf(newCell);
                }
                else
                {
                    this.Element.AppendChild(newCell);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Класс для работы с элементом гиперссылки в Excel
    /// </summary>
    public sealed class HyperlinkCellGroup : CellsGroup<Hyperlink>
    {
        #region Fields

        private OpenXmlElement parent;

        #endregion

        #region Constructors

        public HyperlinkCellGroup(Hyperlink hyperlink, WorksheetElement worksheet)
            : base(hyperlink, worksheet)
        {
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных элемента гиперссылка
        /// </summary>
        public override void ParseElement()
        {
            string text = this.Element.Reference.Value;
            this.Left = ExcelHelper.GetColumnIndex(text);
            this.Right = this.Left;
            this.Bottom = ExcelHelper.GetRowIndex(text);
            this.Top = this.Bottom;
        }

        /// <summary>
        /// Метод для клонирования хранимого элемента.
        /// </summary>
        /// <returns>Возвращает новый объект хранилища с клонируемым элементом Excel</returns>
        public override ElementBase<Hyperlink> Clone()
        {
            HyperlinkCellGroup newHyperlink = new HyperlinkCellGroup(this.CloneElement(), this.Worksheet)
            {
                parent = this.Element.Parent ?? this.parent
            };
            newHyperlink.ParseElement();
            newHyperlink.MoveBy = this.MoveBy;
            return newHyperlink;
        }

        /// <summary>
        /// Метод для вставки элемента.
        /// </summary>
        public override void Insert()
        {
            if (this.parent != null)
            {
                this.Worksheet.Hyperlinks.Add(this);
                this.parent.AppendChild(this.Element);
                this.parent = null;
            }
        }

        /// <summary>
        /// Метод для удаления объекта и его элемента
        /// </summary>
        public override void Remove()
        {
            base.Remove();
            this.Worksheet.Hyperlinks.Remove(this);
        }

        /// <summary>
        /// Метод для обновления позиции элемента в документе Excel
        /// </summary>
        public override void Update()
        {
            if (this.MoveBy != 0)
            {
                this.Top += this.MoveBy;
                this.Bottom += this.MoveBy;
                this.Element.Reference.Value = this.Reference;
                this.MoveBy = 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// Класс для работы с элементом смерженные ячейкм в Excel
    /// </summary>
    public sealed class MergeCellGroup : CellsGroup<MergeCell>
    {
        #region Fields

        private OpenXmlElement parent;

        #endregion

        #region Constructors

        public MergeCellGroup(MergeCell mergeCell, WorksheetElement worksheet)
            : base(mergeCell, worksheet)
        {
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных из элемента смерженные ячейкм
        /// </summary>
        public override void ParseElement()
        {
            string text = this.Element.Reference.Value;
            string[] refs = text.Split(':');
            if (refs.Length != 2)
            {
                throw new ArgumentException("Wrong parameter '" + text + "'", "reference");
            }
            this.Left = ExcelHelper.GetColumnIndex(refs[0]);
            this.Right = ExcelHelper.GetColumnIndex(refs[1]);
            this.Bottom = ExcelHelper.GetRowIndex(refs[0]);
            this.Top = ExcelHelper.GetRowIndex(refs[1]);
        }

        /// <summary>
        /// Метод для клонирования хранимого элемента и вставки его перед передаваемым элементом, или перед клонируемым, если параметр отсутствует
        /// </summary>
        /// <returns>Возвращает новый объект хранилища с клонируемым элементом Excel</returns>
        public override ElementBase<MergeCell> Clone()
        {
            MergeCellGroup newMergeCellGroup = new MergeCellGroup(this.CloneElement(), this.Worksheet)
            {
                parent = this.Element.Parent ?? this.parent
            };
            newMergeCellGroup.ParseElement();
            newMergeCellGroup.MoveBy = this.MoveBy;
            return newMergeCellGroup;
        }

        /// <summary>
        /// Метод для вставки элемента.
        /// </summary>
        public override void Insert()
        {
            if (this.parent != null)
            {
                this.Worksheet.MergeCells.Add(this);
                this.parent.AppendChild(this.Element);
                this.parent = null;
            }
        }

        /// <summary>
        /// Метод для удаления объекта и его элемента
        /// </summary>
        public override void Remove()
        {
            base.Remove();
            this.Worksheet.MergeCells.Remove(this);
        }

        /// <summary>
        /// Метод для обновления позиции элемента в документе Excel
        /// </summary>
        public override void Update()
        {
            if (this.MoveBy != 0)
            {
                this.Top += this.MoveBy;
                this.Bottom += this.MoveBy;
                this.Element.Reference.Value = this.Reference;
                this.MoveBy = 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// Якорь, используемый для привязки надписи или картинки к Worksheet
    /// </summary>
    public sealed class AnchorCellGroup : CellsGroup<Xdr.TwoCellAnchor>
    {
        #region Fields

        private OpenXmlElement parent;

        #endregion

        #region Constructors

        public AnchorCellGroup(Xdr.TwoCellAnchor anchor, WorksheetElement worksheet)
            : base(anchor, worksheet)
        {
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных элемента "якорь"
        /// </summary>
        public override void ParseElement()
        {
            // Top и Bottom должны отсчитываться от 1, а в маркере индекс отсчитывается от 0
            Xdr.FromMarker fromMarker = this.Element.FromMarker;
            this.Top = int.Parse(fromMarker.RowId.Text) + 1;
            this.Left = ExcelHelper.GetColumnName(int.Parse(fromMarker.ColumnId.Text));

            Xdr.ToMarker toMarker = this.Element.ToMarker;
            this.Bottom = int.Parse(toMarker.RowId.Text) + 1;
            this.Right = ExcelHelper.GetColumnName(int.Parse(toMarker.ColumnId.Text));
        }

        /// <summary>
        /// Метод для клонирования хранимого элемента и вставки его перед клонируемым элементом. Игнорирует передаваемый элемент
        /// </summary>
        /// <returns>Возвращает новый объект хранилища с клонируемым элементом Excel</returns>
        public override ElementBase<Xdr.TwoCellAnchor> Clone()
        {
            var newAnchor = new AnchorCellGroup(this.CloneElement(), this.Worksheet)
            {
                parent = this.Element.Parent ?? this.parent
            };
            newAnchor.ParseElement();
            newAnchor.MoveBy = this.MoveBy;
            return newAnchor;
        }

        /// <summary>
        /// Метод для вставки элемента.
        /// </summary>
        public override void Insert()
        {
            if (this.parent != null)
            {
                this.Worksheet.Anchors.Add(this);
                this.parent.AppendChild(this.Element);
                this.parent = null;
            }
        }

        /// <summary>
        /// Метод для удаления объекта и его элемента
        /// </summary>
        public override void Remove()
        {
            base.Remove();
            this.Worksheet.Anchors.Remove(this);
        }

        /// <summary>
        /// Метод для обновления позиции элемента в документе Excel
        /// </summary>
        public override void Update()
        {
            if (this.MoveBy != 0)
            {
                this.Top += this.MoveBy;
                this.Bottom += this.MoveBy;

                // Top и Bottom должны отсчитываться от 1, а в маркере индекс отсчитывается от 0
                this.Element.FromMarker.RowId.Text = (this.Top - 1).ToString();
                this.Element.ToMarker.RowId.Text = (this.Bottom - 1).ToString();

                this.MoveBy = 0;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает элемент, который является базовым для замены плейсхолдеров. Обычно это параграф.
        /// </summary>
        /// <returns>Элемент, который является базовым для замены плейсхолдеров.</returns>
        public OpenXmlElement GetPlaceholderBaseElement()
        {
            // если в замену изображения передать сам Element, то он обходит дочерние элементы,
            // берёт первый, и в нём не может найти "Shape" (т.к. это какой-нибудь FromMarker);
            // поэтому при замене по Element будет работать замена текста, но не картинок;
            // для картинок нам желательно передать то же самое, как и для элемента вне табличных групп, а именно, параграф

            A.Paragraph paragraph = this.Element
                .GetFirstChild<Xdr.Shape>()
                ?.GetFirstChild<Xdr.TextBody>()
                ?.GetFirstChild<A.Paragraph>();

            return (OpenXmlElement)paragraph ?? this.Element;
        }

        #endregion
    }

    /// <summary>
    /// Класс для работы с именнованными группами в Excel, определяющими таблицы внутри шаблона Excel.
    /// </summary>
    public sealed class TableGroup : CellsGroup<DefinedName>
    {
        #region Fields

        /// <summary>
        /// Справочник с плейсхолдерами строк, по номеру строки
        /// </summary>
        private readonly Dictionary<RowCellGroup, List<IPlaceholder>> rowsPlaceholders;

        /// <summary>
        /// Справочник с плейсхолдерами гиперссылок
        /// </summary>
        private readonly Dictionary<HyperlinkCellGroup, List<IPlaceholder>> hyperlinksPlaceholders;

        /// <summary>
        /// Справочник с плейсхолдерами в якорях (надписях)
        /// </summary>
        private readonly Dictionary<AnchorCellGroup, List<IPlaceholder>> anchorsPlaceholders;

        #endregion

        #region Constructors

        public TableGroup(DefinedName defineName, TableGroupType type)
            : base(defineName)
        {
            this.IsValid = true;
            this.ErrorText = null;
            this.Name = defineName.Name;
            this.Type = type;

            this.rowsPlaceholders = new Dictionary<RowCellGroup, List<IPlaceholder>>();
            this.hyperlinksPlaceholders = new Dictionary<HyperlinkCellGroup, List<IPlaceholder>>();
            this.anchorsPlaceholders = new Dictionary<AnchorCellGroup, List<IPlaceholder>>();
            this.ExpandableSources = new List<FormulaSourceGroup>();

            this.InnerGroups = new List<TableGroup>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список объектов смерженных ячеек, входящих в данную группу
        /// </summary>
        public List<MergeCellGroup> MergeCells { get; private set; }

        /// <summary>
        /// Список объектов гиперссылок, входящих в данную таблицу
        /// </summary>
        public List<HyperlinkCellGroup> Hyperlinks { get; private set; }

        /// <summary>
        /// Список объектов строк, входящих в данную таблицу
        /// </summary>
        public List<RowCellGroup> Rows { get; private set; }

        /// <summary>
        /// Список объектов якорей (надписей), входящих в данную таблицу
        /// </summary>
        public List<AnchorCellGroup> Anchors { get; private set; }

        /// <summary>
        /// Список источников данных формул, которые должны расширяться вместе с таблицей.
        /// </summary>
        public List<FormulaSourceGroup> ExpandableSources { get; }

        /// <summary>
        /// Имя элемента Worksheet, к которому относится данная именованная область
        /// </summary>
        public string WorksheetName { get; private set; }

        /// <summary>
        /// Название именованной области
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public string Name { get; private set; }

        /// <summary>
        /// Тип таблицы
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public TableGroupType Type { get; private set; }

        /// <summary>
        /// Определяет, является ли данная таблица валидной
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Текст ошибки при ошибке валидации
        /// </summary>
        public string ErrorText { get; private set; }

        /// <summary>
        /// Подчиненная группа таблицы
        /// </summary>
        public IList<TableGroup> InnerGroups { get; }

        /// <summary>
        /// Обозначает, что группа имеет родительскую группу
        /// </summary>
        public bool HasParent { get; private set; }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод для получения основных данных из хранимого элемента
        /// </summary>
        public override void ParseElement()
        {
            this.ParseDefineName(this.Element.InnerText);
        }

        /// <summary>
        /// Данный метод недоступен для текущего типа
        /// </summary>
        public override ElementBase<DefinedName> Clone()
        {
            ExcelHelper.NotSupported(nameof(Clone), nameof(TableGroup));
            return null;
        }

        /// <summary>
        /// Данный метод недоступен для текущего типа
        /// </summary>
        public override void Insert()
        {
            ExcelHelper.NotSupported(nameof(Insert), nameof(TableGroup));
        }

        /// <summary>
        /// Данный метод недоступен для текущего типа
        /// </summary>
        public override void Update()
        {
            ExcelHelper.NotSupported(nameof(Update), nameof(TableGroup));
        }

        public override string GetDisplayString()
        {
            return string.Format(
                LocalizationManager.GetString("KrMessages_ExcelTemplate_DefineNameDisplayFormat"),
                this.Name,
                this.Worksheet.Name,
                this.Reference);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Парсит текст элемента DefineName на состовляющие (имя, координаты элемента). Если имя имеет 2 или более диапозона, то считаем, что данная группа не валидна.
        /// </summary>
        /// <param name="text">Текст элемента DefineName. Группы: 1 - worksheetName, 2 - left, 3 - bottom, 4 - right, 5 - top.</param>
        private void ParseDefineName(string text)
        {
            MatchCollection matches = Regex.Matches(text, @",?'?((?:[^']|'{2})+)'?!\$?([A-Z]+)?\$?(\d+)(?:\:\$?([A-Z]+)?\$?(\d+))?");
            if (matches.Count == 0)
            {
                this.IsValid = false;
                this.ErrorText = string.Format(LocalizationManager.GetString("KrMessages_ExcelTemplate_DefineNameParsingError"), this.Name);
                return;
            }
            if (matches.Count > 1)
            {
                this.IsValid = false;
                this.ErrorText = string.Format(LocalizationManager.GetString("KrMessages_ExcelTemplate_DefineNameRangesError"), this.Name);
                return;
            }

            Match match = matches[0];

            // Заменяем '' на ' в имени страницы
            this.WorksheetName = match.Groups[1].Value.Replace("''", "'", StringComparison.Ordinal);
            this.Left = match.Groups[2].Value;
            this.Bottom = int.Parse(match.Groups[3].Value);

            //Right и Top могут отсутствовать. В таком случае в них записывается значение Left и Bottom соответственно.
            this.Right = match.Groups[4].Success ? match.Groups[4].Value : this.Left;
            this.Top = match.Groups[5].Success ? int.Parse(match.Groups[5].Value) : this.Bottom;

            // Могут быть ситуации, когда не заданы колонки в диапозоне. В таком случае в Left ставим FirstColumn, а в Right LastColumn
            if (string.IsNullOrEmpty(this.Left))
            {
                this.Left = ExcelHelper.FirstColumn;
            }
            if (string.IsNullOrEmpty(this.Right))
            {
                this.Right = ExcelHelper.LastColumn;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Производит инициализацию группы. Инициализация производится сверху вниз от болших групп к малым.
        /// </summary>
        /// <param name="worksheet">Объект Worksheet, к которому относится данная таблица</param>
        public void Initialize(WorksheetElement worksheet)
        {
            Check.ArgumentNotNull(worksheet, nameof(worksheet));

            this.Worksheet = worksheet;

            this.MergeCells = new List<MergeCellGroup>();
            this.Hyperlinks = new List<HyperlinkCellGroup>();
            this.Rows = new List<RowCellGroup>();
            this.Anchors = new List<AnchorCellGroup>();

            this.AddTableToTables(worksheet.Tables);

            foreach (RowCellGroup row in this.Worksheet.Rows)
            {
                if (this.IsCrossed(row))
                {
                    this.Rows.Add(row);
                }
            }
        }

        private void AddTableToTables(IList<TableGroup> tables, TableGroup parentTable = null)
        {
            foreach (TableGroup table in tables)
            {
                // Если таблица содержит текущую, то добавляем
                if (table.IsInclude(this))
                {
                    this.AddTableToTables(table.InnerGroups, table);
                    return;
                }
            }

            tables.Add(this);
            if (parentTable != null)
            {
                parentTable.SetChildGroup(this);
            }
            else
            {
                foreach (HyperlinkCellGroup hyperlink in this.Worksheet.Hyperlinks)
                {
                    if (this.IsInclude(hyperlink))
                    {
                        this.Hyperlinks.Add(hyperlink);
                    }
                }

                foreach (MergeCellGroup mergeCell in this.Worksheet.MergeCells)
                {
                    if (this.IsInclude(mergeCell))
                    {
                        this.MergeCells.Add(mergeCell);
                    }
                    else if (this.IsCrossed(mergeCell))
                    {
                        this.IsValid = false;
                        this.ErrorText = string.Format(LocalizationManager.GetString("KrMessages_ExcelTemplate_DefineNameCrossesMergeCell"),
                            this.GetDisplayString(),
                            mergeCell.GetDisplayString());
                        return;
                    }
                }

                foreach (AnchorCellGroup anchor in this.Worksheet.Anchors)
                {
                    if (this.IsInclude(anchor))
                    {
                        this.Anchors.Add(anchor);
                    }
                }
            }
        }

        /// <summary>
        /// Производит установку дочерней группы.
        /// Метод переносит гиперссылки и смерженные ячейки из родительской группы, если те входят в дочернюю группу.
        /// </summary>
        /// <param name="table">Группа, с которой создается связь.</param>
        private void SetChildGroup(TableGroup table)
        {
            table.HasParent = true;

            for (int i = this.Hyperlinks.Count - 1; i >= 0; i--)
            {
                var hyperLink = this.Hyperlinks[i];
                if (table.IsInclude(hyperLink))
                {
                    table.Hyperlinks.Add(hyperLink);
                    this.Hyperlinks.RemoveAt(i);
                }
            }

            // Для Jump-таблиц не переносим MergeCell из родителя
            if (table.Type != TableGroupType.Jump)
            {
                for (int i = this.MergeCells.Count - 1; i >= 0; i--)
                {
                    var mergeCell = this.MergeCells[i];
                    if (table.IsInclude(mergeCell))
                    {
                        table.MergeCells.Add(mergeCell);
                        this.MergeCells.RemoveAt(i);
                    }
                }
            }

            for (int i = this.Anchors.Count - 1; i >= 0; i--)
            {
                var anchor = this.Anchors[i];
                if (table.IsInclude(anchor))
                {
                    table.Anchors.Add(anchor);
                    this.Anchors.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Производит очистку таблицы от старых объектов
        /// </summary>
        public void Clear()
        {
            if (this.Type != TableGroupType.Jump)
            {
                this.Rows.ForEach(x => x.Remove());
                this.MergeCells.ForEach(x => x.Remove());
            }

            this.Hyperlinks.ForEach(x => x.Remove());
            this.Anchors.ForEach(x => x.Remove());

            this.InnerGroups.ForEach(x => x.Clear());
        }

        /// <summary>
        /// Удаление элемента из документа. Если есть дочерняя группа, она также удаляется из документа
        /// </summary>
        public override void Remove()
        {
            base.Remove();
            this.InnerGroups.ForEach(x => x.Remove());
        }

        /// <summary>
        /// Получает список плейсхолдеров, относящихся к переданной гиперссылке
        /// </summary>
        /// <param name="hyperlink">Объект гиперссылки, по которой ищутся плейсхолдеры</param>
        /// <returns>Возвращает список плейсхолдеров, относящихся к переданной гиперссылке</returns>
        public IList<IPlaceholder> GetHyperlinkPlaceholders(HyperlinkCellGroup hyperlink)
        {
            return this.hyperlinksPlaceholders.TryGetValue(hyperlink, out List<IPlaceholder> result)
                ? result
                : ExcelHelper.EmptyPlaceholders;
        }

        /// <summary>
        /// Получает список плейсхолдеров, относящихся к переданной строке
        /// </summary>
        /// <param name="row">Объект строки, по которой ищутся плейсхолдеры</param>
        /// <returns>Возвращает список плейсхолдеров, относящихся к переданной строке</returns>
        public IList<IPlaceholder> GetRowPlaceholders(RowCellGroup row)
        {
            return this.rowsPlaceholders.TryGetValue(row, out List<IPlaceholder> result)
                ? result
                : ExcelHelper.EmptyPlaceholders;
        }

        /// <summary>
        /// Получает список плейсхолдеров, относящихся к переданному якорю (надписи)
        /// </summary>
        /// <param name="anchor">Объект якоря (надписи), по которой ищутся плейсхолдеры</param>
        /// <returns>Возвращает список плейсхолдеров, относящихся к переданному якорю (надписи)</returns>
        public IList<IPlaceholder> GetAnchorPlaceholders(AnchorCellGroup anchor)
        {
            return this.anchorsPlaceholders.TryGetValue(anchor, out List<IPlaceholder> result)
                ? result
                : ExcelHelper.EmptyPlaceholders;
        }

        /// <summary>
        /// Метод для получения всех плейсхолдеров текущей таблицы
        /// </summary>
        /// <returns>Возвращает список всех плейсхолдеров таблицы</returns>
        public List<IPlaceholder> GetAllPlaceholders()
        {
            List<IPlaceholder> placeholders = new List<IPlaceholder>();
            foreach (List<IPlaceholder> rowPlaceholders in this.rowsPlaceholders.Values)
            {
                placeholders.AddRange(rowPlaceholders);
            }
            foreach (List<IPlaceholder> hyperlinkPlaceholders in this.hyperlinksPlaceholders.Values)
            {
                placeholders.AddRange(hyperlinkPlaceholders);
            }
            foreach (List<IPlaceholder> anchorPlaceholders in this.anchorsPlaceholders.Values)
            {
                placeholders.AddRange(anchorPlaceholders);
            }

            return placeholders;
        }

        /// <summary>
        /// Метод для добавления плейсхолдера, связанного с объектом гиперссылки
        /// </summary>
        /// <param name="hyperlink">Объект гиперссылки, к которой принадлежит плейсхолдер</param>
        /// <param name="placeholder">Плейсхолдер</param>
        public void AddHyperlinkPlaceholder(HyperlinkCellGroup hyperlink, IPlaceholder placeholder)
        {
            if (this.hyperlinksPlaceholders.TryGetValue(hyperlink, out List<IPlaceholder> placeholders))
            {
                placeholders.Add(placeholder);
            }
            else
            {
                this.hyperlinksPlaceholders.Add(hyperlink, new List<IPlaceholder> { placeholder });
            }
        }

        /// <summary>
        /// Метод для добавления плейсхолдера, связанного с объектом строки.
        /// </summary>
        /// <param name="row">Объект строки, к которой принадлежит плейсхолдер</param>
        /// <param name="placeholder">Плейсхолдер</param>
        public void AddRowPlaceholder(RowCellGroup row, IPlaceholder placeholder)
        {
            if (this.rowsPlaceholders.TryGetValue(row, out List<IPlaceholder> placeholders))
            {
                placeholders.Add(placeholder);
            }
            else
            {
                this.rowsPlaceholders.Add(row, new List<IPlaceholder> { placeholder });
            }
        }

        /// <summary>
        /// Метод для добавления плейсхолдера, связанного с объектом якоря (надписи)
        /// </summary>
        /// <param name="anchor">Объект якоря (надписи), к которому принадлежит плейсхолдер</param>
        /// <param name="placeholder">Плейсхолдер</param>
        public void AddAnchorPlaceholder(AnchorCellGroup anchor, IPlaceholder placeholder)
        {
            if (this.anchorsPlaceholders.TryGetValue(anchor, out List<IPlaceholder> placeholders))
            {
                placeholders.Add(placeholder);
            }
            else
            {
                this.anchorsPlaceholders.Add(anchor, new List<IPlaceholder> { placeholder });
            }
        }

        #endregion
    }

    /// <summary>
    /// Экземпляр таблицы со строками для замены.
    /// </summary>
    public sealed class TableGroupInstance
    {
        #region Fields

        /// <summary>
        /// Группа таблицы, по которой была сгенерирована данная таблица.
        /// </summary>
        private readonly TableGroup tableGroup;

        /// <summary>
        /// Группы строк.
        /// </summary>
        private readonly IList<RowCellGroup> rows;

        /// <summary>
        /// Общее число строк таблицы.
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Смещение всей таблицы относительно родителя.
        /// </summary>
        private int offsetByParent;

        /// <summary>
        /// Общий сдвиг таблицы.
        /// </summary>
        private int moveBy;

        #endregion

        #region Constructors

        public TableGroupInstance(
            TableGroup tableGroup,
            TableGroupInstance parentTableGroupInstance = null,
            IList<RowCellGroup> rows = null)
        {
            if (parentTableGroupInstance is null)
            {
                this.ExpandableSources = new List<FormulaSourceGroup>(tableGroup.ExpandableSources);
            }
            else
            {
                this.offsetByParent = parentTableGroupInstance.moveBy + parentTableGroupInstance.offsetByParent;

                // Добавляем в качестве источников данных формул для расширения с таблицей те, что расширяет родительская таблица
                // и те, что появились после создания родительской таблицы
                this.ExpandableSources = new List<FormulaSourceGroup>(parentTableGroupInstance.ExpandableSources);
                for (int i = 0; i < parentTableGroupInstance.Rows.Length; i++)
                {
                    var parentRow = parentTableGroupInstance.Rows[i];
                    if (parentRow != null)
                    {
                        this.ExpandableSources.AddRange(parentRow.FormulaSources.Where(x => x.IsExpandable));
                    }
                }

                // Если нет явно указанных строк, берем из родительской таблицы.
                if (rows is null)
                {
                    rows = new RowCellGroup[tableGroup.Height];
                    int from = tableGroup.Bottom - parentTableGroupInstance.Rows[0].Bottom;
                    int to = from + tableGroup.Height;
                    for (int i = from; i < to; i++)
                    {
                        rows[i - from] = parentTableGroupInstance.Rows[i];
                        parentTableGroupInstance.Rows[i] = null;
                    }
                }
            }

            this.rows = rows ?? tableGroup.Rows;
            this.tableGroup = tableGroup;

            this.MergeCells = new MergeCellGroup[tableGroup.MergeCells.Count];
            this.Hyperlinks = new HyperlinkCellGroup[tableGroup.Hyperlinks.Count];
            this.Rows = new RowCellGroup[this.rows.Count];
            this.Anchors = new AnchorCellGroup[tableGroup.Anchors.Count];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список объектов смерженных ячеек, входящих в данную группу
        /// </summary>
        public MergeCellGroup[] MergeCells { get; }

        /// <summary>
        /// Список объектов гиперссылок, входящих в данную таблицу
        /// </summary>
        public HyperlinkCellGroup[] Hyperlinks { get; }

        /// <summary>
        /// Список объектов строк, входящих в данную таблицу
        /// </summary>
        public RowCellGroup[] Rows { get; }

        /// <summary>
        /// Список объектов якорей (надписей), входящих в данную таблицу
        /// </summary>
        public AnchorCellGroup[] Anchors { get; }

        /// <summary>
        /// Список источников данных для формул, которые должны расширяться вместе с таблицей.
        /// </summary>
        public List<FormulaSourceGroup> ExpandableSources { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Метод для создания клонов для данного объекта.
        /// </summary>
        public void CreateClone()
        {
            this.moveBy = 0;
            this.FillWithClonesArray<MergeCellGroup, MergeCell>(this.MergeCells, this.tableGroup.MergeCells);
            this.FillWithClonesArray<HyperlinkCellGroup, Hyperlink>(this.Hyperlinks, this.tableGroup.Hyperlinks);
            this.FillWithClonesArray<AnchorCellGroup, Xdr.TwoCellAnchor>(this.Anchors, this.tableGroup.Anchors);
            this.FillWithClonesArray<RowCellGroup, Row>(this.Rows, this.rows);

            // Перемещаем свежесклонированные элементы на смещение таблицы
            if (this.offsetByParent != 0)
            {
                this.MergeCells.ForEach(x => x.Move(this.offsetByParent));
                this.Hyperlinks.ForEach(x => x.Move(this.offsetByParent));
                this.Anchors.ForEach(x => x.Move(this.offsetByParent));
            }

            for (int rowIndex = 0; rowIndex < this.Rows.Length; rowIndex++)
            {
                var row = this.Rows[rowIndex];
                if (row != null
                    && row.Formulas != null)
                {
                    // Переносим источники данных для формул, если они находятся в этой же строке
                    for (int formulaIndex = 0; formulaIndex < row.Formulas.Count; formulaIndex++)
                    {
                        var formula = row.Formulas[formulaIndex];
                        for (int sourceIndex = 0; sourceIndex < formula.FormulaSources.Count; sourceIndex++)
                        {
                            var source = formula.FormulaSources[sourceIndex].Item3;
                            RowCellGroup sourceRow;
                            if (source.InFormulaTableGroup)
                            {
                                sourceRow = this.Rows[source.Bottom - row.Bottom + rowIndex];
                            }
                            else
                            {
                                sourceRow = formula.OriginalFormula.FormulaSources[sourceIndex].Item3.Row;
                            }

                            source.Row = sourceRow;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для добавления созданных клонов в структуру документа.
        /// </summary>
        public void InsertClone()
        {
            this.InsertClonesArray<MergeCellGroup, MergeCell>(this.MergeCells);
            this.InsertClonesArray<HyperlinkCellGroup, Hyperlink>(this.Hyperlinks);
            this.InsertClonesArray<AnchorCellGroup, Xdr.TwoCellAnchor>(this.Anchors);
            this.InsertClonesArray<RowCellGroup, Row>(this.Rows);
            this.rowCount++;
        }

        /// <summary>
        /// Метод для перемещения всех объектов, начиная с указанной позиции, на заданное значение.
        /// </summary>
        /// <param name="moveBy">Значение, на которое производят смещение.</param>
        /// <param name="moveFrom">Значение, элементы ниже которого должны быть смещены.</param>
        public void Move(int moveBy, int moveFrom)
        {
            if (moveBy == 0)
            {
                return;
            }

            this.moveBy += moveBy;
            // Смещаем все строки, которые ниже вложенных групп на смещение, которые вызвали эти группы.
            foreach (var rowCellGroup in this.Rows)
            {
                if (rowCellGroup != null && rowCellGroup.Bottom > moveFrom)
                {
                    rowCellGroup.Move(moveBy);
                }
            }

            foreach (var anchor in this.Anchors)
            {
                if (anchor.Bottom > moveFrom)
                {
                    anchor.Move(moveBy);
                }
            }

            foreach (var hyperlink in this.Hyperlinks)
            {
                if (hyperlink.Bottom > moveFrom)
                {
                    hyperlink.Move(moveBy);
                }
            }

            foreach (var mergeCell in this.MergeCells)
            {
                if (mergeCell.Bottom > moveFrom)
                {
                    mergeCell.Move(moveBy);
                }
            }
        }

        /// <summary>
        /// Метод для перемещения всех объектов.
        /// </summary>
        /// <param name="moveBy">Значение, на которое производят смещение.</param>
        public void Move(int moveBy)
        {
            if (moveBy == 0)
            {
                return;
            }

            this.moveBy += moveBy;
            this.Rows.ForEach(x =>
            {
                if (x != null)
                {
                    x.Move(moveBy);
                }
            });
            this.MergeCells.ForEach(x => x.Move(moveBy));
            this.Hyperlinks.ForEach(x => x.Move(moveBy));
            this.Anchors.ForEach(x => x.Move(moveBy));
        }

        public void ExpandFormulaSources(int? expandBy = null)
        {
            if (expandBy is null)
            {
                expandBy = (this.rowCount - 1) * this.Rows.Length;
            }
            for (int i = 0; i < this.ExpandableSources.Count; i++)
            {
                var source = this.ExpandableSources[i];
                source.ExpandBy(expandBy.Value);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Вспомогательный метод для создания копий элементов.
        /// </summary>
        private void FillWithClonesArray<T, S>(T[] array, IList<T> source) where T : ElementBase<S> where S : OpenXmlElement
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (T)source[i].Clone();
            }
        }

        /// <summary>
        /// Вспомогательный метод для вставки копий элементов.
        /// </summary>
        private void InsertClonesArray<T, S>(T[] array) where T : ElementBase<S> where S : OpenXmlElement
        {
            for (int i = 0; i < array.Length; i++)
            {
                var elem = array[i];
                if (elem != null)
                {
                    elem.Insert();
                }
            }
        }


        #endregion
    }

    public sealed class FormulaElement : WorksheetBase<CellFormula>
    {
        #region Fields

        private string originalFormulaText;

        #endregion

        #region Constructors

        public FormulaElement(
            Cell element,
            WorksheetElement worksheet,
            FormulaElement originalFormulaElement = null)
            : base(element.CellFormula, worksheet)
        {
            this.OriginalFormula = originalFormulaElement;
            this.Reference = element.CellReference;
        }

        #endregion

        #region Properties

        private string OriginalFormulaText => this.OriginalFormula?.OriginalFormulaText ?? this.originalFormulaText;

        public FormulaElement OriginalFormula { get; }

        public IList<(int, int, FormulaSourceGroup)> FormulaSources { get; private set; }

        public string Reference { get; }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод не предусмотрен для формул
        /// </summary>
        public override ElementBase<CellFormula> Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод не предусмотрен для формул
        /// </summary>
        public override void Insert()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удаляем текст ячейки вместе с формулой, где она находится.
        /// </summary>
        public override void Remove()
        {
            if (this.Element.Parent is Cell cell)
            {
                cell.CellValue?.Remove();
            }
            base.Remove();
        }

        public override void Update()
        {
            if (this.FormulaSources is null
                || this.FormulaSources.Count == 0)
            {
                return;
            }

            // Если текст формулы не изменялся, т.е. в нем не было плейсхолдеров
            var sb = StringBuilderHelper.Acquire().Append(this.Element.Text);
            if (this.Element.Text == this.OriginalFormulaText)
            {
                // То просто заменяем формулы в обратном порядке
                for (int i = this.FormulaSources.Count - 1; i >= 0; i--)
                {
                    var (index, length, formulaSource) = this.FormulaSources[i];
                    formulaSource.Update();
                    if (formulaSource.Check())
                    {
                        sb.Remove(index, length).Insert(index, formulaSource.GetDisplayString());
                    }
                    else
                    {
                        this.Remove();
                        return;
                    }
                }
            }
            else
            {
                // Иначе парсим текст для определния новой позиции, а затем заменяем
                var oldFormulaSources = this.FormulaSources;
                this.FormulaSources = null;
                this.ParseElementFormula();

                for (int i = this.FormulaSources.Count - 1; i >= 0; i--)
                {
                    var (index, length, _) = this.FormulaSources[i];
                    var (_, _, formulaSource) = oldFormulaSources[i];
                    formulaSource.Update();
                    if (formulaSource.Check())
                    {
                        sb.Remove(index, length).Insert(index, formulaSource.GetDisplayString());
                    }
                    else
                    {
                        this.Remove();
                        return;
                    }
                }
            }

            this.Element.Text = sb.ToStringAndRelease();
        }

        public override void ParseElement()
        {
            if (this.OriginalFormula is null)
            {
                this.ParseElementFormula();
            }
            else
            {
                this.FormulaSources = new List<(int, int, FormulaSourceGroup)>(this.OriginalFormula.FormulaSources.Count);
                foreach (var (index, length, source) in this.OriginalFormula.FormulaSources)
                {
                    var newSource = new FormulaSourceGroup(
                            this,
                            source.Worksheet,
                            source.Left,
                            source.Right,
                            source.Bottom,
                            source.Top)
                    {
                        TableGroup = source.TableGroup,
                        InFormulaTableGroup = source.InFormulaTableGroup,
                    };
                    newSource.Move(source.MoveBy);
                    newSource.ParseElement();
                    this.FormulaSources.Add((
                        index,
                        length,
                        newSource));
                }
            }
        }

        private void ParseElementFormula()
        {
            var formulaText = this.Element.Text;
            // Формула может быть пустой, тогда она берется из расшаренной формулы с учетом смещения
            if (string.IsNullOrEmpty(formulaText))
            {
                if (this.Worksheet.SharedFormulas.TryGetValue(this.Element.SharedIndex.Value, out var sourceFormula))
                {
                    string columnIndex = ExcelHelper.GetColumnIndex(this.Reference);
                    int rowIndex = ExcelHelper.GetRowIndex(this.Reference);

                    string sourceFormulaColumnIndex = ExcelHelper.GetColumnIndex(sourceFormula.Reference);
                    int sourceFormulaRowIndex = ExcelHelper.GetRowIndex(sourceFormula.Reference);
                    int horizontalOffset = ExcelHelper.ColumnNameToInt(columnIndex) - ExcelHelper.ColumnNameToInt(sourceFormulaColumnIndex),
                        verticalOffset = rowIndex - sourceFormulaRowIndex;
                    this.FormulaSources = new List<(int, int, FormulaSourceGroup)>(sourceFormula.FormulaSources.Count);
                    foreach (var (index, length, source) in sourceFormula.FormulaSources)
                    {
                        var newSource = new FormulaSourceGroup(
                                this,
                                source.Worksheet,
                                ExcelHelper.GetColumnName(ExcelHelper.ColumnNameToInt(source.Left) + horizontalOffset),
                                ExcelHelper.GetColumnName(ExcelHelper.ColumnNameToInt(source.Right) + horizontalOffset),
                                source.Bottom + verticalOffset,
                                source.Top + verticalOffset)
                        {
                            TableGroup = source.TableGroup,
                            InFormulaTableGroup = source.InFormulaTableGroup,
                        };

                        newSource.ParseElement();
                        this.FormulaSources.Add((
                            index,
                            length,
                            newSource));
                    }
                    this.Element.FormulaType = CellFormulaValues.Normal;
                    this.Element.Text = sourceFormula.Element.Text;
                }
                else
                {
                    this.FormulaSources = Array.Empty<(int, int, FormulaSourceGroup)>();
                }
            }
            else
            {
                var formulaTextSplited = formulaText.Split('"');
                var indexOffset = 0; // Индекс смещения formulaTextPart вунтри formulaText
                for (int i = 0; i < formulaTextSplited.Length; i += 2)
                {
                    if (i > 0)
                    {
                        indexOffset += 2 + formulaTextSplited[i - 1].Length; // Смещаем индекс на 2 кавычки + на пропущенную часть
                    }

                    var formulaTextPart = formulaTextSplited[i];
                    if (string.IsNullOrWhiteSpace(formulaTextPart))
                    {
                        continue;
                    }
                    var sourceMatches = Regex.Matches(
                        formulaTextPart,
                        @"(?:(?<worksheet>\w+)!|'(?<worksheet>(?:[^']|'{2})+)'!)?\$?(?<left>[A-Z]+)?\$?(?<bottom>\d+)(?:\:\$?(?<right>[A-Z]+)?\$?(?<top>\d+))?");

                    if (sourceMatches.Count > 0)
                    {
                        this.FormulaSources ??= new List<(int, int, FormulaSourceGroup)>(sourceMatches.Count);
                        foreach (Match match in sourceMatches)
                        {
                            // Заменяем '' на '
                            var worksheetName = match.Groups["worksheet"].Success ? match.Groups["worksheet"].Value.Replace("''", "'", StringComparison.Ordinal) : null;
                            var left = match.Groups["left"].Value;
                            var bottom = int.Parse(match.Groups["bottom"].Value);

                            //Right и Top могут отсутствовать. В таком случае в них записывается значение Left и Bottom соответственно.
                            var right = match.Groups["right"].Success ? match.Groups["right"].Value : left;

                            // Если у нас не задан Left/Right, то ожидаем формулу вида "номер_строки:номер_строки".
                            // В Excel нет формулы "номер_строки", поэтому, если не задан и left и top, то это не формула, пропускаем ее
                            var hasTop = match.Groups["top"].Success;
                            if (!hasTop
                                && string.IsNullOrEmpty(left))
                            {
                                continue;
                            }
                            var top = hasTop ? int.Parse(match.Groups["top"].Value) : bottom;

                            // Могут быть ситуации, когда не заданы колонки в диапозоне. В таком случае в Left ставим FirstColumn, а в Right LastColumn
                            if (string.IsNullOrEmpty(left))
                            {
                                left = ExcelHelper.FirstColumn;
                            }
                            if (string.IsNullOrEmpty(right))
                            {
                                right = ExcelHelper.LastColumn;
                            }

                            var formulaSource = new FormulaSourceGroup(
                                this,
                                string.IsNullOrEmpty(worksheetName) ? this.Worksheet : this.Worksheet.WorksheetHash[worksheetName],
                                left,
                                right,
                                bottom,
                                top);
                            formulaSource.ParseElement();
                            this.FormulaSources.Add((match.Index + indexOffset, match.Length, formulaSource));
                        }
                    }

                    // Смещаем индекс на текущую часть
                    indexOffset += formulaTextPart.Length;
                }

                this.FormulaSources ??= Array.Empty<(int, int, FormulaSourceGroup)>();

                if (this.Element.SharedIndex != null && this.Element.SharedIndex.HasValue)
                {
                    this.Worksheet.SharedFormulas[this.Element.SharedIndex.Value] = this;
                    this.Element.SharedIndex = null;
                    this.Element.FormulaType = CellFormulaValues.Normal;
                    this.Element.Reference = null;
                }
            }

            // Установка оригинального текста, по которому шел парсинг
            this.originalFormulaText = this.Element.Text;
        }

        #endregion
    }

    public sealed class FormulaSourceGroup : CellsGroup<CellFormula>
    {
        #region Fields

        private int expandBy; // = 0;

        private RowCellGroup row;

        private TableGroup tableGroup;

        #endregion

        #region Constructors

        public FormulaSourceGroup(
            FormulaElement element,
            WorksheetElement worksheet,
            string left,
            string right,
            int bottom,
            int top)
            : base(element.Element, worksheet)
        {
            this.InFormulaWorksheet = element.Worksheet == worksheet;

            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Определяет, находится ли источник данных для формулы в той же группе, что и сама формула.
        /// </summary>
        public bool InFormulaWorksheet { get; }

        /// <summary>
        /// Флаг определяет, что источник данных находится в той же группе (или ее подгруппе), что и сама формула.
        /// </summary>
        public bool InFormulaTableGroup { get; set; }

        /// <summary>
        /// Флаг определяет, что данный источник данных расширяется вместе со своей таблицей.
        /// </summary>
        public bool IsExpandable => this.TableGroup != null;

        /// <summary>
        /// Строка, к которой относится данная группа.
        /// </summary>
        public RowCellGroup Row
        {
            get => this.row;
            set
            {
                if (this.row != value)
                {
                    if (this.row != null)
                    {
                        this.row.FormulaSources.Remove(this);
                    }

                    this.row = value;

                    if (this.row != null)
                    {
                        this.row.FormulaSources.Add(this);
                    }
                }
            }
        }

        /// <summary>
        /// Определяет таблицу, к которой относится источник данных.
        /// </summary>
        public TableGroup TableGroup
        {
            get => this.tableGroup;
            set
            {
                if (this.tableGroup != value)
                {
                    if (this.tableGroup != null)
                    {
                        this.tableGroup.ExpandableSources.Remove(this);
                    }

                    this.tableGroup = value;

                    if (this.tableGroup != null)
                    {
                        this.tableGroup.ExpandableSources.Add(this);
                    }
                }
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string GetDisplayString()
        {
            return (this.InFormulaWorksheet ? string.Empty : $"'{this.Worksheet.Name}'!") + base.GetDisplayString();
        }

        /// <summary>
        /// Метод не предусмотрен для данного класса
        /// </summary>
        public override ElementBase<CellFormula> Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод не предусмотрен для данного класса
        /// </summary>
        public override void Insert()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод не предусмотрен для данного класса
        /// </summary>
        public override void Update()
        {
            if (this.MoveBy != 0 || this.expandBy != 0)
            {
                this.Top += this.MoveBy + this.expandBy;
                this.Bottom += this.MoveBy;

                this.MoveBy = 0;
                this.expandBy = 0;
            }
        }

        public override void ParseElement()
        {
            // Парсинг идет из элемента, создающего этот объект
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Метод для расширения области, которую занимает данный источник данных на заданное число.
        /// </summary>
        /// <param name="rowCount">Число строк, на которые нужно увеличить данную область.</param>
        public void ExpandBy(int rowCount)
        {
            this.expandBy += rowCount;
        }

        /// <summary>
        /// Метод для проверки валидности источника данных для формул. Источник данных может стать не валидным, например, если ссылался на таблицу, которая была удалена полностью.
        /// </summary>
        /// <returns>Возвращает true, если источник данных был валидным, иначе false.</returns>
        public bool Check()
        {
            return this.Top >= this.Bottom;
        }

        #endregion
    }

    /// <summary>
    /// Список типов таблиц в Excel
    /// </summary>
    public enum TableGroupType
    {
        Row,
        Jump,
        Group,
        Table,
    }

    public sealed class SharedStringTableContainer
    {
        #region Fields

        private readonly SharedStringTable sharedStringTable;
        private readonly List<SharedStringItem> items;
        private readonly int initialItemsCount;

        #endregion

        #region Constructors

        public SharedStringTableContainer(SharedStringTable sharedStringTable)
        {
            this.sharedStringTable = sharedStringTable;

            this.items = sharedStringTable.Elements<SharedStringItem>().ToList();
            this.initialItemsCount = this.items.Count;
        }

        #endregion

        #region Properties

        public int Count => this.items.Count;

        #endregion

        #region Public Methods

        public SharedStringItem ElementAt(int index)
        {
            return this.items[index];
        }

        public void Add(SharedStringItem newItem)
        {
            this.items.Add(newItem);
        }

        public void Save()
        {
            for (int i = this.initialItemsCount; i < this.items.Count; i++)
            {
                this.sharedStringTable.AppendChild(this.items[i]);
            }

            (this.sharedStringTable.Count ??= new UInt32Value()).Value = (uint) this.items.Count;
            (this.sharedStringTable.UniqueCount ??= new UInt32Value()).Value = (uint) this.items.Count;
        }

        #endregion
    }
}