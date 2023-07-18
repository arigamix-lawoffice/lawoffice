<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="29b2fb61-6880-43de-a40f-6688e1d0e247" Name="WorkflowLinkModes" Group="WorkflowEngine">
	<Description>Типы связи для переходов</Description>
	<SchemePhysicalColumn ID="1aca9753-e67a-4044-bc5d-656ad20fcc98" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="43e1fa98-b92a-40ef-9c5a-43b7332d575f" Name="Name" Type="String(Max) Not Null" />
	<SchemePrimaryKey ID="86d1154f-229a-4ddf-b431-2b9598e25d5f" Name="pk_WorkflowLinkModes">
		<SchemeIndexedColumn Column="1aca9753-e67a-4044-bc5d-656ad20fcc98" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="1aca9753-e67a-4044-bc5d-656ad20fcc98">0</ID>
		<Name ID="43e1fa98-b92a-40ef-9c5a-43b7332d575f">$WorkflowEngine_LinkModes_Default</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="1aca9753-e67a-4044-bc5d-656ad20fcc98">1</ID>
		<Name ID="43e1fa98-b92a-40ef-9c5a-43b7332d575f">$WorkflowEngine_LinkModes_AlwaysCreateNew</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="1aca9753-e67a-4044-bc5d-656ad20fcc98">2</ID>
		<Name ID="43e1fa98-b92a-40ef-9c5a-43b7332d575f">$WorkflowEngine_LinkModes_NeverCreateNew</Name>
	</SchemeRecord>
</SchemeTable>