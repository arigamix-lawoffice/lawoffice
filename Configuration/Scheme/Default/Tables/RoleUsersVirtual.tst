<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="47428527-dd1f-4e52-9ba5-a2a988abdf93" Name="RoleUsersVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Состав роли без учёта замещений.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="47428527-dd1f-0052-2000-02a988abdf93" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="47428527-dd1f-0152-4000-02a988abdf93" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="47428527-dd1f-0052-3100-02a988abdf93" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="362c4f82-8806-41d5-a1ef-dd26a60eb515" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Персональная роль пользователя, включённого в состав роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="362c4f82-8806-00d5-4000-0d26a60eb515" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d08083f1-76b6-4295-9256-6b7ecc2d5526" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="47428527-dd1f-0052-5000-02a988abdf93" Name="pk_RoleUsersVirtual">
		<SchemeIndexedColumn Column="47428527-dd1f-0052-3100-02a988abdf93" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="47428527-dd1f-0052-7000-02a988abdf93" Name="idx_RoleUsersVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="47428527-dd1f-0152-4000-02a988abdf93" />
	</SchemeIndex>
</SchemeTable>