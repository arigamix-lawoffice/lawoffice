<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="076b2050-e20b-412b-942b-b4cb063e6941" Name="SignatureSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Таблица настроек цифровой подписи</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="076b2050-e20b-002b-2000-04cb063e6941" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="076b2050-e20b-012b-4000-04cb063e6941" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7003b99c-4de7-4680-b9cf-66aaae4dd012" Name="SignatureType" Type="Reference(Typified) Not Null" ReferencedTable="577baaea-6832-4eb7-9333-60661367720e">
		<Description>Вид подписи</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7003b99c-4de7-0080-4000-06aaae4dd012" Name="SignatureTypeID" Type="Int16 Not Null" ReferencedColumn="dfe71de9-ef54-4eac-8f54-64d5311db556" />
		<SchemeReferencingColumn ID="d8f149aa-fb78-4729-9bc1-99da199474b3" Name="SignatureTypeName" Type="String(128) Not Null" ReferencedColumn="8b961e78-2f42-4496-bd8e-5c1a0f4f65cc" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0fa495d1-28d3-4a16-8dfc-922efc4f53da" Name="SignatureProfile" Type="Reference(Typified) Not Null" ReferencedTable="eca29bb9-3085-4556-b19a-6015cbc8fb25">
		<Description>Профиль подписи</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0fa495d1-28d3-0016-4000-022efc4f53da" Name="SignatureProfileID" Type="Int16 Not Null" ReferencedColumn="8c01d076-b862-4d75-852c-453efccfe590" />
		<SchemeReferencingColumn ID="ff1fafed-294f-4c27-a6aa-6f143943590e" Name="SignatureProfileName" Type="String(128) Not Null" ReferencedColumn="10a5a04e-258d-41d3-b450-9057a6f39ebd" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="12496ede-a245-43e3-95d3-6cdaae96b9b9" Name="TSPAddress" Type="String(Max) Null">
		<Description>Адрес TSP-сервера</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8bebe64c-beaa-40bd-a47c-5e582bf970b1" Name="OCSPAddress" Type="String(Max) Null">
		<Description>Адрес OCSP-сервера</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d1cd3e18-486d-4739-b5c5-bd00221a0b97" Name="CRLAddress" Type="String(Max) Null">
		<Description>Адрес CRL-сервера</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="028ec54d-1ee8-4b02-871a-0318caeb4350" Name="SignaturePackaging" Type="Reference(Typified) Not Null" ReferencedTable="15620b78-46b8-4520-aa60-4bfefe67c731">
		<Description>Упаковка подписи</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="028ec54d-1ee8-0002-4000-0318caeb4350" Name="SignaturePackagingID" Type="Int16 Not Null" ReferencedColumn="b7928247-9f27-44e6-b926-72d7fa2c134f" />
		<SchemeReferencingColumn ID="f128dc81-32a4-4c54-98d4-25e893e42458" Name="SignaturePackagingName" Type="String(128) Not Null" ReferencedColumn="cc696a35-a6e0-445d-a5db-87e4cba0e9e0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1e2b1aa8-fe91-457a-9233-941426f2493d" Name="TSPUserName" Type="String(Max) Null">
		<Description>Логин для TSP-сервера</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c2f994b8-dfd4-40db-8dd6-72523bc12349" Name="TSPPassword" Type="String(Max) Null">
		<Description>Пароль для TSP-сервера</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="a02b39d9-dca7-496b-b025-6e94713e838e" Name="TSPDigestAlgorithm" Type="Reference(Typified) Null" ReferencedTable="9180bf30-3b8b-4adc-a285-d9ee97aea219">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a02b39d9-dca7-006b-4000-0e94713e838e" Name="TSPDigestAlgorithmID" Type="Int16 Null" ReferencedColumn="76be674e-1438-45c0-801b-913ab4659034" />
		<SchemeReferencingColumn ID="6a0410d6-614a-4d4a-8aa8-f108b814833d" Name="TSPDigestAlgorithmOID" Type="String(128) Null" ReferencedColumn="837783b9-67ce-4c34-9a24-b52e10ff339e" />
		<SchemeReferencingColumn ID="c4e1681d-189d-4195-ace5-92563331a037" Name="TSPDigestAlgorithmName" Type="String(128) Null" ReferencedColumn="be2988ca-1609-42aa-bc38-f98da127781a" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8852c285-134c-4cab-b5f1-434a8cb93f92" Name="UseSystemRootCertificates" Type="Boolean Null">
		<Description>Использовать, как доверенные, серверные корневые и промежуточные сертификаты</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="076b2050-e20b-002b-5000-04cb063e6941" Name="pk_SignatureSettings" IsClustered="true">
		<SchemeIndexedColumn Column="076b2050-e20b-012b-4000-04cb063e6941" />
	</SchemePrimaryKey>
</SchemeTable>