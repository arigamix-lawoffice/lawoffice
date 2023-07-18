#nullable enable

using Tessa.Platform;
using Tessa.TextRecognition;

namespace Tessa.Extensions.Default.Console.TextRecognition.Synchronous
{
    /// <summary>
    /// Контекст синхронной операции распознавания файла.
    /// </summary>
    public sealed class OperationContext : Base.OperationContext
    {
        #region Properties

        /// <summary>
        /// Путь к файлу для распознавания.
        /// </summary>
        public string SourceFilePath { get; }

        /// <summary>
        /// Путь к директории, в которую будет помещен результат распознавания.
        /// </summary>
        public string TargetFolderPath { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Создаёт экземпляр класса <see cref="OperationContext"/>.
        /// </summary>
        /// <param name="sourceFilePath"><inheritdoc cref="TargetFolderPath" path="/summary"/></param>
        /// <param name="targetFolderPath"><inheritdoc cref="Parameters" path="/summary"/></param>
        /// <param name="parameters"><inheritdoc cref="Base.OperationContext.Parameters" path="/summary"/></param>
        public OperationContext(string sourceFilePath, string targetFolderPath, OcrParameters parameters)
            : base(parameters)
        {
            this.SourceFilePath = NotWhiteSpaceOrThrow(sourceFilePath);
            this.TargetFolderPath = NotWhiteSpaceOrThrow(targetFolderPath);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string GetDescription() => StringBuilderHelper.Acquire(16)
            .AppendLine($"  - {nameof(this.SourceFilePath)}: \"{this.SourceFilePath}\",")
            .AppendLine($"  - {nameof(this.TargetFolderPath)}: \"{this.TargetFolderPath}\",")
            .ToStringAndRelease() + base.GetDescription();

        #endregion
    }
}
