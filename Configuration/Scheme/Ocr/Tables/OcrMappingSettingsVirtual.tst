<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="0c83bfa8-8e9a-454d-b805-6de0c679f4ec" Name="OcrMappingSettingsVirtual" Group="Ocr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Virtual table for mapping settings</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0c83bfa8-8e9a-004d-2000-0de0c679f4ec" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0c83bfa8-8e9a-014d-4000-0de0c679f4ec" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7a99ba5b-3938-49f8-9adc-f329d58ab58c" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266">
		<Description>Card type for mapping</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7a99ba5b-3938-00f8-4000-0329d58ab58c" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266">
			<Description>Card type identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="50288df9-8dcd-44ce-be72-4001b7e0a497" Name="Section" Type="Reference(Abstract) Not Null">
		<Description>Section for mapping</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="50288df9-8dcd-00ce-4000-0001b7e0a497" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Section identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0c83bfa8-8e9a-004d-5000-0de0c679f4ec" Name="pk_OcrMappingSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="0c83bfa8-8e9a-014d-4000-0de0c679f4ec" />
	</SchemePrimaryKey>
</SchemeTable>