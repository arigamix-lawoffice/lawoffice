using System;
using System.Runtime.Serialization;
using System.Security;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Исключение, возникающее при прерывании выполнения <see cref="IKrProcessRunner"/>.
    /// </summary>
    [Serializable]
    public class ProcessRunnerInterruptedException: Exception
    {
        #region constructors

        public ProcessRunnerInterruptedException()
            : base()
        {

        }

        public ProcessRunnerInterruptedException(
            string message)
            : base(message)
        {

        }

        public ProcessRunnerInterruptedException(
            string message,
            Exception innerException) 
            : base (message, innerException)
        {

        }

        [SecuritySafeCritical]
        protected ProcessRunnerInterruptedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {

        }

        #endregion

    }
}