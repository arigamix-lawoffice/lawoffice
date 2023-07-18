<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="a2b54bf4-20ae-4fdd-8b2e-21ef246cfb32" Name="WeSubprocessActionStartMapping" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Маппинг параметров процесса, передаваемых в параметры подпроцесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2b54bf4-20ae-00dd-2000-01ef246cfb32" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a2b54bf4-20ae-01dd-4000-01ef246cfb32" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2b54bf4-20ae-00dd-3100-01ef246cfb32" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="857d211c-de3f-4614-a850-79d10abc105b" Name="SourceParam" Type="Reference(Typified) Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="857d211c-de3f-0014-4000-09d10abc105b" Name="SourceParamID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="e45bf469-7e50-474b-8fee-388d8cd37cfe" Name="SourceParamText" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ea42000d-d96d-46f9-b0f5-31b8c9d1ae98" Name="TargetParam" Type="Reference(Typified) Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ea42000d-d96d-00f9-4000-01b8c9d1ae98" Name="TargetParamID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="ec723b64-7d73-484e-a494-77ca7a087d06" Name="TargetParamText" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2b54bf4-20ae-00dd-5000-01ef246cfb32" Name="pk_WeSubprocessActionStartMapping">
		<SchemeIndexedColumn Column="a2b54bf4-20ae-00dd-3100-01ef246cfb32" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2b54bf4-20ae-00dd-7000-01ef246cfb32" Name="idx_WeSubprocessActionStartMapping_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a2b54bf4-20ae-01dd-4000-01ef246cfb32" />
	</SchemeIndex>
</SchemeTable>