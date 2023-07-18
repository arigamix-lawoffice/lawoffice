using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Default.Shared.Conditions
{
    /// <summary>
    /// Обработчик источника данных условий для карточек правил доступа.
    /// </summary>
    public sealed class KrPermissionsConditionSourceHandler : FieldsConditionSourceHandlerBase
    {
        #region Constructors

        /// <inheritdoc cref="FieldsConditionSourceHandlerBase(IDbScope)"/>
        public KrPermissionsConditionSourceHandler(
            [OptionalDependency] IDbScope dbScope = null)
            : base(dbScope)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override string SectionName => "KrPermissions";

        /// <inheritdoc/>
        protected override string FieldName => "Conditions";

        #endregion
    }
}
