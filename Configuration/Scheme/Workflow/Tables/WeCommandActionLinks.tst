<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="97e973ba-8fa9-4e3d-96d3-2f077ca11531" Name="WeCommandActionLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция определяет список переходов, которые должны быть вызваны после получения команды</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="97e973ba-8fa9-003d-2000-0f077ca11531" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="97e973ba-8fa9-013d-4000-0f077ca11531" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="97e973ba-8fa9-003d-3100-0f077ca11531" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="cfc71ddb-8702-4240-a706-df2f58e53213" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cfc71ddb-8702-0040-4000-0f2f58e53213" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="584a0b1b-ae1f-446f-9e29-393bbb122cac" Name="LinkName" Type="String(128) Not Null" />
		<SchemePhysicalColumn ID="bb860bc2-b4dc-429a-ab49-a00994e370c9" Name="LinkCaption" Type="String(128) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="97e973ba-8fa9-003d-5000-0f077ca11531" Name="pk_WeCommandActionLinks">
		<SchemeIndexedColumn Column="97e973ba-8fa9-003d-3100-0f077ca11531" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="97e973ba-8fa9-003d-7000-0f077ca11531" Name="idx_WeCommandActionLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="97e973ba-8fa9-013d-4000-0f077ca11531" />
	</SchemeIndex>
</SchemeTable>