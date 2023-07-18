<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="22a3da4b-ac30-4a40-a069-3d6ee66079a0" Name="KrInfoForInitiator" Group="Kr" IsVirtual="true" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="22a3da4b-ac30-0040-2000-0d6ee66079a0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="22a3da4b-ac30-0140-4000-0d6ee66079a0" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="22a3da4b-ac30-0040-3100-0d6ee66079a0" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="da380039-5492-4141-8519-10baaf8e78d3" Name="ApproverRole" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="97478926-9e4a-4ffa-aae1-da336b88a85c" Name="ApproverUser" Type="String(128) Null" />
	<SchemePhysicalColumn ID="41331b48-2667-40e4-93fe-d5adfbb3247b" Name="InProgress" Type="DateTime Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="22a3da4b-ac30-0040-5000-0d6ee66079a0" Name="pk_KrInfoForInitiator">
		<SchemeIndexedColumn Column="22a3da4b-ac30-0040-3100-0d6ee66079a0" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="22a3da4b-ac30-0040-7000-0d6ee66079a0" Name="idx_KrInfoForInitiator_ID" IsClustered="true">
		<SchemeIndexedColumn Column="22a3da4b-ac30-0140-4000-0d6ee66079a0" />
	</SchemeIndex>
</SchemeTable>