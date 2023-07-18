<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="18ad7847-b0f7-4d74-bc04-d96cbf18eecd" Name="KrPermissionsControlTypes" Group="Kr">
	<SchemePhysicalColumn ID="42344e54-e11e-46e3-8965-b4cac26b5f23" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="a491d554-6ec8-4063-898c-70b983301c6d" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="f37a0a6a-3187-4856-80ee-67d8d1e02b8a" Name="pk_KrPermissionsControlTypes">
		<SchemeIndexedColumn Column="42344e54-e11e-46e3-8965-b4cac26b5f23" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="42344e54-e11e-46e3-8965-b4cac26b5f23">0</ID>
		<Name ID="a491d554-6ec8-4063-898c-70b983301c6d">$KrPermissions_ControlType_Tab</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="42344e54-e11e-46e3-8965-b4cac26b5f23">1</ID>
		<Name ID="a491d554-6ec8-4063-898c-70b983301c6d">$KrPermissions_ControlType_Block</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="42344e54-e11e-46e3-8965-b4cac26b5f23">2</ID>
		<Name ID="a491d554-6ec8-4063-898c-70b983301c6d">$KrPermissions_ControlType_Control</Name>
	</SchemeRecord>
</SchemeTable>