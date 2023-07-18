<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b6509d11-f1b3-4f54-b34f-4a996f66c71c" Name="TemplatesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Информация по шаблонам карточек, подготовленная для вывода в UI.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b6509d11-f1b3-0054-2000-0a996f66c71c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b6509d11-f1b3-0154-4000-0a996f66c71c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5cfb93f4-d528-48c1-859b-82c6ea1c48e5" Name="Card" Type="Reference(Abstract) Not Null">
		<Description>Карточка, из которой был создан шаблон.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5cfb93f4-d528-00c1-4000-02c6ea1c48e5" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="ce871da7-2277-484d-a791-b3a90fac5a14" Name="CardDigest" Type="String(128) Not Null">
			<Description>Digest карточки, из которой был создан шаблон, или специальный плейсхолдер "без дайджеста", если Digest отсутствовал.</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b6509d11-f1b3-0054-5000-0a996f66c71c" Name="pk_TemplatesVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="b6509d11-f1b3-0154-4000-0a996f66c71c" />
	</SchemePrimaryKey>
</SchemeTable>