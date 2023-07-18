<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="7836e13c-4ebf-47f2-8968-504ab0d2fce4" Name="KrSigningActionNotificationRolesVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Подписание". Таблица с обрабатываемыми вариантами завершения задания действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7836e13c-4ebf-00f2-2000-004ab0d2fce4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7836e13c-4ebf-01f2-4000-004ab0d2fce4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7836e13c-4ebf-00f2-3100-004ab0d2fce4" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a1a8f868-e204-483b-aac9-c343a66b8b62" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a1a8f868-e204-003b-4000-0343a66b8b62" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="4114faec-3442-4b34-871c-92d0f59ae5f5" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8c746cd8-af32-42b2-a892-b67ee2c6d738" Name="Option" Type="Reference(Typified) Null" ReferencedTable="3f87675e-0a60-4ece-a5c9-3c203e2c9ffb" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8c746cd8-af32-00b2-4000-067ee2c6d738" Name="OptionRowID" Type="Guid Null" ReferencedColumn="3f87675e-0a60-00ce-3100-0c203e2c9ffb" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7836e13c-4ebf-00f2-5000-004ab0d2fce4" Name="pk_KrSigningActionNotificationRolesVirtual">
		<SchemeIndexedColumn Column="7836e13c-4ebf-00f2-3100-004ab0d2fce4" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7836e13c-4ebf-00f2-7000-004ab0d2fce4" Name="idx_KrSigningActionNotificationRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7836e13c-4ebf-01f2-4000-004ab0d2fce4" />
	</SchemeIndex>
</SchemeTable>