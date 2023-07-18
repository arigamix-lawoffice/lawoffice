#nullable enable

using System.Collections.Generic;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Дескриптор, содержащий параметры настраиваемого диалога с параметрами фильтрации представления.
    /// </summary>
    public sealed class FilterViewDialogDescriptor
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="dialogName"><inheritdoc cref="DialogName" path="/summary"/></param>
        /// <param name="parametersMapping"><inheritdoc cref="ParametersMapping" path="/summary"/></param>
        public FilterViewDialogDescriptor(
            string dialogName,
            IReadOnlyCollection<ParameterMapping> parametersMapping)
        {
            this.DialogName = NotWhiteSpaceOrThrow(dialogName);
            this.ParametersMapping = NotNullOrThrow(parametersMapping);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Имя типа диалога.
        /// </summary>
        public string DialogName { get; }

        /// <summary>
        /// Алиас формы диалога или <see langword="null"/>, если требуется создать форму для первой вкладки типа диалога.
        /// </summary>
        public string? FormAlias { get; init; }

        /// <summary>
        /// Коллекция, содержащая информацию о связи параметров представления и полей карточки.
        /// </summary>
        public IReadOnlyCollection<ParameterMapping> ParametersMapping { get; }

        #endregion
    }
}
