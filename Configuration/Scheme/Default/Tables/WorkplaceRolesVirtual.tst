<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="67f548c6-9fdf-44c1-9d61-eea3098021f5" Name="WorkplaceRolesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="67f548c6-9fdf-00c1-2000-0ea3098021f5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="67f548c6-9fdf-01c1-4000-0ea3098021f5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="67f548c6-9fdf-00c1-3100-0ea3098021f5" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="decd81a1-9364-48c5-95ee-a1b2b5bd6b7d" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="decd81a1-9364-00c5-4000-01b2b5bd6b7d" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="8c258293-f39a-4389-ad5c-771736a9df0a" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="67f548c6-9fdf-00c1-5000-0ea3098021f5" Name="pk_WorkplaceRolesVirtual">
		<SchemeIndexedColumn Column="67f548c6-9fdf-00c1-3100-0ea3098021f5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="67f548c6-9fdf-00c1-7000-0ea3098021f5" Name="idx_WorkplaceRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="67f548c6-9fdf-01c1-4000-0ea3098021f5" />
	</SchemeIndex>
</SchemeTable>