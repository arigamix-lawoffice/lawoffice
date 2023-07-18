<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="7d0b5402-9d55-4269-964d-25b5ddcb2690" Name="WorkflowEngineCheckContextRole" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со списком контекстных ролей для расширения на тайлы "Проверка контекстных ролей"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d0b5402-9d55-0069-2000-05b5ddcb2690" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7d0b5402-9d55-0169-4000-05b5ddcb2690" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d0b5402-9d55-0069-3100-05b5ddcb2690" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7a8c3c02-dbf8-46b5-afd2-30d6eb115583" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7a8c3c02-dbf8-00b5-4000-00d6eb115583" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="f58bd820-dda1-462f-a098-05c782534bca" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d0b5402-9d55-0069-5000-05b5ddcb2690" Name="pk_WorkflowEngineCheckContextRole">
		<SchemeIndexedColumn Column="7d0b5402-9d55-0069-3100-05b5ddcb2690" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7d0b5402-9d55-0069-7000-05b5ddcb2690" Name="idx_WorkflowEngineCheckContextRole_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7d0b5402-9d55-0169-4000-05b5ddcb2690" />
	</SchemeIndex>
</SchemeTable>