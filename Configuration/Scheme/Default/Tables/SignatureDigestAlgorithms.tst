<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9180bf30-3b8b-4adc-a285-d9ee97aea219" Name="SignatureDigestAlgorithms" Group="System">
	<Description>Идентификаторы алгоритмов хеширования</Description>
	<SchemePhysicalColumn ID="76be674e-1438-45c0-801b-913ab4659034" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="be2988ca-1609-42aa-bc38-f98da127781a" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="837783b9-67ce-4c34-9a24-b52e10ff339e" Name="OID" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="c42eadd9-e92c-4c85-8583-bd76919d55fc" Name="pk_SignatureDigestAlgorithms">
		<SchemeIndexedColumn Column="76be674e-1438-45c0-801b-913ab4659034" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">1</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">$Enum_Signature_DigestAlgos_GOST12_256</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">1.2.643.7.1.1.2.2</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">2</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">$Enum_Signature_DigestAlgos_GOST12_512</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">1.2.643.7.1.1.2.3</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">3</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">$Enum_Signature_DigestAlgos_GOST94</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">1.2.643.2.2.9</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">7</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">SHA1</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">1.3.14.3.2.26</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">8</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">SHA256</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">2.16.840.1.101.3.4.2.1</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">9</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">SHA384</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">2.16.840.1.101.3.4.2.2</OID>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="76be674e-1438-45c0-801b-913ab4659034">10</ID>
		<Name ID="be2988ca-1609-42aa-bc38-f98da127781a">SHA512</Name>
		<OID ID="837783b9-67ce-4c34-9a24-b52e10ff339e">2.16.840.1.101.3.4.2.3</OID>
	</SchemeRecord>
</SchemeTable>