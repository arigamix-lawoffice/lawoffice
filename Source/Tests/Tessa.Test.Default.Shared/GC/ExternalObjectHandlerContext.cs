using System;
using System.Threading;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.GC
{
    /// <inheritdoc cref="IExternalObjectHandlerContext"/>
    public sealed class ExternalObjectHandlerContext :
        IExternalObjectHandlerContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExternalObjectHandlerContext"/>.
        /// </summary>
        /// <param name="objectInfo">Информация о внешнем объекте.</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        public ExternalObjectHandlerContext(
            ExternalObjectInfo objectInfo,
            IValidationResultBuilder validationResult)
        {
            this.ObjectInfo = objectInfo ?? throw new ArgumentNullException(nameof(objectInfo));
            this.ValidationResult = validationResult ?? throw new ArgumentNullException(nameof(validationResult));
        }

        #endregion

        #region IExternalObjectHandlerContext Members

        /// <inheritdoc/>
        public ExternalObjectInfo ObjectInfo { get; }

        /// <inheritdoc/>
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc/>
        public bool Cancel { get; set; }

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}
