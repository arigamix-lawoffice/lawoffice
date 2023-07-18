<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="b55966a9-f474-47c9-b025-8e3408208646" Name="AclGenerationRuleTriggerModes" Group="Acl">
	<SchemePhysicalColumn ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="32d164ed-92d7-4a89-b647-c86aa03fe62d" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="59efae4b-27ee-4ed0-b8b1-fa483ebb92b9" Name="pk_AclGenerationRuleTriggerModes">
		<SchemeIndexedColumn Column="fe9505d2-be3a-4c1b-b718-1fd73093dc1f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">0</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">AnyChanges</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">1</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">FieldChanged</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">2</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">RowAdded</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">3</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">RowDeleted</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">4</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">CardCreated</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">5</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">CardDeleted</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">6</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">TaskCreated</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="fe9505d2-be3a-4c1b-b718-1fd73093dc1f">7</ID>
		<Name ID="32d164ed-92d7-4a89-b647-c86aa03fe62d">TaskCompleted</Name>
	</SchemeRecord>
</SchemeTable>