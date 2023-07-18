<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2baf9cf1-a8d5-4e82-bccd-769d7c70e10a" Name="KrPermissionsFileCheckRules" Group="Kr">
	<Description>Правила проверки файлов в расширенных настройках доступа к файлам.</Description>
	<SchemePhysicalColumn ID="ed8d4653-0c62-477b-bc81-ada61ddbc5c5" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="9fa664e7-5718-4a33-966e-593915e5aac2" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="92b8f0e6-43b2-498f-8123-d69152ea0b07" Name="pk_KrPermissionsFileCheckRules">
		<SchemeIndexedColumn Column="ed8d4653-0c62-477b-bc81-ada61ddbc5c5" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="ed8d4653-0c62-477b-bc81-ada61ddbc5c5">0</ID>
		<Name ID="9fa664e7-5718-4a33-966e-593915e5aac2">$KrPermissions_FileCheckRules_AllFiles</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ed8d4653-0c62-477b-bc81-ada61ddbc5c5">1</ID>
		<Name ID="9fa664e7-5718-4a33-966e-593915e5aac2">$KrPermissions_FileCheckRules_FilesOfOtherUsers</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ed8d4653-0c62-477b-bc81-ada61ddbc5c5">2</ID>
		<Name ID="9fa664e7-5718-4a33-966e-593915e5aac2">$KrPermissions_FileCheckRules_OwnFiles</Name>
	</SchemeRecord>
</SchemeTable>