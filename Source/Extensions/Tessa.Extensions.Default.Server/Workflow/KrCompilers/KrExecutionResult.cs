using System;
using System.Collections.Generic;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrExecutionResult"/>
    public class KrExecutionResult :
        IKrExecutionResult
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="result"><inheritdoc cref="Result" path="/summary"/></param>
        /// <param name="confirmedIDs"><inheritdoc cref="ConfirmedIDs" path="/summary"/></param>
        /// <param name="status"><inheritdoc cref="Status" path="/summary"/></param>
        /// <param name="interruptedStageID"><inheritdoc cref="InterruptedStageID" path="/summary"/></param>
        public KrExecutionResult(
            ValidationResult result,
            IReadOnlyCollection<Guid> confirmedIDs,
            KrExecutionStatus status,
            Guid? interruptedStageID = null)
        {
            this.Result = NotNullOrThrow(result);
            this.ConfirmedIDs = NotNullOrThrow(confirmedIDs);
            this.Status = status;
            this.InterruptedStageID = interruptedStageID;
        }

        #endregion

        #region IKrExecutionResult Members

        /// <inheritdoc/>
        public ValidationResult Result { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection<Guid> ConfirmedIDs { get; }

        /// <inheritdoc/>
        public KrExecutionStatus Status { get; }

        /// <inheritdoc/>
        public Guid? InterruptedStageID { get; }

        #endregion
    }
}
