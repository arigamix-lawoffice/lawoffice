namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект, поддерживающий выполнение сценариев времени выполнения.
    /// </summary>
    public interface IRuntimeSources
    {
        /// <summary>
        /// Текст SQL запроса условия времени выполнения.
        /// </summary>
        /// <remarks>
        /// Запрос должен возвращать 0/NULL или 1.
        /// </remarks>
        string RuntimeSqlCondition { get; }

        /// <summary>
        /// C# код условия времени выполнения.
        /// </summary>
        string RuntimeSourceCondition { get; }

        /// <summary>
        /// C# код сценария инициализации времени выполнения.
        /// </summary>
        string RuntimeSourceBefore { get; }

        /// <summary>
        /// C# код сценария постобработки времени выполнения.
        /// </summary>
        string RuntimeSourceAfter { get; }
    }
}
