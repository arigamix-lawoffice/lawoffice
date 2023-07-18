<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="64bbc32d-95d6-434b-9c08-0288344d53bb" Name="FileSourcesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Способы хранения файлов. Виртуальная таблица, обеспечивающая редактирование таблицы FileSources через карточку настроек.
Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка SourceID - идентификатору источника файлов, т.е. аналог FileSources.ID.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="64bbc32d-95d6-004b-2000-0288344d53bb" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="64bbc32d-95d6-014b-4000-0288344d53bb" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="64bbc32d-95d6-004b-3100-0288344d53bb" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="b678b495-da24-486b-aefe-cf6b9c1d7fac" Name="IsDefault" Type="Boolean Not Null">
		<Description>Признак того, что текущая запись является источником файлов по умолчанию.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f25f3e1a-ef94-4ff9-9475-6fee46e254e4" Name="df_FileSourcesVirtual_IsDefault" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f98a828a-bf2d-4226-b2d1-3a102fbd91fe" Name="SourceID" Type="Int16 Not Null">
		<Description>Идентификатор способа хранения файлов. Соответствует колонке FileSources.ID, используется для редактирования.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d3ca6f75-975d-49bd-a910-90be1f0289cc" Name="df_FileSourcesVirtual_SourceID" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fac1e7a4-bc29-463f-b71a-97b1705460cb" Name="SourceIDText" Type="String(32) Not Null">
		<Description>Текстовая информация по идентификатору SourceID и признаку IsDefault.
Используется для удобства отображения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0c06e93b-20bd-44ac-96b4-8fdca17a42f6" Name="Name" Type="String(128) Not Null">
		<Description>Название способа хранения файлов.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="63f3edd3-bc35-4d5c-8adb-840fc59711ef" Name="IsDatabase" Type="Boolean Not Null">
		<Description>True, если запись соответствует контенту в базе данных;
False, если запись соответствует контента на файловой системе.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="08737eff-323b-4454-bdb4-bf32dba30d97" Name="df_FileSourcesVirtual_IsDatabase" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dd25b4ef-d70d-4ba7-9c75-f261af7add42" Name="Path" Type="String(255) Not Null">
		<Description>Если IsDatabase=True, то это название строки подключения к базе данных из конфигурационного файла.
Если IsDatabase=False, то это полный путь к файловой папке.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e12358d5-baf6-48fc-8602-1193652bd24e" Name="Description" Type="String(Max) Null">
		<Description>Текстовое описание источника файлов. Необязательные комментарии.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="95543371-0e8c-4c6e-a8a0-7512619e6cf1" Name="Size" Type="Int32 Not Null">
		<Description>Текущий размер занятого места в файловой папке или в базе данных. Не задаётся и не используется системой.
Рекомендуется для реализации логики в расширениях, связанной с выбором источника файлов в зависимости от свободного места.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f1197ad8-7127-4a70-b156-0ee782b903a9" Name="df_FileSourcesVirtual_Size" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a8beea86-063d-40ee-87ba-58349696660d" Name="MaxSize" Type="Int32 Not Null">
		<Description>Максимальный размер занятого места в файловой папке или в базе данных. Не задаётся и не используется системой.
Рекомендуется для реализации логики в расширениях, связанной с выбором источника файлов в зависимости от свободного места.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3f4ba101-1320-4180-80e1-6d10a059ebb2" Name="df_FileSourcesVirtual_MaxSize" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="516a926c-2aeb-49ca-a3ad-48418ce822cc" Name="FileExtensions" Type="String(Max) Null">
		<Description>Расширения файлов, которые будут размещаться в этом местоположении независимо от местоположения по умолчанию.
Несколько расширений указывается без точки и разделяется пробелом.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="64bbc32d-95d6-004b-5000-0288344d53bb" Name="pk_FileSourcesVirtual">
		<SchemeIndexedColumn Column="64bbc32d-95d6-004b-3100-0288344d53bb" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="64bbc32d-95d6-004b-7000-0288344d53bb" Name="idx_FileSourcesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="64bbc32d-95d6-014b-4000-0288344d53bb" />
	</SchemeIndex>
</SchemeTable>