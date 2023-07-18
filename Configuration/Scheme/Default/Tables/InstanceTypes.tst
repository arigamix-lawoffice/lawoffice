<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="2a567cee-1489-4a90-acf5-4f6d2c5bd67e" Name="InstanceTypes" Group="System">
	<Description>Instance types.</Description>
	<SchemePhysicalColumn ID="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="b0f00c90-f975-417c-98bc-d4132cb06b72" Name="Name" Type="String(8) Not Null">
		<Description>Unique name for instance type.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="16dba900-6843-461c-aa37-601d2b4e3250" Name="pk_InstanceTypes">
		<SchemeIndexedColumn Column="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a">0</ID>
		<Name ID="b0f00c90-f975-417c-98bc-d4132cb06b72">Card</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a">1</ID>
		<Name ID="b0f00c90-f975-417c-98bc-d4132cb06b72">File</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a">2</ID>
		<Name ID="b0f00c90-f975-417c-98bc-d4132cb06b72">Task</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e1a61f56-f8ba-4ffd-ab6f-48c20e5f018a">3</ID>
		<Name ID="b0f00c90-f975-417c-98bc-d4132cb06b72">Dialog</Name>
	</SchemeRecord>
</SchemeTable>