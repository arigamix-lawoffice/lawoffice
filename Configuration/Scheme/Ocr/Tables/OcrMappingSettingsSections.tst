<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="3a7dbf8d-2f25-4b98-a406-2582bfeee594" Name="OcrMappingSettingsSections" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Section settings by card type for mapping fields during verification</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a7dbf8d-2f25-0098-2000-0582bfeee594" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3a7dbf8d-2f25-0198-4000-0582bfeee594" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a7dbf8d-2f25-0098-3100-0582bfeee594" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b27f4f9a-5987-4426-b8ff-61e4f0424975" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="6f34e6c5-93e9-49ed-ad42-63d8a001ded7" IsReferenceToOwner="true">
		<Description>Parent card type row reference</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b27f4f9a-5987-0026-4000-01e4f0424975" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="6f34e6c5-93e9-00ed-3100-03d8a001ded7">
			<Description>Parent card type row identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="81e32ca7-4522-4c35-bc8a-f53d652908dc" Name="Section" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Section for mapping</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="81e32ca7-4522-0035-4000-053d652908dc" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Section identifier</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="b1667e41-b7cf-4469-b06f-f5901ec1d6ea" Name="SectionName" Type="String(Max) Not Null">
			<Description>Section name</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a7dbf8d-2f25-0098-5000-0582bfeee594" Name="pk_OcrMappingSettingsSections">
		<SchemeIndexedColumn Column="3a7dbf8d-2f25-0098-3100-0582bfeee594" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a7dbf8d-2f25-0098-7000-0582bfeee594" Name="idx_OcrMappingSettingsSections_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3a7dbf8d-2f25-0198-4000-0582bfeee594" />
	</SchemeIndex>
</SchemeTable>