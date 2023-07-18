<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b0ca69b1-7c90-4ce7-995c-2f9540ec45ef" Name="KrUniversalTaskActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Настраиваемое задание".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b0ca69b1-7c90-00e7-2000-0f9540ec45ef" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b0ca69b1-7c90-01e7-4000-0f9540ec45ef" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7e54fc73-494f-439a-8bdf-2e763d4c174c" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7e54fc73-494f-009a-4000-0e763d4c174c" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="0621dbfa-0ca6-4537-a793-df84fddf2ead" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="56c54bce-ad54-4a63-9f52-dce3a5ba4d4c" Name="Kind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="56c54bce-ad54-0063-4000-0ce3a5ba4d4c" Name="KindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="69a585f6-03c3-43eb-a126-062ee70dee4d" Name="KindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ca0e0761-1d53-4d1c-9d15-31dbe2c764f3" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6a2b3c93-cad9-4857-bba4-338879d40e66" Name="Period" Type="Double Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8d9b1214-f194-47a4-8963-fe26b17dd3df" Name="df_KrUniversalTaskActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e89e4d2c-4dde-4b11-8a42-a520505206a3" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="02cd52a4-448c-48fa-bcfb-7b87d6dab36c" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="76e48538-61f5-4148-b1a5-8f6753298874" Name="Result" Type="String(Max) Not Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a7f43086-e034-458f-b2fa-a904b9d4f381" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Разрешено редактировать карточку при наличии задания согласования.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5ade4add-334e-400b-9328-4e298bcbe984" Name="df_KrUniversalTaskActionVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8a38ee21-7d49-4e3b-bf2d-863f1c11aff9" Name="CanEditAnyFiles" Type="Boolean Not Null">
		<Description>Разрешено редактировать любые файлы при наличии задания согласования. Если флаг не выставлен, то сотрудник, при наличии соответсвующих прав, может редактировать только свои файлы.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="4f1711ff-044d-409f-bea2-9217bf20b6c3" Name="df_KrUniversalTaskActionVirtual_CanEditAnyFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d4bf617c-0321-409a-b0e0-5a3bca4cb448" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d4bf617c-0321-009a-4000-0a3bca4cb448" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="8ed1cdae-a494-442a-bb2a-c6cb1f0b9c73" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1fca1a9b-c1ae-4475-9e4f-4b2d9d905361" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="292aa481-7514-4a4e-98fb-603cba10f2ff" Name="df_KrUniversalTaskActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a8e17c52-3833-4b4e-bc1f-280e7b0d564f" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5875e9c5-8130-44ad-824e-c478cd9ff90e" Name="df_KrUniversalTaskActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="945e184d-9fd5-410d-ae40-21bce6c33205" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="adac7469-bb03-4043-9604-87baba9e46d0" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Исполнитель задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="adac7469-bb03-0043-4000-07baba9e46d0" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="ec51f0fb-ac12-48dc-913c-4c67b2767529" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b0ca69b1-7c90-00e7-5000-0f9540ec45ef" Name="pk_KrUniversalTaskActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="b0ca69b1-7c90-01e7-4000-0f9540ec45ef" />
	</SchemePrimaryKey>
</SchemeTable>