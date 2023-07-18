<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d2683167-0425-4093-ba65-0196ded5437a" Name="WorkflowTasks" Group="Workflow" InstanceType="Cards" ContentType="Collections">
	<Description>Список активных заданий Workflow. В качестве RowID используется идентификатор задания.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d2683167-0425-0093-2000-0196ded5437a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d2683167-0425-0193-4000-0196ded5437a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d2683167-0425-0093-3100-0196ded5437a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7cdee1a1-c389-4f64-bc03-b3d257c30050" Name="Process" Type="Reference(Typified) Not Null" ReferencedTable="a2db2754-b0ca-4d38-988d-0de6d58057cb" WithForeignKey="false">
		<Description>Подпроцесс, в рамках которого существует задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7cdee1a1-c389-0064-4000-03d257c30050" Name="ProcessRowID" Type="Guid Not Null" ReferencedColumn="a2db2754-b0ca-0038-3100-0de6d58057cb" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="63a4f848-ef1d-4fb6-8f94-61f8a8f063db" Name="Params" Type="BinaryJson Null">
		<Description>Параметры задания или Null, если параметры отсутствуют.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d2683167-0425-0093-5000-0196ded5437a" Name="pk_WorkflowTasks">
		<SchemeIndexedColumn Column="d2683167-0425-0093-3100-0196ded5437a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d2683167-0425-0093-7000-0196ded5437a" Name="idx_WorkflowTasks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d2683167-0425-0193-4000-0196ded5437a" />
	</SchemeIndex>
</SchemeTable>