namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    public sealed class WfSatelliteTransferRepairExtension : Tessa.Cards.Extensions.Templates.CardSatelliteTransferRepairExtension
    {
        #region Base Overrides

        protected override string StoreKey => WfHelper.SatelliteKey;

        protected override string SatelliteSectionName => WfHelper.SatelliteSection;

        #endregion
    }
}
