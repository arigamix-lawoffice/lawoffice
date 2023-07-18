<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="91acf9b9-8476-4dc8-a239-ac6b8f250077" Name="RoleDeputiesManagementRoles" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Роли секции "Мои замещения"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="91acf9b9-8476-00c8-2000-0c6b8f250077" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="91acf9b9-8476-01c8-4000-0c6b8f250077" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="91acf9b9-8476-00c8-3100-0c6b8f250077" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0eb392fb-b8eb-43c6-b685-2623aa63f8ea" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="0f489948-bc16-42a6-8953-b92100807296" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0eb392fb-b8eb-00c6-4000-0623aa63f8ea" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="0f489948-bc16-00a6-3100-092100807296" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="d3ca15b7-7d9d-4f7d-adbd-96a4cd79da2d" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, по которой будет проводиться замещение</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d3ca15b7-7d9d-007d-4000-06a4cd79da2d" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="dc1700bc-7cb8-4984-929a-124105b609a2" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="91acf9b9-8476-00c8-5000-0c6b8f250077" Name="pk_RoleDeputiesManagementRoles">
		<SchemeIndexedColumn Column="91acf9b9-8476-00c8-3100-0c6b8f250077" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="91acf9b9-8476-00c8-7000-0c6b8f250077" Name="idx_RoleDeputiesManagementRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="91acf9b9-8476-01c8-4000-0c6b8f250077" />
	</SchemeIndex>
	<SchemeIndex ID="fe39d26d-4e4d-4dbc-b543-30b4f256d608" Name="ndx_RoleDeputiesManagementRoles_ParentRowID">
		<SchemeIndexedColumn Column="0eb392fb-b8eb-00c6-4000-0623aa63f8ea" />
	</SchemeIndex>
	<SchemeIndex ID="e88b272a-a040-4cab-bd5a-f3a3d8b761ab" Name="ndx_RoleDeputiesManagementRoles_RoleID">
		<SchemeIndexedColumn Column="d3ca15b7-7d9d-007d-4000-06a4cd79da2d" />
	</SchemeIndex>
</SchemeTable>