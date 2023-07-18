<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="46a9cc0e-9492-48ce-8ac8-bc04551a041a" Name="OcrMappingSettingsFields" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Field settings by section for mapping during verification</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="46a9cc0e-9492-00ce-2000-0c04551a041a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="46a9cc0e-9492-01ce-4000-0c04551a041a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="46a9cc0e-9492-00ce-3100-0c04551a041a" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="845b61bd-078b-4163-b976-8588ce3a98fb" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="3a7dbf8d-2f25-4b98-a406-2582bfeee594" IsReferenceToOwner="true">
		<Description>Parent section row reference</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="845b61bd-078b-0063-4000-0588ce3a98fb" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="3a7dbf8d-2f25-0098-3100-0582bfeee594">
			<Description>Parent section row identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="02ca4c9f-de10-4445-be4b-61ddcbd90927" Name="Field" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Field for mapping</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="02ca4c9f-de10-0045-4000-01ddcbd90927" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Field identifier</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="c2fff4c2-a2dd-49af-af0e-3a0783cb7eed" Name="FieldName" Type="String(Max) Not Null">
			<Description>Field name</Description>
		</SchemePhysicalColumn>
		<SchemePhysicalColumn ID="7204cb00-8223-45ab-b080-201259e55fd9" Name="FieldComplexColumnIndex" Type="Int32 Not Null">
			<Description>Column index for complex field by section</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="993a7207-4230-43c2-a37f-0d42b1663c9b" Name="Caption" Type="String(256) Not Null">
		<Description>Field caption</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0ad4ce74-47e2-4e65-a380-699b2b16388b" Name="ViewRefSection" Type="String(128) Null">
		<Description>View section reference</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0c3f6622-71e2-4935-87f3-01de958f25b0" Name="ViewParameter" Type="String(64) Null">
		<Description>View search parameter</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5bbaae25-f8a3-4ad9-8724-d1b708e5fd24" Name="ViewReferencePrefix" Type="String(32) Null">
		<Description>View prefix reference</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="54ef34b0-2ca6-4987-901e-5c746f7916aa" Name="ViewAlias" Type="String(128) Null">
		<Description>View alias</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="46a9cc0e-9492-00ce-5000-0c04551a041a" Name="pk_OcrMappingSettingsFields">
		<SchemeIndexedColumn Column="46a9cc0e-9492-00ce-3100-0c04551a041a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="46a9cc0e-9492-00ce-7000-0c04551a041a" Name="idx_OcrMappingSettingsFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="46a9cc0e-9492-01ce-4000-0c04551a041a" />
	</SchemeIndex>
</SchemeTable>