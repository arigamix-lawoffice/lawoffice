namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    public sealed class WfTaskSatelliteTransferRepairExtension : Tessa.Cards.Extensions.Templates.CardSatelliteTransferRepairExtension
    {
        #region Base Overrides

        protected override bool IsTaskSatellite => true;

        protected override string TaskRowIDColumnName => "TaskRowID";

        protected override string StoreKey => WfHelper.TaskSatelliteListKey;

        protected override string SatelliteSectionName => WfHelper.TaskSatelliteSection;

        #endregion
    }
}
