<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="006f8d09-5eff-46fc-8bc5-d7c5f6a81d44" Name="KrVirtualFiles" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для карточи виртуального файла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="006f8d09-5eff-00fc-2000-07c5f6a81d44" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="006f8d09-5eff-01fc-4000-07c5f6a81d44" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b5f49aaf-9563-42f0-9455-96cfc8368c89" Name="Name" Type="String(Max) Not Null">
		<Description>Имя карточки виртуального файла</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="212e07f9-9d53-4df5-a9a1-16bbc24d9275" Name="FileName" Type="String(Max) Null">
		<Description>Переопределение имени файла</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a596a2d4-2f52-40f7-bd26-43bbe10bb862" Name="FileID" Type="Guid Not Null">
		<Description>Сгенерированный идентификатор файла</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="043abfbf-a379-4824-8ca0-94e97c60279d" Name="FileVersionID" Type="Guid Not Null">
		<Description>Сгенерированный идентификатор основной версии файла</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="87484422-b47c-4475-bc99-c49142a4cb0f" Name="InitializationScenario" Type="String(Max) Null">
		<Description>Сценарий инициализации файла</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e29cf0d2-72ee-432d-893a-df5c1bf0a689" Name="FileTemplate" Type="Reference(Typified) Not Null" ReferencedTable="98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0">
		<Description>Шаблон файла, по которому генерируется виртуальный файл</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e29cf0d2-72ee-002d-4000-0f5c1bf0a689" Name="FileTemplateID" Type="Guid Not Null" ReferencedColumn="98e0c3a9-0b9a-01ec-4000-0a077f6ff5f0" />
		<SchemeReferencingColumn ID="3b6d3223-2bd5-43ce-8651-7f56e69be98e" Name="FileTemplateName" Type="String(256) Not Null" ReferencedColumn="db93e6bd-9e6a-4232-bf8c-bfe652e5573c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="231bd0be-2150-4eba-8de6-d985c7399972" Name="FileCategory" Type="Reference(Typified) Null" ReferencedTable="e1599715-02d4-4ca9-b63e-b4b1ce642c7a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="231bd0be-2150-00ba-4000-0985c7399972" Name="FileCategoryID" Type="Guid Null" ReferencedColumn="e1599715-02d4-01a9-4000-04b1ce642c7a" />
		<SchemeReferencingColumn ID="0a508d3d-a305-4d39-b916-ea33b461e1cb" Name="FileCategoryName" Type="String(255) Null" ReferencedColumn="e2598c40-038d-4af4-9907-f2514170cc4d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="98166aa5-21f6-45d8-9102-b15f57ef5f12" Name="Conditions" Type="BinaryJson Null">
		<Description>Настройки условий в формате json</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="006f8d09-5eff-00fc-5000-07c5f6a81d44" Name="pk_KrVirtualFiles" IsClustered="true">
		<SchemeIndexedColumn Column="006f8d09-5eff-01fc-4000-07c5f6a81d44" />
	</SchemePrimaryKey>
	<SchemeIndex ID="0bc2dcd0-8df5-4702-81f7-31422bbbc195" Name="ndx_KrVirtualFiles_FileTemplateID">
		<Description>Индекс, чтобы файловые шаблоны удалялись быстро, независимо от количества виртуальных файлов.</Description>
		<SchemeIndexedColumn Column="e29cf0d2-72ee-002d-4000-0f5c1bf0a689" />
	</SchemeIndex>
</SchemeTable>