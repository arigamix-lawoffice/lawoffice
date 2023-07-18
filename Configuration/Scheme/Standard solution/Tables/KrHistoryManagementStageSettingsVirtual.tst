<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e08c3797-0f25-4841-a2a4-37bb0b938f88" Name="KrHistoryManagementStageSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e08c3797-0f25-0041-2000-07bb0b938f88" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e08c3797-0f25-0141-4000-07bb0b938f88" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="4d00e2ac-6a9a-4518-8047-9cc31965be01" Name="TaskHistoryGroupType" Type="Reference(Typified) Not Null" ReferencedTable="319be329-6cd3-457a-b792-41c26a266b95">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4d00e2ac-6a9a-0018-4000-0cc31965be01" Name="TaskHistoryGroupTypeID" Type="Guid Not Null" ReferencedColumn="319be329-6cd3-017a-4000-01c26a266b95" />
		<SchemeReferencingColumn ID="3e92e025-b4c6-48e0-aeeb-5bf4f142ac3c" Name="TaskHistoryGroupTypeCaption" Type="String(128) Not Null" ReferencedColumn="bf5a5121-9947-45f6-a8a0-2608885b4e19" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="95287b87-b12e-478a-878b-e05ab3ce3670" Name="ParentTaskHistoryGroupType" Type="Reference(Typified) Null" ReferencedTable="319be329-6cd3-457a-b792-41c26a266b95">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="95287b87-b12e-008a-4000-005ab3ce3670" Name="ParentTaskHistoryGroupTypeID" Type="Guid Null" ReferencedColumn="319be329-6cd3-017a-4000-01c26a266b95" />
		<SchemeReferencingColumn ID="e7e925cd-ce47-4b90-a340-aabd42593134" Name="ParentTaskHistoryGroupTypeCaption" Type="String(128) Null" ReferencedColumn="bf5a5121-9947-45f6-a8a0-2608885b4e19" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4244f8b8-bcf1-4e62-b59f-c6c695d6f1f1" Name="NewIteration" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ee8cdabf-9064-4462-ad4b-cc7b7606e4ac" Name="df_KrHistoryManagementStageSettingsVirtual_NewIteration" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e08c3797-0f25-0041-5000-07bb0b938f88" Name="pk_KrHistoryManagementStageSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="e08c3797-0f25-0141-4000-07bb0b938f88" />
	</SchemePrimaryKey>
</SchemeTable>