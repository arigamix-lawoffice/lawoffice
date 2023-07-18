using System;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <summary>
    /// Контекст <see cref="IKrSqlExecutor"/>.
    /// </summary>
    public interface IKrSqlExecutorContext : IExtensionContext
    {
        /// <summary>
        /// Запрос на выполнение.
        /// </summary>
        string Query { get; }

        /// <summary>
        /// Результат валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Функция формирования сообщения об ошибки в процессе обработки и выполнения запроса.
        /// </summary>
        /// <remarks>
        /// Параметры: вызывающий объект; описание ошибки.
        /// Возвращаемое значение - полный текст ошибки.
        /// </remarks>
        Func<IKrSqlExecutorContext, string, object[], string> GetErrorTextFunc { get; }

        /// <summary>
        /// Текущий вторичный процесс кнопки.
        /// </summary>
        IKrSecondaryProcess SecondaryProcess { get; }

        /// <summary>
        /// Идентификатор группы этапов.
        /// </summary>
        Guid StageGroupID { get; }

        /// <summary>
        /// Идентификатор типа этапа.
        /// </summary>
        Guid StageTypeID { get; }

        /// <summary>
        /// Идентификатор шаблона этапов.
        /// </summary>
        Guid StageTemplateID { get; }

        /// <summary>
        /// Идентификатор строки этапа.
        /// </summary>
        Guid StageRowID { get; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        Guid? UserID { get; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Идентификатор карточки.
        /// </summary>
        Guid? CardID { get; }

        /// <summary>
        /// Идентификатор типа карточки.
        /// </summary>
        Guid? CardTypeID { get; }

        /// <summary>
        /// Идентификатор типа документа.
        /// </summary>
        Guid? DocTypeID { get; }

        /// <summary>
        /// Эффективный идентификатор типа.
        /// Если тип использует типы документов, то это тип документа.
        /// Иначе это тип карточки.
        /// </summary>
        Guid? TypeID { get; }

        /// <summary>
        /// Состояние карточки.
        /// </summary>
        KrState? State { get; }

        /// <summary>
        /// Название этапа.
        /// </summary>
        string StageName { get; }

        /// <summary>
        /// Название шаблона этапов.
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Название группы этапов.
        /// </summary>
        string GroupName { get; }
    }
}