using System;
using System.Threading;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Tessa.Cards;
using Tessa.Extensions.Platform.Client.Scanning;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Контекст расширений <see cref="IPdfStampExtension"/>, выполняющих простановку штампов PDF для файлов карточки.
    /// </summary>
    public sealed class PdfStampExtensionContext :
        IPdfStampExtensionContext
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений его свойств.
        /// </summary>
        /// <param name="editor">
        /// Редактируемое представление карточки <paramref name="card"/> на клиенте.
        /// Позволяет выполнить сохранение или обновление карточки, или получить информацию из контекста.
        /// Значение не должно быть равно <c>null</c>.
        /// </param>
        /// <param name="model">
        /// Модель карточки, для файлов которой выполняется конвертация с простановкой штампа,
        /// со средствами управления её объектами в UI.
        /// Значение не должно быть равно <c>null</c>.
        /// </param>
        /// <param name="card">
        /// Карточка, для файлов которой выполняется конвертация с простановкой штампа.
        /// Значение не должно быть равно <c>null</c>.
        /// </param>
        /// <param name="fileContainer">
        /// Контейнер, управляющий файлами в карточке <paramref name="card"/>. Обычно средствами этого объекта
        /// могут быть добавлены или заменены файлы в карточке.
        /// Значение не должно быть равно <c>null</c>.
        /// </param>
        /// <param name="fileControl">
        /// Элемент управления файлами, с которым связана текущая обработка. Обычно средствами этого элемента управления
        /// будет добавлен или заменён файл в карточке после генерации. Может быть равен <c>null</c>.
        /// </param>
        /// <param name="totalPages">Общее количество страниц в генерируемом документе.</param>
        /// <param name="context">
        /// Внешний контекст обработки документов PDF. Обычно это объект <see cref="IPdfGeneratorContext"/>.
        /// Может быть равен <c>null</c>.
        /// </param>
        /// <param name="document">Текущий документ PDF. Значение не должно быть равно <c>null</c>.</param>
        /// <param name="stampWriter">
        /// Объект, посредством которого можно настроить вывод штампа.
        /// Если штамп будет указан непустым, то он будет выведен на текущей страницы после завершения работы расширений.
        /// Значение не должно быть равно <c>null</c>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public PdfStampExtensionContext(
            ICardEditorModel editor,
            ICardModel model,
            Card card,
            IFileUIContainer fileContainer,
            IFileControl fileControl,
            int totalPages,
            object context,
            PdfDocument document,
            PdfStampWriter stampWriter,
            CancellationToken cancellationToken = default)
        {
            if (totalPages <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPages));
            }

            this.Editor = editor ?? throw new ArgumentNullException(nameof(editor));
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Card = card ?? throw new ArgumentNullException(nameof(card));
            this.FileContainer = fileContainer ?? throw new ArgumentNullException(nameof(fileContainer));
            this.FileControl = fileControl;
            this.TotalPages = totalPages;
            this.Context = context;
            this.Document = document ?? throw new ArgumentNullException(nameof(document));
            this.StampWriter = stampWriter ?? throw new ArgumentNullException(nameof(stampWriter));
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Редактируемое представление карточки <see cref="Card"/> на клиенте.
        /// Позволяет выполнить сохранение или обновление карточки, или получить информацию из контекста.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public ICardEditorModel Editor { get; }

        /// <summary>
        /// Модель карточки, для файлов которой выполняется конвертация с простановкой штампа,
        /// со средствами управления её объектами в UI.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public ICardModel Model { get; }

        /// <summary>
        /// Карточка, для файлов которой выполняется конвертация с простановкой штампа.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public Card Card { get; }

        /// <summary>
        /// Контейнер, управляющий файлами в карточке <see cref="Card"/>. Обычно средствами этого объекта
        /// могут быть добавлены или заменены файлы в карточке.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public IFileUIContainer FileContainer { get; }

        /// <summary>
        /// Элемент управления файлами, с которым связана текущая обработка. Обычно средствами этого элемента управления
        /// будет добавлен или заменён файл в карточке после генерации. Может быть равен <c>null</c>.
        /// </summary>
        public IFileControl FileControl { get; }

        /// <summary>
        /// Внешний контекст обработки документов PDF. Обычно это объект <see cref="IPdfGeneratorContext"/>.
        /// Может быть равен <c>null</c>.
        /// </summary>
        public object Context { get; }

        /// <summary>
        /// Текущий документ PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public PdfDocument Document { get; }

        /// <summary>
        /// Текущая страница PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public PdfPage Page { get; private set; }

        /// <summary>
        /// Объект, предоставляющий средства рисования текста и графических примитивов поверх страницы PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public XGraphics Graphics { get; private set; }

        /// <summary>
        /// Номер текущей обрабатываемой страницы, отсчитываемый от <c>1</c> до значения <see cref="TotalPages"/>.
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Общее количество страниц в генерируемом документе.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Объект, посредством которого можно настроить вывод штампа.
        /// Если штамп будет указан непустым, то он будет выведен на текущей страницы после завершения работы расширений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public PdfStampWriter StampWriter { get; }

        /// <summary>
        /// Произвольная информация, существующая в рамках цепочки расширений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        public ISerializableObject Info { get; } = new SerializableObject();

        /// <summary>
        /// Информация для расширений, заданная в генераторе, обычно это объект <see cref="ScanDocumentGenerator"/>.
        /// Информация недоступна для изменений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        /// <doc path='exception[@type="ObjectSealedException"]'/>
        public ISerializableObject GeneratorInfo { get; } = new SerializableObject();

        /// <summary>
        /// Информация для расширений, существующая в рамках запроса по генерации для объекта <see cref="IPdfGenerator"/>.
        /// Информация недоступна для изменений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        /// <doc path='exception[@type="ObjectSealedException"]'/>
        public ISerializableObject GeneratorContextInfo { get; } = new SerializableObject();

        #endregion

        #region Methods

        /// <summary>
        /// Изменяет текущую страницу PDF в контексте расширения.
        /// </summary>
        /// <param name="page">Текущая страница PDF. Не равна <c>null</c>.</param>
        /// <param name="graphics">
        /// Объект, предоставляющий средства рисования текста и графических примитивов поверх страницы PDF.
        /// Не равен <c>null</c>.
        /// </param>
        /// <param name="pageNumber">
        /// Номер текущей обрабатываемой страницы, отсчитываемый от <c>1</c> до значения <see cref="TotalPages"/>.
        /// </param>
        public void SetPage(PdfPage page, XGraphics graphics, int pageNumber)
        {
            this.Page = page ?? throw new ArgumentNullException(nameof(page));
            this.Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            this.PageNumber = pageNumber;

            this.StampWriter.Clear();
        }


        /// <summary>
        /// Очищает информацию по странице PDF в свойствах <see cref="Page"/>, <see cref="Graphics"/> и <see cref="PageNumber"/>.
        /// Вызовите метод, чтобы предотвратить утечки памяти после того, как объект уже не требуется использовать.
        /// Не используйте объект после вызова этого метода, кроме случаев, когда позже вызывается метод <see cref="SetPage"/>.
        /// </summary>
        public void ClearPage()
        {
            this.Page = null;
            this.Graphics = null;
            this.PageNumber = 0;

            this.StampWriter.Clear();
        }

        #endregion

        #region IExtensionContext Members

        /// <doc path='info[@type="IExtensionContext" and @item="CancellationToken"]'/>
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}
