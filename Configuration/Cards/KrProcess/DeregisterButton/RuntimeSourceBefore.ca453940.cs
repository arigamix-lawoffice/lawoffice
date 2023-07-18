var mainProcessStarted = await IsMainProcessStartedAsync();
var state = WorkflowProcess.State;
if (!IsMainProcess() 
	&& !mainProcessStarted 
	&& await GetContextualSatelliteAsync() != null
	&& state != KrState.Approved
	&& state != KrState.Signed)
{
	await ForEachStageInMainProcessAsync(row => SetStageStateAsync(row, KrStageState.Inactive));
}
ProcessInfo.IsMainProcessStarted = mainProcessStarted;