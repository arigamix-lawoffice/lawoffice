<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="6efad92a-46be-4d44-a495-271b264f016b" Name="WorkflowEngineSettingsObjectTypeFields" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Поля для типа объекта</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6efad92a-46be-0044-2000-071b264f016b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6efad92a-46be-0144-4000-071b264f016b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6efad92a-46be-0044-3100-071b264f016b" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="78c5d807-8e94-4634-8e06-aebb03c5a3b3" Name="Field" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="78c5d807-8e94-0034-4000-0ebb03c5a3b3" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="ab3295b2-54a4-48a0-81fe-74a4a956d81e" Name="FieldName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="daf48027-4492-472d-9168-86bb9a4cb83b" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="140a411c-3b68-44c0-8a5b-cf641d2421f2" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="daf48027-4492-002d-4000-06bb9a4cb83b" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="140a411c-3b68-00c0-3100-0f641d2421f2" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6efad92a-46be-0044-5000-071b264f016b" Name="pk_WorkflowEngineSettingsObjectTypeFields">
		<SchemeIndexedColumn Column="6efad92a-46be-0044-3100-071b264f016b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6efad92a-46be-0044-7000-071b264f016b" Name="idx_WorkflowEngineSettingsObjectTypeFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6efad92a-46be-0144-4000-071b264f016b" />
	</SchemeIndex>
</SchemeTable>