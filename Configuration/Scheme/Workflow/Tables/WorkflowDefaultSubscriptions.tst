<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="d8b78ce3-bedf-4faa-9fba-75ddbecf4e04" Name="WorkflowDefaultSubscriptions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с подписками узла по умолчанию</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d8b78ce3-bedf-00aa-2000-05ddbecf4e04" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d8b78ce3-bedf-01aa-4000-05ddbecf4e04" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d8b78ce3-bedf-00aa-3100-05ddbecf4e04" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b71d466b-d3e2-4c70-bee0-565b5946af97" Name="Signal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b71d466b-d3e2-0070-4000-065b5946af97" Name="SignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="01723541-3193-42ca-abbe-a76815dcd190" Name="SignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d8b78ce3-bedf-00aa-5000-05ddbecf4e04" Name="pk_WorkflowDefaultSubscriptions">
		<SchemeIndexedColumn Column="d8b78ce3-bedf-00aa-3100-05ddbecf4e04" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d8b78ce3-bedf-00aa-7000-05ddbecf4e04" Name="idx_WorkflowDefaultSubscriptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d8b78ce3-bedf-01aa-4000-05ddbecf4e04" />
	</SchemeIndex>
</SchemeTable>