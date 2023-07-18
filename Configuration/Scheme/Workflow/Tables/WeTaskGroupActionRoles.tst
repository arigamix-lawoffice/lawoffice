<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="0656f18d-bb1c-47c9-8d40-24300c7f4b53" Name="WeTaskGroupActionRoles" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со списком ролей для действия Группа заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0656f18d-bb1c-00c9-2000-04300c7f4b53" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0656f18d-bb1c-01c9-4000-04300c7f4b53" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0656f18d-bb1c-00c9-3100-04300c7f4b53" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b6c4b198-6065-4028-909b-997e0b3dfe6c" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b6c4b198-6065-0028-4000-097e0b3dfe6c" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="c1c6787c-1855-4f02-be92-1d36f75a9faf" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0656f18d-bb1c-00c9-5000-04300c7f4b53" Name="pk_WeTaskGroupActionRoles">
		<SchemeIndexedColumn Column="0656f18d-bb1c-00c9-3100-04300c7f4b53" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0656f18d-bb1c-00c9-7000-04300c7f4b53" Name="idx_WeTaskGroupActionRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0656f18d-bb1c-01c9-4000-04300c7f4b53" />
	</SchemeIndex>
</SchemeTable>