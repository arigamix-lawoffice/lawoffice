#nullable enable

using System;
using Tessa.Platform;
using Tessa.TextRecognition;

namespace Tessa.Extensions.Default.Console.TextRecognition.Asynchronous
{
    /// <summary>
    /// Контекст асинхронной операции распознавания файла.
    /// </summary>
    public sealed class OperationContext : Base.OperationContext
    {
        #region Properties

        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid FileID { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Создаёт экземпляр класса <see cref="OperationContext"/>.
        /// </summary>
        /// <param name="fileID"><inheritdoc cref="FileID" path="/summary"/></param>
        /// <param name="parameters"><inheritdoc cref="OcrParameters" path="/summary"/></param>
        public OperationContext(Guid fileID, OcrParameters parameters)
            : base(parameters)
        {
            this.FileID = fileID;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string GetDescription() => StringBuilderHelper.Acquire(16)
            .AppendLine($"  - {nameof(this.FileID)}: \"{this.FileID}\",")
            .ToStringAndRelease() + base.GetDescription();

        #endregion
    }
}
