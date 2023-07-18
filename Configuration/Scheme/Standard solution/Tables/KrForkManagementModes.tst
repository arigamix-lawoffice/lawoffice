<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="75e444ae-a785-4e30-a6e0-15020a31654d" Name="KrForkManagementModes" Group="KrStageTypes">
	<SchemePhysicalColumn ID="f7410455-fcaf-4595-9a1a-d64e6f141769" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="dcf079bd-8726-45ef-a061-8e6ba0f400bb" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="2e9ec6e6-0f85-4f60-893a-0057fe47fca1" Name="pk_KrForkManagementModes">
		<SchemeIndexedColumn Column="f7410455-fcaf-4595-9a1a-d64e6f141769" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="f7410455-fcaf-4595-9a1a-d64e6f141769">0</ID>
		<Name ID="dcf079bd-8726-45ef-a061-8e6ba0f400bb">$KrStages_ForkManagement_AddMode</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="f7410455-fcaf-4595-9a1a-d64e6f141769">1</ID>
		<Name ID="dcf079bd-8726-45ef-a061-8e6ba0f400bb">$KrStages_ForkManagement_RemoveMode</Name>
	</SchemeRecord>
</SchemeTable>