<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="fb7c3c18-d7cd-4f16-84e2-33ab0269adbd" Name="TemplateFiles" Group="System" InstanceType="Files" ContentType="Entries">
	<Description>Файлы в шаблоне карточек.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fb7c3c18-d7cd-0016-2000-03ab0269adbd" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fb7c3c18-d7cd-0116-4000-03ab0269adbd" Name="ID" Type="Guid Not Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="dd49150f-89e9-4670-8873-7b9424db00e1" Name="SourceFile" Type="Reference(Typified) Not Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dd49150f-89e9-0070-4000-0b9424db00e1" Name="SourceFileID" Type="Guid Not Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="14e23b99-b43d-480c-bd01-6fbb8572d8c8" Name="SourceVersion" Type="Reference(Typified) Not Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="14e23b99-b43d-000c-4000-0fbb8572d8c8" Name="SourceVersionRowID" Type="Guid Not Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fb7c3c18-d7cd-0016-5000-03ab0269adbd" Name="pk_TemplateFiles" IsClustered="true">
		<SchemeIndexedColumn Column="fb7c3c18-d7cd-0116-4000-03ab0269adbd" />
	</SchemePrimaryKey>
</SchemeTable>