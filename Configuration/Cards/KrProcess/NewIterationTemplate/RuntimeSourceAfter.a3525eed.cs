if (InitiationCause == KrProcessRunnerInitiationCause.StartProcess) {
	var user = Session.User;
	await AddTaskHistoryRecordAsync(
		DefaultTaskTypes.KrStartApprovalProcessTypeID,
		DefaultTaskTypes.KrStartApprovalProcessTypeName,
		"$CardTypes_TypesNames_KrStartApprovalProcess",
		DefaultCompletionOptions.NewApprovalCycle,
		WorkflowProcess.AuthorComment,
		user.ID,
		user.Name,
		await GetCycleAsync() + 1
	);
}