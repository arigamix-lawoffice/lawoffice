<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="669078fd-2901-4084-ac61-13a063581197" Name="BusinessProcessEditRoles" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, имеющих доступ на редактирование  шаблона и экземпляра процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="669078fd-2901-0084-2000-03a063581197" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="669078fd-2901-0184-4000-03a063581197" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="669078fd-2901-0084-3100-03a063581197" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5bf3c79b-680d-4723-9081-790deeb348e5" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5bf3c79b-680d-0023-4000-090deeb348e5" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="9bb0d1fb-460c-4c2e-9756-4e5f3f06c5e0" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="669078fd-2901-0084-5000-03a063581197" Name="pk_BusinessProcessEditRoles">
		<SchemeIndexedColumn Column="669078fd-2901-0084-3100-03a063581197" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="669078fd-2901-0084-7000-03a063581197" Name="idx_BusinessProcessEditRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="669078fd-2901-0184-4000-03a063581197" />
	</SchemeIndex>
</SchemeTable>