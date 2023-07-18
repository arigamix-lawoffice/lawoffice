<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="abc045c0-75b1-4a25-8d9e-d6b323118f08" Name="KrWeRequestCommentOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры запроса комментария.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="abc045c0-75b1-0025-2000-06b323118f08" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="abc045c0-75b1-0125-4000-06b323118f08" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="d8302662-05c9-4e1c-b951-73d9a67ca8e4" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d8302662-05c9-001c-4000-03d9a67ca8e4" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="5bc6f85e-b567-4c6c-9775-f263a337e5cf" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="688f1e47-86fb-4947-ba09-e21332bbc471" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9cf21059-dc09-42f8-afc2-7237457ed754" Name="df_KrWeRequestCommentOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="32eb0d05-51ac-4ef9-b519-9ba467f1ddbb" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0c091cae-b145-4f73-8b90-e5b4d5870381" Name="df_KrWeRequestCommentOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4497395b-5a0e-45d1-a955-bad45b3e81d4" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="789faca2-a78b-417f-b8bf-22dfb4a61d78" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="abc045c0-75b1-0025-5000-06b323118f08" Name="pk_KrWeRequestCommentOptionsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="abc045c0-75b1-0125-4000-06b323118f08" />
	</SchemePrimaryKey>
</SchemeTable>