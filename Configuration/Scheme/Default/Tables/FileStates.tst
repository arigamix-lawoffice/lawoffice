<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="de9ba182-3fc4-4f20-9060-fa83b74fd46c" Name="FileStates" Group="System">
	<Description>Состояние версии файла.</Description>
	<SchemePhysicalColumn ID="a658b07f-0314-4974-8fda-8cff054dbe7d" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор состояния для версии файла.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6f170a38-63d4-4985-ac60-7b692ebbe599" Name="Name" Type="String(128) Not Null">
		<Description>Название состояния для версии файла.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="83557e69-1ed5-4f06-a11f-44b2882de661" Name="pk_FileStates">
		<SchemeIndexedColumn Column="a658b07f-0314-4974-8fda-8cff054dbe7d" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="a658b07f-0314-4974-8fda-8cff054dbe7d">0</ID>
		<Name ID="6f170a38-63d4-4985-ac60-7b692ebbe599">Uploading</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a658b07f-0314-4974-8fda-8cff054dbe7d">1</ID>
		<Name ID="6f170a38-63d4-4985-ac60-7b692ebbe599">Success</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="a658b07f-0314-4974-8fda-8cff054dbe7d">2</ID>
		<Name ID="6f170a38-63d4-4985-ac60-7b692ebbe599">Error</Name>
	</SchemeRecord>
</SchemeTable>