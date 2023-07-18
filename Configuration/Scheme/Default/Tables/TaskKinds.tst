<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="856068b1-0e78-4aa8-8e7a-4f53d91a7298" Name="TaskKinds" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Виды заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="856068b1-0e78-00a8-2000-0f53d91a7298" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="856068b1-0e78-01a8-4000-0f53d91a7298" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="63d9110b-7628-4bf9-9dae-750c3035e48d" Name="Caption" Type="String(128) Not Null">
		<Description>Название вида заданий.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="856068b1-0e78-00a8-5000-0f53d91a7298" Name="pk_TaskKinds" IsClustered="true">
		<SchemeIndexedColumn Column="856068b1-0e78-01a8-4000-0f53d91a7298" />
	</SchemePrimaryKey>
</SchemeTable>