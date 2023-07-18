<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="961887ca-e67c-4283-89fc-265dbf17e4c1" Name="FileConverterCacheVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Информация, отображаемая в карточке файловых конвертеров.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="961887ca-e67c-0083-2000-065dbf17e4c1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="961887ca-e67c-0183-4000-065dbf17e4c1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d085466b-3f23-4e9e-bd58-a670f53f23c5" Name="FileCount" Type="Int32 Not Null">
		<Description>Количество файлов в кэше.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c56ee1a4-8838-478a-a216-765ebf6e23bf" Name="df_FileConverterCacheVirtual_FileCount" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="eccfe07b-09bf-49ed-aa1a-8bd535e49ca9" Name="FileCountText" Type="String(128) Not Null">
		<Description>Количество файлов в кэше в виде строки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="26c1c300-d0b8-47f4-8780-ec4c9ea2d59e" Name="OldestFileAccessTime" Type="DateTime Null">
		<Description>Дата и время доступа для последнего не использованного файла в кэше.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="07b272ce-195f-4521-920b-edbc29018316" Name="NewestFileAccessTime" Type="DateTime Null">
		<Description>Дата и время доступа для последнего использованного файла в кэше.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="961887ca-e67c-0083-5000-065dbf17e4c1" Name="pk_FileConverterCacheVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="961887ca-e67c-0183-4000-065dbf17e4c1" />
	</SchemePrimaryKey>
</SchemeTable>