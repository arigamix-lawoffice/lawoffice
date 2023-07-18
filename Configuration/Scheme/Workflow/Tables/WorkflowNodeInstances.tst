<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="e2eda913-f68f-4d42-88ba-25f80bd4c3e5" Name="WorkflowNodeInstances" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список экземпляров узлов в экзепляре процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e2eda913-f68f-0042-2000-05f80bd4c3e5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e2eda913-f68f-0142-4000-05f80bd4c3e5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e2eda913-f68f-0042-3100-05f80bd4c3e5" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="0d97b9ac-22ff-48ad-8997-cae67992e28d" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e2eda913-f68f-0042-5000-05f80bd4c3e5" Name="pk_WorkflowNodeInstances">
		<SchemeIndexedColumn Column="e2eda913-f68f-0042-3100-05f80bd4c3e5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e2eda913-f68f-0042-7000-05f80bd4c3e5" Name="idx_WorkflowNodeInstances_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e2eda913-f68f-0142-4000-05f80bd4c3e5" />
	</SchemeIndex>
</SchemeTable>