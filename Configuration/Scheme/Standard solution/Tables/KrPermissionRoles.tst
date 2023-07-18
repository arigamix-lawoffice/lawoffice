<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="79a6e9e0-e52f-456f-871a-00b6895566ec" Name="KrPermissionRoles" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, для пользователей которых применяются разрешения из карточки с правами.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="79a6e9e0-e52f-006f-2000-00b6895566ec" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="79a6e9e0-e52f-016f-4000-00b6895566ec" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="79a6e9e0-e52f-006f-3100-00b6895566ec" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4f36b67b-3a7f-4638-bae0-33dc6e2a37b5" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, для пользователей которой применяются разрешения из карточки с правами.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4f36b67b-3a7f-0038-4000-03dc6e2a37b5" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="fc5ac637-214a-4922-9454-e89ec43c15d3" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="240ec8bc-7c6f-4660-9fef-9336b28f0997" Name="IsContext" Type="Boolean Not Null">
		<Description>Признак того, что роль является контекстной.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="79a6e9e0-e52f-006f-5000-00b6895566ec" Name="pk_KrPermissionRoles">
		<SchemeIndexedColumn Column="79a6e9e0-e52f-006f-3100-00b6895566ec" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="68511f06-1e93-4bec-96ab-440a8720cc0d" Name="ndx_KrPermissionRoles_IDRoleIDIsContext">
		<Description>Для каждой карточки любая роль указана не более 1го раза</Description>
		<SchemeIndexedColumn Column="79a6e9e0-e52f-016f-4000-00b6895566ec" />
		<SchemeIndexedColumn Column="4f36b67b-3a7f-0038-4000-03dc6e2a37b5" />
		<SchemeIndexedColumn Column="240ec8bc-7c6f-4660-9fef-9336b28f0997" />
	</SchemeUniqueKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="79a6e9e0-e52f-006f-7000-00b6895566ec" Name="idx_KrPermissionRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="79a6e9e0-e52f-016f-4000-00b6895566ec" />
	</SchemeIndex>
	<SchemeIndex ID="4953306c-5121-4ff6-8a89-b766f9e7907c" Name="ndx_KrPermissionRoles_RoleID">
		<SchemeIndexedColumn Column="4f36b67b-3a7f-0038-4000-03dc6e2a37b5" />
	</SchemeIndex>
</SchemeTable>