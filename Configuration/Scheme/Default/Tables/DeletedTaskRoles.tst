<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8340a9b3-74ba-4771-af73-35bed38db55e" Name="DeletedTaskRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, на которые были назначены задания в удалённой карточке.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8340a9b3-74ba-0071-2000-05bed38db55e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8340a9b3-74ba-0171-4000-05bed38db55e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8340a9b3-74ba-0071-3100-05bed38db55e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a20f4938-cfff-4c8d-a6c0-ae726055493b" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую было назначено задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a20f4938-cfff-008d-4000-0e726055493b" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a5352dbe-bce4-41e9-a2e0-dcaa40d65474" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="4338e955-d006-4c2c-b0e6-3df6c713e514" Name="RoleType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки для роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4338e955-d006-002c-4000-0df6c713e514" Name="RoleTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8340a9b3-74ba-0071-5000-05bed38db55e" Name="pk_DeletedTaskRoles">
		<SchemeIndexedColumn Column="8340a9b3-74ba-0071-3100-05bed38db55e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8340a9b3-74ba-0071-7000-05bed38db55e" Name="idx_DeletedTaskRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8340a9b3-74ba-0171-4000-05bed38db55e" />
	</SchemeIndex>
</SchemeTable>