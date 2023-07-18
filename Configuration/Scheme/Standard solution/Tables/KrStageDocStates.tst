<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4f6c7635-031d-411a-9219-069e05a7e8b6" Name="KrStageDocStates" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4f6c7635-031d-001a-2000-069e05a7e8b6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4f6c7635-031d-011a-4000-069e05a7e8b6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4f6c7635-031d-001a-3100-069e05a7e8b6" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="ed746c66-05f3-4106-8d9c-91868bec5826" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ed746c66-05f3-0006-4000-01868bec5826" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="d8232d40-7af5-43f6-988b-c5d61d72c51f" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4f6c7635-031d-001a-5000-069e05a7e8b6" Name="pk_KrStageDocStates">
		<SchemeIndexedColumn Column="4f6c7635-031d-001a-3100-069e05a7e8b6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="4f6c7635-031d-001a-7000-069e05a7e8b6" Name="idx_KrStageDocStates_ID" IsClustered="true">
		<SchemeIndexedColumn Column="4f6c7635-031d-011a-4000-069e05a7e8b6" />
	</SchemeIndex>
	<SchemeIndex ID="4d566d35-87bf-4760-bc39-d843fe638a14" Name="ndx_KrStageDocStates_StateIDID" IsUnique="true">
		<SchemeIndexedColumn Column="ed746c66-05f3-0006-4000-01868bec5826" />
		<SchemeIndexedColumn Column="4f6c7635-031d-011a-4000-069e05a7e8b6" />
	</SchemeIndex>
</SchemeTable>