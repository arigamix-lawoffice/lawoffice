var callerCard = context.StoreCard;
var cancellationToken = context.CancellationToken;

if (callerCard is null)
{
	return false;
}

var tasks = callerCard.TryGetTasks();
if (tasks is null
	|| tasks.Count == 0)
{
    return false;
}

var settings = context.Settings["TaskConditionSettings"];
var checkCreation = settings.RawFields.TryGet<bool?>("CheckTaskCreation") ?? false;
var checkCompletion = settings.RawFields.TryGet<bool?>("CheckTaskCompletion") ?? false;
var checkRoles = settings.RawFields.TryGet<bool?>("CheckTaskFunctionRolesChanges") ?? false;

var taskTypes = context.Settings["TaskConditionTaskTypes"].TryGetRows()?.Select(x => x.Get<Guid>("TypeID")).ToArray();
var taskKinds = context.Settings["TaskConditionTaskKinds"].TryGetRows()?.Select(x => x.Get<Guid>("TaskKindID")).ToArray();
var taskOptions = context.Settings["TaskConditionCompletionOptions"].TryGetRows()?.Select(x => x.Get<Guid>("CompletionOptionID")).ToArray();
var taskFunctionRoles = context.Settings["TaskConditionFunctionRoles"].TryGetRows()?.Select(x => x.Get<Guid>("FunctionRoleID")).ToArray();

var checkTaskTypes = !(taskTypes is null || taskTypes.Length == 0);
var checkTaskKinds = !(taskKinds is null || taskKinds.Length == 0);
var completionOptionsChecked = taskOptions is null || taskOptions.Length == 0;
var functionRolesChecked = taskFunctionRoles is null || taskFunctionRoles.Length == 0;

foreach (var task in tasks)
{
	bool taskStateChecked = false;
	if (checkCreation && task.State == CardRowState.Inserted)
	{
		taskStateChecked = true;
	}
	else if (checkCompletion 
		&& task.Action == CardTaskAction.Complete
		&& (completionOptionsChecked
			|| (task.OptionID is not null && taskOptions.Contains(task.OptionID.Value))))
	{
		taskStateChecked = true;
	}
	else if (checkRoles
		&& task.TaskAssignedRoles.Any(x => 
			x.State != CardTaskAssignedRoleState.None
			&& (functionRolesChecked || taskFunctionRoles.Contains(x.TaskRoleID))))
	{
		taskStateChecked = true;
	}
	
	if (!taskStateChecked)
	{
		continue;
	}

	if (checkTaskTypes
	    && !taskTypes.Contains(task.TypeID))
	{
	    continue;
	}

	if (checkTaskKinds
	    && task.Card.Sections.TryGetValue("TaskCommonInfo", out var taskCommonInfo)
	    && !taskKinds.Contains(taskCommonInfo.RawFields.TryGet<Guid?>("KindID") ?? Guid.Empty))
	{
	    continue;
	}

	return true;
}

return false;