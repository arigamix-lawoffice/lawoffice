var card = await context.GetCardAsync().ConfigureAwait(false);
if (card is null || card.Tasks.Count == 0)
{
	return false;
}

var settings = context.Settings["KrTaskTypeConditionSettings"];
bool isAuthor = settings.RawFields.TryGet<bool>("IsAuthor"),
	 isPerformer = settings.RawFields.TryGet<bool>("IsPerformer"),
	 inProgress = settings.RawFields.TryGet<bool>("InProgress");

// Если на стоят флаги проверки автора или исполнителя, то проверка априори не успешна
if (!(isAuthor || isPerformer))
{
	return false;
}

var taskIDs = card.Tasks
	.Where(x => 
		(isAuthor && x.TaskSessionRoles.Any(y => y.FunctionRoleID == CardFunctionRoles.AuthorID)) // Если нужно првоерить, что автор
		|| (isPerformer && x.TaskSessionRoles.Any(y => y.FunctionRoleID == CardFunctionRoles.PerformerID) // Если нужно проверить, что исполнитель
			&& (!inProgress || x.StoredState == CardTaskState.InProgress))) // Если нужно проверить, что в работе
	.Select(x => x.TypeID)
	.ToArray();

return context.Settings["KrTaskTypeCondition"].Rows.Any(row => 
{
	var settingTypeID = row.TryGet<Guid>("TaskTypeID");
	return taskIDs.Contains(settingTypeID);
});
