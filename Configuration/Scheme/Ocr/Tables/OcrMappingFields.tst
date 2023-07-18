<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="f60796c1-b504-424d-93c8-2fd5b9107867" Name="OcrMappingFields" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Parameters for mapping verified fields</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f60796c1-b504-004d-2000-0fd5b9107867" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f60796c1-b504-014d-4000-0fd5b9107867" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f60796c1-b504-004d-3100-0fd5b9107867" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7cfbb5ba-d8a5-421b-a550-e076a9cec7e3" Name="Section" Type="String(Max) Not Null">
		<Description>Section for mapping</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b22cdd9e-b005-407d-8720-215419b40006" Name="Field" Type="String(Max) Not Null">
		<Description>Field for mapping</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3b6e9d58-f46f-41d4-a3dd-e82275e7bbcf" Name="Displayed" Type="String(Max) Null">
		<Description>Field displayed value</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b65d811a-1ddc-446c-98d2-8058967200e4" Name="Value" Type="String(Max) Null">
		<Description>Field value for mapping</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f60796c1-b504-004d-5000-0fd5b9107867" Name="pk_OcrMappingFields">
		<SchemeIndexedColumn Column="f60796c1-b504-004d-3100-0fd5b9107867" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f60796c1-b504-004d-7000-0fd5b9107867" Name="idx_OcrMappingFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f60796c1-b504-014d-4000-0fd5b9107867" />
	</SchemeIndex>
</SchemeTable>