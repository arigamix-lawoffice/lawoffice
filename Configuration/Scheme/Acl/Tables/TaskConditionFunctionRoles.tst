<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="b59a92cb-8414-4a3d-91f9-9d41de829d3f" Name="TaskConditionFunctionRoles" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список функциональных ролей для условий проверки заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b59a92cb-8414-003d-2000-0d41de829d3f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b59a92cb-8414-013d-4000-0d41de829d3f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b59a92cb-8414-003d-3100-0d41de829d3f" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="770e4077-bb74-4a0b-a5c3-a9a9d52fe0a6" Name="FunctionRole" Type="Reference(Typified) Not Null" ReferencedTable="a59078ce-8acf-4c45-a49a-503fa88a0580">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="770e4077-bb74-000b-4000-09a9d52fe0a6" Name="FunctionRoleID" Type="Guid Not Null" ReferencedColumn="bd4fdcea-8042-488a-94c9-770b49357cfe" />
		<SchemeReferencingColumn ID="23b41a8b-dc25-4314-9945-bab09f47269d" Name="FunctionRoleCaption" Type="String(128) Not Null" ReferencedColumn="f8b3afc6-cea7-4a98-b907-e716e0a426c6" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b59a92cb-8414-003d-5000-0d41de829d3f" Name="pk_TaskConditionFunctionRoles">
		<SchemeIndexedColumn Column="b59a92cb-8414-003d-3100-0d41de829d3f" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b59a92cb-8414-003d-7000-0d41de829d3f" Name="idx_TaskConditionFunctionRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b59a92cb-8414-013d-4000-0d41de829d3f" />
	</SchemeIndex>
</SchemeTable>