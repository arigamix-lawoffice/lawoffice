<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0" Name="FileTemplates" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="98e0c3a9-0b9a-00ec-2000-0a077f6ff5f0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="98e0c3a9-0b9a-01ec-4000-0a077f6ff5f0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="db93e6bd-9e6a-4232-bf8c-bfe652e5573c" Name="Name" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="56f50c10-166e-4828-9add-2b3dc4c44981" Name="GroupName" Type="String(256) Null" />
	<SchemePhysicalColumn ID="a37dc331-dd29-46d5-bd68-ed00b4473ae4" Name="PlaceholdersInfo" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="bd279b0b-e071-4af7-bc46-74cafddf6af8" Name="AliasMetadata" Type="String(Max) Null">
		<Description>Метаинформация по алиасам плейсхолдеров.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="52ea7a14-1cef-474d-b39c-7ad3dbd9cf1a" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="54994b70-b619-4280-b9ff-31c20453a462">
		<Description>Тип шаблона</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="52ea7a14-1cef-004d-4000-0ad3dbd9cf1a" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="7e8ccde0-a31e-4a70-b7fa-1da3b9003bd5">
			<SchemeDefaultConstraint IsPermanent="true" ID="bbbad08e-fc14-43e6-9d30-3efc8e1fb733" Name="df_FileTemplates_TypeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="3fc00e48-dc30-41b1-9dde-17b2d4487ba4" Name="TypeName" Type="String(256) Not Null" ReferencedColumn="5874eefd-1cb3-4db9-8504-a7084d5b1824">
			<SchemeDefaultConstraint IsPermanent="true" ID="ebb2ba1e-309c-4d63-b7ec-35e620c1a5a2" Name="df_FileTemplates_TypeName" Value="$FileTemplateType_Card" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e707f0fa-6e17-455e-bd03-5b35665018d5" Name="BeforeDocumentReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="9d156ccb-8111-4f86-913b-a6534b491485" Name="BeforeTableReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c24fb6e2-45a9-4cbd-adf8-f9adad10702c" Name="BeforeRowReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="30e808d1-e922-494e-a156-4ad20647f15e" Name="BeforePlaceholderReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="a94e1885-3ba0-4d55-9889-828e223c6be9" Name="AfterPlaceholderReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="b7102c06-6b70-4a81-9509-30bd23812a86" Name="AfterRowReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="374ab86e-72d8-4cc2-98ae-8a45f8e211c7" Name="AfterTableReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="68378cb0-bf8c-4bd8-bbbb-532df07146b4" Name="AfterDocumentReplace" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="cd092d8c-7fdc-4c39-999c-1bae9bb51145" Name="System" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="06ccf05e-36f3-4d24-82e0-c79f48072057" Name="df_FileTemplates_System" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="29a6d369-0719-4e9c-a08c-c4941a566458" Name="ConvertToPDF" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2f3f057b-a890-4c58-b479-151944010d46" Name="df_FileTemplates_ConvertToPDF" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="98e0c3a9-0b9a-00ec-5000-0a077f6ff5f0" Name="pk_FileTemplates" IsClustered="true">
		<SchemeIndexedColumn Column="98e0c3a9-0b9a-01ec-4000-0a077f6ff5f0" />
	</SchemePrimaryKey>
	<SchemeIndex ID="d5314bd7-0791-4307-98e8-118470de8dfb" Name="ndx_FileTemplates_TypeIDSystem">
		<SchemeIndexedColumn Column="52ea7a14-1cef-004d-4000-0ad3dbd9cf1a" />
		<SchemeIndexedColumn Column="cd092d8c-7fdc-4c39-999c-1bae9bb51145" />
	</SchemeIndex>
</SchemeTable>