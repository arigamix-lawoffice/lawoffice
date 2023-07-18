<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="e432240e-e30a-4568-aa91-bb9a9e55ea61" Name="OcrSettingsPatterns" Group="Ocr" InstanceType="Cards" ContentType="Collections">
	<Description>Templates for fields verification</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e432240e-e30a-0068-2000-0b9a9e55ea61" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Card identifier</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e432240e-e30a-0168-4000-0b9a9e55ea61" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747">
			<Description>Card identifier</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e432240e-e30a-0068-3100-0b9a9e55ea61" Name="RowID" Type="Guid Not Null">
		<Description>Unique record identifier</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e676ad99-16c6-48d5-85e0-29a5ac785864" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="ff8635f6-7856-45e1-82cf-6f00315ce790">
		<Description>Template type</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e676ad99-16c6-00d5-4000-09a5ac785864" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="57e6c61d-fbc1-4452-bbc4-fa483bb59120">
			<Description>Template type identifier</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="b073b5d6-91c2-41b9-b4fc-6ddb0fd8d5ff" Name="TypeName" Type="String(16) Not Null" ReferencedColumn="499c95de-0a1d-4c3d-b9c1-d7342205dff3">
			<Description>Template type name</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="912bbaae-6367-4e4b-ba6b-4ae6627f689e" Name="Value" Type="String(Max) Not Null">
		<Description>Template value as a regular expression</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="be3bf9a9-04be-4957-af61-27709383c8f1" Name="Order" Type="Int32 Not Null">
		<Description>Table row order</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7e3bb487-0b7b-4f51-b094-69991d2fce7a" Name="df_OcrSettingsPatterns_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3a2e8007-d801-4fbf-9829-467747802dd4" Name="Description" Type="String(Max) Null">
		<Description>Description of the template with possible examples</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e432240e-e30a-0068-5000-0b9a9e55ea61" Name="pk_OcrSettingsPatterns">
		<SchemeIndexedColumn Column="e432240e-e30a-0068-3100-0b9a9e55ea61" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e432240e-e30a-0068-7000-0b9a9e55ea61" Name="idx_OcrSettingsPatterns_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e432240e-e30a-0168-4000-0b9a9e55ea61" />
	</SchemeIndex>
</SchemeTable>