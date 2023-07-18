<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b98383dc-ecf0-4ad0-b92d-dd599775b8f5" Name="Protocols" Group="Common" InstanceType="Cards" ContentType="Entries">
	<Description>Протоколы.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b98383dc-ecf0-00d0-2000-0d599775b8f5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b98383dc-ecf0-01d0-4000-0d599775b8f5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="51156ba3-860d-490f-b69c-3d74ec806cdc" Name="ProtocolFile" Type="Reference(Typified) Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c" WithForeignKey="false">
		<Description>Идентификатор файла протокола или Null, если файл протокола ещё не был сформирован.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="51156ba3-860d-000f-4000-0d74ec806cdc" Name="ProtocolFileID" Type="Guid Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7dca62cf-bf60-4572-b870-7aa423b4dd8f" Name="Date" Type="DateTime Null">
		<Description>Дата и время совещания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f1ad8949-89c0-49de-99f5-e3a67d806ae7" Name="Agenda" Type="String(Max) Null">
		<Description>Повестка.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b98383dc-ecf0-00d0-5000-0d599775b8f5" Name="pk_Protocols" IsClustered="true">
		<SchemeIndexedColumn Column="b98383dc-ecf0-01d0-4000-0d599775b8f5" />
	</SchemePrimaryKey>
</SchemeTable>