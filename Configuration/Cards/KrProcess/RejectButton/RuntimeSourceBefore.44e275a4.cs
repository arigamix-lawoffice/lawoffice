var mainProcessStarted = await IsMainProcessStartedAsync();

if (!IsMainProcess() && !mainProcessStarted && await GetContextualSatelliteAsync() != null)
{
	await ForEachStageInMainProcessAsync(row => SetStageStateAsync(row, KrStageState.Inactive), true);
}
ProcessInfo.IsMainProcessStarted = mainProcessStarted;