<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="6f34e6c5-93e9-49ed-ad42-63d8a001ded7" Name="OcrMappingSettingsTypes" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Card type settings for mapping fields during verification</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6f34e6c5-93e9-00ed-2000-03d8a001ded7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6f34e6c5-93e9-01ed-4000-03d8a001ded7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6f34e6c5-93e9-00ed-3100-03d8a001ded7" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="2ed54d3c-41cc-42fd-bb64-1143255f3de8" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266">
		<Description>Card type for mapping</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2ed54d3c-41cc-00fd-4000-0143255f3de8" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266">
			<Description>Card type identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="6c61b5ca-7284-4ea1-8b27-6422ed2a3671" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1">
			<Description>Card type caption</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="b63690c0-9bab-4ceb-9545-63403ecce308" Name="TypeName" Type="String(128) Not Null">
			<Description>Card type name</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6f34e6c5-93e9-00ed-5000-03d8a001ded7" Name="pk_OcrMappingSettingsTypes">
		<SchemeIndexedColumn Column="6f34e6c5-93e9-00ed-3100-03d8a001ded7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6f34e6c5-93e9-00ed-7000-03d8a001ded7" Name="idx_OcrMappingSettingsTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6f34e6c5-93e9-01ed-4000-03d8a001ded7" />
	</SchemeIndex>
</SchemeTable>