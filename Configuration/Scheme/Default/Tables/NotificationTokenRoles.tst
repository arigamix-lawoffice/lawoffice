<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a88ed4f8-dcce-400a-931f-99defee9949c" Name="NotificationTokenRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, получающих уведомления о необходимости обновления токена.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a88ed4f8-dcce-000a-2000-09defee9949c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a88ed4f8-dcce-010a-4000-09defee9949c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a88ed4f8-dcce-000a-3100-09defee9949c" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="8b1844d9-dbfc-47f0-b01b-c7fb1e771d4d" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, получающая уведомление.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8b1844d9-dbfc-00f0-4000-07fb1e771d4d" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="32ed3ee3-2e51-4048-a8bb-0b46bc6a17cc" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a88ed4f8-dcce-000a-5000-09defee9949c" Name="pk_NotificationTokenRoles">
		<SchemeIndexedColumn Column="a88ed4f8-dcce-000a-3100-09defee9949c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a88ed4f8-dcce-000a-7000-09defee9949c" Name="idx_NotificationTokenRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a88ed4f8-dcce-010a-4000-09defee9949c" />
	</SchemeIndex>
	<SchemeIndex ID="29b4219d-82ed-41a8-83f2-4862433976b6" Name="ndx_NotificationTokenRoles_RoleID" IsUnique="true">
		<SchemeIndexedColumn Column="8b1844d9-dbfc-00f0-4000-07fb1e771d4d" />
	</SchemeIndex>
</SchemeTable>