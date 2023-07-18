<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="27d6b3b7-8347-4e3c-982c-437f6c87ab13" Name="KrForkSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="27d6b3b7-8347-003c-2000-037f6c87ab13" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="27d6b3b7-8347-013c-4000-037f6c87ab13" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="931442b3-14ee-4b88-bcfd-82f39995205f" Name="AfterEachNestedProcess" Type="String(Max) Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0b72cc87-8ed9-4aa5-ac63-102180f85bde" Name="df_KrForkSettingsVirtual_AfterEachNestedProcess" Value="" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="27d6b3b7-8347-003c-5000-037f6c87ab13" Name="pk_KrForkSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="27d6b3b7-8347-013c-4000-037f6c87ab13" />
	</SchemePrimaryKey>
</SchemeTable>