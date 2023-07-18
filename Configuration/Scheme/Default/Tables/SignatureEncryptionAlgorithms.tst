<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="93f36ef0-b0ca-4726-9038-b10339db4b00" Name="SignatureEncryptionAlgorithms" Group="System">
	<Description>Идентификаторы алгоритмов подписи</Description>
	<SchemePhysicalColumn ID="11536554-e25e-4225-9a51-8f7604f31a27" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="988356f8-f06d-4db4-ba8c-d43b66dcb42c" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19" Name="OID" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="e4aedf0b-e845-43c7-8121-62edeab52762" Name="pk_SignatureEncryptionAlgorithms">
		<SchemeIndexedColumn Column="11536554-e25e-4225-9a51-8f7604f31a27" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="11536554-e25e-4225-9a51-8f7604f31a27">1</ID>
		<Name ID="988356f8-f06d-4db4-ba8c-d43b66dcb42c">$Enum_Signature_EncAlgos_GOST12_256</Name>
		<OID ID="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19">1.2.643.7.1.1.1.1</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="11536554-e25e-4225-9a51-8f7604f31a27">2</ID>
		<Name ID="988356f8-f06d-4db4-ba8c-d43b66dcb42c">$Enum_Signature_EncAlgos_GOST12_512</Name>
		<OID ID="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19">1.2.643.7.1.1.1.2</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="11536554-e25e-4225-9a51-8f7604f31a27">3</ID>
		<Name ID="988356f8-f06d-4db4-ba8c-d43b66dcb42c">$Enum_Signature_EncAlgos_GOST2001</Name>
		<OID ID="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19">1.2.643.2.2.19</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="11536554-e25e-4225-9a51-8f7604f31a27">4</ID>
		<Name ID="988356f8-f06d-4db4-ba8c-d43b66dcb42c">$Enum_Signature_EncAlgos_Others</Name>
		<OID ID="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19"></OID>
	</SchemeRecord>
</SchemeTable>