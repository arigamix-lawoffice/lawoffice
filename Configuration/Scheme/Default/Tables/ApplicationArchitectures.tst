<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="27977834-b755-4a4a-9180-90748e71f361" Name="ApplicationArchitectures" Group="System">
	<Description>Архитектура процессора (разрядность) для приложений, запускаемых пользователем. Настройка задаётся администратором в карточке сотрудника.</Description>
	<SchemePhysicalColumn ID="43f5ccb9-e6a2-4343-ac35-022f7b9b4971" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="35e159bf-cdbf-45f2-a479-38c8c228565e" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="1b7ddade-d9b3-4eda-bdea-8582a754f2a9" Name="pk_ApplicationArchitectures">
		<SchemeIndexedColumn Column="43f5ccb9-e6a2-4343-ac35-022f7b9b4971" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="43f5ccb9-e6a2-4343-ac35-022f7b9b4971">0</ID>
		<Name ID="35e159bf-cdbf-45f2-a479-38c8c228565e">$Enum_ApplicationArchitectures_Auto</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="43f5ccb9-e6a2-4343-ac35-022f7b9b4971">1</ID>
		<Name ID="35e159bf-cdbf-45f2-a479-38c8c228565e">$Enum_ApplicationArchitectures_32bit</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="43f5ccb9-e6a2-4343-ac35-022f7b9b4971">2</ID>
		<Name ID="35e159bf-cdbf-45f2-a479-38c8c228565e">$Enum_ApplicationArchitectures_64bit</Name>
	</SchemeRecord>
</SchemeTable>