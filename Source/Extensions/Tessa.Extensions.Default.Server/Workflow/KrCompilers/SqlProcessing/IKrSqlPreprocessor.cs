namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <summary>
    /// Объект, выполняющий предобработку запроса.
    /// </summary>
    public interface IKrSqlPreprocessor
    {
        /// <summary>
        /// Выполнет предобработку запроса.
        /// </summary>
        /// <param name="context">Контекст выполнения запроса.</param>
        /// <returns>Результат предобработки запроса.</returns>
        IKrSqlPreprocessorResult Preprocess(IKrSqlExecutorContext context);
    }
}