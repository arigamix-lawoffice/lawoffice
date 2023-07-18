namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Режим работы этапа "Управление процессом" (<see cref="StageTypeDescriptors.ProcessManagementDescriptor"/>).
    /// </summary>
    public enum ProcessManagementStageTypeMode
    {
        /// <summary>
        /// Переход на этап.
        /// </summary>
        StageMode = 0,

        /// <summary>
        /// Переход на группу.
        /// </summary>
        GroupMode = 1,

        /// <summary>
        /// Переход на следующую группу.
        /// </summary>
        NextGroupMode = 2,

        /// <summary>
        /// Переход на предыдущую группу.
        /// </summary>
        PrevGroupMode = 3,

        /// <summary>
        /// Переход на начало текущей группы.
        /// </summary>
        CurrentGroupMode = 4,

        /// <summary>
        /// Отправить процесс.
        /// </summary>
        SendSignalMode = 5,

        /// <summary>
        /// Отменить процесс.
        /// </summary>
        CancelProcessMode = 6,

        /// <summary>
        /// Пропустить процесс.
        /// </summary>
        SkipProcessMode = 7,
    }
}
