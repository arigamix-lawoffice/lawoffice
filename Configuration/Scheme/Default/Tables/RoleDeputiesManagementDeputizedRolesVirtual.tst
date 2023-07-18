<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="997561ee-218f-4f22-946b-87a78755c3e6" Name="RoleDeputiesManagementDeputizedRolesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, который относится к каждой строке в таблице с замещаемыми сотрудниками RoleDeputiesManagementDeputizedVirtual.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="997561ee-218f-0022-2000-07a78755c3e6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="997561ee-218f-0122-4000-07a78755c3e6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="997561ee-218f-0022-3100-07a78755c3e6" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="21b99bb1-fe6f-4dd4-8e2e-f55b7065ff63" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="c55a7921-d82d-4f8b-b801-f1c693c4c2e3" IsReferenceToOwner="true">
		<Description>Строка в родительской секции</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21b99bb1-fe6f-00d4-4000-055b7065ff63" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="c55a7921-d82d-008b-3100-01c693c4c2e3" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="dda1d970-7904-43bb-b490-3e3c950996b3" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, которая относится к замещаемому сотруднику</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dda1d970-7904-00bb-4000-0e3c950996b3" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="eebb4d0d-05d7-476d-a81e-0fd7ad8dcf14" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="997561ee-218f-0022-5000-07a78755c3e6" Name="pk_RoleDeputiesManagementDeputizedRolesVirtual">
		<SchemeIndexedColumn Column="997561ee-218f-0022-3100-07a78755c3e6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="997561ee-218f-0022-7000-07a78755c3e6" Name="idx_RoleDeputiesManagementDeputizedRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="997561ee-218f-0122-4000-07a78755c3e6" />
	</SchemeIndex>
</SchemeTable>