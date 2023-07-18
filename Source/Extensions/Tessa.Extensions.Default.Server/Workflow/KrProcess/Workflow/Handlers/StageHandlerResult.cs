using System;
using System.Diagnostics;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Представляет сообщение для ProcessRunner содержащее результаты обработки этапа.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct StageHandlerResult : IEquatable<StageHandlerResult>
    {
        #region static

        /// <summary>
        /// Сообщение для ProcessRunner о том, что обработчик не обработал этап.
        /// </summary>
        public static readonly StageHandlerResult EmptyResult =
            new StageHandlerResult(StageHandlerAction.None, null, null);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что этап в активном состоянии.
        /// </summary>
        public static readonly StageHandlerResult InProgressResult =
            new StageHandlerResult(StageHandlerAction.InProgress, null, null);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что этап завершен.
        /// </summary>
        public static readonly StageHandlerResult CompleteResult =
            new StageHandlerResult(StageHandlerAction.Complete, null, null);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что этап следует пропустить.
        /// </summary>
        public static readonly StageHandlerResult SkipResult =
            new StageHandlerResult(StageHandlerAction.Skip, null, null);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что  следует совершить переход на другой этап в текущей группе.
        /// </summary>
        /// <param name="stageRowID">Идентификатор этапа, на который нужно совершить переход.</param>
        /// <param name="keepStageStates">Признак того, нужно ли сохранять состояния этапов при переходе.</param>
        public static StageHandlerResult Transition(Guid stageRowID, bool keepStageStates = false) =>
            new StageHandlerResult(StageHandlerAction.Transition, stageRowID, keepStageStates);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что следует совершить переход в начало другой группы этапов.
        /// </summary>
        /// <param name="stageGroupID">Идентификатор группы этапов.</param>
        /// <param name="keepStageStates">Признак того, нужно ли сохранять состояния этапов при переходе</param>
        public static StageHandlerResult GroupTransition(Guid stageGroupID, bool keepStageStates = false) =>
            new StageHandlerResult(StageHandlerAction.GroupTransition, stageGroupID, keepStageStates);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что следует совершить переход в начало текущей группы.
        /// </summary>
        /// <param name="keepStageStates">Признак того, нужно ли сохранять состояния этапов при переходе.</param>
        public static StageHandlerResult CurrentGroupTransition(bool keepStageStates = false) =>
            new StageHandlerResult(StageHandlerAction.CurrentGroupTransition, null, keepStageStates);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что следует совершить переход на следующую группу с учетом пересчета набора групп.
        /// </summary>
        /// <param name="keepStageStates">Признак того, нужно ли сохранять состояния этапов при переходе.</param>
        public static StageHandlerResult NextGroupTransition(bool keepStageStates = false) =>
            new StageHandlerResult(StageHandlerAction.NextGroupTransition, null, keepStageStates);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что следует совершить переход на следующую группу с учетом пересчета набора групп.
        /// </summary>
        /// <param name="keepStageStates">Признак того, нужно ли сохранять состояния этапов при переходе.</param>
        public static StageHandlerResult PreviousGroupTransition(bool keepStageStates = false) =>
            new StageHandlerResult(StageHandlerAction.PreviousGroupTransition, null, keepStageStates);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что необходимо пропустить весь процесс.
        /// </summary>
        public static readonly StageHandlerResult SkipProcessResult =
            new StageHandlerResult(StageHandlerAction.SkipProcess, null, null);

        /// <summary>
        /// Сообщение для ProcessRunner о том, что необходимо отменить весь процесс.
        /// </summary>
        public static readonly StageHandlerResult CancelProcessResult =
            new StageHandlerResult(StageHandlerAction.CancelProcess, null, null);

        #endregion

        #region constructor

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="StageHandlerResult"/>.
        /// </summary>
        /// <param name="action">Результат взаимодействия StageRunner c этапом после выполнения его обработчика.</param>
        /// <param name="transitionID">Идентификатор объекта (этапа или группы этапов) к которому должен быть выполнен переход.</param>
        /// <param name="keepStageStates">Значение <see langword="true"/>, есди необходимо сохранить состояния этапов, иначе - <see langword="false"/>.</param>
        public StageHandlerResult(
            StageHandlerAction action,
            Guid? transitionID,
            bool? keepStageStates)
        {
            this.Action = action;
            this.TransitionID = transitionID;
            this.KeepStageStates = keepStageStates;
        }

        #endregion

        #region public

        /// <summary>
        /// Возвращает результат взаимодействия StageRunner c этапом после выполнения его обработчика.
        /// </summary>
        public StageHandlerAction Action { get; }

        /// <summary>
        /// Возвращает идентификатор объекта (этапа или группы этапов) к которому должен быть выполнен переход.
        /// </summary>
        public Guid? TransitionID { get; }

        /// <summary>
        /// Возвращает значение флага показывающего, что необходимо сохранить состояния этапов.
        /// </summary>
        public bool? KeepStageStates { get; }

        /// <inheritdoc />
        public bool Equals(
            StageHandlerResult other)
        {
            return this.Action == other.Action
                && this.TransitionID.Equals(other.TransitionID)
                && this.KeepStageStates.Equals(other.KeepStageStates);
        }

        /// <inheritdoc />
        public override bool Equals(
            object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is StageHandlerResult result
                && this.Equals(result);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) this.Action * 397) ^ this.TransitionID.GetHashCode();
            }
        }

        public static bool operator ==(
            StageHandlerResult left,
            StageHandlerResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(
            StageHandlerResult left,
            StageHandlerResult right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Private Methods

        private string GetDebuggerDisplay() =>
            $"{DebugHelper.GetTypeName(this)}: " +
            $"{nameof(this.Action)} = {this.Action}, " +
            $"{nameof(this.TransitionID)} = {DebugHelper.FormatNullable(this.TransitionID?.ToString("B"))}, " +
            $"{nameof(this.KeepStageStates)} = {DebugHelper.FormatNullable(this.KeepStageStates)}";

        #endregion
    }
}