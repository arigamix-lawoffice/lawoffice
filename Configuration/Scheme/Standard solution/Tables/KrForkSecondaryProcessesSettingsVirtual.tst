<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="08119dad-4504-49f5-8273-a1851cc4a0d0" Name="KrForkSecondaryProcessesSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="08119dad-4504-00f5-2000-01851cc4a0d0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="08119dad-4504-01f5-4000-01851cc4a0d0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="08119dad-4504-00f5-3100-01851cc4a0d0" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b7898dcf-9bc9-46c7-a8f4-b79f5e0f046d" Name="SecondaryProcess" Type="Reference(Typified) Not Null" ReferencedTable="caac66aa-0cbb-4e2b-83fd-7c368e814d64">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b7898dcf-9bc9-00c7-4000-079f5e0f046d" Name="SecondaryProcessID" Type="Guid Not Null" ReferencedColumn="caac66aa-0cbb-012b-4000-0c368e814d64" />
		<SchemeReferencingColumn ID="031ec42a-5611-4a3c-a763-00f56861fbaa" Name="SecondaryProcessName" Type="String(255) Not Null" ReferencedColumn="444b8925-572a-449b-901e-8660ddeb3b6c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="08119dad-4504-00f5-5000-01851cc4a0d0" Name="pk_KrForkSecondaryProcessesSettingsVirtual">
		<SchemeIndexedColumn Column="08119dad-4504-00f5-3100-01851cc4a0d0" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="08119dad-4504-00f5-7000-01851cc4a0d0" Name="idx_KrForkSecondaryProcessesSettingsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="08119dad-4504-01f5-4000-01851cc4a0d0" />
	</SchemeIndex>
</SchemeTable>