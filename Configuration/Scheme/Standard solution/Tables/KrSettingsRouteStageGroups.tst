<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9416f8fb-95b0-4617-98a6-f576580bfd49" Name="KrSettingsRouteStageGroups" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Разрешения по группам этапов в маршрутах.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9416f8fb-95b0-0017-2000-0576580bfd49" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9416f8fb-95b0-0117-4000-0576580bfd49" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9416f8fb-95b0-0017-3100-0576580bfd49" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="8c2eb44f-8412-4b6b-b329-5fcd691f2598" Name="StageGroup" Type="Reference(Typified) Not Null" ReferencedTable="fde6b6e3-f7b6-467f-96e1-e2df41a22f05" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8c2eb44f-8412-006b-4000-0fcd691f2598" Name="StageGroupID" Type="Guid Not Null" ReferencedColumn="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
		<SchemeReferencingColumn ID="a3406d41-e16a-490b-a456-3d56364c73c6" Name="StageGroupName" Type="String(255) Not Null" ReferencedColumn="fc8faabd-cc86-44b3-8430-1a0e816cea27" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1d1d2b58-bbd1-47c5-9c05-3b75f77bf8a1" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="39e6d38f-4e35-45e9-8c71-42a932dce18c" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1d1d2b58-bbd1-00c5-4000-0b75f77bf8a1" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="39e6d38f-4e35-00e9-3100-02a932dce18c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9416f8fb-95b0-0017-5000-0576580bfd49" Name="pk_KrSettingsRouteStageGroups">
		<SchemeIndexedColumn Column="9416f8fb-95b0-0017-3100-0576580bfd49" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9416f8fb-95b0-0017-7000-0576580bfd49" Name="idx_KrSettingsRouteStageGroups_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9416f8fb-95b0-0117-4000-0576580bfd49" />
	</SchemeIndex>
</SchemeTable>