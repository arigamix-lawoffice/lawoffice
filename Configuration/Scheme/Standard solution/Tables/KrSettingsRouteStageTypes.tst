<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="6681dd3c-cd54-405d-83bb-93ce533198fe" Name="KrSettingsRouteStageTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Разрешения по типам доступных этапов в маршрутах.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6681dd3c-cd54-005d-2000-03ce533198fe" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6681dd3c-cd54-015d-4000-03ce533198fe" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6681dd3c-cd54-005d-3100-03ce533198fe" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="ec5e7f29-4534-4523-97b2-9104ac2a4a8d" Name="StageType" Type="Reference(Typified) Not Null" ReferencedTable="7454f645-850f-4e9b-8c80-1f129c5cb1c4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ec5e7f29-4534-0023-4000-0104ac2a4a8d" Name="StageTypeID" Type="Guid Not Null" ReferencedColumn="faee08f2-f030-4f6d-83ad-d7fc2feff82f" />
		<SchemeReferencingColumn ID="d90fd94b-2c4f-4393-bdb1-6c9a9dc3a2d8" Name="StageTypeCaption" Type="String(300) Not Null" ReferencedColumn="a4fe24fc-2b3f-408a-8d2b-0440bd205eb8" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="4e9e85bc-0636-4a0b-9650-c801a13853aa" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="39e6d38f-4e35-45e9-8c71-42a932dce18c" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4e9e85bc-0636-000b-4000-0801a13853aa" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="39e6d38f-4e35-00e9-3100-02a932dce18c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6681dd3c-cd54-005d-5000-03ce533198fe" Name="pk_KrSettingsRouteStageTypes">
		<SchemeIndexedColumn Column="6681dd3c-cd54-005d-3100-03ce533198fe" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6681dd3c-cd54-005d-7000-03ce533198fe" Name="idx_KrSettingsRouteStageTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6681dd3c-cd54-015d-4000-03ce533198fe" />
	</SchemeIndex>
</SchemeTable>