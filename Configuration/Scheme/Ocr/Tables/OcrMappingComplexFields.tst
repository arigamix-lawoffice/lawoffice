<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="e8135496-a897-44b9-bc24-9214646453fe" Name="OcrMappingComplexFields" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Parameters for mapping verified complex fields</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8135496-a897-00b9-2000-0214646453fe" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e8135496-a897-01b9-4000-0214646453fe" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8135496-a897-00b9-3100-0214646453fe" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4ee7e95f-2eff-4072-b2c5-00108a747125" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="f60796c1-b504-424d-93c8-2fd5b9107867" IsReferenceToOwner="true">
		<Description>Parent complex field row reference</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4ee7e95f-2eff-0072-4000-00108a747125" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="f60796c1-b504-004d-3100-0fd5b9107867">
			<Description>Parent record identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b654b6dc-746a-4352-b4f0-372f5f48b4dd" Name="Field" Type="String(Max) Not Null">
		<Description>Field for mapping</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="83442b3c-b661-4212-8af8-cbb568a9e25a" Name="Value" Type="String(Max) Null">
		<Description>Field value for mapping</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8135496-a897-00b9-5000-0214646453fe" Name="pk_OcrMappingComplexFields">
		<SchemeIndexedColumn Column="e8135496-a897-00b9-3100-0214646453fe" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8135496-a897-00b9-7000-0214646453fe" Name="idx_OcrMappingComplexFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e8135496-a897-01b9-4000-0214646453fe" />
	</SchemeIndex>
</SchemeTable>