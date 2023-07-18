using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    /// <summary>
    /// Представляет состояние процесса.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class KrProcessState : IEquatable<KrProcessState>
    {
        /// <summary>
        /// Состояние процесса по умолчанию.
        /// </summary>
        public static readonly KrProcessState Default = new KrProcessState(nameof(Default));

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessState"/>.
        /// </summary>
        /// <param name="name">Название состояния.</param>
        /// <param name="parameters">Дополнительные параметры состояния.</param>
        /// <param name="nextState">Рекомендация, какое состояние следует выставить после текущего.</param>
        public KrProcessState(
            string name,
            Dictionary<string, object> parameters = null,
            KrProcessState nextState = null)
        {
            this.Name = name;
            this.Parameters = parameters ?? new Dictionary<string, object>(StringComparer.Ordinal);
            this.NextState = nextState;
        }

        /// <summary>
        /// Название состояния, которое уникально идентифицирует его.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Дополнительные параметры состояния.
        /// </summary>
        public Dictionary<string, object> Parameters { get; }

        /// <summary>
        /// Рекомендация, какое состояние следует выставить после текущего.
        /// </summary>
        public KrProcessState NextState { get; }

        /// <inheritdoc />
        public bool Equals(
            KrProcessState other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || string.Equals(this.Name, other.Name, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public override bool Equals(
            object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is KrProcessState state && this.Equals(state);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            this.Name?.GetHashCode(StringComparison.Ordinal) ?? 0;

        public static bool operator ==(
            KrProcessState left,
            KrProcessState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(
            KrProcessState left,
            KrProcessState right)
        {
            return !Equals(left, right);
        }

        private string GetDebuggerDisplay() =>
            $"{DebugHelper.GetTypeName(this)}: " +
            $"{nameof(this.Name)} = {DebugHelper.FormatNullable(this.Name)}, " +
            $"{nameof(this.Parameters)} = {DebugHelper.FormatNullable(string.Join(", ", this.Parameters.Select(p => $"{p.Key} = {p.Value}")))}" +
            $"{nameof(this.NextState)} = {DebugHelper.FormatNullable(this.NextState?.GetDebuggerDisplay())}";
    }
}
