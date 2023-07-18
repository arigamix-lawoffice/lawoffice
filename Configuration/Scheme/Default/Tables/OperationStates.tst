<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e726339c-e2fc-4d7c-a9b4-011577ff2106" Name="OperationStates" Group="System">
	<Description>Состояние операции.</Description>
	<SchemePhysicalColumn ID="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор состояния.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3c2e9608-7121-4117-b448-b6a3ada082b7" Name="Name" Type="String(128) Not Null">
		<Description>Имя состояния.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="f8039354-f67a-4411-9687-d5c278d0a2cc" Name="pk_OperationStates">
		<SchemeIndexedColumn Column="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf">0</ID>
		<Name ID="3c2e9608-7121-4117-b448-b6a3ada082b7">$Enum_OperationStates_Created</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf">1</ID>
		<Name ID="3c2e9608-7121-4117-b448-b6a3ada082b7">$Enum_OperationStates_InProgress</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf">2</ID>
		<Name ID="3c2e9608-7121-4117-b448-b6a3ada082b7">$Enum_OperationStates_Completed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3f1b4bb9-16db-4c3a-b735-b41c3fd51bdf">3</ID>
		<Name ID="3c2e9608-7121-4117-b448-b6a3ada082b7">$Enum_OperationStates_Postponed</Name>
	</SchemeRecord>
</SchemeTable>