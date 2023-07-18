<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3e31b54e-1a4c-4e9c-bcf5-26e4780d6419" Name="LocalizationLibraries" Group="System">
	<SchemePhysicalColumn ID="84abc23e-1f9b-446f-93ba-4fab68dd841c" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="de6d2d8d-5b75-4309-b0f6-ca10027efb81" Name="Name" Type="String(128) Not Null">
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fe72b990-0d2d-4f82-8358-3d43a9d4159d" Name="Priority" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="9c403ab5-8fc9-468e-bed8-2051ab14efde" Name="DetachedCultures" Type="String(Max) Null">
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="9a8745e9-9ce3-4729-8b2a-68df07082ed7" Name="pk_LocalizationLibraries">
		<SchemeIndexedColumn Column="84abc23e-1f9b-446f-93ba-4fab68dd841c" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="83861932-4317-479a-9093-c0ae3602a617" Name="idx_LocalizationLibraries_Name" IsClustered="true">
		<SchemeIndexedColumn Column="de6d2d8d-5b75-4309-b0f6-ca10027efb81" />
	</SchemeUniqueKey>
</SchemeTable>