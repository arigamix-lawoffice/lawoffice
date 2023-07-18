using Tessa.Cards;
using Tessa.Cards.Metadata;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsMaskGenerator" />
    public sealed class KrPermissionsMaskGenerator : IKrPermissionsMaskGenerator
    {
        #region IKrPermissionsMaskGenerator Implementation

        /// <inheritdoc />
        public object GenerateMaskValue(
            Card card,
            CardSection section,
            CardRow row,
            CardMetadataColumn columnMeta,
            object originalValue,
            string defaultMask)
        {
            if (columnMeta.ParentRowSection is not null)
            {
                return originalValue;
            }

            if (!string.IsNullOrWhiteSpace(defaultMask))
            {
                return defaultMask;
            }
            return null;
        }

        #endregion
    }
}
