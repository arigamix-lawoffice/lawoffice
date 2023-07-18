<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="97805de9-ed94-41a3-bf8b-fc1fa17c7d30" Name="KrStageRoles" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей для шаблона этапа и группы этапов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="97805de9-ed94-00a3-2000-0c1fa17c7d30" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="97805de9-ed94-01a3-4000-0c1fa17c7d30" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="97805de9-ed94-00a3-3100-0c1fa17c7d30" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="11e403a7-612c-4d07-87a8-4d33ad6f29f1" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роли текущего пользователя, для которого будет применятся правило шаблона</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="11e403a7-612c-0007-4000-0d33ad6f29f1" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="1e13eb3a-9cd3-480c-88b6-1317f9c01f71" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="97805de9-ed94-00a3-5000-0c1fa17c7d30" Name="pk_KrStageRoles">
		<SchemeIndexedColumn Column="97805de9-ed94-00a3-3100-0c1fa17c7d30" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="97805de9-ed94-00a3-7000-0c1fa17c7d30" Name="idx_KrStageRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="97805de9-ed94-01a3-4000-0c1fa17c7d30" />
	</SchemeIndex>
	<SchemeIndex ID="1b80820f-39bd-4b4c-a90f-89d5a58234d3" Name="ndx_KrStageRoles_RoleIDID" IsUnique="true">
		<SchemeIndexedColumn Column="11e403a7-612c-0007-4000-0d33ad6f29f1" />
		<SchemeIndexedColumn Column="97805de9-ed94-01a3-4000-0c1fa17c7d30" />
	</SchemeIndex>
</SchemeTable>