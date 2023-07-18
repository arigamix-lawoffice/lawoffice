<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="d299cae6-f32d-48b5-8930-031e78b3a2a1" Name="KrApprovalActionNotificationActionRolesVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d299cae6-f32d-00b5-2000-031e78b3a2a1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d299cae6-f32d-01b5-4000-031e78b3a2a1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d299cae6-f32d-00b5-3100-031e78b3a2a1" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b8210e9d-3c0a-4d21-ba3d-57be7f552f52" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b8210e9d-3c0a-0021-4000-07be7f552f52" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="629ad4cf-da31-458b-bfe4-6a878b6a9083" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2fb7e48b-b12b-4b4c-87b7-01a9f20b8538" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="244719bf-4d4a-4df6-b2fe-a00b1bf6d173" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2fb7e48b-b12b-004c-4000-01a9f20b8538" Name="OptionRowID" Type="Guid Not Null" ReferencedColumn="244719bf-4d4a-00f6-3100-000b1bf6d173" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d299cae6-f32d-00b5-5000-031e78b3a2a1" Name="pk_KrApprovalActionNotificationActionRolesVirtual">
		<SchemeIndexedColumn Column="d299cae6-f32d-00b5-3100-031e78b3a2a1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d299cae6-f32d-00b5-7000-031e78b3a2a1" Name="idx_KrApprovalActionNotificationActionRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d299cae6-f32d-01b5-4000-031e78b3a2a1" />
	</SchemeIndex>
</SchemeTable>