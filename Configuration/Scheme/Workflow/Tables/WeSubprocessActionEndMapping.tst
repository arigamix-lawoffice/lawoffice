<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="ea4cd339-7a97-4221-a223-44f9b6ce6ce1" Name="WeSubprocessActionEndMapping" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ea4cd339-7a97-0021-2000-04f9b6ce6ce1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ea4cd339-7a97-0121-4000-04f9b6ce6ce1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ea4cd339-7a97-0021-3100-04f9b6ce6ce1" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="8fbba877-9560-4874-a4e8-6835c6ba62ff" Name="SourceParam" Type="Reference(Abstract) Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8fbba877-9560-0074-4000-0835c6ba62ff" Name="SourceParamID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="e6f5c804-8c49-4403-b653-c7851f852217" Name="SourceParamText" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c880de95-9f32-45d4-a4a6-81195a2f3ec1" Name="TargetParam" Type="Reference(Abstract) Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c880de95-9f32-00d4-4000-01195a2f3ec1" Name="TargetParamID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="ca172f47-0cc7-4cc9-b592-fc5cac479479" Name="TargetParamText" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ea4cd339-7a97-0021-5000-04f9b6ce6ce1" Name="pk_WeSubprocessActionEndMapping">
		<SchemeIndexedColumn Column="ea4cd339-7a97-0021-3100-04f9b6ce6ce1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ea4cd339-7a97-0021-7000-04f9b6ce6ce1" Name="idx_WeSubprocessActionEndMapping_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ea4cd339-7a97-0121-4000-04f9b6ce6ce1" />
	</SchemeIndex>
</SchemeTable>