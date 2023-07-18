<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="11426c91-c7c4-4455-9eda-7ce5fd497982" Name="KrSettingsCycleGroupingStates" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="11426c91-c7c4-0055-2000-0ce5fd497982" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="11426c91-c7c4-0155-4000-0ce5fd497982" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="11426c91-c7c4-0055-3100-0ce5fd497982" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fb5f29a5-3065-4ada-b7a0-c0a86ee8c1dd" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fb5f29a5-3065-00da-4000-00a86ee8c1dd" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="72580319-6751-4dac-aaed-2b571d976e77" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0543649d-9880-4f14-93cc-b2c3278d8ea4" Name="Types" Type="Reference(Typified) Not Null" ReferencedTable="4012de1a-efd8-442d-a25c-8fe78008e38d" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0543649d-9880-0014-4000-02c3278d8ea4" Name="TypesRowID" Type="Guid Not Null" ReferencedColumn="4012de1a-efd8-002d-3100-0fe78008e38d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="11426c91-c7c4-0055-5000-0ce5fd497982" Name="pk_KrSettingsCycleGroupingStates">
		<SchemeIndexedColumn Column="11426c91-c7c4-0055-3100-0ce5fd497982" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="30f549da-4670-4f0c-a8ba-9f6057bc2bff" Name="ndx_KrSettingsCycleGroupingStates" />
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="11426c91-c7c4-0055-7000-0ce5fd497982" Name="idx_KrSettingsCycleGroupingStates_ID" IsClustered="true">
		<SchemeIndexedColumn Column="11426c91-c7c4-0155-4000-0ce5fd497982" />
	</SchemeIndex>
</SchemeTable>