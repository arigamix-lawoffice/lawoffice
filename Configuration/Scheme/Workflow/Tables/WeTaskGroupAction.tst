<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="915d8549-af3d-4d44-84a1-cef16ed89941" Name="WeTaskGroupAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для действия Группа заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="915d8549-af3d-0044-2000-0ef16ed89941" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="915d8549-af3d-0144-4000-0ef16ed89941" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="824d390d-686f-4679-be99-039220eab20a" Name="Digest" Type="String(Max) Not Null">
		<Description>Описание задания</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e5fbe9e9-e761-4b65-9000-f195b5efaae1" Name="Period" Type="Double Not Null">
		<Description>Количество дней на выполнение задания</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e9168fa-3c8a-4925-b8f1-2210274eaab8" Name="Planned" Type="DateTime Null" />
	<SchemePhysicalColumn ID="a3e65b3c-2d7e-4d4e-938c-f46cb5cdc12e" Name="Result" Type="String(Max) Not Null">
		<Description>Результат в истории заданий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c55e0e75-e0d9-4ebf-8ada-1a6c6f3450c2" Name="Parallel" Type="Boolean Not Null">
		<Description>Флаг определяет, должна ли производится параллельная отправка заданий</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="dce2d1e8-23fb-43fc-b84c-7c106e3d16b5" Name="df_WeTaskGroupAction_Parallel" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4fa1a6b1-9b1f-4eef-b1db-9714cbadaae4" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип задания</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4fa1a6b1-9b1f-00ef-4000-0714cbadaae4" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="42f9e0f6-b272-4b0b-8fc3-c2d746f7daae" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="59599b4e-19d6-453e-8e12-9f39a48ff043" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="59599b4e-19d6-003e-4000-0f39a48ff043" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="1b480cc9-4d9b-46f1-868d-15b6bba37f82" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2c4b8080-08ad-4eb7-995b-7281df0b80bd" Name="InitTaskScript" Type="String(Max) Null">
		<Description>Скрипт, вызываемый при создании задания</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4268b5f8-f1fd-4f7e-b3bf-5c5a1b469d40" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4268b5f8-f1fd-007e-4000-0c5a1b469d40" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="17fd4956-7086-40e8-b2c0-cb7987497881" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c85670d1-285d-4455-9e62-ae3c0577dc72" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f660d79f-4d19-475f-91cf-cd8805a62720" Name="df_WeTaskGroupAction_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="af9fa120-0fa7-4b60-8e6c-52bcd01ac60e" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="49fc092c-9240-40ed-9372-a9c8180a5a2e" Name="df_WeTaskGroupAction_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="94fbe329-5e3c-47bf-aa52-e12714a77f03" Name="NotificationScript" Type="String(Max) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="915d8549-af3d-0044-5000-0ef16ed89941" Name="pk_WeTaskGroupAction" IsClustered="true">
		<SchemeIndexedColumn Column="915d8549-af3d-0144-4000-0ef16ed89941" />
	</SchemePrimaryKey>
</SchemeTable>