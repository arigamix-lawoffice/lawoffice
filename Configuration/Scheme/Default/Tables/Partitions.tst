<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ca00fac-d04e-4b82-8139-9778518e00bf" Name="Partitions" Group="System">
	<SchemePhysicalColumn ID="fc636796-f583-4306-ad69-30fb2a070f9a" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true" />
	<SchemePhysicalColumn ID="6af8d64d-cff0-4ece-9db3-b1f38e73814d" Name="Name" Type="String(128) Not Null">
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f4b6f6e0-92c1-4162-bc20-3d0cbdf80e84" Name="Definition" Type="Xml Not Null" />
	<SchemePrimaryKey ID="691a4d80-23a1-4854-b9e4-3acce0751c4a" Name="pk_Partitions" IsClustered="true">
		<SchemeIndexedColumn Column="fc636796-f583-4306-ad69-30fb2a070f9a" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="5797338d-b343-48f8-8ea0-547b704d3dd8" Name="ndx_Partitions_Name">
		<SchemeIndexedColumn Column="6af8d64d-cff0-4ece-9db3-b1f38e73814d" />
	</SchemeUniqueKey>
</SchemeTable>