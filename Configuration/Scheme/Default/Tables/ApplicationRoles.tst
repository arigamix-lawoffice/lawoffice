<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="7d23077a-8730-4ad7-9bcd-9a3d52c7e119" Name="ApplicationRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, которым доступно приложение.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d23077a-8730-00d7-2000-0a3d52c7e119" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7d23077a-8730-01d7-4000-0a3d52c7e119" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d23077a-8730-00d7-3100-0a3d52c7e119" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f945a532-4d58-4373-9604-98ec3226c3f5" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f945a532-4d58-0073-4000-08ec3226c3f5" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="2b2ea24a-4498-4c18-b558-d91385670d6f" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d23077a-8730-00d7-5000-0a3d52c7e119" Name="pk_ApplicationRoles">
		<SchemeIndexedColumn Column="7d23077a-8730-00d7-3100-0a3d52c7e119" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d23077a-8730-00d7-7000-0a3d52c7e119" Name="idx_ApplicationRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7d23077a-8730-01d7-4000-0a3d52c7e119" />
	</SchemeIndex>
</SchemeTable>