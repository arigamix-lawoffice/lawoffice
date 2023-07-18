<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="122da60d-3efc-42a2-bd84-510c0819807b" Name="PersonalRoleStaticRolesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица для отображения и редактирования статических ролей, в которые входит сотрудник.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="122da60d-3efc-00a2-2000-010c0819807b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="122da60d-3efc-01a2-4000-010c0819807b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="122da60d-3efc-00a2-3100-010c0819807b" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b27bd1a3-53ed-410e-98a3-9909291aefa7" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Ссылка на роль, в которую включен сотрудник.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b27bd1a3-53ed-000e-4000-0909291aefa7" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="14f130d2-1bbc-428e-8508-f0c817beb125" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="122da60d-3efc-00a2-5000-010c0819807b" Name="pk_PersonalRoleStaticRolesVirtual">
		<SchemeIndexedColumn Column="122da60d-3efc-00a2-3100-010c0819807b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="122da60d-3efc-00a2-7000-010c0819807b" Name="idx_PersonalRoleStaticRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="122da60d-3efc-01a2-4000-010c0819807b" />
	</SchemeIndex>
</SchemeTable>