<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="5a8e7767-cd46-4ace-9da3-e3ea6f38cff2" Name="FileSignatureEventTypes" Group="System">
	<Description>События, в результате которых подпись была добавлена в систему.</Description>
	<SchemePhysicalColumn ID="5c07267f-b144-4691-8334-df3d75c898da" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор события.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3d99a984-8ccf-463f-977c-6950dd4336c6" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое название события.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="1249ae5e-d971-41a4-9fa3-a10354888522" Name="pk_FileSignatureEventTypes" IsClustered="true">
		<SchemeIndexedColumn Column="5c07267f-b144-4691-8334-df3d75c898da" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="5c07267f-b144-4691-8334-df3d75c898da">0</ID>
		<Name ID="3d99a984-8ccf-463f-977c-6950dd4336c6">$Enum_FileSignatureEventTypes_Other</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5c07267f-b144-4691-8334-df3d75c898da">1</ID>
		<Name ID="3d99a984-8ccf-463f-977c-6950dd4336c6">$Enum_FileSignatureEventTypes_Imported</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5c07267f-b144-4691-8334-df3d75c898da">2</ID>
		<Name ID="3d99a984-8ccf-463f-977c-6950dd4336c6">$Enum_FileSignatureEventTypes_Signed</Name>
	</SchemeRecord>
</SchemeTable>