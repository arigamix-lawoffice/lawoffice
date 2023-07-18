<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="7adfd330-ab0e-458f-9ac4-f2060bde8c97" Name="WorkflowCounters" Group="Workflow" InstanceType="Cards" ContentType="Collections">
	<Description>Счётчики заданий, используемые для реализации блоков "И" в Workflow.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7adfd330-ab0e-008f-2000-02060bde8c97" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7adfd330-ab0e-018f-4000-02060bde8c97" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7adfd330-ab0e-008f-3100-02060bde8c97" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a999c227-3213-4d30-b6d4-dc4755de42dc" Name="Number" Type="Int32 Not Null">
		<Description>Номер счётчика, уникальный в пределах подпроцесса.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6dbd2bfd-947a-4e37-8dc9-b2ce901fd91a" Name="CurrentValue" Type="Int32 Not Null">
		<Description>Текущее значение счётчика, т.е. количество заданий, которое ожидается для выполнения блока "И".</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="3ba9bd69-f929-4753-b711-b1bc25e2bd79" Name="Process" Type="Reference(Typified) Not Null" ReferencedTable="a2db2754-b0ca-4d38-988d-0de6d58057cb">
		<Description>Подпроцесс, в рамках которого существует счётчик.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3ba9bd69-f929-0053-4000-01bc25e2bd79" Name="ProcessRowID" Type="Guid Not Null" ReferencedColumn="a2db2754-b0ca-0038-3100-0de6d58057cb" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7adfd330-ab0e-008f-5000-02060bde8c97" Name="pk_WorkflowCounters">
		<SchemeIndexedColumn Column="7adfd330-ab0e-008f-3100-02060bde8c97" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7adfd330-ab0e-008f-7000-02060bde8c97" Name="idx_WorkflowCounters_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7adfd330-ab0e-018f-4000-02060bde8c97" />
	</SchemeIndex>
	<SchemeIndex ID="5b2e262c-8214-4ac0-a328-26ff2749b310" Name="ndx_WorkflowCounters_ProcessRowID">
		<Description>Быстрое удаление процессов Workflow API для FK.</Description>
		<SchemeIndexedColumn Column="3ba9bd69-f929-0053-4000-01bc25e2bd79" />
	</SchemeIndex>
</SchemeTable>