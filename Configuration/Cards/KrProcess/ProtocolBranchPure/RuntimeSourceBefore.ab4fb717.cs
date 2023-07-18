var plannedKey = KrConstants.KrResolutionSettingsVirtual.Planned;
var inDaysKey = KrConstants.KrResolutionSettingsVirtual.DurationInDays;
var commentKey = KrConstants.KrResolutionSettingsVirtual.Comment;

var settings = Stage.SettingsStorage;
var info = ProcessInfoStorage;

settings[commentKey] = info["Question"];
if (info.TryGetValue("Planned", out var pObj) && pObj is DateTime planned) {
	settings[plannedKey] = planned;
	settings[inDaysKey] = null;
} else if (info.TryGetValue("DurationInDays", out var dObj) && dObj is double days) {
	settings[plannedKey] = null;
	settings[inDaysKey] = days;
}

var performers = (IList<object>) info["Performers"];
foreach (var perf in performers.Cast<IList<object>>()) {
	AddPerformer((Guid)perf[0], (string)perf[1], Stage);
}