<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9247ed2e-109d-4543-b888-1fe9da9479aa" Name="KrPermissionsFileEditAccessSettings" Group="Kr">
	<Description>Настройки доступа на изменение файлов в расширенных настройках доступа к файлам.</Description>
	<SchemePhysicalColumn ID="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="867b9598-88c4-4fb5-b6f7-c9672320f9bd" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="c83e0ad2-2b62-43de-affe-6c48e1b43bb5" Name="pk_KrPermissionsFileEditAccessSettings">
		<SchemeIndexedColumn Column="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb">0</ID>
		<Name ID="867b9598-88c4-4fb5-b6f7-c9672320f9bd">$KrPermissions_FileEditAccessSettings_Disallowed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0fdf8f67-3ebf-4d07-a559-5a02f370f0eb">1</ID>
		<Name ID="867b9598-88c4-4fb5-b6f7-c9672320f9bd">$KrPermissions_FileEditAccessSettings_Allowed</Name>
	</SchemeRecord>
</SchemeTable>