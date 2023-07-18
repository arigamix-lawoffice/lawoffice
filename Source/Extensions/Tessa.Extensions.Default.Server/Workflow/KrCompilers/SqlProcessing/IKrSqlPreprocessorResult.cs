using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <summary>
    /// Результат предобработки запроса.
    /// </summary>
    public interface IKrSqlPreprocessorResult
    {
        /// <summary>
        /// Обработанный текст запроса.
        /// </summary>
        string Query { get; }

        /// <summary>
        /// Параметры запроса, полученные при предобработке.
        /// </summary>
        List<KeyValuePair<string, object>> Parameters { get; }
    }
}