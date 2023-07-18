<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9cf234bf-ca74-46e7-a91a-564526cc1517" Name="KrAmendingActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Доработка".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9cf234bf-ca74-00e7-2000-064526cc1517" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9cf234bf-ca74-01e7-4000-064526cc1517" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="eb0dbd50-7149-41e1-a865-c05d4c7b4a2c" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eb0dbd50-7149-00e1-4000-005d4c7b4a2c" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="2f62131f-d3eb-4ffd-ba32-ed3c7fc9dffc" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="57820160-9b20-425e-918c-a8146e8ed787" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="57820160-9b20-005e-4000-08146e8ed787" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="6a5d5e2c-3655-44bd-ac21-8263031e53ba" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="cad7b3ca-3d3a-4bcc-94e0-1d483af9f104" Name="Kind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cad7b3ca-3d3a-00cc-4000-0d483af9f104" Name="KindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="ec1826c9-b7fd-4295-828e-43402a27a786" Name="KindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="82352ab3-f9f4-427f-95e4-f9169fc28794" Name="Digest" Type="String(Max) Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d0fe183d-96d5-4da0-8149-b674b5b504d2" Name="Period" Type="Double Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="045dec93-8155-4c86-bbe9-cb76ed64a870" Name="df_KrAmendingActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9148a176-3da6-42f3-bd04-eeb87d4ed63c" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="151a1cef-049e-41f1-b7de-6685b6e175cf" Name="IsChangeState" Type="Boolean Not Null">
		<Description>Изменять состояние документа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="60b62d4d-5a2a-46a5-b791-fc52d0156af0" Name="df_KrAmendingActionVirtual_IsChangeState" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ccca1652-b07d-4234-b6a0-a485fa6e1b8c" Name="IsIncrementCycle" Type="Boolean Not Null">
		<Description>Увеличить цикл согласования.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6db1bc3e-b32e-4b35-aa0b-f7243d9a5326" Name="df_KrAmendingActionVirtual_IsIncrementCycle" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="070833cb-dd22-4125-ad36-4bbbf6c93346" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="070833cb-dd22-0025-4000-0bbbf6c93346" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="e7b9b28d-2135-4213-92f5-d8e5041b2115" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c37de76d-614e-48f1-a82b-d797eaf52f00" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="748d85e2-0bfc-46af-ab77-f607a0b0782e" Name="df_KrAmendingActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b0281540-1858-471a-8491-576c3a9f00b6" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ce17306e-325b-4893-ade1-abf0a5f2a696" Name="df_KrAmendingActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b02223e7-08ad-4db8-93a1-dc79a1420124" Name="NotificationScript" Type="String(Max) Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="42de1855-6b81-4662-8752-238a343cdae2" Name="InitTaskScript" Type="String(Max) Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a5704ae1-c9c2-483a-acdc-0178943252a4" Name="Result" Type="String(Max) Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="77fffb65-5ab5-4f37-8ed4-3d6a3e75fb76" Name="CompleteOptionTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий выполняющися при завершении задания.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="0009b246-935e-456c-b036-19c340c068b1" Name="CompleteOptionNotification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление при завершении задания. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0009b246-935e-006c-4000-09c340c068b1" Name="CompleteOptionNotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="d499ac1d-0587-4111-9ba5-98e4bc0a7e29" Name="CompleteOptionNotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1571434c-60bb-4faf-8003-ea5e5840501d" Name="CompleteOptionExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление при завершении задания. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1480773c-e007-42cf-8d48-66b65c2bb2ff" Name="df_KrAmendingActionVirtual_CompleteOptionExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b32c57ce-398a-4a53-b9e2-5a1b64f11c90" Name="CompleteOptionExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление при завершении задания. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7a559cc0-52c9-4da7-9cff-e93d1470acc9" Name="df_KrAmendingActionVirtual_CompleteOptionExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4d85f3fd-4755-4076-b44c-863c5e1e513a" Name="CompleteOptionNotificationScript" Type="String(Max) Null">
		<Description>Уведомление при завершении задания. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="07a5fc84-e3f1-4a88-a08e-df47308ef4e3" Name="CompleteOptionSendToPerformer" Type="Boolean Not Null">
		<Description>Уведомление при завершении задания. Отправлять исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a99d4a43-6bc3-4b3c-8da1-f517a312415e" Name="df_KrAmendingActionVirtual_CompleteOptionSendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="743d6152-b662-42ac-b880-57c20bb0f9cb" Name="CompleteOptionSendToAuthor" Type="Boolean Not Null">
		<Description>Уведомление при завершении задания. Отправлять автору.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d407146f-dc3e-484a-8b19-885e3dd8bb01" Name="df_KrAmendingActionVirtual_CompleteOptionSendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9cf234bf-ca74-00e7-5000-064526cc1517" Name="pk_KrAmendingActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="9cf234bf-ca74-01e7-4000-064526cc1517" />
	</SchemePrimaryKey>
</SchemeTable>