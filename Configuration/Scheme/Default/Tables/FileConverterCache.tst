<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b376adb4-3134-4ec5-9597-a83e8c9db0f1" Name="FileConverterCache" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Информация по сконвертированным файлам, добавленным в кэш.
Идентификатор RowID равен идентификатору файла в кэше Files.RowID.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b376adb4-3134-00c5-2000-083e8c9db0f1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b376adb4-3134-01c5-4000-083e8c9db0f1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b376adb4-3134-00c5-3100-083e8c9db0f1" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a76b6adc-5ba7-462b-a81f-a7c12abb3f47" Name="VersionID" Type="Guid Not Null">
		<Description>Идентификатор версии исходного файла.

Совместно с RequestHash определяет уникальность сохранённого в кэше файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cee59190-01e2-4afc-85fb-c2b9a00152f7" Name="RequestHash" Type="Binary(32) Not Null">
		<Description>Хеш, посчитанный для данных в запросе на преобразование файла.
Для расчёта обычно используется функция хеширования HMAC-SHA256, размер хеша в которой 256 бит или 32 байта.

Совместно с VersionID определяет уникальность сохранённого в кэше файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c901bf37-6b2d-46e0-a4ae-73713909ab6e" Name="ResponseInfo" Type="BinaryJson Null">
		<Description>Сериализованная информация Info из ответа на запрос по конвертации
или Null, если информация отсутствовала или неизвестна.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="794b6767-ca20-4012-8f28-24f4bafeaaa6" Name="LastAccessTime" Type="DateTime Not Null">
		<Description>Дата последнего обращения</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b376adb4-3134-00c5-5000-083e8c9db0f1" Name="pk_FileConverterCache">
		<SchemeIndexedColumn Column="b376adb4-3134-00c5-3100-083e8c9db0f1" />
	</SchemePrimaryKey>
	<SchemeIndex ID="57b17227-af25-4449-9678-a22338a8c490" Name="ndx_FileConverterCache_VersionIDRequestHash" IsUnique="true">
		<SchemeIndexedColumn Column="a76b6adc-5ba7-462b-a81f-a7c12abb3f47" />
		<SchemeIndexedColumn Column="cee59190-01e2-4afc-85fb-c2b9a00152f7" />
		<SchemeIncludedColumn Column="b376adb4-3134-00c5-3100-083e8c9db0f1" />
	</SchemeIndex>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b376adb4-3134-00c5-7000-083e8c9db0f1" Name="idx_FileConverterCache_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b376adb4-3134-01c5-4000-083e8c9db0f1" />
	</SchemeIndex>
	<SchemeIndex ID="ae5f5444-372b-4622-9478-c7ac382dc14b" Name="ndx_FileConverterCache_RowID">
		<SchemeIndexedColumn Column="b376adb4-3134-00c5-3100-083e8c9db0f1" />
		<SchemeIncludedColumn Column="c901bf37-6b2d-46e0-a4ae-73713909ab6e" />
	</SchemeIndex>
</SchemeTable>