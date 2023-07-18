<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="d0771103-6c21-4602-af56-d264e82f8b57" Name="KrWeEditInterjectOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры доработки автором.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0771103-6c21-0002-2000-0264e82f8b57" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d0771103-6c21-0102-4000-0264e82f8b57" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a77313f9-fc16-4d4e-b437-99df1471f027" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a77313f9-fc16-004e-4000-09df1471f027" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="8039de7b-d94c-4c2a-bb38-8064a5e8f879" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b3e90ef8-939a-465e-af3a-a5e8d70489de" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль на которую отправляется задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b3e90ef8-939a-005e-4000-05e8d70489de" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="365d5440-eaa6-41f1-abf2-9db143b13d6c" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a56b7852-c38b-4cbc-9661-934d980d0e0c" Name="Kind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<Description>Вид задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a56b7852-c38b-00bc-4000-034d980d0e0c" Name="KindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="6c7ba16e-67a9-4676-805f-b80ea9c74b11" Name="KindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="83c6c4bd-bd3d-4891-8ac7-b8ffd9cd6451" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f8501e54-ecfa-4cfe-9993-8305939dae66" Name="Period" Type="Double Not Null">
		<Description>Количество дней на выполнение задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="491c7e49-7240-400f-97c2-ab4697e8e1d2" Name="df_KrWeEditInterjectOptionsVirtual_Period" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="274dc9a6-e377-402f-840d-818ed4a3f367" Name="Planned" Type="DateTime Null">
		<Description>Дата, до которой должно быть исполнено задание.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="c7d10caa-1fd1-48ad-9783-0a03a5c4d5f1" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c7d10caa-1fd1-00ad-4000-0a03a5c4d5f1" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="d61e1bd3-50fa-47a6-af0f-7d42f24bd4d7" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b9bb59f2-7905-44a0-8bfb-08d210363199" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="089f69b7-03b3-434b-9a1b-0690fafbf599" Name="df_KrWeEditInterjectOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0aceb248-0aaa-45d0-8ba2-66edcf6fbd54" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="458eb42e-4eea-48ca-8b58-4a073c8103a2" Name="df_KrWeEditInterjectOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b49743dc-50b7-4c68-ad9c-82c13052eed8" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7424e368-ba48-4f44-b59d-4ddb88748e2a" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d0771103-6c21-0002-5000-0264e82f8b57" Name="pk_KrWeEditInterjectOptionsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="d0771103-6c21-0102-4000-0264e82f8b57" />
	</SchemePrimaryKey>
</SchemeTable>