<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="648381d6-8647-4ec6-87a4-3cbd6bae380c" Name="AccessLevels" Group="System">
	<Description>Уровни доступа пользователей.</Description>
	<SchemePhysicalColumn ID="5c20848a-0f1c-49ea-b6c1-454b0702295f" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="6df97497-8f88-42ac-a445-d03ca67ed96b" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="ce24e3a3-8ef2-4ca8-8aaa-1e37de761988" Name="pk_AccessLevels">
		<SchemeIndexedColumn Column="5c20848a-0f1c-49ea-b6c1-454b0702295f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="5c20848a-0f1c-49ea-b6c1-454b0702295f">0</ID>
		<Name ID="6df97497-8f88-42ac-a445-d03ca67ed96b">$Enum_AccessLevels_Regular</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5c20848a-0f1c-49ea-b6c1-454b0702295f">1</ID>
		<Name ID="6df97497-8f88-42ac-a445-d03ca67ed96b">$Enum_AccessLevels_Administrator</Name>
	</SchemeRecord>
</SchemeTable>