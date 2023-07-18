#script

return !WorkflowProcess.Stages.ForEachStageInGroup(
	Stage.StageGroupID,
	currStage =>
	{
		if (currStage.State == KrStageState.Completed
			&& (currStage.StageTypeID == StageTypeDescriptors.ApprovalDescriptor.ID
				&& !(currStage.SettingsStorage.TryGet<bool?>(KrConstants.KrApprovalSettingsVirtual.Advisory) ?? false)
				|| currStage.StageTypeID == StageTypeDescriptors.SigningDescriptor.ID)
			&& currStage.InfoStorage.TryGet<bool?>(KrConstants.Keys.Disapproved) == true)
		{
			return false;
		}
		
		return true;
	});