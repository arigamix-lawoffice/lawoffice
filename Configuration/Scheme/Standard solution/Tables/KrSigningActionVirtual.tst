<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="baaceebe-011d-4f1a-9431-00c4f8b233b9" Name="KrSigningActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Подписание".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="baaceebe-011d-001a-2000-00c4f8b233b9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="baaceebe-011d-011a-4000-00c4f8b233b9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="76f49545-fb5b-458b-ab74-ab8a43762475" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="76f49545-fb5b-008b-4000-0b8a43762475" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="2bd93c31-b116-48c1-ad2e-70aa05ec134d" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="281c75c5-905f-4bff-b010-f8d445ee444f" Name="Kind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="281c75c5-905f-00ff-4000-08d445ee444f" Name="KindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="820fe5c9-a066-452a-8345-a00b78bb5473" Name="KindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="954b82fb-882f-4ed5-93fb-74ff505f6396" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="144b7023-3b4b-4074-98e9-36feb9592a75" Name="Period" Type="Double Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="996fe6d9-9eb2-4d37-bef1-a769153ed76f" Name="df_KrSigningActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3f6e4d0f-c88c-4f49-8eab-38c3388d2558" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a94bc685-a92b-4616-b285-9e4b7c6f5ef5" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9ff032b6-6f12-486e-ac88-87c38c56ac55" Name="Result" Type="String(Max) Not Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d4f65a11-773d-4f05-8952-6996accb4d15" Name="IsParallel" Type="Boolean Not Null">
		<Description>Отправлять задания всем исполнителям сразу, а не последовательно в порядке их указания в поле "Согласующие".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="767dce2a-00bc-4527-a0cb-9d074f51471b" Name="df_KrSigningActionVirtual_IsParallel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a0915424-947e-484d-9126-1f768a07bbca" Name="ReturnWhenApproved" Type="Boolean Not Null">
		<Description>Отправить процесс на доработку автором при согласовании.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3e04d071-e275-4406-88ef-5c40795cdad6" Name="df_KrSigningActionVirtual_ReturnWhenApproved" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cd207e6c-4be9-45ef-a76d-a0de3c41064b" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Разрешено редактировать карточку при наличии задания подписания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d91ef582-6607-45d5-867a-517953b76ca9" Name="df_KrSigningActionVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5026467e-c3c6-4925-81e3-6afdf865f347" Name="CanEditAnyFiles" Type="Boolean Not Null">
		<Description>Разрешено редактировать любые файлы при наличии задания подписания. Если флаг не выставлен, то сотрудник, при наличии соответсвующих прав, может редактировать только свои файлы.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="46f600ef-ebf1-444d-9f66-70f39957057b" Name="df_KrSigningActionVirtual_CanEditAnyFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2d385d8c-378d-4b34-83bc-a3e74e9cd7c2" Name="ChangeStateOnStart" Type="Boolean Not Null">
		<Description>Изменять состояние при старте.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="266924b4-d39f-4162-9500-f2a24347bf3a" Name="df_KrSigningActionVirtual_ChangeStateOnStart" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9a3ce3e8-55bf-4d70-8518-2d402ef9ff9c" Name="ChangeStateOnEnd" Type="Boolean Not Null">
		<Description>Изменять состояние при завершении.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="293baa05-4879-4916-9823-b5520dec6f12" Name="df_KrSigningActionVirtual_ChangeStateOnEnd" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="10aa0561-4a7a-45a6-9ebd-ec39a39cac13" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="10aa0561-4a7a-00a6-4000-0c39a39cac13" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="ebfcc8e5-af2a-4265-aa96-5cc396c1d44a" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8440335b-ec41-4a40-aae3-0a9910c95b40" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="fa313ef2-9508-49df-a325-277a894d0c07" Name="df_KrSigningActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c860497c-6ffb-47ed-b19c-7c2ff3dc6390" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7e0cb108-1f81-4962-bfb9-fd17ae329ace" Name="df_KrSigningActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="372d1b19-0a02-4bd3-99c3-5b296cc92f34" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="75b2f1b3-3875-4482-9f26-a53888b1ce19" Name="SqlPerformersScript" Type="String(Max) Not Null">
		<Description>SQL-скрипт расчёта вычисляемых исполнителей.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b865927f-2817-4d45-b197-8d8787aa0ef6" Name="ExpectAllSigners" Type="Boolean Not Null">
		<Description>Ожидать решения всех подписантов.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="48a95a31-b26c-4d2b-8ec6-91ee96e9bd04" Name="df_KrSigningActionVirtual_ExpectAllSigners" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7e8284b1-c3d1-4f55-8b3e-2cd7b4847f2f" Name="AllowAdditionalApproval" Type="Boolean Not Null">
		<Description>Признак того, что разрешено дополнительное согласование.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e2b2325d-db9f-407c-acc7-9a99e08a71cc" Name="df_KrSigningActionVirtual_AllowAdditionalApproval" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="baaceebe-011d-001a-5000-00c4f8b233b9" Name="pk_KrSigningActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="baaceebe-011d-011a-4000-00c4f8b233b9" />
	</SchemePrimaryKey>
</SchemeTable>