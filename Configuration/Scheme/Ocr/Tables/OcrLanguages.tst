<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="a6d84d24-0461-4c65-aa5d-f41ee300f081" ID="a5b1b1cf-ad8c-4459-a880-a5ff9f435398" Name="OcrLanguages" Group="Ocr">
	<Description>Supported languages ​​for text recognition</Description>
	<SchemePhysicalColumn ID="729faf35-c1d7-4e03-9c60-8773284c30a8" Name="ID" Type="Int32 Not Null">
		<Description>Record identifier</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cb988837-dc52-43d5-94c1-380f4d5721e6" Name="ISO" Type="AnsiStringFixedLength(3) Null">
		<Description>Language name in ISO 639-2 format</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e988e73e-24be-4d58-b81f-0be9bf9cf291" Name="Code" Type="AnsiStringFixedLength(2) Null">
		<Description>Language code</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bb53aba8-a233-433f-82d6-7ae0e4054955" Name="Caption" Type="String(16) Not Null">
		<Description>Language caption</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="2e523f30-f783-4e28-a535-63e08421bcd2" Name="pk_OcrLanguages">
		<SchemeIndexedColumn Column="729faf35-c1d7-4e03-9c60-8773284c30a8" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="50e74dbe-ee2a-4624-b6d5-7ce4eb1675d8" Name="ndx_OcrLanguages_ISO">
		<SchemeIndexedColumn Column="cb988837-dc52-43d5-94c1-380f4d5721e6" />
	</SchemeUniqueKey>
	<SchemeIndex ID="9ec9b537-af24-4692-8431-37b317f736a3" Name="ndx_OcrLanguages_Code" IsUnique="true">
		<SchemeIndexedColumn Column="e988e73e-24be-4d58-b81f-0be9bf9cf291" />
		<SchemeIncludedColumn Column="cb988837-dc52-43d5-94c1-380f4d5721e6" />
		<SchemeIncludedColumn Column="bb53aba8-a233-433f-82d6-7ae0e4054955" />
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="729faf35-c1d7-4e03-9c60-8773284c30a8">2</ID>
		<ISO ID="cb988837-dc52-43d5-94c1-380f4d5721e6">eng</ISO>
		<Code ID="e988e73e-24be-4d58-b81f-0be9bf9cf291">en</Code>
		<Caption ID="bb53aba8-a233-433f-82d6-7ae0e4054955">English</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="729faf35-c1d7-4e03-9c60-8773284c30a8">3</ID>
		<ISO ID="cb988837-dc52-43d5-94c1-380f4d5721e6">slv</ISO>
		<Code ID="e988e73e-24be-4d58-b81f-0be9bf9cf291">sl</Code>
		<Caption ID="bb53aba8-a233-433f-82d6-7ae0e4054955">Slovenian</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="729faf35-c1d7-4e03-9c60-8773284c30a8">1</ID>
		<ISO ID="cb988837-dc52-43d5-94c1-380f4d5721e6">rus</ISO>
		<Code ID="e988e73e-24be-4d58-b81f-0be9bf9cf291">ru</Code>
		<Caption ID="bb53aba8-a233-433f-82d6-7ae0e4054955">Russian</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="729faf35-c1d7-4e03-9c60-8773284c30a8">0</ID>
		<ISO ID="cb988837-dc52-43d5-94c1-380f4d5721e6" xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" />
		<Code ID="e988e73e-24be-4d58-b81f-0be9bf9cf291" xsi:nil="true" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" />
		<Caption ID="bb53aba8-a233-433f-82d6-7ae0e4054955">Auto</Caption>
	</SchemeRecord>
</SchemeTable>