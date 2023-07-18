<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="778c5e62-6064-447e-92ac-68913d6a42cd" Name="KrProcessManagementStageTypeModes" Group="KrStageTypes">
	<SchemePhysicalColumn ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="48aeec20-f020-42b5-87f3-30dcff12f057" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="fdab40b8-aa62-40c8-8596-aa1813ab4929" Name="pk_KrProcessManagementStageTypeModes">
		<SchemeIndexedColumn Column="99558c30-ca4e-42f3-952f-9c486b6d4c4c" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">0</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_StageMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">1</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_GroupMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">2</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_NextGroupMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">3</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_PrevGroupMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">4</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_CurrentGroupMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">5</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_SignalMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">6</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_CancelProcessMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="99558c30-ca4e-42f3-952f-9c486b6d4c4c">7</ID>
		<Name ID="48aeec20-f020-42b5-87f3-30dcff12f057">$KrStages_ProcessManagement_SkipProcessMode</Name>
	</SchemeRecord>
</SchemeTable>