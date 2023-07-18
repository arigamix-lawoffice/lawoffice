var satellite = await this.GetContextualSatelliteAsync();

if (satellite is null)
{
	return;
}

var currentStageID = satellite.GetApprovalInfoSection().Fields.Get<Guid>(KrConstants.KrProcessCommonInfo.CurrentApprovalStageRowID);

Guid? currentGroupID = null;

foreach (var row in satellite.GetStagesSection().Rows)
{
	if (currentGroupID.HasValue)
	{
		if (currentGroupID.Value == row.Get<Guid>(KrConstants.KrStages.StageGroupID))
		{
			await this.SetStageStateAsync(row, KrStageState.Inactive);
		}
		else
		{
			break;
		}
	}
	else
	{
		if (row.RowID == currentStageID)
		{
			currentGroupID = row.Get<Guid>(KrConstants.KrStages.StageGroupID);
		}
	}
}