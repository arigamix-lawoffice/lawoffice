<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="5ee285b4-a72c-4a41-88a1-3e052fa1ee44" Name="WorkflowEngineTaskSubscriptions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Подписки узлов на действия из заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ee285b4-a72c-0041-2000-0e052fa1ee44" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5ee285b4-a72c-0141-4000-0e052fa1ee44" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ee285b4-a72c-0041-3100-0e052fa1ee44" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5ce92f6b-8a5f-4aaa-88b3-0a403b105a8d" Name="Task" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8" WithForeignKey="false">
		<Description>Ссылка на задание</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5ce92f6b-8a5f-00aa-4000-0a403b105a8d" Name="TaskID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1619d7eb-2676-4529-9803-4a71cd7edcdc" Name="Node" Type="Reference(Typified) Not Null" ReferencedTable="69f72d3a-97c1-4d67-a348-071ab861b3c7">
		<Description>Ссылка на экземпляр узла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1619d7eb-2676-0029-4000-0a71cd7edcdc" Name="NodeRowID" Type="Guid Not Null" ReferencedColumn="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ee285b4-a72c-0041-5000-0e052fa1ee44" Name="pk_WorkflowEngineTaskSubscriptions">
		<SchemeIndexedColumn Column="5ee285b4-a72c-0041-3100-0e052fa1ee44" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5ee285b4-a72c-0041-7000-0e052fa1ee44" Name="idx_WorkflowEngineTaskSubscriptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5ee285b4-a72c-0141-4000-0e052fa1ee44" />
	</SchemeIndex>
	<SchemeIndex ID="82724c92-df0b-452c-a810-86df68a187e9" Name="ndx_WorkflowEngineTaskSubscriptions_NodeRowID">
		<SchemeIndexedColumn Column="1619d7eb-2676-0029-4000-0a71cd7edcdc" />
	</SchemeIndex>
	<SchemeIndex ID="7c3a42cf-7b3e-4663-bb18-cedd47cd2f31" Name="ndx_WorkflowEngineTaskSubscriptions_TaskID">
		<SchemeIndexedColumn Column="5ce92f6b-8a5f-00aa-4000-0a403b105a8d" />
	</SchemeIndex>
</SchemeTable>