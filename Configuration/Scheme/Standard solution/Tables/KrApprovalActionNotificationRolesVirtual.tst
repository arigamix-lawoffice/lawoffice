<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ae419e33-eb19-456c-a319-54da9ace8821" Name="KrApprovalActionNotificationRolesVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Таблица с обрабатываемыми вариантами завершения задания действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae419e33-eb19-006c-2000-04da9ace8821" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ae419e33-eb19-016c-4000-04da9ace8821" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae419e33-eb19-006c-3100-04da9ace8821" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="768380ea-e6e0-4275-9f1a-fb9e78d51368" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="768380ea-e6e0-0075-4000-0b9e78d51368" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="309041a3-ba86-443c-954a-7093fcf3a900" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8cd8ad54-7bfc-47d6-abd3-82d02aa9f271" Name="Option" Type="Reference(Typified) Null" ReferencedTable="cea61a5b-0420-41ba-a5f2-e21c21c30f5a" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8cd8ad54-7bfc-00d6-4000-02d02aa9f271" Name="OptionRowID" Type="Guid Null" ReferencedColumn="cea61a5b-0420-00ba-3100-021c21c30f5a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae419e33-eb19-006c-5000-04da9ace8821" Name="pk_KrApprovalActionNotificationRolesVirtual">
		<SchemeIndexedColumn Column="ae419e33-eb19-006c-3100-04da9ace8821" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae419e33-eb19-006c-7000-04da9ace8821" Name="idx_KrApprovalActionNotificationRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ae419e33-eb19-016c-4000-04da9ace8821" />
	</SchemeIndex>
</SchemeTable>