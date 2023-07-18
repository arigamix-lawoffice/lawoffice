<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="62c1a795-1688-48a1-b0af-d77032c90bab" Name="SessionServiceTypes" Group="System">
	<Description>Типы сессий, которые определяются типом веб-сервиса: для desktop- или для web-клиентов, или веб-сервис отсутствует (прямое взаимодействие с БД).</Description>
	<SchemePhysicalColumn ID="bca8f77c-494c-4ba7-b920-32b46d20172e" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="87a4d12a-5f37-4185-b5b9-a61089d64376" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="934e9161-b373-43b9-b9f2-6438b5d9613e" Name="pk_SessionServiceTypes">
		<SchemeIndexedColumn Column="bca8f77c-494c-4ba7-b920-32b46d20172e" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="bca8f77c-494c-4ba7-b920-32b46d20172e">0</ID>
		<Name ID="87a4d12a-5f37-4185-b5b9-a61089d64376">$Enum_SessionServiceTypes_Unknown</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="bca8f77c-494c-4ba7-b920-32b46d20172e">1</ID>
		<Name ID="87a4d12a-5f37-4185-b5b9-a61089d64376">$Enum_SessionServiceTypes_DesktopClient</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="bca8f77c-494c-4ba7-b920-32b46d20172e">2</ID>
		<Name ID="87a4d12a-5f37-4185-b5b9-a61089d64376">$Enum_SessionServiceTypes_WebClient</Name>
	</SchemeRecord>
</SchemeTable>