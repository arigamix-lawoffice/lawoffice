#nullable enable

using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Предоставляет информацию о связи параметра представления и поля карточки.
    /// </summary>
    public sealed class ParameterMapping
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ParameterMapping"/>.
        /// </summary>
        /// <param name="alias"><inheritdoc cref="Alias" path="/summary"/></param>
        /// <param name="valueSectionName"><inheritdoc cref="ValueSectionName" path="/summary"/></param>
        /// <param name="valueFieldName"><inheritdoc cref="ValueFieldName" path="/summary"/></param>
        public ParameterMapping(
            string alias,
            string valueSectionName,
            string valueFieldName)
        {
            this.Alias = NotEmptyOrThrow(alias);
            this.ValueSectionName = NotEmptyOrThrow(valueSectionName);
            this.ValueFieldName = NotEmptyOrThrow(valueFieldName);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Алиас параметра представления.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Имя секции, содержащей поле <see cref="ValueFieldName"/>.
        /// </summary>
        public string ValueSectionName { get; }

        /// <summary>
        /// Поле, содержащее значение параметра.
        /// </summary>
        public string ValueFieldName { get; }

        /// <summary>
        /// Имя секции, содержащей поле <see cref="DisplayValueFieldName"/>. Если не задано, то используется строковое представление значения параметра.
        /// </summary>
        public string? DisplayValueSectionName { get; init; }

        /// <summary>
        /// Имя поля, содержащего отображаемое значение параметра. Если не задано, то используется строковое представление значения параметра.
        /// </summary>
        /// <remarks>Для корректной работы должно быть задано значение <see cref="DisplayValueSectionName"/>.</remarks>
        public string? DisplayValueFieldName { get; init; }

        /// <summary>
        /// Условный оператор. Если не задан, то используется оператор по умолчанию.
        /// </summary>
        public CriteriaOperator? CriteriaOperator { get; init; }

        #endregion
    }
}
