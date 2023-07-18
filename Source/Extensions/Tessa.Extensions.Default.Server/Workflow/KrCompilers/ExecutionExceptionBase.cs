using System;
using System.Runtime.Serialization;
using System.Security;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    [Serializable]
    public abstract class ExecutionExceptionBase: Exception
    {
        protected ExecutionExceptionBase(string errorMessageText, string sourceText)
            : base()
        {
            this.ErrorMessageText = errorMessageText;
            this.SourceText = sourceText;
        }

        protected ExecutionExceptionBase(string errorMessageText, string sourceText, Exception innerException)
            : base(string.Empty, innerException)
        {
            this.ErrorMessageText = errorMessageText;
            this.SourceText = sourceText;
        }

        [SecuritySafeCritical]
        protected ExecutionExceptionBase(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {

        }

        public string ErrorMessageText { get; }

        public string SourceText { get; }
    }
}