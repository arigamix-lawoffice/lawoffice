<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e8f3015f-4085-4df8-bafb-4c5b466965c0" Name="KrForkNestedProcessesSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8f3015f-4085-00f8-2000-0c5b466965c0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e8f3015f-4085-01f8-4000-0c5b466965c0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8f3015f-4085-00f8-3100-0c5b466965c0" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="65ed31aa-bcf4-48d3-8a8c-d35e07c77c15" Name="NestedProcessID" Type="Guid Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8f3015f-4085-00f8-5000-0c5b466965c0" Name="pk_KrForkNestedProcessesSettingsVirtual">
		<SchemeIndexedColumn Column="e8f3015f-4085-00f8-3100-0c5b466965c0" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8f3015f-4085-00f8-7000-0c5b466965c0" Name="idx_KrForkNestedProcessesSettingsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e8f3015f-4085-01f8-4000-0c5b466965c0" />
	</SchemeIndex>
</SchemeTable>