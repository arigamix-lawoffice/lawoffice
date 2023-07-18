<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a96047e7-3b08-42bd-8455-1032520a608f" Name="FormatSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Секция карточки с настройками форматирования.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a96047e7-3b08-00bd-2000-0032520a608f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a96047e7-3b08-01bd-4000-0032520a608f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="38480a7b-400d-476c-8aa2-28be9591d798" Name="Name" Type="String(32) Not Null">
		<Description>Имя объекта форматирования, связанное с объектом культуры CultureInfo.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="adc1e27d-7efd-4af6-a21a-263e5290733f" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое пользователю имя объекта форматирования. Может быть строкой локализации.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="60a80344-0951-43ea-97cd-cb1e3f466b82" Name="DateFormat" Type="Reference(Typified) Not Null" ReferencedTable="585825ed-e297-4eb3-bea2-a732ad75c6b6">
		<Description>Формат для ввода и вывода дат.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="60a80344-0951-00ea-4000-0b1e3f466b82" Name="DateFormatID" Type="Int16 Not Null" ReferencedColumn="8921cfa1-255c-4604-a13a-a92cf8c96aaa" />
		<SchemeReferencingColumn ID="d7cf34f4-7b06-4044-bade-abed8ecdf5f6" Name="DateFormatCaption" Type="String(128) Not Null" ReferencedColumn="be6a7ba7-f83e-4ce5-9ee6-ada094e4c047" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d9f5dd74-6420-4859-96c8-b12894b3574b" Name="DateSeparator" Type="String(1) Not Null">
		<Description>Символ-разделитель для вывода дат.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="922a7aa6-2d80-450c-9c34-475f6168ab48" Name="df_FormatSettings_DateSeparator" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="08accd5d-412c-47f3-bece-01672c80e072" Name="DaysWithLeadingZero" Type="Boolean Not Null">
		<Description>Признак того, что дни в числовом представлении выводятся с ведущим нулём, когда число меньше 10.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="66f17ed5-fb57-4757-afca-c3520334d11d" Name="df_FormatSettings_DaysWithLeadingZero" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="42bc3704-11ca-4fcc-8d33-9c4157486819" Name="MonthsWithLeadingZero" Type="Boolean Not Null">
		<Description>Признак того, что месяцы в числовом представлении выводятся с ведущим нулём, когда число меньше 10.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a16eabb9-cbf9-4011-b906-1093b93111a7" Name="df_FormatSettings_MonthsWithLeadingZero" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b2e9c150-f370-4f63-9b2e-e5ba1d711af8" Name="HoursWithLeadingZero" Type="Boolean Not Null">
		<Description>Признак того, что часы выводятся с ведущим нулём, когда число меньше 10. Минуты и секунды всегда выводятся с ведущим нулём.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="89db42ee-153c-4c67-872a-d180292c8702" Name="df_FormatSettings_HoursWithLeadingZero" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3b3aa3c3-3749-4bfb-8943-adaddd7cfe02" Name="Time24Hour" Type="Boolean Not Null">
		<Description>Признак того, что при вводе и выводе времени используется 24-часовой формат без использования дезигнатора "до полудня" и "после полудня".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8b07df62-7da4-48cb-b98a-be0b1ea10bb2" Name="df_FormatSettings_Time24Hour" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e08a3a8-afe1-4f73-9352-fbb0db4b6642" Name="TimeSeparator" Type="String(1) Not Null">
		<Description>Символ-разделитель для вывода времени.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="863a531c-829b-47a7-ab8f-ebfc0b8a4921" Name="df_FormatSettings_TimeSeparator" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6ac4e956-53c5-49bb-bdfd-b727f2bd880e" Name="TimeAmDesignator" Type="String(32) Null">
		<Description>Строка-дезигнатор "до полудня".</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fcfced46-988b-42bd-9cfe-2456363b657c" Name="TimePmDesignator" Type="String(32) Null">
		<Description>Строка-дезигнатор "после полудня".</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e6b1c08-26f4-4609-8580-271fd3a9250f" Name="NumberGroupSeparator" Type="String(1) Not Null">
		<Description>Символ-разделитель групп разрядов при выводе чисел (между тройками цифр).</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d7e86595-9d1a-4c71-8c48-ceaf84ab347c" Name="df_FormatSettings_NumberGroupSeparator" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="319f4e23-9495-4fdf-aebd-726df9ed8982" Name="NumberDecimalSeparator" Type="String(1) Not Null">
		<Description>Символ разделитель для целой и дробной части числа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7937c196-0896-4da9-b22e-a3a4c011d733" Name="df_FormatSettings_NumberDecimalSeparator" Value="" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a96047e7-3b08-00bd-5000-0032520a608f" Name="pk_FormatSettings" IsClustered="true">
		<SchemeIndexedColumn Column="a96047e7-3b08-01bd-4000-0032520a608f" />
	</SchemePrimaryKey>
	<SchemeIndex ID="5111f8d0-903c-4ef2-b6d5-6cd2e2f93892" Name="ndx_FormatSettings_Name" IsUnique="true">
		<SchemeIndexedColumn Column="38480a7b-400d-476c-8aa2-28be9591d798" />
		<SchemeIncludedColumn Column="adc1e27d-7efd-4af6-a21a-263e5290733f" />
	</SchemeIndex>
</SchemeTable>