<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="1c83c672-7de7-47f5-8c63-ec41bb5aa7ca" Name="WorkflowEngineSubprocessSubscriptions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Подписки узлов к подпроцессам</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c83c672-7de7-00f5-2000-0c41bb5aa7ca" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1c83c672-7de7-01f5-4000-0c41bb5aa7ca" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c83c672-7de7-00f5-3100-0c41bb5aa7ca" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="53663d4c-87c7-4cfb-97fb-12b2358511ef" Name="Subprocess" Type="Reference(Typified) Not Null" ReferencedTable="27debe30-ae5f-4f69-89c9-5706e1592540" WithForeignKey="false">
		<Description>ID подпроцесса</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="53663d4c-87c7-00fb-4000-02b2358511ef" Name="SubprocessRowID" Type="Guid Not Null" ReferencedColumn="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b4aac806-3833-4263-8b10-9988a034711a" Name="Node" Type="Reference(Typified) Not Null" ReferencedTable="69f72d3a-97c1-4d67-a348-071ab861b3c7">
		<Description>ID экземпляра узла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4aac806-3833-0063-4000-0988a034711a" Name="NodeRowID" Type="Guid Not Null" ReferencedColumn="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c83c672-7de7-00f5-5000-0c41bb5aa7ca" Name="pk_WorkflowEngineSubprocessSubscriptions">
		<SchemeIndexedColumn Column="1c83c672-7de7-00f5-3100-0c41bb5aa7ca" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c83c672-7de7-00f5-7000-0c41bb5aa7ca" Name="idx_WorkflowEngineSubprocessSubscriptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="1c83c672-7de7-01f5-4000-0c41bb5aa7ca" />
	</SchemeIndex>
	<SchemeIndex ID="79419c9a-98bb-449f-a7ca-63aabe99a90d" Name="ndx_WorkflowEngineSubprocessSubscriptions_SubprocessRowID">
		<SchemeIndexedColumn Column="53663d4c-87c7-00fb-4000-02b2358511ef" />
	</SchemeIndex>
	<SchemeIndex ID="36684726-0884-4c44-afbc-2ff288762baf" Name="ndx_WorkflowEngineSubprocessSubscriptions_NodeRowID">
		<SchemeIndexedColumn Column="b4aac806-3833-0063-4000-0988a034711a" />
	</SchemeIndex>
</SchemeTable>