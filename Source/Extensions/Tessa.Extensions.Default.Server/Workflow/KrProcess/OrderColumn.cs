using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class OrderColumn : IEquatable<OrderColumn>
    {
        public OrderColumn(
            string sectionName,
            string orderFieldName)
        {
            this.SectionName = sectionName;
            this.OrderFieldName = orderFieldName;
        }

        /// <summary>
        /// Секция, в которой есть поле для сортировки.
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        /// Поле для сортировки.
        /// </summary>
        public string OrderFieldName { get; }

        /// <inheritdoc />
        public bool Equals(
            OrderColumn other)
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
                && string.Equals(this.OrderFieldName, other.OrderFieldName, StringComparison.Ordinal);
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

            return obj is OrderColumn column
                && this.Equals(column);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.SectionName != null ? StringComparer.InvariantCulture.GetHashCode(this.SectionName) : 0) * 397)
                    ^ (this.OrderFieldName != null ? StringComparer.InvariantCulture.GetHashCode(this.OrderFieldName) : 0);
            }
        }
    }
}