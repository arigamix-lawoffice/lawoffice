<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="48d261cd-1054-41d7-9046-485e22d15060" Name="WeEmailActionRecievers" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список получателей письма</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="48d261cd-1054-00d7-2000-085e22d15060" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="48d261cd-1054-01d7-4000-085e22d15060" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="48d261cd-1054-00d7-3100-085e22d15060" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b4495523-da78-4e1f-835a-6076dfbf1f8a" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4495523-da78-001f-4000-0076dfbf1f8a" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="d06760ff-5eed-4a3d-b15e-727fe37e9de4" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="48d261cd-1054-00d7-5000-085e22d15060" Name="pk_WeEmailActionRecievers">
		<SchemeIndexedColumn Column="48d261cd-1054-00d7-3100-085e22d15060" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="48d261cd-1054-00d7-7000-085e22d15060" Name="idx_WeEmailActionRecievers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="48d261cd-1054-01d7-4000-085e22d15060" />
	</SchemeIndex>
</SchemeTable>