<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="300db9a6-f6a0-48a8-b6c3-5f8891817cdd" Name="DeletedVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="300db9a6-f6a0-00a8-2000-0f8891817cdd" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="300db9a6-f6a0-01a8-4000-0f8891817cdd" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9a77f36e-cd22-4a73-b067-334910c6254c" Name="CardStorage" Type="String(Max) Not Null">
		<Description>Текстовое представление пакета удалённой карточки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dcc84486-d845-4020-9bc8-a91656ff224a" Name="CardIDString" Type="String(64) Not Null">
		<Description>Текстовое представление идентификатора удалённой карточки.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="300db9a6-f6a0-00a8-5000-0f8891817cdd" Name="pk_DeletedVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="300db9a6-f6a0-01a8-4000-0f8891817cdd" />
	</SchemePrimaryKey>
</SchemeTable>