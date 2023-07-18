<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="319be329-6cd3-457a-b792-41c26a266b95" Name="TaskHistoryGroupTypes" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="319be329-6cd3-007a-2000-01c26a266b95" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="319be329-6cd3-017a-4000-01c26a266b95" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="bf5a5121-9947-45f6-a8a0-2608885b4e19" Name="Caption" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="86052b22-2146-4912-9964-2535dff89c92" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1a4c5c31-5ce8-439e-828a-40a60adbf5e7" Name="Placeholders" Type="String(128) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="319be329-6cd3-007a-5000-01c26a266b95" Name="pk_TaskHistoryGroupTypes" IsClustered="true">
		<SchemeIndexedColumn Column="319be329-6cd3-017a-4000-01c26a266b95" />
	</SchemePrimaryKey>
</SchemeTable>