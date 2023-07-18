<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="faf66527-24c2-4f20-afa8-46915e5fd4d6" Name="SignatureCertificateSettings" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="faf66527-24c2-0020-2000-06915e5fd4d6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="faf66527-24c2-0120-4000-06915e5fd4d6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="faf66527-24c2-0020-3100-06915e5fd4d6" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="7cb97b17-c31c-406d-96e1-2567d8e77efe" Name="StartDate" Type="Date Null" />
	<SchemePhysicalColumn ID="61f490f5-31da-4313-a485-743a92596d5b" Name="EndDate" Type="Date Null" />
	<SchemePhysicalColumn ID="3e95d9c5-4ecb-43b8-803b-21f9dce6680e" Name="IsValidDate" Type="Boolean Null" />
	<SchemePhysicalColumn ID="c300e725-56f4-4329-9bc4-1fe8fca1964b" Name="Company" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="79276aa9-59cd-4bb6-8c47-5040940ca672" Name="Subject" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="f8d89415-9541-4248-bac4-e0c9ef1cdc45" Name="Issuer" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="faf66527-24c2-0020-5000-06915e5fd4d6" Name="pk_SignatureCertificateSettings">
		<SchemeIndexedColumn Column="faf66527-24c2-0020-3100-06915e5fd4d6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="faf66527-24c2-0020-7000-06915e5fd4d6" Name="idx_SignatureCertificateSettings_ID" IsClustered="true">
		<SchemeIndexedColumn Column="faf66527-24c2-0120-4000-06915e5fd4d6" />
	</SchemeIndex>
</SchemeTable>