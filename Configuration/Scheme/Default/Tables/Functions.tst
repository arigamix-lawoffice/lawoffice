<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="57e45ca3-5036-4268-b8f9-86c4933a4d2d" Name="Functions" Group="System">
	<Description>Contains metadata that describes functions which used by Tessa</Description>
	<SchemePhysicalColumn ID="5b026f77-5c8e-49e1-82b9-4d27993d3fdc" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ID of a function</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d11c41f4-11ed-4bb7-afe6-27273fa2a729" Name="Name" Type="String(128) Not Null">
		<Description>Unique name of a function</Description>
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2ab60887-a082-47d9-b3f1-f8d50d37b659" Name="Definition" Type="Xml Not Null">
		<Description>Definition of a function</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="e2eaebbc-091d-4caa-be87-aff3ddedcbe2" Name="pk_Functions" IsClustered="true">
		<SchemeIndexedColumn Column="5b026f77-5c8e-49e1-82b9-4d27993d3fdc" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="97b3d7bf-aaaf-4654-9a07-c7487bccdf9b" Name="ndx_Functions_Name">
		<SchemeIndexedColumn Column="d11c41f4-11ed-4bb7-afe6-27273fa2a729" />
	</SchemeUniqueKey>
</SchemeTable>