<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="ff8635f6-7856-45e1-82cf-6f00315ce790" Name="OcrPatternTypes" Group="Ocr">
	<Description>Template types for field verification</Description>
	<SchemePhysicalColumn ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120" Name="ID" Type="Int32 Not Null">
		<Description>Template type identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3" Name="Name" Type="String(16) Not Null">
		<Description>Template type name</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="53f6d4d3-d474-46bd-8599-530e35e35ed8" Name="pk_OcrPatternTypes">
		<SchemeIndexedColumn Column="57e6c61d-fbc1-4452-bbc4-fa483bb59120" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="a0ab72d7-cd8d-4f5c-b411-8cbbae192dc8" Name="ndx_OcrPatternTypes_Name">
		<SchemeIndexedColumn Column="499c95de-0a1d-4c3d-b9c1-d7342205dff3" />
	</SchemeUniqueKey>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">0</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Boolean</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">1</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Integer</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">2</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Double</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">3</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">DateTime</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">4</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Date</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">5</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Time</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="57e6c61d-fbc1-4452-bbc4-fa483bb59120">6</ID>
		<Name ID="499c95de-0a1d-4c3d-b9c1-d7342205dff3">Interval</Name>
	</SchemeRecord>
</SchemeTable>