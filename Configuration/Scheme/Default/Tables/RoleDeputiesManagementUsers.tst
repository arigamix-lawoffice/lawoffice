<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b8f9c863-22fd-4d63-a7cf-b9f0de519b47" Name="RoleDeputiesManagementUsers" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Сотрудники секции "Мои замещения"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8f9c863-22fd-0063-2000-09f0de519b47" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b8f9c863-22fd-0163-4000-09f0de519b47" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8f9c863-22fd-0063-3100-09f0de519b47" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="36b19609-dfe9-4424-ae05-c1e668f4d4b6" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="0f489948-bc16-42a6-8953-b92100807296" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="36b19609-dfe9-0024-4000-01e668f4d4b6" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="0f489948-bc16-00a6-3100-092100807296" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ef765f08-078b-4051-8364-f55ff263285f" Name="PersonalRole" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Заместитель</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ef765f08-078b-0051-4000-055ff263285f" Name="PersonalRoleID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="37e6faa7-af57-451d-93ff-bcc8b1dce43c" Name="PersonalRoleName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8f9c863-22fd-0063-5000-09f0de519b47" Name="pk_RoleDeputiesManagementUsers">
		<SchemeIndexedColumn Column="b8f9c863-22fd-0063-3100-09f0de519b47" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b8f9c863-22fd-0063-7000-09f0de519b47" Name="idx_RoleDeputiesManagementUsers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b8f9c863-22fd-0163-4000-09f0de519b47" />
	</SchemeIndex>
	<SchemeIndex ID="a9f7f072-6d08-43e7-b7d7-03efe8bf334a" Name="ndx_RoleDeputiesManagementUsers_ParentRowIDPersonalRoleID">
		<SchemeIndexedColumn Column="36b19609-dfe9-0024-4000-01e668f4d4b6" />
		<SchemeIndexedColumn Column="ef765f08-078b-0051-4000-055ff263285f" />
	</SchemeIndex>
	<SchemeIndex ID="49998887-5ff4-4675-92f2-083b6259f94b" Name="ndx_RoleDeputiesManagementUsers_PersonalRoleIDParentRowID">
		<SchemeIndexedColumn Column="ef765f08-078b-0051-4000-055ff263285f" />
		<SchemeIndexedColumn Column="36b19609-dfe9-0024-4000-01e668f4d4b6" />
	</SchemeIndex>
	<SchemeIndex ID="e5c5a089-7b8b-4c41-9d02-706876966f38" Name="ndx_RoleDeputiesManagementUsers_RowIDParentRowID">
		<SchemeIndexedColumn Column="b8f9c863-22fd-0063-3100-09f0de519b47" />
		<SchemeIndexedColumn Column="36b19609-dfe9-0024-4000-01e668f4d4b6" />
	</SchemeIndex>
</SchemeTable>