<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="585825ed-e297-4eb3-bea2-a732ad75c6b6" Name="DateFormats" Group="System">
	<Description>Формат для отображаемых дат, определяет порядок следования дня, месяца и года.</Description>
	<SchemePhysicalColumn ID="8921cfa1-255c-4604-a13a-a92cf8c96aaa" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="e9727480-97b2-45ad-9cdf-2ee8382c5bf3" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047" Name="Caption" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="02298dae-63a9-4b58-8bf5-45c00537a640" Name="pk_DateFormats">
		<SchemeIndexedColumn Column="8921cfa1-255c-4604-a13a-a92cf8c96aaa" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="8921cfa1-255c-4604-a13a-a92cf8c96aaa">0</ID>
		<Name ID="e9727480-97b2-45ad-9cdf-2ee8382c5bf3">MonthDayYear</Name>
		<Caption ID="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047">$Enum_DateFormats_MonthDayYear</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8921cfa1-255c-4604-a13a-a92cf8c96aaa">1</ID>
		<Name ID="e9727480-97b2-45ad-9cdf-2ee8382c5bf3">DayMonthYear</Name>
		<Caption ID="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047">$Enum_DateFormats_DayMonthYear</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8921cfa1-255c-4604-a13a-a92cf8c96aaa">2</ID>
		<Name ID="e9727480-97b2-45ad-9cdf-2ee8382c5bf3">YearMonthDay</Name>
		<Caption ID="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047">$Enum_DateFormats_YearMonthDay</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8921cfa1-255c-4604-a13a-a92cf8c96aaa">3</ID>
		<Name ID="e9727480-97b2-45ad-9cdf-2ee8382c5bf3">YearDayMonth</Name>
		<Caption ID="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047">$Enum_DateFormats_YearDayMonth</Caption>
	</SchemeRecord>
</SchemeTable>