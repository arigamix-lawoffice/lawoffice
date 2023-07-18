<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="01c6933a-204d-490e-a6db-fc69345c7e32" Name="KrRouteModes" Group="Kr">
	<Description>Перечисление режимов работы системы маршрутов.</Description>
	<SchemePhysicalColumn ID="287cc66f-012e-48f2-b7cf-f2d890a4997c" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="3dc935a0-3529-4960-8285-d9954bb5f9e2" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="110fc5d6-4fa8-4602-beb9-99e3b71f2c60" Name="pk_KrRouteModes">
		<SchemeIndexedColumn Column="287cc66f-012e-48f2-b7cf-f2d890a4997c" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="287cc66f-012e-48f2-b7cf-f2d890a4997c">0</ID>
		<Name ID="3dc935a0-3529-4960-8285-d9954bb5f9e2">$KrRoute_Mode_RoutesNotUsed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="287cc66f-012e-48f2-b7cf-f2d890a4997c">1</ID>
		<Name ID="3dc935a0-3529-4960-8285-d9954bb5f9e2">$KrRoute_Mode_RoutesUsed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="287cc66f-012e-48f2-b7cf-f2d890a4997c">2</ID>
		<Name ID="3dc935a0-3529-4960-8285-d9954bb5f9e2">$KrRoute_Mode_RoutesUsedProcessActive</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="287cc66f-012e-48f2-b7cf-f2d890a4997c">3</ID>
		<Name ID="3dc935a0-3529-4960-8285-d9954bb5f9e2">$KrRoute_Mode_RoutesUsedProcessInactive</Name>
	</SchemeRecord>
</SchemeTable>