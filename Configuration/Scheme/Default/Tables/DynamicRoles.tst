<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="4a282d48-6d78-4923-85e4-8d3f9be213fa" Name="DynamicRoles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Динамические роли.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4a282d48-6d78-0023-2000-0d3f9be213fa" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4a282d48-6d78-0123-4000-0d3f9be213fa" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0eded7e2-f8b9-43b5-ad3b-63be0e60e402" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b6e22bd3-3b0e-47f7-995a-0785a820d63c" Name="SqlText" Type="String(Max) Not Null">
		<Description>Текст SQL запроса, формирующего состав динамической роли.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="788f20a1-550a-45a5-878e-e95191b92e67" Name="SchedulingType" Type="Reference(Typified) Not Null" ReferencedTable="3cf60a31-28d4-42ad-86b2-343a298ea7a8">
		<Description>Способ указания расписания для выполнения заданий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="788f20a1-550a-00a5-4000-095191b92e67" Name="SchedulingTypeID" Type="Int16 Not Null" ReferencedColumn="86ffec88-74e3-4f9b-84fb-aa917ec217ff">
			<Description>Идентификатор способа указания расписания для выполнения заданий.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="01191e1b-99ca-445d-a028-16ecf2b2cd99" Name="CronScheduling" Type="String(32) Null" IsSparse="true">
		<Description>Строка Cron, определяющая расписание пересчёта роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f6ac2913-f370-48bb-a5df-392acf5da6fb" Name="PeriodScheduling" Type="Int32 Null" IsSparse="true">
		<Description>Интервал в секундах, определяющий период пересчёта состава роли в секундах.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e3bc421a-0f51-4e59-8dae-6bc5c441d471" Name="LastErrorDate" Type="DateTime Null">
		<Description>Дата и время сообщения об ошибке, произошедшей при последней генерации состава динамической роли, или NULL, если ошибок не было.
Дата должна указываться в формате UTC (Coordinated Universal Time).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ec3caa66-e83f-421f-b902-28f1640061e0" Name="LastErrorText" Type="String(256) Null">
		<Description>Текст сообщения об ошибке, произошедшей при последней генерации состава динамической роли, или NULL, если ошибок не было.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8d985b02-2159-480a-a3a0-138b3d21c781" Name="LastSuccessfulRecalcDate" Type="DateTime Null">
		<Description>Дата и время последнего успешного расчёта.
Дата должна указываться в формате UTC (Coordinated Universal Time).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="02d54a50-b969-4aaf-9341-cfc8c63efd63" Name="ScheduleAtLaunch" Type="Boolean Not Null">
		<Description>Запланировать пересчёт при запуске Chronos. Актуально при задании расписания строкой CRON.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="54b6c425-cb53-47ab-89d5-85c9343ccc17" Name="df_DynamicRoles_ScheduleAtLaunch" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4a282d48-6d78-0023-5000-0d3f9be213fa" Name="pk_DynamicRoles" IsClustered="true">
		<SchemeIndexedColumn Column="4a282d48-6d78-0123-4000-0d3f9be213fa" />
	</SchemePrimaryKey>
</SchemeTable>