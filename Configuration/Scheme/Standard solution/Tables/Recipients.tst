<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="386509d9-4130-467f-9a52-0004aa15247e" Name="Recipients" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Получатели</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="386509d9-4130-007f-2000-0004aa15247e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="386509d9-4130-017f-4000-0004aa15247e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="386509d9-4130-007f-3100-0004aa15247e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c2ccd7e2-84b9-44f7-817f-c233ce673900" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c2ccd7e2-84b9-00f7-4000-0233ce673900" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6f55f11a-58b9-4d43-98e5-1b25cd3a338b" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="386509d9-4130-007f-5000-0004aa15247e" Name="pk_Recipients">
		<SchemeIndexedColumn Column="386509d9-4130-007f-3100-0004aa15247e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="386509d9-4130-007f-7000-0004aa15247e" Name="idx_Recipients_ID" IsClustered="true">
		<SchemeIndexedColumn Column="386509d9-4130-017f-4000-0004aa15247e" />
	</SchemeIndex>
</SchemeTable>