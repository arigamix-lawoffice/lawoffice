using System;
using System.Runtime.Serialization;
using System.Security;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    [Serializable]
    public class ScriptExecutionException : ExecutionExceptionBase
    {
        public ScriptExecutionException(string errorMessageText, string sourceText)
            : base(errorMessageText, sourceText)
        {
        }

        public ScriptExecutionException(string errorMessageText, string sourceText, Exception innerException)
            : base(errorMessageText, sourceText, innerException)
        {
        }

        [SecuritySafeCritical]
        protected ScriptExecutionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {

        }
    }
}