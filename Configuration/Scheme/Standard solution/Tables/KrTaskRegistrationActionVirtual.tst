<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="12b90f64-b971-4198-ad0e-0e3d1988f946" Name="KrTaskRegistrationActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Праметры действия "Задание регистрации".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="12b90f64-b971-0098-2000-0e3d1988f946" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="12b90f64-b971-0198-4000-0e3d1988f946" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="54c6826b-cd4a-41f6-8ec1-453f88c7c52a" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="54c6826b-cd4a-00f6-4000-053f88c7c52a" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="4a4e12a1-3643-4797-afa3-adb7edc19544" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a60ec421-4405-49e1-a44e-538c65641b39" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Исполнитель задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a60ec421-4405-00e1-4000-038c65641b39" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="f1e51153-fd3c-486a-872c-f72a895f8c76" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2147a696-2349-4ce1-8f1c-6c5ec533ea73" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2147a696-2349-00e1-4000-0c5ec533ea73" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="86d1e77c-3262-4648-b7c6-27674394b154" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="831b2ba7-0dfb-4ce1-97f7-f798a363146a" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="90f07c0e-66dc-4783-b071-fbddcd98e8d6" Name="Period" Type="Double Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5d8e654b-3bc7-4def-98a5-c54104697951" Name="df_KrTaskRegistrationActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6ebc1402-334a-4c4c-96b3-bdc07b5fb3e9" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d869c1d9-35a4-43db-a0a5-c5c71baccb09" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="51f62a1d-4905-43a1-9848-3cf4709709af" Name="Result" Type="String(Max) Not Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="a1f23a30-aaaa-4e60-9f35-30a41337319e" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a1f23a30-aaaa-0060-4000-00a41337319e" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="32eb6cb0-1c7d-442e-8fdb-11c0e2c62e67" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="886c2503-1cce-44d0-a6d9-95da1beccb46" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8c7a45ea-4ceb-4e8a-89e2-ee5eb82f48ba" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d68ea204-6d2c-48c5-9bb6-231eaa981dc6" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8a2cd034-b3a8-4f21-aca0-77fc277fafc7" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Разрешено редактировать карточку при наличии задания регистрации.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="15597452-4dd9-4b17-a6cb-2a20d9c264a9" Name="df_KrTaskRegistrationActionVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2fea4fe6-5db6-4c3e-898c-b4140e9e84d4" Name="CanEditAnyFiles" Type="Boolean Not Null">
		<Description>Разрешено редактировать любые файлы при наличии задания регистрации. Если флаг не выставлен, то сотрудник, при наличии соответсвующих прав, может редактировать только свои файлы.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c3559d1b-5ffa-40c9-9466-46d16496dbab" Name="df_KrTaskRegistrationActionVirtual_CanEditAnyFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="12b90f64-b971-0098-5000-0e3d1988f946" Name="pk_KrTaskRegistrationActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="12b90f64-b971-0198-4000-0e3d1988f946" />
	</SchemePrimaryKey>
</SchemeTable>