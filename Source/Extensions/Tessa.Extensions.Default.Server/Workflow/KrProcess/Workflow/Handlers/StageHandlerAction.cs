namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Варианты взаимодействия StageRunner c этапом после выполнения его обработчика.
    /// </summary>
    public enum StageHandlerAction
    {
        /// <summary>
        /// Обработчик не обработал событие этапа.
        /// </summary>
        None,

        /// <summary>
        /// Этап активен.
        /// </summary>
        InProgress,

        /// <summary>
        /// Этап завершен.
        /// </summary>
        Complete,

        /// <summary>
        /// Этап пропущен.
        /// </summary>
        Skip,

        /// <summary>
        /// Переход к другому этапу в пределах группы.
        /// </summary>
        Transition,

        /// <summary>
        /// Переход в начало другой группы.
        /// </summary>
        GroupTransition,

        /// <summary>
        /// Переход на следующую группу с учетом пересчета набора групп.
        /// </summary>
        NextGroupTransition,

        /// <summary>
        /// Переход на предыдущую группу с учетом пересчета набора групп.
        /// </summary>
        PreviousGroupTransition,

        /// <summary>
        /// Переход на текущую группу.
        /// </summary>
        CurrentGroupTransition,

        /// <summary>
        /// Пропуск всего процесса.
        /// </summary>
        SkipProcess,

        /// <summary>
        /// Отмена всего процесса.
        /// </summary>
        CancelProcess,
    }
}