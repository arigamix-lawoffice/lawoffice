<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="1d7d2be8-692e-478d-9ce4-fc791833ffba" Name="KrUniversalTaskActionNotificationRolesVitrual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Настраиваемое задание". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1d7d2be8-692e-008d-2000-0c791833ffba" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1d7d2be8-692e-018d-4000-0c791833ffba" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1d7d2be8-692e-008d-3100-0c791833ffba" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="04f7d664-5751-4e93-8b10-dde1b02f5f64" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется уведомление при завершении задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="04f7d664-5751-0093-4000-0de1b02f5f64" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="ac6708ba-89e0-41f7-934d-3fe3d95e41e9" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e272dff4-3c3f-4d1f-b291-4270a68e2c1c" Name="Button" Type="Reference(Typified) Null" ReferencedTable="e85631c4-0014-4842-86f4-9a6ba66166f3" IsReferenceToOwner="true" WithForeignKey="false">
		<Description>Настраиваемый вариант завершения с которым связан данный список рассылки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e272dff4-3c3f-001f-4000-0270a68e2c1c" Name="ButtonRowID" Type="Guid Null" ReferencedColumn="e85631c4-0014-0042-3100-0a6ba66166f3" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1d7d2be8-692e-008d-5000-0c791833ffba" Name="pk_KrUniversalTaskActionNotificationRolesVitrual">
		<SchemeIndexedColumn Column="1d7d2be8-692e-008d-3100-0c791833ffba" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="1d7d2be8-692e-008d-7000-0c791833ffba" Name="idx_KrUniversalTaskActionNotificationRolesVitrual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="1d7d2be8-692e-018d-4000-0c791833ffba" />
	</SchemeIndex>
</SchemeTable>