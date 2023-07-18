<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3e09239e-ebb7-4b0a-a4e1-51eae83e3c0c" Name="TimeZonesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Временные зоны</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3e09239e-ebb7-000a-2000-01eae83e3c0c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3e09239e-ebb7-010a-4000-01eae83e3c0c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3e09239e-ebb7-000a-3100-01eae83e3c0c" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="b94066e1-ecea-4e80-aa19-197233a54602" Name="CodeName" Type="String(256) Not Null">
		<Description>Код зоны. Это строка, общепринятый идентификатор зоны, известный .net. Например, для Москвы это "Russian Standard Time".</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a96f0a9c-e21d-42b3-adbd-322937f76dcf" Name="UtcOffsetMinutes" Type="Int32 Not Null">
		<Description>Смещение относительно UTC в минутах с учётом направления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3605316a-d5e4-4d2f-9c25-29c213f0d37d" Name="IsNegativeOffsetDirection" Type="Boolean Not Null">
		<Description>Направление сдвига False - полжительное, True - отрицательное</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="07bb8b2d-ac61-49bb-b70b-4f4f28bafa5b" Name="df_TimeZonesVirtual_IsNegativeOffsetDirection" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb74936d-1b2e-4da4-ad2a-686bebb18d3f" Name="OffsetTime" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2a5b7515-9996-4488-8cfa-43433cb218e3" Name="df_TimeZonesVirtual_OffsetTime" Value="1753-01-01T00:00:00Z" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c9d0cb03-476a-4e6e-a76c-a260553bdd52" Name="DisplayName" Type="String(256) Not Null">
		<Description>Строка, содержит константу локализации для справочника зон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="836d6b1f-0171-4f96-be4f-0ea7ffa0b9fa" Name="ShortName" Type="String(20) Not Null">
		<Description>Строка вида "UTC+/-XX:XX", формируется автоматом, будет использоваться для отображения заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="01a5002a-e1e0-4974-9430-753f28ea8ebc" Name="ZoneID" Type="Int16 Not Null">
		<Description>Это число, его мы генерируем при формировании списка зон, начиная с 1. 0 - зона с именем "default", смещение utc = 180 - как у Москвы.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3e09239e-ebb7-000a-5000-01eae83e3c0c" Name="pk_TimeZonesVirtual">
		<SchemeIndexedColumn Column="3e09239e-ebb7-000a-3100-01eae83e3c0c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3e09239e-ebb7-000a-7000-01eae83e3c0c" Name="idx_TimeZonesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3e09239e-ebb7-010a-4000-01eae83e3c0c" />
	</SchemeIndex>
</SchemeTable>