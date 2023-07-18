<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3cf60a31-28d4-42ad-86b2-343a298ea7a8" Name="SchedulingTypes" Group="Roles">
	<Description>Способы указания расписания для выполнения заданий.</Description>
	<SchemePhysicalColumn ID="86ffec88-74e3-4f9b-84fb-aa917ec217ff" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор способа указания расписания для выполнения заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="90b8d68f-27e5-4a03-9424-6f9cc408c260" Name="Name" Type="String(128) Not Null">
		<Description>Имя способа указания расписания для выполнения заданий (для информативности).</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="8403b987-981d-4e21-819b-61617e7c985c" Name="pk_SchedulingTypes">
		<SchemeIndexedColumn Column="86ffec88-74e3-4f9b-84fb-aa917ec217ff" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="86ffec88-74e3-4f9b-84fb-aa917ec217ff">0</ID>
		<Name ID="90b8d68f-27e5-4a03-9424-6f9cc408c260">Period</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="86ffec88-74e3-4f9b-84fb-aa917ec217ff">1</ID>
		<Name ID="90b8d68f-27e5-4a03-9424-6f9cc408c260">Cron</Name>
	</SchemeRecord>
</SchemeTable>