#using Tessa.Extensions.Default.Shared.Settings;
var cardObject = await GetCardObjectAsync();
var decisions = cardObject.Sections["ProtocolDecisions"].TryGetRows();
var responsibles = cardObject.Sections["ProtocolResponsibles"].Rows;
var branches = (IList<object>)Stage.SettingsStorage[KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic];
if (decisions == null || decisions.Count == 0) {
    AddError("$KrProtocols_NoDecisionsWarning");
    return;
}

var krSettings = Resolve<KrSettings>();
var protocolBranchSecondaryProcessID = new Guid(0xF6D8C61A, 0xD0E9, 0x4905, 0xBE, 0x0B, 0x07, 0x9A, 0x53, 0xE6, 0xE2, 0x27);
var protocolBranchSecondaryProcessName = "$KrProtocols_Branch";
foreach (var decision in decisions) {
    var decisionRowID = decision.RowID;
	var rowID = Guid.NewGuid();
	var row = new Dictionary<string, object>();
	row[KrConstants.KrForkSecondaryProcessesSettingsVirtual.RowID] = rowID;
	row[KrConstants.KrForkSecondaryProcessesSettingsVirtual.ID] = CardID;
	row[KrConstants.StageRowIDReferenceToOwner] = Stage.RowID;
	row[KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID] = protocolBranchSecondaryProcessID;
	row[KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessName] = protocolBranchSecondaryProcessName;
	branches.Add(row);
	
	var info = GetProcessInfoForBranch(rowID);
	var performers = new List<List<object>>();
	info["Question"] = decision.Get<string>("Question");
	var planned = decision.Get<DateTime?>("Planned");
	if (planned != null) {
		info["Planned"] = DateTime
		    .SpecifyKind(planned.Value.Date, DateTimeKind.Local)
		    .Add(new TimeSpan(23, 59, 59));
	} else {
		info["DurationInDays"] = krSettings.ProtocolTaskDefaultDuration;
	}
	info["Performers"] = responsibles
		.Where(p => p.ParentRowID == decisionRowID)
		.Select(p => new List<object> { p.Get<Guid>("UserID"), p.Get<string>("UserName"), })
		.ToList();
}