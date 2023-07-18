<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4c274eda-ab9a-403f-9e5b-0b933283b5a3" Name="KrPermissionRuleAccessSettings" Group="Kr">
	<Description>Список настроек доступа для расширенных прав доступа</Description>
	<SchemePhysicalColumn ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="02a113f5-4f18-4b6c-9613-973485ab92e1" Name="pk_KrPermissionRuleAccessSettings">
		<SchemeIndexedColumn Column="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">0</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_AllowEdit</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">1</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_DisallowEdit</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">2</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_DisallowRowAdding</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">3</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_DisallowRowDeleting</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">4</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_MaskData</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c">5</ID>
		<Name ID="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58">$KrPermissions_AccessSettings_DisallowRowEdit</Name>
	</SchemeRecord>
</SchemeTable>