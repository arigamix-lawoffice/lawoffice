<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f113a406-970b-4c1b-820f-9d960c37692a" Name="SequencesInfo" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f113a406-970b-001b-2000-0d960c37692a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f113a406-970b-011b-4000-0d960c37692a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="83d3882f-1d18-45e2-b4a6-b145ea439915" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f113a406-970b-001b-5000-0d960c37692a" Name="pk_SequencesInfo" IsClustered="true">
		<SchemeIndexedColumn Column="f113a406-970b-011b-4000-0d960c37692a" />
	</SchemePrimaryKey>
	<SchemeIndex ID="563d7a13-964a-4e0e-a2fe-5c732d30ce32" Name="ndx_SequencesInfo_Name" IsUnique="true">
		<SchemeIndexedColumn Column="83d3882f-1d18-45e2-b4a6-b145ea439915" />
	</SchemeIndex>
	<SchemeIndex ID="26aeca38-c4df-4da9-9886-57ece1ed931f" Name="ndx_SequencesInfo_Name_26aeca38" SupportsSqlServer="false">
		<SchemeIndexedColumn Column="83d3882f-1d18-45e2-b4a6-b145ea439915">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>