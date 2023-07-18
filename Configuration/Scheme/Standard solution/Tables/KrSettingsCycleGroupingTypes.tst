<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4012de1a-efd8-442d-a25c-8fe78008e38d" Name="KrSettingsCycleGroupingTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4012de1a-efd8-002d-2000-0fe78008e38d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4012de1a-efd8-012d-4000-0fe78008e38d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4012de1a-efd8-002d-3100-0fe78008e38d" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="ae190f31-c7bc-48dd-a557-f5c6b97572d7" Name="Description" Type="String(255) Null" />
	<SchemeComplexColumn ID="a60799be-9aea-4991-8285-3825f9560438" Name="DefaultMode" Type="Reference(Typified) Not Null" ReferencedTable="3e451f29-8808-4398-930e-d5c172c21de7">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a60799be-9aea-0091-4000-0825f9560438" Name="DefaultModeID" Type="Int32 Not Null" ReferencedColumn="6516e6c3-9da2-44e8-bc82-573539d3be6e">
			<SchemeDefaultConstraint IsPermanent="true" ID="0d7f3c88-1a3c-4067-b67d-116ce57ef599" Name="df_KrSettingsCycleGroupingTypes_DefaultModeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="680660f5-afb9-4aa7-80fd-5bf612328c4c" Name="DefaultModeName" Type="String(255) Not Null" ReferencedColumn="3ba2832a-8dc7-465f-939a-dab23916f05a">
			<SchemeDefaultConstraint IsPermanent="true" ID="942f4dd4-4986-48a4-871d-3e8ced8db1e7" Name="df_KrSettingsCycleGroupingTypes_DefaultModeName" Value="$UI_Controls_FilesControl_ShowAllCycleFiles" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4012de1a-efd8-002d-5000-0fe78008e38d" Name="pk_KrSettingsCycleGroupingTypes">
		<SchemeIndexedColumn Column="4012de1a-efd8-002d-3100-0fe78008e38d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="4012de1a-efd8-002d-7000-0fe78008e38d" Name="idx_KrSettingsCycleGroupingTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="4012de1a-efd8-012d-4000-0fe78008e38d" />
	</SchemeIndex>
</SchemeTable>