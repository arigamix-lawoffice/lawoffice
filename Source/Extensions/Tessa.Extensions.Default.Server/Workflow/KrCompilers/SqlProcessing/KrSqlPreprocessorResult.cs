using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <inheritdoc cref="IKrSqlPreprocessorResult"/>
    public sealed class KrSqlPreprocessorResult : IKrSqlPreprocessorResult
    {
        #region Constructors

        public KrSqlPreprocessorResult(
            string query,
            List<KeyValuePair<string, object>> parameters)
        {
            this.Query = query;
            this.Parameters = parameters;
        }

        #endregion

        #region IKrSqlPreprocessorResult Members

        /// <inheritdoc />
        public string Query { get; }

        /// <inheritdoc />
        public List<KeyValuePair<string, object>> Parameters { get; }

        #endregion

    }
}