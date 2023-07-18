<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="afee6930-bb0c-48b3-b0ac-3a1447a31d12" Name="OcrRequestsLanguages" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Information by languages ​​used in text recognition requests</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="afee6930-bb0c-00b3-2000-0a1447a31d12" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="afee6930-bb0c-01b3-4000-0a1447a31d12" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="afee6930-bb0c-00b3-3100-0a1447a31d12" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e69e117c-dfc0-4c53-a2e9-ac54891d1072" Name="Language" Type="Reference(Typified) Not Null" ReferencedTable="a5b1b1cf-ad8c-4459-a880-a5ff9f435398">
		<Description>Language reference</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e69e117c-dfc0-0053-4000-0c54891d1072" Name="LanguageID" Type="Int32 Not Null" ReferencedColumn="729faf35-c1d7-4e03-9c60-8773284c30a8">
			<Description>Language identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="f1c64fda-648d-4d57-9d1d-8280af402fce" Name="LanguageISO" Type="AnsiStringFixedLength(3) Null" ReferencedColumn="cb988837-dc52-43d5-94c1-380f4d5721e6">
			<Description>Language name in ISO 639-2 format</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="9ac46dfb-b066-44cb-aec6-b5033ee6f67d" Name="LanguageCaption" Type="String(16) Not Null" ReferencedColumn="bb53aba8-a233-433f-82d6-7ae0e4054955">
			<Description>Language caption</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2225f776-dc06-438b-bb26-188fe9b3924f" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="d64806e9-ef31-4133-806b-670b178cc5bc" IsReferenceToOwner="true">
		<Description>Request reference</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2225f776-dc06-008b-4000-088fe9b3924f" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="d64806e9-ef31-0033-3100-070b178cc5bc">
			<Description>Request identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="afee6930-bb0c-00b3-5000-0a1447a31d12" Name="pk_OcrRequestsLanguages">
		<SchemeIndexedColumn Column="afee6930-bb0c-00b3-3100-0a1447a31d12" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="afee6930-bb0c-00b3-7000-0a1447a31d12" Name="idx_OcrRequestsLanguages_ID" IsClustered="true">
		<SchemeIndexedColumn Column="afee6930-bb0c-01b3-4000-0a1447a31d12" />
	</SchemeIndex>
</SchemeTable>