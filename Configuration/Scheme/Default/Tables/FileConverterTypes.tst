<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a1dd7426-13e0-42fb-a45a-0a714108e274" Name="FileConverterTypes" Group="System">
	<Description>Варианты конвертеров файлов из формата в формат</Description>
	<SchemePhysicalColumn ID="51f44556-0314-496f-bd4b-4162d05b07c7" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="5d525d57-ea0c-401e-b14a-81c08323b102" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="1990cba3-c0df-430d-b986-81f9f54c635d" Name="pk_FileConverterTypes">
		<SchemeIndexedColumn Column="51f44556-0314-496f-bd4b-4162d05b07c7" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="51f44556-0314-496f-bd4b-4162d05b07c7">0</ID>
		<Name ID="5d525d57-ea0c-401e-b14a-81c08323b102">$Enum_FileConverterTypes_None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="51f44556-0314-496f-bd4b-4162d05b07c7">1</ID>
		<Name ID="5d525d57-ea0c-401e-b14a-81c08323b102">$Enum_FileConverterTypes_OpenLibre</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="51f44556-0314-496f-bd4b-4162d05b07c7">2</ID>
		<Name ID="5d525d57-ea0c-401e-b14a-81c08323b102">$Enum_FileConverterTypes_OnlyOfficeService</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="51f44556-0314-496f-bd4b-4162d05b07c7">3</ID>
		<Name ID="5d525d57-ea0c-401e-b14a-81c08323b102">$Enum_FileConverterTypes_OnlyOfficeDocumentBuilder</Name>
	</SchemeRecord>
</SchemeTable>