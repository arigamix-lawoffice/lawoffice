using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Default.Shared.Conditions
{
    /// <summary>
    /// Обработчик источника данных условий для карточек виртуальных файлов.
    /// </summary>
    public sealed class KrVirtualFileConditionSourceHandler : FieldsConditionSourceHandlerBase
    {
        #region Constructors

        /// <inheritdoc cref="FieldsConditionSourceHandlerBase(IDbScope)"/>
        public KrVirtualFileConditionSourceHandler(
            [OptionalDependency] IDbScope dbScope = null)
            : base(dbScope)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override string SectionName => "KrVirtualFiles";

        /// <inheritdoc/>
        protected override string FieldName => "Conditions";

        #endregion
    }
}
