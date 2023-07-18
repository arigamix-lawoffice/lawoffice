<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="beee4f3d-a385-4fc8-884f-bc1ccf55fc5b" Name="KrStageState" Group="Kr">
	<SchemePhysicalColumn ID="a4844cd9-4328-48d8-8f37-acdd6bce5ffe" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="fed52724-582f-49a9-8a3a-9cc2af1c5109" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="fd10079c-93ac-4ff7-a0a6-0aebb77e6d3d" Name="pk_KrStageState" IsClustered="true">
		<SchemeIndexedColumn Column="a4844cd9-4328-48d8-8f37-acdd6bce5ffe" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="a4844cd9-4328-48d8-8f37-acdd6bce5ffe">0</ID>
		<Name ID="fed52724-582f-49a9-8a3a-9cc2af1c5109">$KrStates_Stage_Inactive</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a4844cd9-4328-48d8-8f37-acdd6bce5ffe">1</ID>
		<Name ID="fed52724-582f-49a9-8a3a-9cc2af1c5109">$KrStates_Stage_Active</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a4844cd9-4328-48d8-8f37-acdd6bce5ffe">2</ID>
		<Name ID="fed52724-582f-49a9-8a3a-9cc2af1c5109">$KrStates_Stage_Completed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a4844cd9-4328-48d8-8f37-acdd6bce5ffe">3</ID>
		<Name ID="fed52724-582f-49a9-8a3a-9cc2af1c5109">$KrStates_Stage_Skipped</Name>
	</SchemeRecord>
</SchemeTable>