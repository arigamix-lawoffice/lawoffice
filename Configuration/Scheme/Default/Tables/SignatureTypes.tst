<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="577baaea-6832-4eb7-9333-60661367720e" Name="SignatureTypes" Group="System">
	<Description>Таблица видов подписей</Description>
	<SchemePhysicalColumn ID="dfe71de9-ef54-4eac-8f54-64d5311db556" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор вида подписи</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8b961e78-2f42-4496-bd8e-5c1a0f4f65cc" Name="Name" Type="String(128) Not Null">
		<Description>Название вида подписи</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="6e7a7104-02aa-4190-a47d-5d67497db6ef" Name="pk_SignatureTypes">
		<SchemeIndexedColumn Column="dfe71de9-ef54-4eac-8f54-64d5311db556" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="dfe71de9-ef54-4eac-8f54-64d5311db556">0</ID>
		<Name ID="8b961e78-2f42-4496-bd8e-5c1a0f4f65cc">$Enum_SignatureTypes_None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="dfe71de9-ef54-4eac-8f54-64d5311db556">1</ID>
		<Name ID="8b961e78-2f42-4496-bd8e-5c1a0f4f65cc">$Enum_SignatureTypes_CAdES</Name>
	</SchemeRecord>
</SchemeTable>