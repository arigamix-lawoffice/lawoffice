<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="f704ecbb-e3b2-4a79-8ca8-222417f3dc4d" Name="OcrResultsVirtual" Group="Ocr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Information with text recognition results</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f704ecbb-e3b2-0079-2000-022417f3dc4d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f704ecbb-e3b2-0179-4000-022417f3dc4d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="eec0c5a7-cd86-47ee-b97e-ba9eaac97240" Name="Text" Type="String(Max) Null">
		<Description>Recognized text</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2265c6f6-6bcb-40d0-ae6e-d64792b85ff6" Name="Confidence" Type="Decimal(4, 1) Null">
		<Description>Threshold confidence factor for recognized text</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e333f7b7-4007-4561-950a-e764a06a8034" Name="Language" Type="String(Max) Null">
		<Description>Language for recognized text</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f704ecbb-e3b2-0079-5000-022417f3dc4d" Name="pk_OcrResultsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="f704ecbb-e3b2-0179-4000-022417f3dc4d" />
	</SchemePrimaryKey>
</SchemeTable>