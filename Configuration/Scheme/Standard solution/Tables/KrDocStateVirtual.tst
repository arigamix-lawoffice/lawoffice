<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e4345324-ad03-46ca-a157-5f71742e5816" Name="KrDocStateVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная карточка для состояния документа.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e4345324-ad03-00ca-2000-0f71742e5816" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e4345324-ad03-01ca-4000-0f71742e5816" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a6efb587-23e5-43a5-959d-179010303975" Name="StateID" Type="Int16 Not Null">
		<Description>Числовой идентификатор состояния.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="67c624bb-eb7f-4a44-a934-c7efa869ff21" Name="Name" Type="String(128) Not Null">
		<Description>Название состояния. Может быть строкой локализации.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e68655b6-c95f-44dd-8fc1-8c13026cf635" Name="Partition" Type="Reference(Typified) Not Null" ReferencedTable="5ca00fac-d04e-4b82-8139-9778518e00bf">
		<Description>Библиотека схемы, в которую включается состояние документа.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e68655b6-c95f-00dd-4000-0c13026cf635" Name="PartitionID" Type="Guid Not Null" ReferencedColumn="fc636796-f583-4306-ad69-30fb2a070f9a" />
		<SchemeReferencingColumn ID="9713e5b9-83d9-4b0a-8599-edfdb319207a" Name="PartitionName" Type="String(128) Not Null" ReferencedColumn="6af8d64d-cff0-4ece-9db3-b1f38e73814d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e4345324-ad03-00ca-5000-0f71742e5816" Name="pk_KrDocStateVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="e4345324-ad03-01ca-4000-0f71742e5816" />
	</SchemePrimaryKey>
</SchemeTable>