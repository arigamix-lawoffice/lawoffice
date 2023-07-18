<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="66b31fcc-b8fa-465a-91f2-0dd391cc76ec" Name="Tables" Group="System">
	<Description>Contains metadata that describes tables which used by Tessa</Description>
	<SchemePhysicalColumn ID="eb89dd4a-b477-458a-ace7-c89ca9c6d364" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ID of a table</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="496bed22-d010-4388-bcdc-0d84b7322068" Name="Name" Type="String(128) Not Null">
		<Description>Name of a table</Description>
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="00a93180-aa37-4015-b757-dd9e9aa104cc" Name="Definition" Type="Xml Not Null">
		<Description>Definition of a table</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="3daf6f53-b9bf-4e7b-ba8a-20ec57e089b9" Name="pk_Tables" IsClustered="true">
		<SchemeIndexedColumn Column="eb89dd4a-b477-458a-ace7-c89ca9c6d364" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="e58cbc89-4615-4152-a61a-9d38e91aa687" Name="ndx_Tables_Name">
		<SchemeIndexedColumn Column="496bed22-d010-4388-bcdc-0d84b7322068" />
	</SchemeUniqueKey>
</SchemeTable>