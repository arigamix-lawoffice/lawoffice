namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Статус запуска процесса.
    /// </summary>
    public enum KrProcessLaunchStatus
    {
        /// <summary>
        /// Не определён.
        /// </summary>
        Undefined,

        /// <summary>
        /// В процессе выполнения.
        /// </summary>
        /// <remarks>Используется для асинхронных процессов.</remarks>
        InProgress,

        /// <summary>
        /// Процесс успешно завершён.
        /// </summary>
        Complete,

        /// <summary>
        /// При запуске процесса произошла ошибка.
        /// </summary>
        Error,

        /// <summary>
        /// Запуск процесса запрещён ограничениями.
        /// </summary>
        Forbidden,
    }
}
