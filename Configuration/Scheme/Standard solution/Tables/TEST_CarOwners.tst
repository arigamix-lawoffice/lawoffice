<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2cff79c9-6e5a-4e98-8c8f-7a14eb7bec80" Name="TEST_CarOwners" Group="Test" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2cff79c9-6e5a-0098-2000-0a14eb7bec80" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2cff79c9-6e5a-0198-4000-0a14eb7bec80" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2cff79c9-6e5a-0098-3100-0a14eb7bec80" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c4379411-4f45-4db1-93c9-5ffe47b2a8b9" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c4379411-4f45-00b1-4000-0ffe47b2a8b9" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="65bdab77-e47b-4944-acc0-19da7bd9f2ab" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2cff79c9-6e5a-0098-5000-0a14eb7bec80" Name="pk_TEST_CarOwners">
		<SchemeIndexedColumn Column="2cff79c9-6e5a-0098-3100-0a14eb7bec80" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2cff79c9-6e5a-0098-7000-0a14eb7bec80" Name="idx_TEST_CarOwners_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2cff79c9-6e5a-0198-4000-0a14eb7bec80" />
	</SchemeIndex>
</SchemeTable>