namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrSecondarySatelliteTransferRepairExtension : Tessa.Cards.Extensions.Templates.CardSatelliteTransferRepairExtension
    {
        #region Base Overrides

        protected override string StoreKey => KrConstants.KrSecondarySatelliteListInfoKey;

        protected override string SatelliteSectionName => KrConstants.KrSecondaryProcessCommonInfo.Name;

        #endregion
    }
}
