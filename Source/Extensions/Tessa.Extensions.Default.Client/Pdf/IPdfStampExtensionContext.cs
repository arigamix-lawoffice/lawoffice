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
    public interface IPdfStampExtensionContext :
        IExtensionContext
    {
        /// <summary>
        /// Редактируемое представление карточки <see cref="Card"/> на клиенте.
        /// Позволяет выполнить сохранение или обновление карточки, или получить информацию из контекста.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        ICardEditorModel Editor { get; }

        /// <summary>
        /// Модель карточки, для файлов которой выполняется конвертация с простановкой штампа,
        /// со средствами управления её объектами в UI.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        ICardModel Model { get; }

        /// <summary>
        /// Карточка, для файлов которой выполняется конвертация с простановкой штампа.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        Card Card { get; }

        /// <summary>
        /// Контейнер, управляющий файлами в карточке <see cref="Card"/>. Обычно средствами этого объекта
        /// могут быть добавлены или заменены файлы в карточке.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        IFileUIContainer FileContainer { get; }

        /// <summary>
        /// Элемент управления файлами, с которым связана текущая обработка. Обычно средствами этого элемента управления
        /// будет добавлен или заменён файл в карточке после генерации. Может быть равен <c>null</c>.
        /// </summary>
        IFileControl FileControl { get; }

        /// <summary>
        /// Внешний контекст обработки документов PDF. Обычно это объект <see cref="IPdfGeneratorContext"/>.
        /// Может быть равен <c>null</c>.
        /// </summary>
        object Context { get; }

        /// <summary>
        /// Текущий документ PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        PdfDocument Document { get; }

        /// <summary>
        /// Текущая страница PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        PdfPage Page { get; }

        /// <summary>
        /// Объект, предоставляющий средства рисования текста и графических примитивов поверх страницы PDF.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        XGraphics Graphics { get; }

        /// <summary>
        /// Номер текущей обрабатываемой страницы, отсчитываемый от <c>1</c> до значения <see cref="TotalPages"/>.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Общее количество страниц в генерируемом документе.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Объект, посредством которого можно настроить вывод штампа.
        /// Если штамп будет указан непустым, то он будет выведен на текущей страницы после завершения работы расширений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        PdfStampWriter StampWriter { get; }

        /// <summary>
        /// Произвольная информация, существующая в рамках цепочки расширений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        ISerializableObject Info { get; }

        /// <summary>
        /// Информация для расширений, заданная в генераторе, обычно это объект <see cref="ScanDocumentGenerator"/>.
        /// Информация недоступна для изменений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        /// <doc path='exception[@type="ObjectSealedException"]'/>
        ISerializableObject GeneratorInfo { get; }

        /// <summary>
        /// Информация для расширений, существующая в рамках запроса по генерации для объекта <see cref="IPdfGenerator"/>.
        /// Информация недоступна для изменений.
        /// </summary>
        /// <remarks>
        /// <doc path='para[@item="property" and @message="IsAlwaysNotNull"]'/>
        /// </remarks>
        /// <doc path='exception[@type="ObjectSealedException"]'/>
        ISerializableObject GeneratorContextInfo { get; }
    }
}
