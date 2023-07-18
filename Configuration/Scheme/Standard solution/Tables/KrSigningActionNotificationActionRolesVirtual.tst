<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a6311a94-817e-48e0-afb5-6dca269563d1" Name="KrSigningActionNotificationActionRolesVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6311a94-817e-00e0-2000-0dca269563d1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a6311a94-817e-01e0-4000-0dca269563d1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6311a94-817e-00e0-3100-0dca269563d1" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f54cb4d4-503b-43e7-ba4d-3316031c5603" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f54cb4d4-503b-00e7-4000-0316031c5603" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="f38d434e-7b58-4665-93b4-9a4fc5649151" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="552df13d-0f4b-482a-a63e-abccfe7c1b15" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="b4c6c410-c5cb-40e3-b800-3cd854c94a2c" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="552df13d-0f4b-002a-4000-0bccfe7c1b15" Name="OptionRowID" Type="Guid Not Null" ReferencedColumn="b4c6c410-c5cb-00e3-3100-0cd854c94a2c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6311a94-817e-00e0-5000-0dca269563d1" Name="pk_KrSigningActionNotificationActionRolesVirtual">
		<SchemeIndexedColumn Column="a6311a94-817e-00e0-3100-0dca269563d1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6311a94-817e-00e0-7000-0dca269563d1" Name="idx_KrSigningActionNotificationActionRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a6311a94-817e-01e0-4000-0dca269563d1" />
	</SchemeIndex>
</SchemeTable>