<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="42a0388c-2064-4dbb-ba35-2ca8979af629" Name="KrStageCommonMethods" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Основная таблица для базовых методов, используемых в шаблонах компиляции KrStageTemplate</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="42a0388c-2064-00bb-2000-0ca8979af629" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="42a0388c-2064-01bb-4000-0ca8979af629" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4d991b20-55f4-4241-90d0-4169563ff7bc" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="f4b2848d-8e1e-4935-9785-1ed3a7c27507" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="078a294e-f823-4c09-acee-1a42a14623ba" Name="Source" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="42a0388c-2064-00bb-5000-0ca8979af629" Name="pk_KrStageCommonMethods" IsClustered="true">
		<SchemeIndexedColumn Column="42a0388c-2064-01bb-4000-0ca8979af629" />
	</SchemePrimaryKey>
	<SchemeIndex ID="8ebd232e-1338-4114-bdcd-4140f1f80449" Name="ndx_KrStageCommonMethods_Name_8ebd232e" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="4d991b20-55f4-4241-90d0-4169563ff7bc" SortOrder="Descending">
			<Expression Dbms="PostgreSql">lower("Name") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="7a801954-7b83-4dbd-a354-fc6a43c62a21" Name="ndx_KrStageCommonMethods_Name" IsUnique="true">
		<SchemeIndexedColumn Column="4d991b20-55f4-4241-90d0-4169563ff7bc">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>