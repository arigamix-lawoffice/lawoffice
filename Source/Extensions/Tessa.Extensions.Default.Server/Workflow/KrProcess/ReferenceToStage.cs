using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Описание прямого ребенка секции KrStages
    /// </summary>
    public sealed class ReferenceToStage : IEquatable<ReferenceToStage>
    {
        public ReferenceToStage(
            string sectionName,
            string rowIDFieldName)
        {
            this.SectionName = sectionName;
            this.RowIDFieldName = rowIDFieldName;
        }

        public string SectionName { get; }

        public string RowIDFieldName { get; }

        public override string ToString() => $"Sec = {this.SectionName}, Field = {this.RowIDFieldName}";

        /// <inheritdoc />
        public bool Equals(
            ReferenceToStage other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.SectionName, other.SectionName, StringComparison.Ordinal)
                && string.Equals(this.RowIDFieldName, other.RowIDFieldName, StringComparison.Ordinal);
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

            return obj is ReferenceToStage stage
                && this.Equals(stage);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.SectionName != null ? StringComparer.InvariantCulture.GetHashCode(this.SectionName) : 0) * 397)
                    ^ (this.RowIDFieldName != null ? StringComparer.InvariantCulture.GetHashCode(this.RowIDFieldName) : 0);
            }
        }
    }
}