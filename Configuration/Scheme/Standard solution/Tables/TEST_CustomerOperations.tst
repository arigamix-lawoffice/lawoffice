<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="7f813c98-3331-46a9-8aa0-bab55a956246" Name="TEST_CustomerOperations" Group="Test" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7f813c98-3331-00a9-2000-0ab55a956246" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7f813c98-3331-01a9-4000-0ab55a956246" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7f813c98-3331-00a9-3100-0ab55a956246" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="9c2ad7e1-5c0b-4028-b5e2-2932ff42f901" Name="OperationName" Type="String(100) Not Null" />
	<SchemePhysicalColumn ID="abb31d87-d109-47a8-b5f6-4efa8a58a1de" Name="ManagerName" Type="String(50) Not Null">
		<Description></Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="fc356195-e2b6-4aae-bbff-5ca4d385934d" Name="Customer" Type="Reference(Typified) Not Null" ReferencedTable="30295c44-c633-4474-9e30-4492e75e7e75" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc356195-e2b6-00ae-4000-0ca4d385934d" Name="CustomerRowID" Type="Guid Not Null" ReferencedColumn="30295c44-c633-0074-3100-0492e75e7e75" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7f813c98-3331-00a9-5000-0ab55a956246" Name="pk_TEST_CustomerOperations">
		<SchemeIndexedColumn Column="7f813c98-3331-00a9-3100-0ab55a956246" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7f813c98-3331-00a9-7000-0ab55a956246" Name="idx_TEST_CustomerOperations_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7f813c98-3331-01a9-4000-0ab55a956246" />
	</SchemeIndex>
</SchemeTable>