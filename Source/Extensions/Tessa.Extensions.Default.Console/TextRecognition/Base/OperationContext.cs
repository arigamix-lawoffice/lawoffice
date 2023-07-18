#nullable enable

using System.Linq;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Validation;
using Tessa.TextRecognition;

namespace Tessa.Extensions.Default.Console.TextRecognition.Base
{
    /// <summary>
    /// Базовый контекст операции распознавания файла.
    /// </summary>
    public abstract class OperationContext : ConsoleOperationContext
    {
        #region Properties

        /// <inheritdoc cref="IValidationResultBuilder" path="/summary"/>
        public IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Параметры распознавания файла.
        /// </summary>
        public OcrParameters Parameters { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Создаёт экземпляр класса <see cref="OperationContext"/>.
        /// </summary>
        /// <param name="parameters"><inheritdoc cref="Parameters" path="/summary"/></param>
        protected OperationContext(OcrParameters parameters)
        {
            this.Parameters = NotNullOrThrow(parameters);
            this.ValidationResult = new ValidationResultBuilder();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Формирует описание параметров операции в строковом представлении.
        /// </summary>
        /// <returns>Описание параметров операции в строковом представлении.</returns>
        public virtual string GetDescription()
        {
            var languages = this.Parameters.Languages.Select(l => l.GetDescription());

            return StringBuilderHelper.Acquire(256)
                .AppendLine($"  - {nameof(this.Parameters.SegmentationMode)}: \"{this.Parameters.SegmentationMode}\",")
                .AppendLine($"  - {nameof(this.Parameters.Languages)}: \"{string.Join("; ", languages)}\",")
                .AppendLine($"  - {nameof(this.Parameters.Confidence)}: \"{this.Parameters.Confidence}\",")
                .AppendLine($"  - {nameof(this.Parameters.Preprocess)}: \"{this.Parameters.Preprocess}\",")
                .AppendLine($"  - {nameof(this.Parameters.DetectRotation)}: \"{this.Parameters.DetectRotation}\",")
                .AppendLine($"  - {nameof(this.Parameters.DetectTables)}: \"{this.Parameters.DetectTables}\",")
                .Append($"  - {nameof(this.Parameters.Overwrite)}: \"{this.Parameters.Overwrite}\"")
                .ToStringAndRelease();
        }

        #endregion
    }
}
