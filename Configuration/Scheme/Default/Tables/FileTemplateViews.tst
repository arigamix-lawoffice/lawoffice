<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="ebd081b9-aaf9-4bab-be51-602803756e8d" Name="FileTemplateViews" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ebd081b9-aaf9-00ab-2000-002803756e8d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ebd081b9-aaf9-01ab-4000-002803756e8d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ebd081b9-aaf9-00ab-3100-002803756e8d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="af49048c-e7ba-4d2a-b048-05847a143475" Name="View" Type="Reference(Typified) Null" ReferencedTable="3519b63c-eea0-48f4-b70a-544e58ece5fc" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="af49048c-e7ba-002a-4000-05847a143475" Name="ViewID" Type="Guid Null" ReferencedColumn="8e4c45ad-ca6f-4f0f-be25-9a9e37a4cfd6" />
		<SchemeReferencingColumn ID="d1dc2f75-7128-4b8f-bd80-d54caf84cd09" Name="ViewAlias" Type="String(128) Null" ReferencedColumn="827d19f5-a1aa-4e74-92c0-8bb9dcbceb7d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ebd081b9-aaf9-00ab-5000-002803756e8d" Name="pk_FileTemplateViews">
		<SchemeIndexedColumn Column="ebd081b9-aaf9-00ab-3100-002803756e8d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ebd081b9-aaf9-00ab-7000-002803756e8d" Name="idx_FileTemplateViews_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ebd081b9-aaf9-01ab-4000-002803756e8d" />
	</SchemeIndex>
</SchemeTable>