<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3f86165e-8a0d-41d9-a7a2-b6a511bf551b" Name="CompilationCache" Group="System">
	<Description>Результаты компиляции объектов системы.</Description>
	<SchemePhysicalColumn ID="6e137cc7-197d-49b0-898b-809da88f4db2" Name="CategoryID" Type="String(64) Not Null">
		<Description>Идентификатор категории, к которой относится объект компиляции.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="447225ae-bd85-4797-9628-c44b04aafaff" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор результата компиляции.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2d5d2db2-6bce-4cde-8b14-0fbf51f570c8" Name="Result" Type="BinaryJson Not Null">
		<Description>Результат компиляции.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="863337a6-20b8-4ae7-b429-90501840cc56" Name="Assembly" Type="Binary(Max) Null">
		<Description>Массив байт, соответствующий сборке.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="c892a9d2-3c76-4b22-8262-3a6fcb9525db" Name="pk_CompilationCache">
		<SchemeIndexedColumn Column="447225ae-bd85-4797-9628-c44b04aafaff" />
		<SchemeIndexedColumn Column="6e137cc7-197d-49b0-898b-809da88f4db2" />
	</SchemePrimaryKey>
</SchemeTable>