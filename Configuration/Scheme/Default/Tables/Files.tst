<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" ID="dd716146-b177-4920-bc90-b1196b16347c" Name="Files" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Файлы, приложенные к карточкам.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd716146-b177-0020-2000-01196b16347c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dd716146-b177-0120-4000-01196b16347c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd716146-b177-0020-3100-01196b16347c" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="5fa1d976-21b8-4df5-b52e-f7beadf93e9d" Name="Name" Type="String(256) Not Null">
		<Description>Имя файла.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="04fd2970-16ed-4430-aa58-15712dc5e3e4" Name="Task" Type="Reference(Typified) Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8" WithForeignKey="false">
		<Description>Внимание! Колонка не используется и может быть удалена в одном из следующих релизов. Для хранения сторонних идентификаторов используйте одну из колонок: Files.Options, FileVersions.LinkID, FileVersions.Options.

Ссылка на задание, к которому приложен файл, или Null, если файл приложен к основной карточке.

Если добавить внешний ключ в этой колонке, то также необходим индекс на неё, иначе при очень большом количестве файлов завершение/удаление любых заданий значительно замедляется.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="04fd2970-16ed-0030-4000-05712dc5e3e4" Name="TaskID" Type="Guid Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e666c16c-49f8-44ee-91b7-80db766b46ba" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип файла.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e666c16c-49f8-00ee-4000-00db766b46ba" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="1f003c87-de57-4887-b8ba-4bff2c95f6eb" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="aba1e9ad-7a37-4faf-99de-1efcd0b5dbc8" Name="Version" Type="Reference(Typified) Not Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aba1e9ad-7a37-00af-4000-0efcd0b5dbc8" Name="VersionRowID" Type="Guid Not Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
		<SchemeReferencingColumn ID="27816893-09aa-43b5-a88c-4cd6f755e117" Name="VersionNumber" Type="Int32 Not Null" ReferencedColumn="1981870c-c436-42ce-a298-b580dba51257">
			<Description>Номер версии файла.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2aaf2c53-6414-4d99-b41f-babeaa504a3a" Name="Created" Type="DateTime Not Null">
		<Description>Дата создания.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b1317e7c-59d9-4abd-8377-120a01ae2f11" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, создавший файл.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b1317e7c-59d9-00bd-4000-020a01ae2f11" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d97a6760-8a0b-4486-80e9-adf935af530c" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="820558aa-9a3a-4bc3-9ab3-e8001237271f" Name="Modified" Type="DateTime Not Null">
		<Description>Дата изменения.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="5b4badc7-fb8d-4f1d-8a40-d7dcdf6b1ccd" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, изменивший файл.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5b4badc7-fb8d-001d-4000-07dcdf6b1ccd" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="e55404e7-4cfe-4387-aca7-6da6a3eab546" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="393be11b-b25d-4c79-8f96-eea94f091786" Name="Category" Type="Reference(Abstract) Null" WithForeignKey="false">
		<Description>Категория файла или Null, если файл не имеет категории.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="393be11b-b25d-0079-4000-0ea94f091786" Name="CategoryID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="8c319072-6a1f-47b9-9c0b-b254717f82c9" Name="CategoryCaption" Type="String(256) Null">
			<Description>Отображаемое имя категории файла или Null, если файл не имеет категории.</Description>
		</SchemePhysicalColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b231d370-31d8-4035-8258-78a76898ec1b" Name="OriginalFile" Type="Reference(Typified) Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c" WithForeignKey="false">
		<Description>Идентификатор файла, копией которого является текущий файл, или Null, если текущий файл не является копией.
Колонка указывается без FK, т.к. файл, на который держится ссылка, может быть уже удалён.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b231d370-31d8-0035-4000-08a76898ec1b" Name="OriginalFileID" Type="Guid Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="966f4f00-f3f1-4314-83d3-5a36b853d32c" Name="OriginalVersion" Type="Reference(Typified) Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8" WithForeignKey="false">
		<Description>Идентификатор версии файла, копией которого является текущий файл, или Null, если текущий файл не является копией.
Колонка указывается без FK, т.к. файл, на версию которого держится ссылка, может быть уже удалён.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="966f4f00-f3f1-0014-4000-0a36b853d32c" Name="OriginalVersionRowID" Type="Guid Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="eedbea4c-ab6f-4225-8460-84b08f42aa29" Name="Options" Type="BinaryJson Null" IsSparse="true">
		<Description>Сериализованные в JSON настройки файла.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd716146-b177-0020-5000-01196b16347c" Name="pk_Files">
		<SchemeIndexedColumn Column="dd716146-b177-0020-3100-01196b16347c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd716146-b177-0020-7000-01196b16347c" Name="idx_Files_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dd716146-b177-0120-4000-01196b16347c" />
	</SchemeIndex>
</SchemeTable>