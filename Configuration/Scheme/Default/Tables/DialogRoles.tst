<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="125ad61a-3698-4d07-9fa0-139c9cc25074" Name="DialogRoles" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="125ad61a-3698-0007-2000-039c9cc25074" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="125ad61a-3698-0107-4000-039c9cc25074" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="125ad61a-3698-0007-3100-039c9cc25074" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="cff25124-fb55-43fb-89be-a24941101438" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cff25124-fb55-00fb-4000-024941101438" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="cffac0e9-3b4d-4387-b030-9c8ef6e8c812" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="125ad61a-3698-0007-5000-039c9cc25074" Name="pk_DialogRoles">
		<SchemeIndexedColumn Column="125ad61a-3698-0007-3100-039c9cc25074" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="125ad61a-3698-0007-7000-039c9cc25074" Name="idx_DialogRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="125ad61a-3698-0107-4000-039c9cc25074" />
	</SchemeIndex>
</SchemeTable>