<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="7c57bbba-8acc-4abf-b3cc-372399b68dbc" Name="SignatureEncryptDigestSettings" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c57bbba-8acc-00bf-2000-072399b68dbc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7c57bbba-8acc-01bf-4000-072399b68dbc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c57bbba-8acc-00bf-3100-072399b68dbc" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="35fe22eb-2cf7-4f33-b9db-572450af9776" Name="EncryptionAlgorithm" Type="Reference(Typified) Not Null" ReferencedTable="93f36ef0-b0ca-4726-9038-b10339db4b00">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="35fe22eb-2cf7-0033-4000-072450af9776" Name="EncryptionAlgorithmID" Type="Int16 Not Null" ReferencedColumn="11536554-e25e-4225-9a51-8f7604f31a27" />
		<SchemeReferencingColumn ID="919824f5-84ff-4667-b0d7-170694bc5237" Name="EncryptionAlgorithmName" Type="String(128) Not Null" ReferencedColumn="988356f8-f06d-4db4-ba8c-d43b66dcb42c" />
		<SchemeReferencingColumn ID="9697aea9-f175-4ca0-a0bd-45cc6d0bde3b" Name="EncryptionAlgorithmOID" Type="String(128) Not Null" ReferencedColumn="e5a0e6cd-63e4-490d-8ca7-6b38d0ed5b19" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7c602477-ab6c-40a0-8386-2a7e29f023fd" Name="DigestAlgorithm" Type="Reference(Typified) Not Null" ReferencedTable="9180bf30-3b8b-4adc-a285-d9ee97aea219">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7c602477-ab6c-00a0-4000-0a7e29f023fd" Name="DigestAlgorithmID" Type="Int16 Not Null" ReferencedColumn="76be674e-1438-45c0-801b-913ab4659034" />
		<SchemeReferencingColumn ID="7835f41a-b833-4dd5-aaae-2b1005525624" Name="DigestAlgorithmName" Type="String(128) Not Null" ReferencedColumn="be2988ca-1609-42aa-bc38-f98da127781a" />
		<SchemeReferencingColumn ID="5845a4a7-409f-479f-968b-569dacd42898" Name="DigestAlgorithmOID" Type="String(128) Not Null" ReferencedColumn="837783b9-67ce-4c34-9a24-b52e10ff339e" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="96817777-49a3-4f05-ad1c-e4bb886bc008" Name="EdsManager" Type="Reference(Typified) Null" ReferencedTable="72eb4e5a-f328-40e6-bb2d-18ea0a9a9d2b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="96817777-49a3-0005-4000-04bb886bc008" Name="EdsManagerName" Type="String(Max) Null" ReferencedColumn="421a8ff8-0ae8-4e30-a79b-b6509612f062" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c57bbba-8acc-00bf-5000-072399b68dbc" Name="pk_SignatureEncryptDigestSettings">
		<SchemeIndexedColumn Column="7c57bbba-8acc-00bf-3100-072399b68dbc" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c57bbba-8acc-00bf-7000-072399b68dbc" Name="idx_SignatureEncryptDigestSettings_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7c57bbba-8acc-01bf-4000-072399b68dbc" />
	</SchemeIndex>
</SchemeTable>