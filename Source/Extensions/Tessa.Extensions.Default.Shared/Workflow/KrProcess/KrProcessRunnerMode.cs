namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Режимы выполнения маршрута.
    /// </summary>
    public enum KrProcessRunnerMode
    {
        /// <summary>
        /// Синхронный режим.
        /// </summary>
        /// <remarks>Рекомендуется указывать всегда, выполнение этапа осуществляется за один запуск. Отправка заданий невозможна.</remarks>
        Sync,

        /// <summary>
        /// Асинхронный режим.
        /// </summary>
        /// <remarks>Рекомендуется указывать, если этап отправляет задания.</remarks>
        Async,
    }
}