<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="7c45a604-9175-45bd-8525-f218a465b77b" Name="WorkflowEngineCommandSubscriptions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Подписки узлов на внешнюю команду</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c45a604-9175-00bd-2000-0218a465b77b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7c45a604-9175-01bd-4000-0218a465b77b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c45a604-9175-00bd-3100-0218a465b77b" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="9febb653-cbe2-4f56-a184-a8edda24c8f1" Name="Command" Type="String(128) Not Null" />
	<SchemeComplexColumn ID="fd3302c1-a906-4096-8414-9838d2621bc7" Name="Node" Type="Reference(Typified) Not Null" ReferencedTable="69f72d3a-97c1-4d67-a348-071ab861b3c7" WithForeignKey="false">
		<Description>Ссылка на экземпляр узла, или шаблон узла, для подписок по умолчанию</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fd3302c1-a906-0096-4000-0838d2621bc7" Name="NodeRowID" Type="Guid Not Null" ReferencedColumn="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c46688ad-0f5d-498d-ba4d-e1c4b78679ac" Name="Process" Type="Reference(Typified) Null" ReferencedTable="27debe30-ae5f-4f69-89c9-5706e1592540" WithForeignKey="false">
		<Description>Ссылка на экземпляр процесса. Задается только для подписок, у которых NodeRowID - ссылка на шаблон узла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c46688ad-0f5d-008d-4000-01c4b78679ac" Name="ProcessRowID" Type="Guid Null" ReferencedColumn="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c45a604-9175-00bd-5000-0218a465b77b" Name="pk_WorkflowEngineCommandSubscriptions">
		<SchemeIndexedColumn Column="7c45a604-9175-00bd-3100-0218a465b77b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c45a604-9175-00bd-7000-0218a465b77b" Name="idx_WorkflowEngineCommandSubscriptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7c45a604-9175-01bd-4000-0218a465b77b" />
	</SchemeIndex>
	<SchemeIndex ID="343a6469-3409-4208-adad-c2449132ed93" Name="ndx_WorkflowEngineCommandSubscriptions_NodeRowID">
		<SchemeIndexedColumn Column="fd3302c1-a906-0096-4000-0838d2621bc7" />
		<SchemeIncludedColumn Column="9febb653-cbe2-4f56-a184-a8edda24c8f1" />
	</SchemeIndex>
	<SchemeIndex ID="71d84cd7-f324-463a-a172-88371485e2ae" Name="ndx_WorkflowEngineCommandSubscriptions_CommandNodeRowIDProcessRowIDRowID">
		<SchemeIndexedColumn Column="9febb653-cbe2-4f56-a184-a8edda24c8f1" />
		<SchemeIndexedColumn Column="fd3302c1-a906-0096-4000-0838d2621bc7" />
		<SchemeIndexedColumn Column="c46688ad-0f5d-008d-4000-01c4b78679ac" />
		<SchemeIndexedColumn Column="7c45a604-9175-00bd-3100-0218a465b77b" />
	</SchemeIndex>
	<SchemeIndex ID="2d51feaa-4a47-47ce-ab00-dc1e2d983f8f" Name="ndx_WorkflowEngineCommandSubscriptions_ProcessRowID">
		<SchemeIndexedColumn Column="c46688ad-0f5d-008d-4000-01c4b78679ac" />
	</SchemeIndex>
</SchemeTable>