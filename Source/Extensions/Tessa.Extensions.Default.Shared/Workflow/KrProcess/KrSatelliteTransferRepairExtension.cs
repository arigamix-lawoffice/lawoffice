namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrSatelliteTransferRepairExtension : Tessa.Cards.Extensions.Templates.CardSatelliteTransferRepairExtension
    {
        #region Base Overrides

        protected override string StoreKey => KrConstants.KrSatelliteInfoKey;

        protected override string SatelliteSectionName => KrConstants.KrApprovalCommonInfo.Name;

        #endregion
    }
}
