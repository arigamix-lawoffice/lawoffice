<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f9d10aed-ae25-42e8-b936-1b97014c4e13" Name="KrStageGroupTemplatesVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f9d10aed-ae25-00e8-2000-0b97014c4e13" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f9d10aed-ae25-01e8-4000-0b97014c4e13" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f9d10aed-ae25-00e8-3100-0b97014c4e13" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="728f9a03-47dc-4bb5-885f-b6e74c74e4b8" Name="Template" Type="Reference(Typified) Not Null" ReferencedTable="5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="728f9a03-47dc-00b5-4000-06e74c74e4b8" Name="TemplateID" Type="Guid Not Null" ReferencedColumn="5a33ac72-f6f5-015a-4000-0a94ed7bf324" />
		<SchemeReferencingColumn ID="c83fbaa4-bc22-45fc-a1ee-8be6b7e291c5" Name="TemplateName" Type="String(255) Not Null" ReferencedColumn="65776ea1-97aa-48db-a170-8cb5d4eed2bc" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f9d10aed-ae25-00e8-5000-0b97014c4e13" Name="pk_KrStageGroupTemplatesVirtual">
		<SchemeIndexedColumn Column="f9d10aed-ae25-00e8-3100-0b97014c4e13" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f9d10aed-ae25-00e8-7000-0b97014c4e13" Name="idx_KrStageGroupTemplatesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f9d10aed-ae25-01e8-4000-0b97014c4e13" />
	</SchemeIndex>
</SchemeTable>