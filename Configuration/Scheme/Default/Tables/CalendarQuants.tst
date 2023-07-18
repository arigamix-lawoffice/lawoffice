<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="094fac6d-4fe8-4d3e-89c2-22a0f74fd705" Name="CalendarQuants" Group="System">
	<SchemePhysicalColumn ID="bf06bb92-8ed7-4e2d-844f-95089cd52c28" Name="QuantNumber" Type="Int64 Null">
		<Description>Номер кванта</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6fa5bd86-d44b-4a81-925b-73b39783099f" Name="StartTime" Type="DateTime Null">
		<Description>Дата\время начала кванта (включительно)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e59d8c3d-2550-455b-a5d1-42afe234c8b9" Name="EndTime" Type="DateTime Null">
		<Description>Дата\время окончания кванта (не включительно)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="02a62ff3-5c0b-4c0f-a06b-590922b4e681" Name="Type" Type="Boolean Null">
		<Description>Тип кванта (0 - рабочее время, 1 - выходной)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="20ad26a7-c788-4daa-a29a-60022b43e585" Name="df_CalendarQuants_Type" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fff7e2cd-5654-49fa-bca8-b2c22d1a7cc8" Name="ID" Type="Int32 Null">
		<Description>Числовой идентификатор календаря</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f249ec6a-49a9-4cf9-8269-496f13a6acfe" Name="df_CalendarQuants_ID" Value="0" />
	</SchemePhysicalColumn>
	<SchemeIndex ID="e37318b1-89ff-4d0e-97e3-959bb42efb96" Name="idx_CalendarQuants_StartTimeEndTime" IsClustered="true">
		<SchemeIndexedColumn Column="6fa5bd86-d44b-4a81-925b-73b39783099f" SortOrder="Descending" />
		<SchemeIndexedColumn Column="e59d8c3d-2550-455b-a5d1-42afe234c8b9" SortOrder="Descending" />
	</SchemeIndex>
	<SchemeIndex ID="891a74db-fc64-4afd-a1c3-d0b9296b504d" Name="ndx_CalendarQuants_QuantNumberID">
		<SchemeIndexedColumn Column="bf06bb92-8ed7-4e2d-844f-95089cd52c28" />
		<SchemeIndexedColumn Column="fff7e2cd-5654-49fa-bca8-b2c22d1a7cc8" />
	</SchemeIndex>
	<SchemeIndex ID="965be6d6-bb17-4177-ba4e-599e81018753" Name="ndx_CalendarQuants_EndTime">
		<SchemeIndexedColumn Column="e59d8c3d-2550-455b-a5d1-42afe234c8b9" />
		<SchemeIncludedColumn Column="bf06bb92-8ed7-4e2d-844f-95089cd52c28" />
	</SchemeIndex>
	<SchemeIndex ID="e990729c-6968-4584-9ad9-f4da3eca8f79" Name="ndx_CalendarQuants_ID">
		<SchemeIndexedColumn Column="fff7e2cd-5654-49fa-bca8-b2c22d1a7cc8" />
	</SchemeIndex>
</SchemeTable>