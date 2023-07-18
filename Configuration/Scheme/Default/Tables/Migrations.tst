<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="fd65afe6-d4bf-4885-872a-3824e64b1c63" Name="Migrations" Group="System">
	<Description>Migrations</Description>
	<SchemePhysicalColumn ID="497faadc-ef08-439f-b336-25e0aeea9f4e" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ID of a migration</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f2d8053f-91f4-4554-8e24-8593bf340e0a" Name="Name" Type="String(128) Not Null">
		<Description>Name of a migration</Description>
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="92da718a-5d41-46a0-82ca-35cf64765a5d" Name="Definition" Type="Xml Not Null">
		<Description>Definition of a migration</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="88b3d95a-7d36-47bc-a6e9-da6173c254d4" Name="pk_Migrations" IsClustered="true">
		<SchemeIndexedColumn Column="497faadc-ef08-439f-b336-25e0aeea9f4e" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="e6b504c2-5813-49fa-8c47-f384b5884e6f" Name="ndx_Migrations_Name">
		<SchemeIndexedColumn Column="f2d8053f-91f4-4554-8e24-8593bf340e0a" />
	</SchemeUniqueKey>
</SchemeTable>