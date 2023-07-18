<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9650456c-27ea-4f62-9073-95b9be1d49ba" Name="RoleDeputiesManagementRolesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9650456c-27ea-0062-2000-05b9be1d49ba" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9650456c-27ea-0162-4000-05b9be1d49ba" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9650456c-27ea-0062-3100-05b9be1d49ba" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="6b98cc49-b57d-4c00-86cc-9735c0913f0e" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="79dca225-d99c-4dfd-94d9-27ed3ab15046" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6b98cc49-b57d-0000-4000-0735c0913f0e" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="79dca225-d99c-00fd-3100-07ed3ab15046" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="992db3b3-e02e-4339-97a8-663ca39e2d63" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, по которой будет проводиться замещение</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="992db3b3-e02e-0039-4000-063ca39e2d63" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="28d9310d-4f43-4bfb-a771-bc5ba71ef4d6" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9650456c-27ea-0062-5000-05b9be1d49ba" Name="pk_RoleDeputiesManagementRolesVirtual">
		<SchemeIndexedColumn Column="9650456c-27ea-0062-3100-05b9be1d49ba" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9650456c-27ea-0062-7000-05b9be1d49ba" Name="idx_RoleDeputiesManagementRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9650456c-27ea-0162-4000-05b9be1d49ba" />
	</SchemeIndex>
</SchemeTable>