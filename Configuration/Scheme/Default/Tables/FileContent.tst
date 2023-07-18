<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="328af88c-b21a-4c2a-b825-45a086d0b24b" Name="FileContent" Group="System">
	<Description>Контент файлов.</Description>
	<SchemeComplexColumn ID="7da8905a-bef9-4648-b2cf-c779c2a6d941" Name="Version" Type="Reference(Typified) Not Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8" WithForeignKey="false">
		<Description>Ссылка на версию файла.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7da8905a-bef9-0048-4000-0779c2a6d941" Name="VersionRowID" Type="Guid Not Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="15b74e8e-3852-419c-9bf5-6099f6d68802" Name="Content" Type="Binary(Max) Null">
		<Description>Контент.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fe8611f5-d157-4513-913f-06f116b1f0ce" Name="Ext" Type="String(100) Null" />
	<SchemePrimaryKey ID="82fbdeef-084f-4585-bbbb-46ca0112bab9" Name="pk_FileContent" IsClustered="true">
		<SchemeIndexedColumn Column="7da8905a-bef9-0048-4000-0779c2a6d941" />
	</SchemePrimaryKey>
</SchemeTable>