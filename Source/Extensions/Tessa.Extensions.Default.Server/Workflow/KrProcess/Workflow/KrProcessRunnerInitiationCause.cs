namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Перечисление причин запуска процесса.
    /// </summary>
    public enum KrProcessRunnerInitiationCause
    {
        /// <summary>
        /// Запуск процесса.
        /// </summary>
        StartProcess,

        /// <summary>
        /// Завершение задания.
        /// </summary>
        CompleteTask,

        /// <summary>
        /// Возврат задания на роль после взятия в работу.
        /// </summary>
        ReinstateTask,

        /// <summary>
        /// Обработка поступившего в процесс сигнала.
        /// </summary>
        Signal,

        /// <summary>
        /// Выполнение процесса в памяти.
        /// </summary>
        InMemoryLaunching,

        /// <summary>
        /// Возобновление сериализованного процесса.
        /// </summary>
        Resurrection,
    }
}