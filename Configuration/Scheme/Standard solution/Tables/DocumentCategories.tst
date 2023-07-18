<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f939aa52-dc1a-40b2-af4a-cb2757e8390a" Name="DocumentCategories" Group="Common" InstanceType="Cards" ContentType="Entries">
	<Description>Категории документов для Протоколов, СЗ и Документа (бывшие типы протоколов)</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f939aa52-dc1a-00b2-2000-0b2757e8390a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f939aa52-dc1a-01b2-4000-0b2757e8390a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="3dd39fa6-b8bd-4084-8aeb-f129f796f450" Name="Name" Type="String(128) Not Null">
		<Description>Наименование типа протокола.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="35ae46da-2264-46ac-a339-9f41ef4acffd" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<Description>Тип документа или карточки, для которого показывается категория</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="35ae46da-2264-00ac-4000-0f41ef4acffd" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="f04955e1-aeeb-47c3-9dd7-648f819d8cc0" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f939aa52-dc1a-00b2-5000-0b2757e8390a" Name="pk_DocumentCategories" IsClustered="true">
		<SchemeIndexedColumn Column="f939aa52-dc1a-01b2-4000-0b2757e8390a" />
	</SchemePrimaryKey>
	<SchemeIndex ID="5aaaeab1-539c-4bdb-adce-753552ed9517" Name="ndx_DocumentCategories_NameTypeID" IsUnique="true">
		<SchemeIndexedColumn Column="3dd39fa6-b8bd-4084-8aeb-f129f796f450">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
		<SchemeIndexedColumn Column="35ae46da-2264-00ac-4000-0f41ef4acffd" />
	</SchemeIndex>
</SchemeTable>