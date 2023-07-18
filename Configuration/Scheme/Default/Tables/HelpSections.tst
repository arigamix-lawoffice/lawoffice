<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="741301fd-f38a-4cca-bab9-df1328d53b53" Name="HelpSections" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Разделы справки.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="741301fd-f38a-00ca-2000-0f1328d53b53" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="741301fd-f38a-01ca-4000-0f1328d53b53" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a10e8dcf-87c7-46c2-ab7f-8efe74cd4016" Name="Code" Type="String(128) Not Null">
		<Description>Уникальная строка для адресации из настроек. Например, CONTRACTS_DUE_DATE.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0864dbe9-b490-4a87-b352-f0878ea82668" Name="Name" Type="String(Max) Not Null">
		<Description>Строковое описание этого раздела для заголовка окна.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1f115761-97a5-448a-a3cd-0430290b7d24" Name="RichText" Type="String(Max) Null">
		<Description>Форматированный текст с собственно данными раздела справки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6cfe8eb4-82de-4094-8864-58b00edc8bbc" Name="PlainText" Type="String(Max) Null">
		<Description>Для полнотекстового поиска. Для этой колонки на сервере извлекается текст из RichText-а при каждом его изменении.</Description>
	</SchemePhysicalColumn>
	<SchemeUniqueKey ID="e9e6a90c-73c1-4591-81b5-37322462d510" Name="ndx_HelpSections_Code">
		<SchemeIndexedColumn Column="a10e8dcf-87c7-46c2-ab7f-8efe74cd4016" />
	</SchemeUniqueKey>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="741301fd-f38a-00ca-5000-0f1328d53b53" Name="pk_HelpSections" IsClustered="true">
		<SchemeIndexedColumn Column="741301fd-f38a-01ca-4000-0f1328d53b53" />
	</SchemePrimaryKey>
</SchemeTable>