using System.Diagnostics;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    /// <summary>
    /// Результат обработки состояния.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class StateHandlerResult : IStateHandlerResult
    {
        #region Static Fields

        /// <summary>
        /// Состояние не было обработано.
        /// </summary>
        public static readonly IStateHandlerResult EmptyResult =
            new StateHandlerResult(false, false, false);

        /// <summary>
        /// Результат обработки состояния с необходимостью сохранить изменения.
        /// </summary>
        public static readonly IStateHandlerResult WithoutContinuationProcessResult =
            new StateHandlerResult(true, false, false);

        /// <summary>
        /// Результат обработки состояния без необходимости сохранения изменений.
        /// </summary>
        public static readonly IStateHandlerResult ContinueCurrentRunResult =
            new StateHandlerResult(true, true, false);

        /// <summary>
        /// Результат обработки состояния без необходимости сохранения изменений.
        /// </summary>
        public static readonly IStateHandlerResult ContinueCurrentRunWithStartingNewGroupResult =
            new StateHandlerResult(true, true, true);

        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StateHandlerResult"/>.
        /// </summary>
        /// <param name="handled">Значение <see langword="true"/>, если состояние было обработано, иначе - <see langword="false"/>.</param>
        /// <param name="continueCurrentRun">Значение <see langword="true"/>, если необходимо после обработки состояния продолжать текущее выполнение runner-a, иначе - <see langword="false"/>.</param>
        /// <param name="forceStartGroup">Значение <see langword="true"/>, если необходимо начать выполнение этапов из следующей группы этапов, несмотря на то, что в текущей группе этапов остались не обработанные этапы., иначе - <see langword="false"/>.</param>
        public StateHandlerResult(
            bool handled,
            bool continueCurrentRun,
            bool forceStartGroup)
        {
            this.Handled = handled;
            this.ContinueCurrentRun = continueCurrentRun;
            this.ForceStartGroup = forceStartGroup;
        }

        #region IStateHandlerResult Members

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