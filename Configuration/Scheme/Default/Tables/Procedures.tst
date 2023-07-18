<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="1bf6a3b2-725a-487c-b4d8-6b082fb56037" Name="Procedures" Group="System">
	<Description>Contains metadata that describes tables which used by Tessa</Description>
	<SchemePhysicalColumn ID="3dfaab66-fbb1-47f7-8e75-eb2823e97c45" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>ID of a procedure</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="20866e7d-1c3a-495f-a5bf-cfe66cb4e957" Name="Name" Type="String(128) Not Null">
		<Description>Name of a procedure</Description>
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="327384ab-7925-4860-9c4f-aa50afd6a2f7" Name="Definition" Type="Xml Not Null">
		<Description>Definition of a procedure</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="e274a54a-5d01-4323-b37a-c5f93766cce3" Name="pk_Procedures" IsClustered="true">
		<SchemeIndexedColumn Column="3dfaab66-fbb1-47f7-8e75-eb2823e97c45" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="e3843a5e-9339-4b26-a02a-0d42a2c6c752" Name="ndx_Procedures_Name">
		<SchemeIndexedColumn Column="20866e7d-1c3a-495f-a5bf-cfe66cb4e957" />
	</SchemeUniqueKey>
</SchemeTable>