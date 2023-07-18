<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2afee5e8-3582-4c7c-9fcf-1e4fddefe548" Name="KrApprovalActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Согласование".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2afee5e8-3582-007c-2000-0e4fddefe548" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2afee5e8-3582-017c-4000-0e4fddefe548" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2ce3d36b-2309-43c3-b84b-f871a7e083df" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2ce3d36b-2309-00c3-4000-0871a7e083df" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a2962e3a-39d5-4aeb-8d53-dea326d47b46" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c7afdc69-3a2f-41f0-98af-238cde347774" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c7afdc69-3a2f-00f0-4000-038cde347774" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="d766f63c-08c0-4511-b28a-7a4e83f21b64" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9bea4b5b-9fe8-4a24-ba30-ec76b232d8ac" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0fea84be-90f2-4ba9-97dd-04de29cf8ca9" Name="Period" Type="Double Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8fe2286e-bc8f-4f82-81b3-641bd02ad92a" Name="df_KrApprovalActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bd9f25e1-3ebf-4e6c-9768-59dcf666d34a" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="012604b7-0113-4854-b5c1-ed9c2ab9a2f9" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c49740a3-b545-4507-8efb-d055fc3ec4af" Name="Result" Type="String(Max) Not Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="885337e5-49b9-4be0-8eb9-b8aaee19ecd9" Name="IsParallel" Type="Boolean Not Null">
		<Description>Отправлять задания всем исполнителям сразу, а не последовательно в порядке их указания в поле "Согласующие".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b9607249-d4d9-4104-aced-561526f8270a" Name="df_KrApprovalActionVirtual_IsParallel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="942ba859-4ff3-479f-bd28-8de97dbcc90d" Name="ReturnWhenApproved" Type="Boolean Not Null">
		<Description>Отправить процесс на доработку автором при согласовании.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="87c1bd21-23e7-4e62-a3dd-bf7a095bbe92" Name="df_KrApprovalActionVirtual_ReturnWhenApproved" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5686d3ec-148b-4a21-853d-a4e39926fa03" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Разрешено редактировать карточку при наличии задания согласования.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3320967f-d28b-4c1e-a746-66d4b28058ac" Name="df_KrApprovalActionVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0165bf03-f4a9-475e-b00c-a301f4b64955" Name="CanEditAnyFiles" Type="Boolean Not Null">
		<Description>Разрешено редактировать любые файлы при наличии задания согласования. Если флаг не выставлен, то сотрудник, при наличии соответсвующих прав, может редактировать только свои файлы.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="cbd49fee-af1d-46fd-89c0-86474e304ca3" Name="df_KrApprovalActionVirtual_CanEditAnyFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3f37b612-f54e-4aaa-a903-135672aea761" Name="ChangeStateOnStart" Type="Boolean Not Null">
		<Description>Изменять состояние при старте.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ceb15a9c-e849-4ba6-adcc-ff409243e9fa" Name="df_KrApprovalActionVirtual_ChangeStateOnStart" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="03392c55-9a68-4821-a139-bb88b5dc307f" Name="ChangeStateOnEnd" Type="Boolean Not Null">
		<Description>Изменять состояние при завершении.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c99874e1-59aa-4e0a-94a3-1be99579577d" Name="df_KrApprovalActionVirtual_ChangeStateOnEnd" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0ba17231-2852-4fed-a712-2f8fce5be5ba" Name="IsAdvisory" Type="Boolean Not Null">
		<Description>Задание является рекомендательным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2506840e-928a-4547-b4dc-92f557217533" Name="df_KrApprovalActionVirtual_IsAdvisory" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="c37b52fd-1d9c-4a94-8eac-328509b28d3d" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c37b52fd-1d9c-0094-4000-028509b28d3d" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="354f1f5e-119f-4316-982e-3613014377d1" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0fea7569-eab2-49e1-be13-26791ecc00ac" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="22076f57-0027-412e-902d-f743d18dce31" Name="df_KrApprovalActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f26ed1a9-f344-428c-8080-5e00a3e3722a" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ff80aecc-d9f1-483b-ab7c-920206f3bfe2" Name="df_KrApprovalActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="41566618-3148-4aa8-b105-639e09e5a482" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="20341b31-c9e7-4f1d-b01c-97c827dbdf68" Name="SqlPerformersScript" Type="String(Max) Not Null">
		<Description>SQL-скрипт расчёта вычисляемых исполнителей.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ee2b6334-dbe9-4f85-8bb2-3b09efa7c2ff" Name="IsDisableAutoApproval" Type="Boolean Not Null">
		<Description>Отключить автосогласование.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0d8a52d7-6c4f-4cfa-843d-40bfa955b815" Name="df_KrApprovalActionVirtual_IsDisableAutoApproval" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9a50d48e-e378-4c13-aefb-99b8e48e56de" Name="ExpectAllApprovers" Type="Boolean Not Null">
		<Description>Ожидать решения всех согласующих.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="10c779f8-8570-46fe-9699-96b4c68f258c" Name="df_KrApprovalActionVirtual_ExpectAllApprovers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2afee5e8-3582-007c-5000-0e4fddefe548" Name="pk_KrApprovalActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="2afee5e8-3582-017c-4000-0e4fddefe548" />
	</SchemePrimaryKey>
</SchemeTable>