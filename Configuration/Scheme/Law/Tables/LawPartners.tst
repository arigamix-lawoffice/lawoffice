<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="54244411-fea4-4bdd-b009-fad9c5915882" Name="LawPartners" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="54244411-fea4-00dd-2000-0ad9c5915882" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="54244411-fea4-01dd-4000-0ad9c5915882" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="54244411-fea4-00dd-3100-0ad9c5915882" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b1136d45-d8f4-471f-839e-5264a09467b7" Name="Partner" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b1136d45-d8f4-001f-4000-0264a09467b7" Name="PartnerID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="93d97302-4410-4123-a82d-57d1fcafe72c" Name="PartnerName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="7560a4ee-942c-4f43-8966-4df8aa72d482" Name="PartnerAddressID" Type="Guid Null" />
		<SchemePhysicalColumn ID="730bca46-adac-4311-9b5a-9e587b487eee" Name="PartnerTaxNumber" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="48c46ad8-fe7e-4931-b878-448e6cbdd8e6" Name="PartnerRegistrationNumber" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="84318c41-c018-449e-b5bc-b6270676fe4b" Name="PartnerContacts" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="85168610-06f1-4376-9feb-ded465c7fd5d" Name="PartnerStreet" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="2fa9664f-e9cf-4e09-960e-85be65146cec" Name="PartnerPostalCode" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="99e4f57d-5ad0-4357-9637-9d615efc1de9" Name="PartnerCity" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="cc144bb6-e721-40c8-a7fc-4e4a4e8aa7c0" Name="PartnerCountry" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="26293725-b9ab-4f8e-8ff6-be50bbc8621b" Name="PartnerPoBox" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="54244411-fea4-00dd-5000-0ad9c5915882" Name="pk_LawPartners">
		<SchemeIndexedColumn Column="54244411-fea4-00dd-3100-0ad9c5915882" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="54244411-fea4-00dd-7000-0ad9c5915882" Name="idx_LawPartners_ID" IsClustered="true">
		<SchemeIndexedColumn Column="54244411-fea4-01dd-4000-0ad9c5915882" />
	</SchemeIndex>
</SchemeTable>