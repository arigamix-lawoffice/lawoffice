<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9dc135b0-21b8-4deb-ab65-bdda57a3fbb5" Name="RoleDeputiesManagementUsersVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9dc135b0-21b8-00eb-2000-0dda57a3fbb5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9dc135b0-21b8-01eb-4000-0dda57a3fbb5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9dc135b0-21b8-00eb-3100-0dda57a3fbb5" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="330145fc-ae8f-48fb-9257-61009c0d52e1" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="79dca225-d99c-4dfd-94d9-27ed3ab15046" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="330145fc-ae8f-00fb-4000-01009c0d52e1" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="79dca225-d99c-00fd-3100-07ed3ab15046" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="70e434c0-e236-4997-925c-d5091ff4189d" Name="PersonalRole" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Заместитель</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="70e434c0-e236-0097-4000-05091ff4189d" Name="PersonalRoleID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="2269a27a-914f-4eb4-8067-01f233d1a343" Name="PersonalRoleName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9dc135b0-21b8-00eb-5000-0dda57a3fbb5" Name="pk_RoleDeputiesManagementUsersVirtual">
		<SchemeIndexedColumn Column="9dc135b0-21b8-00eb-3100-0dda57a3fbb5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9dc135b0-21b8-00eb-7000-0dda57a3fbb5" Name="idx_RoleDeputiesManagementUsersVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9dc135b0-21b8-01eb-4000-0dda57a3fbb5" />
	</SchemeIndex>
</SchemeTable>