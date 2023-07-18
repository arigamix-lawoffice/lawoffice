<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e631fc2a-7628-4d7e-a118-99d310fa12b8" Name="PersonalRoleRolesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица для отображения всех ролей (кроме своей персональной роли), в которые входит сотрудник.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e631fc2a-7628-007e-2000-09d310fa12b8" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e631fc2a-7628-017e-4000-09d310fa12b8" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e631fc2a-7628-007e-3100-09d310fa12b8" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fe22f80e-eea9-463b-a6fb-be3740629a67" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Ссылка на роль, в которую включен сотрудник.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fe22f80e-eea9-003b-4000-0e3740629a67" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="fe7e4131-aee2-4e31-97aa-05e233da0326" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e631fc2a-7628-007e-5000-09d310fa12b8" Name="pk_PersonalRoleRolesVirtual">
		<SchemeIndexedColumn Column="e631fc2a-7628-007e-3100-09d310fa12b8" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e631fc2a-7628-007e-7000-09d310fa12b8" Name="idx_PersonalRoleRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e631fc2a-7628-017e-4000-09d310fa12b8" />
	</SchemeIndex>
</SchemeTable>