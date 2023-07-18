<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ebf6257e-c0c6-4f84-b913-7a66fc196418" Name="KrCreateCardStageTypeModes" Group="KrStageTypes">
	<SchemePhysicalColumn Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="81204cd6-3332-4448-bceb-47e51c9049e9" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1" Name="Name" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="67ccb46e-feda-4350-8f06-702358e950a1" Name="Order" Type="Int16 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d213fbcc-54a5-47f6-85ed-039efc588ec5" Name="df_KrCreateCardStageTypeModes_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="e54ae92b-c69f-4ebd-9278-8e1b6d0f8011" Name="pk_KrCreateCardStageTypeModes">
		<SchemeIndexedColumn Column="81204cd6-3332-4448-bceb-47e51c9049e9" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="81204cd6-3332-4448-bceb-47e51c9049e9">0</ID>
		<Name ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1">$KrStages_CreateCard_OpenMode</Name>
		<Order ID="67ccb46e-feda-4350-8f06-702358e950a1">0</Order>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="81204cd6-3332-4448-bceb-47e51c9049e9">1</ID>
		<Name ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1">$KrStages_CreateCard_StoreAndOpenMode</Name>
		<Order ID="67ccb46e-feda-4350-8f06-702358e950a1">2</Order>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="81204cd6-3332-4448-bceb-47e51c9049e9">2</ID>
		<Name ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1">$KrStages_CreateCard_StartProcessMode</Name>
		<Order ID="67ccb46e-feda-4350-8f06-702358e950a1">3</Order>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="81204cd6-3332-4448-bceb-47e51c9049e9">3</ID>
		<Name ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1">$KrStages_CreateCard_StartProcessAndOpenMode</Name>
		<Order ID="67ccb46e-feda-4350-8f06-702358e950a1">4</Order>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="81204cd6-3332-4448-bceb-47e51c9049e9">4</ID>
		<Name ID="b73e6dca-ef1a-45ff-aef0-03021bbbbee1">$KrStages_CreateCard_StoreMode</Name>
		<Order ID="67ccb46e-feda-4350-8f06-702358e950a1">1</Order>
	</SchemeRecord>
</SchemeTable>