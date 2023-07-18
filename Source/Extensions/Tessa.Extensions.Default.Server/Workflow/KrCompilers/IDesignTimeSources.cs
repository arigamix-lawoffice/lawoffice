namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект, поддерживающий выполнение сценариев времени построения.
    /// </summary>
    public interface IDesignTimeSources
    {
        /// <summary>
        /// Текст SQL запроса условия времени построения.
        /// </summary>
        /// <remarks>
        /// Запрос должен возвращать 0/NULL или 1.
        /// </remarks>
        string SqlCondition { get; }

        /// <summary>
        /// C# код условия времени построения.
        /// </summary>
        string SourceCondition { get; }

        /// <summary>
        /// C# код сценария инициализации времени построения.
        /// </summary>
        string SourceBefore { get; }

        /// <summary>
        /// C# код сценария постобработки времени построения.
        /// </summary>
        string SourceAfter { get; }
    }
}
