namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Перечисление типов скриптов используемых в подсистеме маршрутов.
    /// </summary>
    public enum KrScriptType
    {
        /// <summary>
        /// Скрипт инициализации.
        /// </summary>
        Before,

        /// <summary>
        /// Скрипт условия выполнения.
        /// </summary>
        Condition,

        /// <summary>
        /// Скрипт постобработки.
        /// </summary>
        After,

        /// <summary>
        /// Скрипт условия видимости тайла вторичного процесса.
        /// </summary>
        ProcessVisibility,

        /// <summary>
        /// Скрипт условного выражения определяющего возможность выполнения вторичного процесса.
        /// </summary>
        ProcessExecution,
    }
}