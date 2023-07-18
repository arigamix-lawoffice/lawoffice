<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a2db2754-b0ca-4d38-988d-0de6d58057cb" Name="WorkflowProcesses" Group="Workflow" InstanceType="Cards" ContentType="Collections">
	<Description>Информация по подпроцессам в бизнес-процессе.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2db2754-b0ca-0038-2000-0de6d58057cb" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a2db2754-b0ca-0138-4000-0de6d58057cb" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2db2754-b0ca-0038-3100-0de6d58057cb" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="76e55bcb-5c67-433f-9858-298360f3a9f6" Name="TypeName" Type="String(128) Not Null">
		<Description>Имя типа подпроцесса.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7ba43d77-4412-43a1-9800-b49504ade57f" Name="Params" Type="BinaryJson Null">
		<Description>Параметры подпроцесса или Null, если параметры отсутствуют.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2db2754-b0ca-0038-5000-0de6d58057cb" Name="pk_WorkflowProcesses">
		<SchemeIndexedColumn Column="a2db2754-b0ca-0038-3100-0de6d58057cb" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2db2754-b0ca-0038-7000-0de6d58057cb" Name="idx_WorkflowProcesses_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a2db2754-b0ca-0138-4000-0de6d58057cb" />
	</SchemeIndex>
</SchemeTable>