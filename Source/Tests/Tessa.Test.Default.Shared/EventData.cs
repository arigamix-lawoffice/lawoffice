#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tessa.Platform.Collections;
using Tessa.Platform.Formatting;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Информация о событии.
    /// </summary>
    public readonly struct EventData
    {
        #region Properties

        /// <summary>
        /// Источник события.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Идентификатор события.
        /// </summary>
        public string? EventID { get; }

        /// <summary>
        /// Данные о событии.
        /// </summary>
        public IReadOnlyList<object?> Data { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="source"><inheritdoc cref="Source" path="/summary"/></param>
        /// <param name="ID"><inheritdoc cref="Source" path="/summary"/></param>
        /// <param name="data"><inheritdoc cref="Data" path="/summary"/></param>
        public EventData(
            string source,
            string? eventID,
            IReadOnlyList<object?>? data = null)
        {
            this.Source = NotWhiteSpaceOrThrow(source);
            this.EventID = eventID;
            this.Data = data ?? EmptyHolder<object?>.Collection;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="source"><inheritdoc cref="Source" path="/summary"/></param>
        /// <param name="ID"><inheritdoc cref="Source" path="/summary"/></param>
        /// <param name="data"><inheritdoc cref="Data" path="/summary"/></param>
        public EventData(
            string source,
            string? eventID,
            params object?[]? data)
            : this(
                  source,
                  eventID,
                  data is null
                  ? data
                  : new ReadOnlyCollection<object?>(data))
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"{nameof(this.Source)}=\"{this.Source}\"" +
                $", {nameof(this.EventID)}=\"{FormattingHelper.FormatNullable(this.EventID)}\"" +
                $", {nameof(this.Data)}={this.DataToString()}";
        }

        #endregion

        #region Private Methods

        private string DataToString()
        {
            const int maxCount = 5;
            const string separator = ", ";

            var result = string.Join(separator, this.Data.Take(10));

            if (this.Data.Count <= maxCount)
            {
                return result;
            }

            return $"{result}...";
        }

        #endregion
    }
}
