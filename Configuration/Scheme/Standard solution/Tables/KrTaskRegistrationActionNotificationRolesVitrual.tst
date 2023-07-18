<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="406b3337-8cfc-437d-a7fc-408b96a92c00" Name="KrTaskRegistrationActionNotificationRolesVitrual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Задание регистрации". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="406b3337-8cfc-007d-2000-008b96a92c00" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="406b3337-8cfc-017d-4000-008b96a92c00" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="406b3337-8cfc-007d-3100-008b96a92c00" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="cc0ff8fe-4d6f-4cf1-87c9-ebd1ff75d946" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cc0ff8fe-4d6f-00f1-4000-0bd1ff75d946" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="89fa2354-4273-4f33-a79e-fa80cd954f38" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="71f6eafd-fac4-425f-9e8d-50f770f495c9" Name="Option" Type="Reference(Typified) Null" ReferencedTable="2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="71f6eafd-fac4-005f-4000-00f770f495c9" Name="OptionRowID" Type="Guid Null" ReferencedColumn="2ba2b1a3-b8ad-0047-3100-0a3fa421c7a9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="406b3337-8cfc-007d-5000-008b96a92c00" Name="pk_KrTaskRegistrationActionNotificationRolesVitrual">
		<SchemeIndexedColumn Column="406b3337-8cfc-007d-3100-008b96a92c00" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="406b3337-8cfc-007d-7000-008b96a92c00" Name="idx_KrTaskRegistrationActionNotificationRolesVitrual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="406b3337-8cfc-017d-4000-008b96a92c00" />
	</SchemeIndex>
</SchemeTable>