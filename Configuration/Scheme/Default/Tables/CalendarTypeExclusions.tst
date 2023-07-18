<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3a11e188-f82f-495b-a78f-778f2988db52" Name="CalendarTypeExclusions" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a11e188-f82f-005b-2000-078f2988db52" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3a11e188-f82f-015b-4000-078f2988db52" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a11e188-f82f-005b-3100-078f2988db52" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="cbd5903f-8a11-481c-ac57-445e1fe97a47" Name="Caption" Type="String(400) Null" />
	<SchemePhysicalColumn ID="93c97885-ed2b-4c29-8e9c-7f3df81f47c3" Name="StartTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="cc04bffc-0a45-4869-a06e-a233284665d3" Name="EndTime" Type="DateTime Null" />
	<SchemePhysicalColumn ID="0fe8eb9a-8942-4e4f-bdb5-1b5ce20236e3" Name="IsNotWorkingTime" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="26e4da97-9c73-408d-8863-e5494ba391f3" Name="df_CalendarTypeExclusions_IsNotWorkingTime" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a11e188-f82f-005b-5000-078f2988db52" Name="pk_CalendarTypeExclusions">
		<SchemeIndexedColumn Column="3a11e188-f82f-005b-3100-078f2988db52" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3a11e188-f82f-005b-7000-078f2988db52" Name="idx_CalendarTypeExclusions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3a11e188-f82f-015b-4000-078f2988db52" />
	</SchemeIndex>
</SchemeTable>