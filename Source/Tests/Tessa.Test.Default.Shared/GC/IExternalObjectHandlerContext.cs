using Tessa.Extensions;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.GC.Handlers;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Контекст обработчика внешних объектов.
    /// </summary>
    /// <seealso cref="IExternalObjectHandler"/>
    public interface IExternalObjectHandlerContext :
        IExtensionContext
    {
        /// <summary>
        /// Возвращает обрабатываемый объект.
        /// </summary>
        ExternalObjectInfo ObjectInfo { get; }

        /// <summary>
        /// Возвращает результаты валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, была ли отменена обработка объекта или нет.
        /// </summary>
        bool Cancel { get; set; }
    }
}
