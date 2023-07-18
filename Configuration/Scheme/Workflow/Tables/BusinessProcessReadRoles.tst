<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="8a55a034-893d-4412-9458-189ef63d7008" Name="BusinessProcessReadRoles" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, имеющих доступ на чтение экземпляра шаблона</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a55a034-893d-0012-2000-089ef63d7008" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a55a034-893d-0112-4000-089ef63d7008" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a55a034-893d-0012-3100-089ef63d7008" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5c3891bf-5640-4d43-8a44-0f78d8df9813" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5c3891bf-5640-0043-4000-0f78d8df9813" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="7d6b54a3-6ed9-4015-9a3f-bab6bd9588eb" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a55a034-893d-0012-5000-089ef63d7008" Name="pk_BusinessProcessReadRoles">
		<SchemeIndexedColumn Column="8a55a034-893d-0012-3100-089ef63d7008" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a55a034-893d-0012-7000-089ef63d7008" Name="idx_BusinessProcessReadRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8a55a034-893d-0112-4000-089ef63d7008" />
	</SchemeIndex>
</SchemeTable>