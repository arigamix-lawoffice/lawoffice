<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="30295c44-c633-4474-9e30-4492e75e7e75" Name="TEST_CarCustomers" Group="Test" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="30295c44-c633-0074-2000-0492e75e7e75" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="30295c44-c633-0174-4000-0492e75e7e75" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="30295c44-c633-0074-3100-0492e75e7e75" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="fa39c7cc-cab4-4984-9c14-39971918cc37" Name="FullName" Type="String(256) Not Null">
		<Description>Полное имя покупателя.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fdbfb39c-0ce0-4f03-a614-7c5d52341c81" Name="PurchaseDate" Type="DateTime Not Null">
		<Description>Дата покупки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8854b3ea-4c6b-4da9-843a-e42272118d33" Name="Sale" Type="Reference(Typified) Not Null" ReferencedTable="6dc3a829-b1f4-4e67-ba99-16a30fe91209" IsReferenceToOwner="true">
		<Description>Распродажа, на которой клиент совершил покупку.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8854b3ea-4c6b-00a9-4000-042272118d33" Name="SaleRowID" Type="Guid Not Null" ReferencedColumn="6dc3a829-b1f4-0067-3100-06a30fe91209" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="30295c44-c633-0074-5000-0492e75e7e75" Name="pk_TEST_CarCustomers">
		<SchemeIndexedColumn Column="30295c44-c633-0074-3100-0492e75e7e75" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="30295c44-c633-0074-7000-0492e75e7e75" Name="idx_TEST_CarCustomers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="30295c44-c633-0174-4000-0492e75e7e75" />
	</SchemeIndex>
</SchemeTable>