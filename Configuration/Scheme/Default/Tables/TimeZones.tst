<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="984e22bf-78fc-4c69-b1a6-ca73341c36ea" Name="TimeZones" Group="System">
	<SchemePhysicalColumn ID="2aa45b0b-2eb1-40c7-85e9-812b59053f63" Name="ID" Type="Int16 Not Null">
		<Description>Id зоны</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a55a67f8-6b64-4f1c-8c04-4b8b6d3be55b" Name="CodeName" Type="String(256) Not Null">
		<Description>Код зоны. Это строка, общепринятый идентификатор зоны, известный .net. Например, для Москвы это "Russian Standard Time".</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d08567f5-3a73-4431-8e15-65b99ec110ec" Name="UtcOffsetMinutes" Type="Int32 Not Null">
		<Description>Смещение относительно UTC в минутах с учётом направления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cfb5009c-145d-4f67-9a4c-f1f651fc1431" Name="DisplayName" Type="String(256) Not Null">
		<Description>Строка, содержит константу локализации для справочника зон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="05ba6f34-73ea-4d4e-8ce0-8c7ed5ba8598" Name="ShortName" Type="String(20) Not Null">
		<Description>Строка вида "UTC+/-XX:XX", формируется автоматом, будет использоваться для отображения заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cfe89201-441d-4cf8-8ab5-bf61b1d48152" Name="IsNegativeOffsetDirection" Type="Boolean Not Null">
		<Description>Направление сдвига False - полжительное, True - отрицательн</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7604a961-fe94-4f25-b342-20190ae3475f" Name="df_TimeZones_IsNegativeOffsetDirection" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fc7ca8d3-b0db-4c5c-bf4c-0a5e5ef09b2b" Name="OffsetTime" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a49a1a7f-696d-42c0-a56c-30a3b625d659" Name="df_TimeZones_OffsetTime" Value="1753-01-01T00:00:00Z" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="b7f1a3b4-3e20-4ae4-ba0a-d0ec21835139" Name="pk_TimeZones">
		<SchemeIndexedColumn Column="2aa45b0b-2eb1-40c7-85e9-812b59053f63" />
	</SchemePrimaryKey>
</SchemeTable>