<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="610d8253-e293-4676-abcb-e7a0ac1a084d" Name="WebApplications" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Карточки приложений-ассистентов web-клиента, таких как Deski</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="610d8253-e293-0076-2000-07a0ac1a084d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="610d8253-e293-0176-4000-07a0ac1a084d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fd10c072-8630-4b38-b83c-7c9b4bbb280e" Name="Name" Type="String(256) Not Null">
		<Description>Название приложения, в ней обычно записывается архитектура (ОС пользователя, разрядность). Можно использовать строку локализации. Примеры: "Windows 64 bit" или "Astra Linux"</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="06c7330a-f93e-448e-9f6b-6d67fb376611" Name="AppVersion" Type="String(128) Null" />
	<SchemePhysicalColumn ID="02ad8b32-2d36-4325-ada9-b66ef74a0511" Name="PlatformVersion" Type="String(128) Null" />
	<SchemePhysicalColumn ID="ed069805-8348-43fa-a952-bf49a5375fc9" Name="Description" Type="String(Max) Null">
		<Description>Описание приложения, которое отображается пользователю при скачивании. Может быть строкой локализации.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e88b3cd5-ec06-4824-bcff-07849ba90b03" Name="ExecutableFileName" Type="String(256) Not Null">
		<Description>Имя запускаемого файла среди скаченных приложением.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="17ff1bff-520f-4af7-bd65-4a1b21587fb0" Name="Language" Type="Reference(Typified) Null" ReferencedTable="1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="17ff1bff-520f-00f7-4000-0a1b21587fb0" Name="LanguageID" Type="Int16 Null" ReferencedColumn="f13de4a3-34d7-4e7b-95b6-f34372ed724c" />
		<SchemeReferencingColumn ID="5c364a47-e32d-48ff-982a-11ce81a6cb59" Name="LanguageCaption" Type="String(256) Null" ReferencedColumn="40a3d47c-40f7-48bd-ab8e-edef2f84094d" />
		<SchemeReferencingColumn ID="5c25372a-5f10-4469-ad87-96ef35b4c198" Name="LanguageCode" Type="AnsiString(3) Null" ReferencedColumn="9e7084bb-c1dc-4ace-90c9-800dbcf7f3c2" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2cb346f7-0cdb-49ae-bc42-c3437575a77f" Name="OSName" Type="String(128) Null">
		<Description>Название операционной системы, для которой предназначено приложение. Может быть пустым, если не указано. Используется для выбора наиболее подходящей версии приложения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f76877a2-8932-4a58-959a-93efef80c56e" Name="Client64Bit" Type="Boolean Not Null">
		<Description>Признак того, что клиентское приложение является 64-битным. True - 64-битное приложение, False - 32-битное приложение или разрядность неизвестна.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e22df1a6-4ebf-4118-b328-71ccabbc9fe9" Name="df_WebApplications_Client64Bit" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="610d8253-e293-0076-5000-07a0ac1a084d" Name="pk_WebApplications" IsClustered="true">
		<SchemeIndexedColumn Column="610d8253-e293-0176-4000-07a0ac1a084d" />
	</SchemePrimaryKey>
	<SchemeIndex ID="84dc75fc-fd52-4710-b4df-3901381b4386" Name="ndx_WebApplications_NameLanguageID" IsUnique="true">
		<SchemeIndexedColumn Column="fd10c072-8630-4b38-b83c-7c9b4bbb280e" />
		<SchemeIndexedColumn Column="17ff1bff-520f-00f7-4000-0a1b21587fb0" />
	</SchemeIndex>
</SchemeTable>