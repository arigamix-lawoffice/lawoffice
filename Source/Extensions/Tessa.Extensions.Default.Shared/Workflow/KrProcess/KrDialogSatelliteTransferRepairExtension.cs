using Tessa.Cards.Extensions.Templates;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrDialogSatelliteTransferRepairExtension : CardSatelliteTransferRepairExtension
    {
        #region Base Overrides

        protected override string StoreKey => CardSatelliteHelper.SatelliteKey + "_krdialogsSatellites";

        protected override string SatelliteSectionName => "KrDialogSatellite";

        #endregion
    }
}
