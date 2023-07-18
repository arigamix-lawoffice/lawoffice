<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a8a8e7df-0237-4fda-824f-030df82a1030" Name="KrSecondaryProcessModes" Group="Kr">
	<SchemePhysicalColumn ID="d415b7f3-4c09-48b6-b4ef-d455a9340483" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="c1c9be67-b394-4aa1-bce9-0bfaa59b6c56" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="aeff3964-70da-48f3-9e32-615554f63271" Name="pk_KrSecondaryProcessModes">
		<SchemeIndexedColumn Column="d415b7f3-4c09-48b6-b4ef-d455a9340483" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="d415b7f3-4c09-48b6-b4ef-d455a9340483">0</ID>
		<Name ID="c1c9be67-b394-4aa1-bce9-0bfaa59b6c56">$KrSecondaryProcess_Mode_PureProcess</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="d415b7f3-4c09-48b6-b4ef-d455a9340483">1</ID>
		<Name ID="c1c9be67-b394-4aa1-bce9-0bfaa59b6c56">$KrSecondaryProcess_Mode_Button</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="d415b7f3-4c09-48b6-b4ef-d455a9340483">2</ID>
		<Name ID="c1c9be67-b394-4aa1-bce9-0bfaa59b6c56">$KrSecondaryProcess_Mode_Action</Name>
	</SchemeRecord>
</SchemeTable>