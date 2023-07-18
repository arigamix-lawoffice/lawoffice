<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8b4cd042-334b-4aee-a623-7d8942aa6897" Name="DeviceTypes" Group="System">
	<Description>Типы устройств, с которых пользователь использует приложения Tessa.</Description>
	<SchemePhysicalColumn ID="97971f14-45fc-4fd7-9623-17b38b9853f1" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор устройства.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="881dfb9a-dc17-4209-8913-3060fc0e6dde" Name="Name" Type="String(256) Not Null">
		<Description>Имя устройства.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="aa2b363b-f021-4c54-b3f2-53f2a3fec6cf" Name="pk_DeviceTypes">
		<SchemeIndexedColumn Column="97971f14-45fc-4fd7-9623-17b38b9853f1" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="97971f14-45fc-4fd7-9623-17b38b9853f1">0</ID>
		<Name ID="881dfb9a-dc17-4209-8913-3060fc0e6dde">$Enum_DeviceTypes_Other</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="97971f14-45fc-4fd7-9623-17b38b9853f1">1</ID>
		<Name ID="881dfb9a-dc17-4209-8913-3060fc0e6dde">$Enum_DeviceTypes_Desktop</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="97971f14-45fc-4fd7-9623-17b38b9853f1">2</ID>
		<Name ID="881dfb9a-dc17-4209-8913-3060fc0e6dde">$Enum_DeviceTypes_Phone</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="97971f14-45fc-4fd7-9623-17b38b9853f1">3</ID>
		<Name ID="881dfb9a-dc17-4209-8913-3060fc0e6dde">$Enum_DeviceTypes_Tablet</Name>
	</SchemeRecord>
</SchemeTable>