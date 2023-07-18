using System.Diagnostics;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    /// <summary>
    /// Представляет результат обработки глобального сигнала.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class GlobalSignalHandlerResult : IGlobalSignalHandlerResult
    {
        #region Static Fields

        /// <summary>
        /// Сигнал не был обработан. Необходимо продолжить выполнение runner-а.
        /// </summary>
        public static readonly IGlobalSignalHandlerResult EmptyHandlerResult =
            new GlobalSignalHandlerResult(false, true, false);

        /// <summary>
        /// Сигнал был обработан. Необходимо прервать выполнение runner-а.
        /// </summary>
        public static readonly IGlobalSignalHandlerResult WithoutContinuationProcessResult =
            new GlobalSignalHandlerResult(true, false, false);

        /// <summary>
        /// Сигнал был обработан. Необходимо продолжить выполнение runner-а.
        /// </summary>
        public static readonly IGlobalSignalHandlerResult ContinueCurrentRunResult =
            new GlobalSignalHandlerResult(true, true, false);

        /// <summary>
        /// Сигнал был обработан. Необходимо продолжить выполнение runner-а и начать выполнение этапов из следующей группы этапов.
        /// </summary>
        public static readonly IGlobalSignalHandlerResult ContinueCurrentRunWithStartingNewGroupResult =
            new GlobalSignalHandlerResult(true, true, true);

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GlobalSignalHandlerResult"/>.
        /// </summary>
        /// <param name="handled">Значение <see langword="true"/>, если сигнал был обработан, иначе - <see langword="false"/>.</param>
        /// <param name="continueCurrentRun">Значение <see langword="true"/>, если необходимо после обработки сигнала продолжать текущее выполнение runner-a, иначе - <see langword="false"/>.</param>
        /// <param name="forceStartGroup">Значение <see langword="true"/>, если необходимо начать выполнение этапов из следующей группы этапов, несмотря на то, что в текущей группе этапов остались не обработанные этапы., иначе - <see langword="false"/>.</param>
        public GlobalSignalHandlerResult(
            bool handled,
            bool continueCurrentRun,
            bool forceStartGroup)
        {
            this.Handled = handled;
            this.ContinueCurrentRun = continueCurrentRun;
            this.ForceStartGroup = forceStartGroup;
        }

        #endregion

        #region IGlobalSignalHandlerResult Members

        /// <inheritdoc />
        public bool Handled { get; }

        /// <inheritdoc />
        public bool ContinueCurrentRun { get; }

        /// <inheritdoc />
        public bool ForceStartGroup { get; }

        #endregion

        #region Private Methods

        private string GetDebuggerDisplay() =>
            $"{DebugHelper.GetTypeName(this)}: " +
            $"{nameof(this.Handled)} = {this.Handled}, " +
            $"{nameof(this.ContinueCurrentRun)} = {this.ContinueCurrentRun}, " +
            $"{nameof(this.ForceStartGroup)} = {this.ForceStartGroup}";

        #endregion
    }
}