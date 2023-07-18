<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="057a85c8-c20f-430b-bd3b-6ea9f9fb82ee" Name="TaskStates" Group="System">
	<Description>Состояние задания.</Description>
	<SchemePhysicalColumn ID="413df3de-fc7a-476d-a604-77ee5135e7bc" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор состояния.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e715302d-7604-416a-b7f6-8c8d99b48a17" Name="Name" Type="String(128) Not Null">
		<Description>Имя состояния.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="94be90f1-7146-4210-91e0-11a4a27cf75d" Name="pk_TaskStates">
		<SchemeIndexedColumn Column="413df3de-fc7a-476d-a604-77ee5135e7bc" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="413df3de-fc7a-476d-a604-77ee5135e7bc">0</ID>
		<Name ID="e715302d-7604-416a-b7f6-8c8d99b48a17">$Cards_TaskStates_New</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="413df3de-fc7a-476d-a604-77ee5135e7bc">1</ID>
		<Name ID="e715302d-7604-416a-b7f6-8c8d99b48a17">$Cards_TaskStates_InWork</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="413df3de-fc7a-476d-a604-77ee5135e7bc">2</ID>
		<Name ID="e715302d-7604-416a-b7f6-8c8d99b48a17">$Cards_TaskStates_Postponed</Name>
	</SchemeRecord>
</SchemeTable>