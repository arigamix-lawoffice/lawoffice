<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d894a451-c0ff-4a75-b808-05d24cf077bf" Name="DefaultTimeZone" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Данные для временной зоны по умолчанию. 
Хранятся отдельно, чтобы не затирались при изменении таблички с временными зонами (TimeZones)</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d894a451-c0ff-0075-2000-05d24cf077bf" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d894a451-c0ff-0175-4000-05d24cf077bf" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8588bfaf-9b84-4538-a452-6b870d318b03" Name="CodeName" Type="String(256) Not Null">
		<Description>Код зоны. Это строка, общепринятый идентификатор зоны, известный .net. Например, для Москвы это "Russian Standard Time".</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f183d5fc-f89a-402f-a98d-e220160df890" Name="UtcOffsetMinutes" Type="Int32 Not Null">
		<Description>Смещение относительно UTC в минутах с учётом направления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a6b0da9e-cb72-4f1a-a9b5-ae5d2964911d" Name="DisplayName" Type="String(256) Not Null">
		<Description>Строка, содержит константу локализации для справочника зон.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e95a9ad-3bd4-43bb-8e1e-609beb582860" Name="ShortName" Type="String(20) Not Null">
		<Description>Строка вида "UTC+/-XX:XX", формируется автоматом, будет использоваться для отображения заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d5830b10-db3e-41da-a2db-0a36066d35e0" Name="IsNegativeOffsetDirection" Type="Boolean Not Null">
		<Description>Направление сдвига False - полжительное, True - отрицательн</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="83a61b2b-2178-4c8b-a869-9e99b0e627c5" Name="OffsetTime" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="ccf6ea3b-6447-47c2-ac0d-42c3f33628ee" Name="ZoneID" Type="Int16 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d894a451-c0ff-0075-5000-05d24cf077bf" Name="pk_DefaultTimeZone" IsClustered="true">
		<SchemeIndexedColumn Column="d894a451-c0ff-0175-4000-05d24cf077bf" />
	</SchemePrimaryKey>
</SchemeTable>