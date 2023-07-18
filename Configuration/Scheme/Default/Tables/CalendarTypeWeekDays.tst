<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="67d63f49-ec4f-4e3b-9364-0b6e38d138ec" Name="CalendarTypeWeekDays" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="67d63f49-ec4f-003b-2000-0b6e38d138ec" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="67d63f49-ec4f-013b-4000-0b6e38d138ec" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="67d63f49-ec4f-003b-3100-0b6e38d138ec" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="5d4b3ccb-709f-4b6d-8288-ecc20ffb4f43" Name="Number" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="46f8a3b6-b254-40f8-822f-c022e6ea2cc0" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="6180ed95-79c1-4baa-8641-8ae8ccd4fae1" Name="WorkingDayStart" Type="Time Null">
		<Description>Начало рабочего дня</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d8c5751b-ffe9-4214-8060-4e78ff3fd280" Name="WorkingDayEnd" Type="Time Null">
		<Description>Окончание рабочего дня</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bae7042c-c8d2-4e0f-87ac-c713f2ad78db" Name="LunchStart" Type="Time Null" />
	<SchemePhysicalColumn ID="43bab1a5-630c-4e3d-92df-9aa0c92663fc" Name="LunchEnd" Type="Time Null" />
	<SchemePhysicalColumn ID="f468cbeb-d793-4adc-9756-4fd6d849dda3" Name="IsNotWorkingDay" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2ed829fc-1b0b-4c32-8f57-1fd6f283fa54" Name="df_CalendarTypeWeekDays_IsNotWorkingDay" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="67d63f49-ec4f-003b-5000-0b6e38d138ec" Name="pk_CalendarTypeWeekDays">
		<SchemeIndexedColumn Column="67d63f49-ec4f-003b-3100-0b6e38d138ec" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="67d63f49-ec4f-003b-7000-0b6e38d138ec" Name="idx_CalendarTypeWeekDays_ID" IsClustered="true">
		<SchemeIndexedColumn Column="67d63f49-ec4f-013b-4000-0b6e38d138ec" />
	</SchemeIndex>
</SchemeTable>