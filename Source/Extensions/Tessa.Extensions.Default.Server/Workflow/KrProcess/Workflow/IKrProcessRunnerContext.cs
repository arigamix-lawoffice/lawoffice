using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает контекст <see cref="IKrProcessRunner"/>.
    /// </summary>
    public interface IKrProcessRunnerContext : IExtensionContext
    {
        /// <summary>
        /// Мост до WorkflowAPI
        /// </summary>
        IWorkflowAPIBridge WorkflowAPI { get; }

        /// <summary>
        /// Объект для работы с группами истории заданий.
        /// Может принимать <c>null</c>, если отсутствует возможность управления группами истории заданий.
        /// </summary>
        IKrTaskHistoryResolver TaskHistoryResolver { get; }

        /// <summary>
        /// Стратегия загрузки основной карточки.
        /// </summary>
        IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <summary>
        /// Идентификатор карточки.
        /// </summary>
        Guid? CardID { get; }

        /// <summary>
        /// Тип карточки.
        /// </summary>
        CardType CardType { get; }

        /// <summary>
        /// Идентификатор типа документа.
        /// </summary>
        Guid? DocTypeID { get; }

        /// <summary>
        /// Включенные компоненты типового решения для текущей карточки.
        /// </summary>
        KrComponents? KrComponents { get; }

        /// <summary>
        /// Контекст выполнения процесса.
        /// </summary>
        WorkflowProcess WorkflowProcess { get; }

        /// <summary>
        /// Возвращает объект содержащий информацию по текущему процессу.
        /// </summary>
        ProcessHolder ProcessHolder { get; }

        /// <summary>
        /// Текущий контекстуальный сателлит.
        /// </summary>
        Card ContextualSatellite { get; }

        /// <summary>
        /// Текущий сателлит процесса. Имеет значение по умолчанию для типа, если процесс выполняется в памяти.
        /// </summary>
        Card ProcessHolderSatellite { get; }

        /// <summary>
        /// Причина запуска процесса.
        /// Складывается на основе информации в контексте.
        /// </summary>
        KrProcessRunnerInitiationCause InitiationCause { get; }

        /// <summary>
        /// Информация по запущенному процессу.
        /// Если <see cref="InitiationCause"/> равно <see cref="KrProcessRunnerInitiationCause.InMemoryLaunching"/>, то возвращает значение <see langword="null"/>.
        /// </summary>
        IWorkflowProcessInfo ProcessInfo { get; }

        /// <summary>
        /// Информация по заданию в процессе.
        /// Если <see cref="InitiationCause"/> не равно <see cref="KrProcessRunnerInitiationCause.CompleteTask"/>, то возвращает значение <see langword="null"/>.
        /// </summary>
        IWorkflowTaskInfo TaskInfo { get; }

        /// <summary>
        /// Информация по сигналу поступившему в процесс.
        /// Если <see cref="InitiationCause"/> не равно <see cref="KrProcessRunnerInitiationCause.Signal"/>, то возвращает значение <see langword="null"/>.
        /// </summary>
        IWorkflowSignalInfo SignalInfo { get; }

        /// <summary>
        /// Результат валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Контекст расширения на карточке. Может иметь значение по умолчанию для типа.
        /// </summary>
        ICardExtensionContext CardContext { get; }

        /// <summary>
        /// Конфигурация вторичного процесса. Может иметь значение по умолчанию для типа.
        /// </summary>
        IKrSecondaryProcess SecondaryProcess { get; }

        /// <summary>
        /// Тип родительского процесса, если есть.
        /// </summary>
        string ParentProcessTypeName { get; }

        /// <summary>
        /// Идентификатор родительского процесса, если есть.
        /// </summary>
        Guid? ParentProcessID { get; }

        /// <summary>
        /// Игнорировать скрипты групп и частичный пересчет.
        /// </summary>
        bool IgnoreGroupScripts { get; }

        /// <summary>
        /// Кэш единиц исполнения в рамках одного выполнения Runner-а.
        /// </summary>
        Dictionary<Guid, IKrExecutionUnit> ExecutionUnitCache { get; }

        /// <summary>
        /// Список этапов, которые в процессе текущего выполнения были пропущены по условию скрипта.
        /// </summary>
        List<Guid> SkippedStagesByCondition { get; }

        /// <summary>
        /// Список групп, которые в процессе текущего выполнения были пропущены по условию скрипта.
        /// </summary>
        List<Guid> SkippedGroupsByCondition { get; }

        /// <summary>
        /// Фабрика для получения стандартных стратегий подготовки группы для пересчета.
        /// </summary>
        Func<IPreparingGroupRecalcStrategy> DefaultPreparingGroupStrategyFunc { get; }

        /// <summary>
        /// Стратегия для формирования данных, необходимых для пересчета.
        /// </summary>
        IPreparingGroupRecalcStrategy PreparingGroupStrategy { get; set; }

        /// <summary>
        /// Возвращает значение, показывающее, что текущий процесс создал холдер сателлита.
        /// </summary>
        bool IsProcessHolderCreated { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что при отсутствии этапов, доступных для выполнения, не должно отображаться сообщение.
        /// </summary>
        bool NotMessageHasNoActiveStages { get; }

        /// <summary>
        /// Обновляет карточки содержащие информацию о процессе по данным содержащимся в текущем объекте.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        ValueTask UpdateCardAsync();
    }
}