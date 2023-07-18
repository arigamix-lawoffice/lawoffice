<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e17fd270-5c61-49af-955d-ed6bb983f0d8" Name="FileVersions" Group="System" InstanceType="Files" ContentType="Collections">
	<Description>Версии файла.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e17fd270-5c61-00af-2000-0d6bb983f0d8" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e17fd270-5c61-01af-4000-0d6bb983f0d8" Name="ID" Type="Guid Not Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e17fd270-5c61-00af-3100-0d6bb983f0d8" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="1981870c-c436-42ce-a298-b580dba51257" Name="Number" Type="Int32 Not Null">
		<Description>Номер версии файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="943499a9-3512-4f41-a607-ba52bfe726e9" Name="Name" Type="String(256) Not Null">
		<Description>Имя файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e1f3cccc-802c-4375-b1de-a15721b6647c" Name="Size" Type="Int64 Not Null">
		<Description>Размер контента файла в байтах.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dd062e54-4355-4a0d-86e3-cd50e144505e" Name="Created" Type="DateTime Not Null">
		<Description>Дата обновления файла (фактически дата добавления версии).</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="c597e544-d7c0-42b1-93b3-198b33111277" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, изменивший файл (создавший его новую версию).</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c597e544-d7c0-00b1-4000-098b33111277" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d0b1d7c1-3e05-4639-ad97-760f2d6339d5" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="924797f3-7009-40fe-a948-02e297fefcb3" Name="Source" Type="Reference(Typified) Not Null" ReferencedTable="e8300fe5-3b24-4c27-a45a-6cd8575bfcd5" WithForeignKey="false">
		<Description>Способ хранения контента файла.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="924797f3-7009-00fe-4000-02e297fefcb3" Name="SourceID" Type="Int16 Not Null" ReferencedColumn="983cbcc4-c185-43fd-a57c-edef94a23551">
			<Description>Идентификатор способа хранения файлов.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="52b20231-6681-4131-ade8-f9a791c2a44c" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="de9ba182-3fc4-4f20-9060-fa83b74fd46c">
		<Description>Состояние версии файла.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="52b20231-6681-0031-4000-09a791c2a44c" Name="StateID" Type="Int16 Not Null" ReferencedColumn="a658b07f-0314-4974-8fda-8cff054dbe7d">
			<Description>Идентификатор состояния для версии файла.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="24e55825-2a43-4b6f-903a-4a70b30aebd7" Name="ErrorDate" Type="DateTime Null">
		<Description>Дата ошибки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c1aa96cb-dbdc-4f7f-bad2-e061c7b7b224" Name="ErrorMessage" Type="String(Max) Null">
		<Description>Сообщение об ошибке.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="357bf5b1-51ee-45b7-ba3e-484b3336a99d" Name="Hash" Type="Binary(32) Null">
		<Description>Хеш, посчитанный для контента файла, или Null, если расчёт хеша не выполнен. Хеш расчитывается вручную на клиенте. Обычно используется для файлов приложений.

Для расчёта обычно используется функция хеширования SHA256, размер хеша в которой 256 бит или 32 байта.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f3c993bb-7f3a-4e8c-953d-681cc71fc97e" Name="Options" Type="BinaryJson Null" IsSparse="true">
		<Description>Сериализованные в JSON настройки версии файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3f169ce3-1485-4bf5-971d-ab3d110f5108" Name="LinkID" Type="Guid Null" IsSparse="true">
		<Description>Внешний идентификатор версии файла. Может использоваться в расширениях для связи с содержимым во внешнем местоположении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7cb31ac2-09f5-4f5b-84fc-e58178e1a383" Name="Tags" Type="String(256) Null" IsSparse="true">
		<Description>Теги версии файла.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e17fd270-5c61-00af-5000-0d6bb983f0d8" Name="pk_FileVersions">
		<SchemeIndexedColumn Column="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e17fd270-5c61-00af-7000-0d6bb983f0d8" Name="idx_FileVersions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e17fd270-5c61-01af-4000-0d6bb983f0d8" />
	</SchemeIndex>
</SchemeTable>