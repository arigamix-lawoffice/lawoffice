<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" IsSealed="true" ID="c4fcd8d3-fcb1-451f-98f4-e352cd8a3a41" Name="Scheme" Group="System">
	<Description>Scheme properties</Description>
	<SchemePhysicalColumn ID="7ae3e957-e0d2-441b-af4f-a0f6f365648c" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>Scheme ID</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="980ce038-538d-454e-8c77-716f5ef5d90e" Name="Name" Type="String(128) Not Null">
		<Description>Scheme name</Description>
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="64b0efb9-8434-4566-b0bb-5406bb3b0c39" Name="Description" Type="String(Max) Null">
		<Description>Scheme description</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="241f405c-ab80-4adb-8cc3-873632a52018" Name="CollationSqlServer" Type="String(Max) Null">
		<Description>Scheme Collation for MS SQL Server</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="774a5531-8d0d-4547-8866-e42a3d5d54ea" Name="CollationPostgreSql" Type="String(Max) Null">
		<Description>Scheme Collation for PostgeSQL</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0622ea8c-59c6-4d34-b6a0-4afbfb006e9a" Name="SchemeVersion" Type="String(15) Not Null">
		<Description>Scheme version</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="124df9e1-67c3-4051-904b-09eeb7259515" Name="Modified" Type="DateTime Not Null">
		<Description>Date/time of the last changes</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="57472732-fdba-4199-9885-62d0765c405a" Name="ModifiedByID" Type="Guid Not Null">
		<Description>The ID of the user who made the last change</Description>
		<SchemeDefaultConstraint IsPermanent="true" IsSealed="true" ID="c65e11ec-383d-4e7c-9abd-953e20c8ba1c" Name="df_Scheme_ModifiedByID" Value="11111111-1111-1111-1111-111111111111" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c5899674-f4ad-4133-aa6d-497d6278eaf4" Name="ModifiedByName" Type="String(128) Not Null">
		<Description>The name of the user who made the last change</Description>
		<SchemeDefaultConstraint IsPermanent="true" IsSealed="true" ID="2001ab2f-406a-47be-972f-71a77e855f09" Name="df_Scheme_ModifiedByName" Value="System" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b945f75a-643e-407d-90ad-3bf488b366db" Name="DbmsVersion" Type="String(15) Null" />
	<SchemePrimaryKey ID="dcc6eb2a-d7a6-418f-a7b0-64092c3f804e" Name="pk_Scheme" IsClustered="true">
		<SchemeIndexedColumn Column="7ae3e957-e0d2-441b-af4f-a0f6f365648c" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="247374c4-b6ca-411c-a788-bf9606d5a840" Name="ndx_Scheme_Name">
		<SchemeIndexedColumn Column="980ce038-538d-454e-8c77-716f5ef5d90e" />
	</SchemeUniqueKey>
</SchemeTable>