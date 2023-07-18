<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="7ecf1fb4-cd7b-4fea-9a3b-6e22b2186ed6" Name="TagCards" Group="Tags">
	<Description>Таблица с тегами карточек.</Description>
	<SchemeComplexColumn ID="f0dce15b-e37d-4a9e-b1ff-eee6736128d8" Name="Tag" Type="Reference(Typified) Not Null" ReferencedTable="0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f">
		<Description>Тег.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f0dce15b-e37d-009e-4000-0ee6736128d8" Name="TagID" Type="Guid Not Null" ReferencedColumn="0bf4050e-d7d4-01da-4000-0a4f0148dd7f" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2a465318-c005-403e-ab1d-431b5191f336" Name="Card" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Карточка, на которую добавлен тег.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2a465318-c005-003e-4000-031b5191f336" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a250900a-de99-40ce-abd7-24798faaee88" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который добавил тег.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a250900a-de99-00ce-4000-04798faaee88" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="458156b2-14a5-4d82-8ce5-88bb8af63463" Name="SetAt" Type="DateTime Not Null">
		<Description>Дата установки тега.</Description>
	</SchemePhysicalColumn>
	<SchemeIndex ID="de122a19-70fd-459d-a5dc-e58b6c74918e" Name="ndx_TagCards_CardIDUserID">
		<SchemeIndexedColumn Column="2a465318-c005-003e-4000-031b5191f336" />
		<SchemeIndexedColumn Column="a250900a-de99-00ce-4000-04798faaee88" />
		<SchemeIncludedColumn Column="f0dce15b-e37d-009e-4000-0ee6736128d8" />
	</SchemeIndex>
	<SchemeIndex ID="78e2fac1-82c9-46f3-943b-5b2d4dd5212e" Name="ndx_TagCards_TagIDSetAt">
		<SchemeIndexedColumn Column="f0dce15b-e37d-009e-4000-0ee6736128d8" />
		<SchemeIndexedColumn Column="458156b2-14a5-4d82-8ce5-88bb8af63463" />
		<SchemeIncludedColumn Column="2a465318-c005-003e-4000-031b5191f336" />
	</SchemeIndex>
</SchemeTable>