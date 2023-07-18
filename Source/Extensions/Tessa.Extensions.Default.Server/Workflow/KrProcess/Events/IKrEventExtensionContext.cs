using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Контекст <see cref="IKrEventManager"/>.
    /// </summary>
    public interface IKrEventExtensionContext :
        IExtensionContext
    {
        /// <summary>
        /// Тип события.
        /// </summary>
        string EventType { get; }

        /// <summary>
        /// Дополнительная неперсистентная информация о событии.
        /// </summary>
        IDictionary<string, object> Info { get; }

        /// <summary>
        /// Идентификатор основной карточки или
        /// значение <see langword="null"/>, если процесс запущен вне карточки.
        /// </summary>
        Guid? MainCardID { get; }

        /// <summary>
        /// Тип основной карточки или
        /// значение <see langword="null"/>, если процесс запущен вне карточки.
        /// </summary>
        CardType MainCardType { get; }

        /// <summary>
        /// Тип документа основной карточки или
        /// значение <see langword="null"/>, если процесс запущен вне карточки.
        /// </summary>
        Guid? MainCardDocTypeID { get; }

        /// <summary>
        /// Стратегия загрузки основной карточки.
        /// </summary>
        IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <summary>
        /// Вторичный процесс.
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
        /// Холдер текущего процесса.
        /// </summary>
        ProcessHolder ProcessHolder { get; }

        /// <summary>
        /// Информация по процессу WorkflowAPI.
        /// </summary>
        IWorkflowProcessInfo ProcessInfo { get; }

        /// <summary>
        /// Информация по заданию WorkflowAPI.
        /// </summary>
        IWorkflowTaskInfo TaskInfo { get; }

        /// <summary>
        /// Информация по сигналу WorkflowAPI.
        /// </summary>
        IWorkflowSignalInfo SignalInfo { get; }

        /// <summary>
        /// Режим раннера, запустившего обработку этапа.
        /// </summary>
        KrProcessRunnerMode? RunnerMode { get; }

        /// <summary>
        /// Причина, по которой был вызван раннер: запуск процесса, завершение задания, обработка сигнала и др.
        /// </summary>
        KrProcessRunnerInitiationCause? InitiationCause { get; }
    }
}