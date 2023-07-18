using System;
using System.Runtime.Serialization;
using System.Security;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    [Serializable]
    public class QueryExecutionException: ExecutionExceptionBase
    {
        public QueryExecutionException(string errorMessageText, string sourceText)
            : base(errorMessageText, sourceText)
        {
        }

        public QueryExecutionException(string errorMessageText, string sourceText, Exception innerException)
            : base(errorMessageText, sourceText, innerException)
        {
        }

        [SecuritySafeCritical]
        protected QueryExecutionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {

        }
    }
}