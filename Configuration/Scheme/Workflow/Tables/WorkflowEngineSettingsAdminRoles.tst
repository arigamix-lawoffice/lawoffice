<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="e493b168-0c0a-4ebc-812e-229bc43aec25" Name="WorkflowEngineSettingsAdminRoles" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, имеющих админские права к карточке шаблона БП</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e493b168-0c0a-00bc-2000-029bc43aec25" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e493b168-0c0a-01bc-4000-029bc43aec25" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e493b168-0c0a-00bc-3100-029bc43aec25" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="28d2ff7f-dbf6-40cb-bf5e-d1833af2c833" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="28d2ff7f-dbf6-00cb-4000-01833af2c833" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="15555af9-62f4-4506-b534-2606dd7e0229" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e493b168-0c0a-00bc-5000-029bc43aec25" Name="pk_WorkflowEngineSettingsAdminRoles">
		<SchemeIndexedColumn Column="e493b168-0c0a-00bc-3100-029bc43aec25" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e493b168-0c0a-00bc-7000-029bc43aec25" Name="idx_WorkflowEngineSettingsAdminRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e493b168-0c0a-01bc-4000-029bc43aec25" />
	</SchemeIndex>
</SchemeTable>