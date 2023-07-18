<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3f903804-3c70-4828-9887-5c9268d20b7d" Name="FmAttachments" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица с прикрепленными элементами</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f903804-3c70-0028-2000-0c9268d20b7d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3f903804-3c70-0128-4000-0c9268d20b7d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f903804-3c70-0028-3100-0c9268d20b7d" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="022ebbf5-5cfe-4085-a672-bfea94568afe" Name="Uri" Type="String(1024) Null">
		<Description>Служебное поле где хранится ИД прикрепленного сообщения</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7ba70827-e798-4841-9f91-1e0b6023d6b1" Name="Caption" Type="String(1024) Not Null">
		<Description>Заголовок</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="9eb94554-b933-4be8-80b1-a0bd2b398071" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="74caae68-ee60-4d36-b6af-b81bdd06d4a3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9eb94554-b933-00e8-4000-00bd2b398071" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="9c3cfae0-6106-42dd-81f3-eb05fc71e011" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a83a038a-a641-48f6-a476-9d2d14aba8db" Name="Message" Type="Reference(Typified) Not Null" ReferencedTable="a03f6c5d-e719-43d6-bcc5-d2ea321765ab">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a83a038a-a641-00f6-4000-0d2d14aba8db" Name="MessageRowID" Type="Guid Not Null" ReferencedColumn="a03f6c5d-e719-00d6-3100-02ea321765ab" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a1064ae2-0859-488c-b51d-0705ba3359fe" Name="FileSize" Type="Int64 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2408aba9-2856-40bf-8d41-8001daf7adf4" Name="df_FmAttachments_FileSize" Value="-1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e1c58cd1-5872-49df-b695-123cf4e803ed" Name="OriginalFileID" Type="Guid Null" />
	<SchemePhysicalColumn ID="3a16611e-9b02-44a2-aa68-b740598b5092" Name="ShowInToolbar" Type="Boolean Null">
		<Description>Признак, что вложение нужно показывать в режиме чтения в нижней части контрола.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f903804-3c70-0028-5000-0c9268d20b7d" Name="pk_FmAttachments">
		<SchemeIndexedColumn Column="3f903804-3c70-0028-3100-0c9268d20b7d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f903804-3c70-0028-7000-0c9268d20b7d" Name="idx_FmAttachments_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3f903804-3c70-0128-4000-0c9268d20b7d" />
	</SchemeIndex>
	<SchemeIndex ID="9f6a91a9-4409-4723-a148-43cda6792937" Name="ndx_FmAttachments_MessageRowID">
		<SchemeIndexedColumn Column="a83a038a-a641-00f6-4000-0d2d14aba8db" />
	</SchemeIndex>
</SchemeTable>