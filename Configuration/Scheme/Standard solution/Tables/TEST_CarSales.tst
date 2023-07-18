<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="6dc3a829-b1f4-4e67-ba99-16a30fe91209" Name="TEST_CarSales" Group="Test" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6dc3a829-b1f4-0067-2000-06a30fe91209" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6dc3a829-b1f4-0167-4000-06a30fe91209" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6dc3a829-b1f4-0067-3100-06a30fe91209" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="7539e3db-55fb-45be-9cdd-8a6a58b442de" Name="Name" Type="String(100) Not Null" />
	<SchemePhysicalColumn ID="232f2237-6196-44e0-811b-690ab6239160" Name="EndDate" Type="Date Null" />
	<SchemeComplexColumn ID="1a68d52c-f1ba-4cc2-9ba4-a8452405bae9" Name="Manager" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1a68d52c-f1ba-00c2-4000-08452405bae9" Name="ManagerID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="433e9434-33d0-46d2-a8b3-e57cee06a797" Name="ManagerName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9c2f2563-1e31-47e6-bd4e-92d6c67e5f67" Name="Used" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="93a4b96a-8301-4806-8745-ffa8e630a36d" Name="df_TEST_CarSales_Used" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6dc3a829-b1f4-0067-5000-06a30fe91209" Name="pk_TEST_CarSales">
		<SchemeIndexedColumn Column="6dc3a829-b1f4-0067-3100-06a30fe91209" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6dc3a829-b1f4-0067-7000-06a30fe91209" Name="idx_TEST_CarSales_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6dc3a829-b1f4-0167-4000-06a30fe91209" />
	</SchemeIndex>
</SchemeTable>