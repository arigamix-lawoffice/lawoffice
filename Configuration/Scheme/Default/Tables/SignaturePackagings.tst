<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="15620b78-46b8-4520-aa60-4bfefe67c731" Name="SignaturePackagings" Group="System">
	<Description>Варианты упаковки подписи</Description>
	<SchemePhysicalColumn ID="b7928247-9f27-44e6-b926-72d7fa2c134f" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="cc696a35-a6e0-445d-a5db-87e4cba0e9e0" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="af194aa2-459f-4246-920c-a9cda8201a3e" Name="pk_SignaturePackagings">
		<SchemeIndexedColumn Column="b7928247-9f27-44e6-b926-72d7fa2c134f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="b7928247-9f27-44e6-b926-72d7fa2c134f">3</ID>
		<Name ID="cc696a35-a6e0-445d-a5db-87e4cba0e9e0">$Enum_Signature_Packagings_Detached</Name>
	</SchemeRecord>
</SchemeTable>