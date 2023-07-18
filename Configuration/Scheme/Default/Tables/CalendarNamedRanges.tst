<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="dc5ec614-d5df-40d1-ba43-4e5b97211711" Name="CalendarNamedRanges" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dc5ec614-d5df-00d1-2000-0e5b97211711" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dc5ec614-d5df-01d1-4000-0e5b97211711" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dc5ec614-d5df-00d1-3100-0e5b97211711" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="9abb9e84-ffde-44d0-8124-715f062187f0" Name="StartTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="f65711da-ebe8-47f8-a072-d23e65ef1e6d" Name="EndTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="ec469389-5006-4bfb-a9a7-d664e0d95fc2" Name="IsNotWorkingTime" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="84f72d7d-4650-4571-80ed-b6d8f377f18e" Name="df_CalendarNamedRanges_IsNotWorkingTime" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="146bfb65-6e56-464c-ab6f-2f6ee2dce13f" Name="Name" Type="String(400) Not Null" />
	<SchemePhysicalColumn ID="4255e550-ccdf-420c-840e-de69c709187f" Name="IsManual" Type="Boolean Not Null">
		<Description>Добавлено вручную</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="604deafd-f6be-4c9f-9dca-83b3f8ed710a" Name="df_CalendarNamedRanges_IsManual" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dc5ec614-d5df-00d1-5000-0e5b97211711" Name="pk_CalendarNamedRanges">
		<SchemeIndexedColumn Column="dc5ec614-d5df-00d1-3100-0e5b97211711" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dc5ec614-d5df-00d1-7000-0e5b97211711" Name="idx_CalendarNamedRanges_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dc5ec614-d5df-01d1-4000-0e5b97211711" />
	</SchemeIndex>
</SchemeTable>