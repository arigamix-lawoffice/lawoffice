<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="857ef2b9-6bdb-4913-bbc2-2cf9d1ae0b55" Name="WorkflowEngineTaskActions" Group="WorkflowEngine">
	<Description>Список возможных действий над заданием</Description>
	<SchemePhysicalColumn ID="3b7d3241-404a-47ff-b077-9309ca66ab85" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="e5e566d0-4539-4f29-b52e-31b3cc2e95b9" Name="pk_WorkflowEngineTaskActions">
		<SchemeIndexedColumn Column="3b7d3241-404a-47ff-b077-9309ca66ab85" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="3b7d3241-404a-47ff-b077-9309ca66ab85">0</ID>
		<Name ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808">$WorkflowEngine_TaskActions_InProgress</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3b7d3241-404a-47ff-b077-9309ca66ab85">1</ID>
		<Name ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808">$WorkflowEngine_TaskActions_ReturnToRole</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3b7d3241-404a-47ff-b077-9309ca66ab85">2</ID>
		<Name ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808">$WorkflowEngine_TaskActions_Postpone</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3b7d3241-404a-47ff-b077-9309ca66ab85">3</ID>
		<Name ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808">$WorkflowEngine_TaskActions_ReturnFromPostpone</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="3b7d3241-404a-47ff-b077-9309ca66ab85">4</ID>
		<Name ID="2e22bdfe-2f25-4f96-98a6-a71b02ff8808">$WorkflowEngine_TaskActions_Complete</Name>
	</SchemeRecord>
</SchemeTable>