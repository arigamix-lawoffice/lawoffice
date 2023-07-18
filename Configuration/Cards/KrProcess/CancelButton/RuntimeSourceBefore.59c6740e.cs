var mainProcessStarted = await IsMainProcessStartedAsync();

if (!IsMainProcess() && !mainProcessStarted && await GetContextualSatelliteAsync() != null)
{
	await ForEachStageInMainProcessAsync(p => SetStageStateAsync(p, KrStageState.Inactive), true);
}
ProcessInfo.IsMainProcessStarted = mainProcessStarted;