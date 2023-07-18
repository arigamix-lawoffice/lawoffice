<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="2fb28ad4-1b86-4d22-bc15-2a4943a0bb7f" Name="OcrOperations" Group="Ocr" InstanceType="Cards" ContentType="Entries">
	<Description>Information on text recognition operations</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2fb28ad4-1b86-0022-2000-0a4943a0bb7f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2fb28ad4-1b86-0122-4000-0a4943a0bb7f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="28e95de1-b7d9-4016-814b-5e88109db849" Name="Card" Type="Reference(Abstract) Not Null">
		<Description>Source card</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="28e95de1-b7d9-0016-4000-0e88109db849" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="db5daba3-4324-4b0c-afa4-e1f1847239f0" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Source card type</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="db5daba3-4324-000c-4000-01f1847239f0" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>Card type identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="001a5537-a0fb-4c16-b073-a86b7331119b" Name="CardTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22">
			<Description>Card type name</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a99ae233-fdc9-45f1-89ab-bd7178954564" Name="File" Type="Reference(Typified) Not Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c">
		<Description>Source card file for recognition</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a99ae233-fdc9-00f1-4000-0d7178954564" Name="FileID" Type="Guid Not Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c">
			<Description>File identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="d3617f42-161a-4920-bb02-8e646fb823d6" Name="FileName" Type="String(256) Not Null" ReferencedColumn="5fa1d976-21b8-4df5-b52e-f7beadf93e9d">
			<Description>File name</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="6e15e779-6918-44ee-9d41-d9de4251d895" Name="FileHasText" Type="Boolean Null">
			<Description>Sign of the presence of a text layer in the file</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1bd457a0-8c19-4c2d-aa46-d69b8d374175" Name="FileType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Source card file type</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1bd457a0-8c19-002d-4000-069b8d374175" Name="FileTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>File type identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="5ab177c0-fda3-4fe8-87c8-25d26d7bb554" Name="FileTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22">
			<Description>File type name</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="31045691-b6ae-417d-a959-68c95b9ccaa7" Name="Version" Type="Reference(Typified) Not Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8">
		<Description>Source card file version</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31045691-b6ae-007d-4000-08c95b9ccaa7" Name="VersionRowID" Type="Guid Not Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8">
			<Description>File version identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2fb28ad4-1b86-0022-5000-0a4943a0bb7f" Name="pk_OcrOperations" IsClustered="true">
		<SchemeIndexedColumn Column="2fb28ad4-1b86-0122-4000-0a4943a0bb7f" />
	</SchemePrimaryKey>
</SchemeTable>