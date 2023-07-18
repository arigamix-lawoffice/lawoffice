<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="aec4456f-c927-4a49-89f5-582ab17dc997" Name="CalendarExclusions" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="aec4456f-c927-0049-2000-082ab17dc997" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aec4456f-c927-0149-4000-082ab17dc997" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="aec4456f-c927-0049-3100-082ab17dc997" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="4169697f-f840-4a37-95d7-07a90c2d01fa" Name="StartTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="c0b6cf58-c1d1-4333-a5a6-33204a77d7d3" Name="EndTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="7a6a865c-b7ea-4565-852c-025d9d2d42ea" Name="IsNotWorkingTime" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9848e218-2376-4082-aa93-62b3c997e8ce" Name="df_CalendarExclusions_IsNotWorkingTime" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="aec4456f-c927-0049-5000-082ab17dc997" Name="pk_CalendarExclusions">
		<SchemeIndexedColumn Column="aec4456f-c927-0049-3100-082ab17dc997" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="aec4456f-c927-0049-7000-082ab17dc997" Name="idx_CalendarExclusions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="aec4456f-c927-0149-4000-082ab17dc997" />
	</SchemeIndex>
</SchemeTable>