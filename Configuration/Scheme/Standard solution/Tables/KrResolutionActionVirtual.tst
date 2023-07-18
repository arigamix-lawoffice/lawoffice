<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="aed41831-bbfd-4637-8e5b-c9b69c9ca7f1" Name="KrResolutionActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Выполнение задачи".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="aed41831-bbfd-0037-2000-09b69c9ca7f1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aed41831-bbfd-0137-4000-09b69c9ca7f1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3aa36b62-5604-4499-9cde-05f08866a9e2" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания - От имени кого было отправлено задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3aa36b62-5604-0099-4000-05f08866a9e2" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="594046e2-8d6b-4237-b3d8-36383e1351f6" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e5912dfa-3d50-460d-80a1-0e51a1ebf887" Name="Kind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e5912dfa-3d50-000d-4000-0e51a1ebf887" Name="KindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="763d950a-587a-46a0-90db-4394c67a2c6b" Name="KindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d93a0712-0d10-4836-8bbf-186af46ea861" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c7db003c-343f-4781-87f1-e7f491f3b94e" Name="Period" Type="Double Null">
		<Description>Дайджест задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b67435a9-601b-4408-a545-7573261b93b5" Name="df_KrResolutionActionVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dc05c91d-3401-4ae2-bc0f-a037bee5f7f8" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7c2e7c42-9173-42c1-9a27-010e9b2f9b98" Name="IsMajorPerformer" Type="Boolean Not Null">
		<Description>Первый исполнитель - ответсвенный.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="53724f44-09f0-4348-a434-97a9dcc3221e" Name="df_KrResolutionActionVirtual_IsMajorPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4c807309-f326-49e5-b542-b9b3a9d7fd61" Name="IsMassCreation" Type="Boolean Not Null">
		<Description>Отдельная задача каждому исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="931f8135-4034-419c-9421-1b7da6e5d7e8" Name="df_KrResolutionActionVirtual_IsMassCreation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a771cae9-fd88-4632-8b68-ac868b3d110b" Name="WithControl" Type="Boolean Not Null">
		<Description>Вернуть после завершения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="38447014-ce28-4772-8d95-cc276ffd4879" Name="df_KrResolutionActionVirtual_WithControl" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="088cba44-e531-4e4c-b585-cd38655c64a8" Name="Controller" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Контроллёр. Роль, на которую будет возвращено задание после завершения задания при установленно флаге "Вернуть после завершения" (WithControl) или значение null, если необходимо, что бы задание вернулось в работу автору.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="088cba44-e531-004c-4000-0d38655c64a8" Name="ControllerID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a9c7609a-1eca-4b8e-a1b8-73f43bb81b7c" Name="ControllerName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ae26094f-3f99-4926-888f-d195c8ed6520" Name="SqlPerformersScript" Type="String(Max) Not Null">
		<Description>SQL-скрипт расчёта вычисляемых исполнителей.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="79a9fead-91ed-49ca-9dc3-aa397616ddee" Name="Sender" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Отправитель - тот, кто отправил задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="79a9fead-91ed-00ca-4000-0a397616ddee" Name="SenderID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="34ceb86e-b7bd-4e48-8253-72609358ae97" Name="SenderName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="aed41831-bbfd-0037-5000-09b69c9ca7f1" Name="pk_KrResolutionActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="aed41831-bbfd-0137-4000-09b69c9ca7f1" />
	</SchemePrimaryKey>
</SchemeTable>