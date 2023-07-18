<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="747bb53c-9e47-418d-892d-fb52a18eb42d" Name="RoleGenerators" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Генераторы метаролей.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="747bb53c-9e47-008d-2000-0b52a18eb42d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="747bb53c-9e47-018d-4000-0b52a18eb42d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c843b06f-365a-414f-bc07-e265e8c45b19" Name="Name" Type="String(128) Not Null">
		<Description>Имя генератора метаролей.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3fdaa61f-0833-4161-b429-c1c4e37e8df8" Name="SqlText" Type="String(Max) Not Null">
		<Description>Текст SQL-запроса, возвращающего метароли и их состав.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="48942c7c-b49e-4126-8a26-69156fe36b83" Name="SchedulingType" Type="Reference(Typified) Not Null" ReferencedTable="3cf60a31-28d4-42ad-86b2-343a298ea7a8">
		<Description>Способ указания расписания для выполнения заданий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="48942c7c-b49e-0026-4000-09156fe36b83" Name="SchedulingTypeID" Type="Int16 Not Null" ReferencedColumn="86ffec88-74e3-4f9b-84fb-aa917ec217ff">
			<Description>Идентификатор способа указания расписания для выполнения заданий.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f28aec70-d06a-433c-8f05-21c2b5f9a120" Name="CronScheduling" Type="String(32) Null" IsSparse="true">
		<Description>Строка Cron, определяющая расписание пересчёта роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="57c827ef-796f-4dbd-bd78-c22ba76dc09e" Name="PeriodScheduling" Type="Int32 Null" IsSparse="true">
		<Description>Интервал в секундах, определяющий период пересчёта состава роли в секундах.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3ab5a55b-3318-4b44-8902-b27e73f57131" Name="LastErrorDate" Type="DateTime Null" IsSparse="true">
		<Description>Дата и время сообщения об ошибке, произошедшей при последней генерации метаролей, или NULL, если ошибок не было.
Дата должна указываться в формате UTC (Coordinated Universal Time).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="87364336-f80f-489d-9e3b-685c6f7f21a5" Name="LastErrorText" Type="String(256) Null">
		<Description>Текст сообщения об ошибке, произошедшей при последней генерации метаролей, или NULL, если ошибок не было.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6ff2eb8b-ff51-4abb-b026-afceaedb2db1" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="db5b019f-d256-430e-bcef-2d74c4635c24" Name="LastSuccessfulRecalcDate" Type="DateTime Null">
		<Description>Дата и время последнего успешного расчёта.
Дата должна указываться в формате UTC (Coordinated Universal Time).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9eeffec6-56e4-486c-8939-6716e94e5001" Name="ScheduleAtLaunch" Type="Boolean Not Null">
		<Description>Запланировать пересчёт при запуске Chronos. Актуально при задании расписания строкой CRON.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b4d97498-2221-4838-815d-49e4393bd232" Name="df_RoleGenerators_ScheduleAtLaunch" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e550a92f-0644-4620-863b-659067c4dff4" Name="DisableDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="505292e1-9e5f-4f7f-8bac-0d4efc5f5cab" Name="df_RoleGenerators_DisableDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="747bb53c-9e47-008d-5000-0b52a18eb42d" Name="pk_RoleGenerators" IsClustered="true">
		<SchemeIndexedColumn Column="747bb53c-9e47-018d-4000-0b52a18eb42d" />
	</SchemePrimaryKey>
	<SchemeIndex ID="fc22999d-86dc-42e0-87c0-134e85e62795" Name="ndx_RoleGenerators_Name" IsUnique="true">
		<SchemeIndexedColumn Column="c843b06f-365a-414f-bc07-e265e8c45b19">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>