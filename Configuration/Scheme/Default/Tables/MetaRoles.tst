<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="984ac49b-30d7-48da-ab6a-3fe4bcf7513d" Name="MetaRoles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Метароли.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="984ac49b-30d7-00da-2000-0fe4bcf7513d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="984ac49b-30d7-01da-4000-0fe4bcf7513d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c7689dcf-c44c-409e-a946-b4cbde97c11a" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя роли.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b4eae22e-f3ff-4ff3-ba86-33fabf4d06a5" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="53a3ee37-b714-4503-9e0e-e2ed1ccd164f">
		<Description>Тип метароли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4eae22e-f3ff-00f3-4000-03fabf4d06a5" Name="TypeID" Type="Int16 Not Null" ReferencedColumn="5302e6ab-b1ec-4d3f-881a-068d2553d25e">
			<Description>Идентификатор типа метароли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1205412f-60b4-4550-ba31-9653d8cca021" Name="Generator" Type="Reference(Typified) Not Null" ReferencedTable="747bb53c-9e47-418d-892d-fb52a18eb42d" WithForeignKey="false">
		<Description>Генератор, создавший метароль.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1205412f-60b4-0050-4000-0653d8cca021" Name="GeneratorID" Type="Guid Not Null" ReferencedColumn="747bb53c-9e47-018d-4000-0b52a18eb42d" />
		<SchemeReferencingColumn ID="b4a335fd-81ae-468f-9b6c-fb3174653ccd" Name="GeneratorName" Type="String(128) Not Null" ReferencedColumn="c843b06f-365a-414f-bc07-e265e8c45b19">
			<Description>Имя генератора метаролей.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="3b08dcf1-8eb3-46b0-9981-e06e110bd9d7" Name="IDGuid" Type="Guid Null" IsSparse="true">
		<Description>Уникальный идентификатор метароли, возвращённый генератором.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0845dee0-fb10-4650-8fcd-71c4bf6f1643" Name="IDInteger" Type="Int32 Null" IsSparse="true">
		<Description>Числовой идентификатор метароли, возвращённый генератором.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1668a68c-79f6-454c-9faa-51c336607520" Name="IDString" Type="String(128) Null" IsSparse="true">
		<Description>Строковый идентификатор метароли, возвращённый генератором.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="984ac49b-30d7-00da-5000-0fe4bcf7513d" Name="pk_MetaRoles" IsClustered="true">
		<SchemeIndexedColumn Column="984ac49b-30d7-01da-4000-0fe4bcf7513d" />
	</SchemePrimaryKey>
	<SchemeIndex ID="39a8fb7c-b997-4f4c-98d2-85b1928d08ea" Name="ndx_MetaRoles_GeneratorIDIDGuid">
		<SchemeIndexedColumn Column="1205412f-60b4-0050-4000-0653d8cca021" />
		<SchemeIndexedColumn Column="3b08dcf1-8eb3-46b0-9981-e06e110bd9d7" />
	</SchemeIndex>
</SchemeTable>