<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="bcc286d4-9d77-4750-8084-15417b966528" Name="LicenseTypes" Group="System">
	<Description>Типы лицензий для сессий.</Description>
	<SchemePhysicalColumn ID="7b3eaef7-d50b-4240-9fb2-7e9397088add" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор типа лицензии.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="73ac9fe6-6b29-4b94-b6cb-9be34a4b2824" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое название для типа лицензии.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="95b15da7-8245-47cc-97d7-e665beae4e1f" Name="pk_LicenseTypes">
		<SchemeIndexedColumn Column="7b3eaef7-d50b-4240-9fb2-7e9397088add" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="7b3eaef7-d50b-4240-9fb2-7e9397088add">0</ID>
		<Name ID="73ac9fe6-6b29-4b94-b6cb-9be34a4b2824">$Enum_SessionTypes_Unspecified</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="7b3eaef7-d50b-4240-9fb2-7e9397088add">1</ID>
		<Name ID="73ac9fe6-6b29-4b94-b6cb-9be34a4b2824">$Enum_SessionTypes_Concurrent</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="7b3eaef7-d50b-4240-9fb2-7e9397088add">2</ID>
		<Name ID="73ac9fe6-6b29-4b94-b6cb-9be34a4b2824">$Enum_SessionTypes_Personal</Name>
	</SchemeRecord>
</SchemeTable>