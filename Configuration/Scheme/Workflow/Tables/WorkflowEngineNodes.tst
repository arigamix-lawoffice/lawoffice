<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="69f72d3a-97c1-4d67-a348-071ab861b3c7" Name="WorkflowEngineNodes" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Содержит состояния активных узлов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="69f72d3a-97c1-0067-2000-071ab861b3c7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="69f72d3a-97c1-0167-4000-071ab861b3c7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="69f72d3a-97c1-0067-3100-071ab861b3c7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="58b3b7d8-af88-42e2-b9aa-6711ba83de74" Name="Process" Type="Reference(Typified) Not Null" ReferencedTable="27debe30-ae5f-4f69-89c9-5706e1592540">
		<Description>Ссылка на экземпляр процесса</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="58b3b7d8-af88-00e2-4000-0711ba83de74" Name="ProcessRowID" Type="Guid Not Null" ReferencedColumn="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c1f591e5-f962-4316-a5a6-7d2cf1560c8b" Name="NodeData" Type="BinaryJson Not Null">
		<Description>Состояние экземпляра узла и всех его действий</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f40e299f-3978-4f1e-8812-a70c9e90f06b" Name="df_WorkflowEngineNodes_NodeData" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e98ac80-372b-43a8-8890-8ed4df975b26" Name="NodeID" Type="Guid Not Null">
		<Description>Идентификатор описания узла из шаблона процесса, к которому относится данный экземпляр узла</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="69f72d3a-97c1-0067-5000-071ab861b3c7" Name="pk_WorkflowEngineNodes">
		<SchemeIndexedColumn Column="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="69f72d3a-97c1-0067-7000-071ab861b3c7" Name="idx_WorkflowEngineNodes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="69f72d3a-97c1-0167-4000-071ab861b3c7" />
	</SchemeIndex>
	<SchemeIndex ID="24537202-09c6-4b81-b2cf-3354dd784163" Name="ndx_WorkflowEngineNodes_ProcessRowID">
		<SchemeIndexedColumn Column="58b3b7d8-af88-00e2-4000-0711ba83de74" />
	</SchemeIndex>
	<SchemeIndex ID="27844df9-123e-45c1-9021-619be3ec8f0d" Name="ndx_WorkflowEngineNodes_ID" SupportsPostgreSql="false">
		<FillFactor Dbms="SqlServer">80</FillFactor>
		<SchemeIndexedColumn Column="69f72d3a-97c1-0167-4000-071ab861b3c7" />
	</SchemeIndex>
</SchemeTable>