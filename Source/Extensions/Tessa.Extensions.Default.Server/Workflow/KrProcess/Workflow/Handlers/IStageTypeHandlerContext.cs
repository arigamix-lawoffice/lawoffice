using System;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Описывает контекст обработчика этапа.
    /// </summary>
    public interface IStageTypeHandlerContext : IExtensionContext
    {
        /// <summary>
        /// Стратегия загрузки основной карточки.
        /// </summary>
        IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <summary>
        /// ID основной карточки.
        /// <c>null</c>, если процесс запущен вне карточки.
        /// </summary>
        Guid? MainCardID { get; }

        /// <summary>
        /// Тип основной карточки или <see langword="null"/>, если процесс запущен вне карточки.
        /// </summary>
        CardType MainCardType { get; }

        /// <summary>
        /// Тип документа основной карточки.
        /// <c>null</c>, если процесс запущен вне карточки.
        /// </summary>
        Guid? MainCardDocTypeID { get; }

        /// <summary>
        /// Включенные компоненты типового решения для текущей карточки.
        /// </summary>
        KrComponents? KrComponents { get; }

        /// <summary>
        /// Кнопка, по которой запущен процесс.
        /// </summary>
        IKrSecondaryProcess SecondaryProcess { get; }

        /// <summary>
        /// Текущий контекстуальный сателлит.
        /// </summary>
        Card ContextualSatellite { get; }

        /// <summary>
        /// Текущий сателлит процесса.
        /// </summary>
        Card ProcessHolderSatellite { get; }

        /// <summary>
        /// Холдер текущих процессов.
        /// </summary>
        ProcessHolder ProcessHolder { get; }

        /// <summary>
        /// Контекст расширения, в рамках которого выполняется Kr процесс.
        /// </summary>
        ICardExtensionContext CardExtensionContext { get; }

        /// <summary>
        /// Результат валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Текущий этап процесса.
        /// </summary>
        Stage Stage { get; }

        /// <summary>
        /// Процесс.
        /// </summary>
        WorkflowProcess WorkflowProcess { get; }

        /// <summary>
        /// Информация по процессу WorkfowAPI.
        /// </summary>
        IWorkflowProcessInfo ProcessInfo { get; }

        /// <summary>
        /// Информация по заданию WorkflowAPI
        /// </summary>
        IWorkflowTaskInfo TaskInfo { get; }

        /// <summary>
        /// Информация по сигналу WorkflowAPI.
        /// </summary>
        IWorkflowSignalInfo SignalInfo { get; }

        /// <summary>
        /// Интерфейс для обращения к WorkflowAPI
        /// <c>null</c>, если процесс запущен вне WorkflowAPI.
        /// </summary>
        IWorkflowAPIBridge WorkflowAPI { get; }

        /// <summary>
        /// Объект для получения групп истории заданий или
        /// <c>null</c>, если в текущем контексте работа с историей заданий не поддерживается.
        /// </summary>
        IKrTaskHistoryResolver TaskHistoryResolver { get; }

        /// <summary>
        /// Режим раннера, запустившего обработку этапа.
        /// </summary>
        KrProcessRunnerMode RunnerMode { get; }

        /// <summary>
        /// Причина, по которой был вызван раннер: запуск процесса, завершение задания, обработка сигнала и др.
        /// </summary>
        KrProcessRunnerInitiationCause InitiationCause { get; }

        /// <summary>
        /// Направление дальнейшего движения процесса после прерывания.
        /// Актуально только для обработки прерывания. 
        /// </summary>
        DirectionAfterInterrupt? DirectionAfterInterrupt { get; }

        /// <summary>
        /// Тип родительского процесса, если есть.
        /// </summary>
        string ParentProcessTypeName { get; }

        /// <summary>
        /// Идентификатор родительского процесса, если есть.
        /// </summary>
        Guid? ParentProcessID { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что при отсутствии этапов, доступных для выполнения, не должно отображаться сообщение.
        /// </summary>
        bool NotMessageHasNoActiveStages { get; }
    }
}