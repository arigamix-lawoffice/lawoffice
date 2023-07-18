<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="67b602c1-ea47-4716-92ba-81f625ba36f1" Name="WorkflowSignalProcessingModes" Group="WorkflowEngine">
	<SchemePhysicalColumn ID="03a94b31-a856-4bb3-a570-ba6ab6772730" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="7252edda-77c8-4807-82da-f01e75711c68" Name="Name" Type="String(Max) Not Null" />
	<SchemePrimaryKey ID="9eab0b5c-98c5-48f6-a5b7-a22f094a955c" Name="pk_WorkflowSignalProcessingModes">
		<SchemeIndexedColumn Column="03a94b31-a856-4bb3-a570-ba6ab6772730" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="03a94b31-a856-4bb3-a570-ba6ab6772730">0</ID>
		<Name ID="7252edda-77c8-4807-82da-f01e75711c68">$WorkflowEngine_SignalProcessingMode_Default</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="03a94b31-a856-4bb3-a570-ba6ab6772730">1</ID>
		<Name ID="7252edda-77c8-4807-82da-f01e75711c68">$WorkflowEngine_SignalProcessingMode_Async</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="03a94b31-a856-4bb3-a570-ba6ab6772730">2</ID>
		<Name ID="7252edda-77c8-4807-82da-f01e75711c68">$WorkflowEngine_SignalProcessingMode_AfterUploadingFiles</Name>
	</SchemeRecord>
</SchemeTable>