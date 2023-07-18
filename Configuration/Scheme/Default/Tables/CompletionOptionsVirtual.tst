<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="cfff92c8-26e6-42e5-b45d-837bc374022d" Name="CompletionOptionsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная карточка для варианта завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfff92c8-26e6-00e5-2000-037bc374022d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cfff92c8-26e6-01e5-4000-037bc374022d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7d96783f-06f3-44b2-96de-ba8cf0f58bde" Name="OptionID" Type="Guid Not Null">
		<Description>Идентификатор варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c48253e4-ecf0-47af-bc09-897f397e2fa0" Name="Name" Type="String(128) Not Null">
		<Description>Имя варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6f17a789-df79-42d4-981a-bf2beff3f136" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое пользователю имя варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="cbe4828f-eb6e-4a8c-b9d5-89cf4b22176f" Name="Partition" Type="Reference(Typified) Not Null" ReferencedTable="5ca00fac-d04e-4b82-8139-9778518e00bf">
		<Description>Библиотека схемы, в которую включается вариант завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cbe4828f-eb6e-008c-4000-09cf4b22176f" Name="PartitionID" Type="Guid Not Null" ReferencedColumn="fc636796-f583-4306-ad69-30fb2a070f9a" />
		<SchemeReferencingColumn ID="a4755f2a-f3bd-4d7e-91ed-59e2cf36acab" Name="PartitionName" Type="String(128) Not Null" ReferencedColumn="6af8d64d-cff0-4ece-9db3-b1f38e73814d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfff92c8-26e6-00e5-5000-037bc374022d" Name="pk_CompletionOptionsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="cfff92c8-26e6-01e5-4000-037bc374022d" />
	</SchemePrimaryKey>
</SchemeTable>