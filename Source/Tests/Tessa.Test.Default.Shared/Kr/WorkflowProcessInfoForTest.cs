using System;
using Tessa.Platform;
using Tessa.Platform.Formatting;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет информацию по процессу используемую в тестах.
    /// </summary>
    public sealed class WorkflowProcessInfoForTest : IEquatable<string>, IEquatable<WorkflowProcessInfoForTest>
    {
        #region Properties

        /// <summary>
        /// Возвращает идентификатор процесса.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает имя типа процесса.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Возвращает параметры процесса.
        /// </summary>
        public string Parameters { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkflowProcessInfoForTest"/>.
        /// </summary>
        /// <param name="id">Идентификатор процесса.</param>
        /// <param name="typeName">Имя типа процесса.</param>
        /// <param name="parameters">Параметры процесса.</param>
        public WorkflowProcessInfoForTest(
            Guid id,
            string typeName,
            string parameters)
        {
            this.ID = id;
            this.TypeName = typeName;
            this.Parameters = parameters;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string ToString() =>
            $"{Environment.NewLine}" +
            $"{this.GetType().Name}: " +
            $"{nameof(this.ID)} = {this.ID:B}, " +
            $"{nameof(this.TypeName)} = {this.TypeName}, " +
            $"{nameof(this.Parameters)} = {FormattingHelper.FormatNullable(this.Parameters)}";

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            this.Equals(obj as WorkflowProcessInfoForTest);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.ID, this.TypeName, this.Parameters);

        #endregion

        #region Operators

        /// <summary>
        /// Определяет равны ли два указанных объекта.
        /// </summary>
        /// <param name="x">Первый сравниваемый объект.</param>
        /// <param name="y">Второй сравниваемый объект.</param>
        /// <returns>Значение <see langword="true"/>, если объекты равны, иначе - <see langword="false"/>.</returns>
        public static bool operator ==(WorkflowProcessInfoForTest x, WorkflowProcessInfoForTest y)
        {
            return ReferenceEquals(x, y) && x is not null && x.Equals(y);
        }

        /// <summary>
        /// Определяет не равны ли два указанных объекта.
        /// </summary>
        /// <param name="x">Первый сравниваемый объект.</param>
        /// <param name="y">Второй сравниваемый объект.</param>
        /// <returns>Значение <see langword="true"/>, если объекты не равны, иначе - <see langword="false"/>.</returns>
        public static bool operator !=(WorkflowProcessInfoForTest x, WorkflowProcessInfoForTest y)
        {
            return !(x == y);
        }

        #endregion

        #region IEquatable<string> Members

        /// <inheritdoc/>
        public bool Equals(string other) =>
            string.Equals(other, this.TypeName, StringComparison.Ordinal);

        #endregion

        #region IEquatable<WorkflowProcessInfoForTest> Members

        /// <inheritdoc/>
        public bool Equals(WorkflowProcessInfoForTest other)
        {
            return other is not null
                && this.ID == other.ID
                && string.Equals(this.TypeName, other.TypeName, StringComparison.Ordinal)
                && string.Equals(this.Parameters, other.Parameters, StringComparison.Ordinal);
        }

        #endregion
    }
}
