<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="b4cfec48-deec-40fc-94fc-eae9e645afce" Name="LawPartnerRepresentatives" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4cfec48-deec-00fc-2000-0ae9e645afce" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4cfec48-deec-01fc-4000-0ae9e645afce" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4cfec48-deec-00fc-3100-0ae9e645afce" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="15eff0bc-8f2e-48fd-8471-cbdd1cbadd41" Name="Representative" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="15eff0bc-8f2e-00fd-4000-0bdd1cbadd41" Name="RepresentativeID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="26ff36e4-0032-4e47-b55e-7c0291ebd4e0" Name="RepresentativeName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="f16ced64-68e0-48ca-814a-35af83952171" Name="RepresentativeAddressID" Type="Guid Null" />
		<SchemePhysicalColumn ID="c4f9bc83-5b9d-4123-b7c5-3d4612cc02ee" Name="RepresentativeTaxNumber" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="eba55897-f129-4d63-a7bc-5c599e5bcd9e" Name="RepresentativeRegistrationNumber" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="7994ec4a-65a3-4598-8d56-7403d992bac0" Name="RepresentativeContacts" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="21b51ddf-deae-4567-9f82-50f1f0a39611" Name="RepresentativeStreet" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="22cfabab-51d5-455f-88d9-489c52033c62" Name="RepresentativePostalCode" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="66532004-b0b7-4a19-98b5-a01d54cd47b6" Name="RepresentativeCity" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="7b85e17a-b1fd-4cc3-bc3e-a5ce5ec4300e" Name="RepresentativeCountry" Type="String(Max) Null" />
		<SchemePhysicalColumn ID="980cd894-affb-4b3f-bf5b-199ed0d283b2" Name="RepresentativePoBox" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4cfec48-deec-00fc-5000-0ae9e645afce" Name="pk_LawPartnerRepresentatives">
		<SchemeIndexedColumn Column="b4cfec48-deec-00fc-3100-0ae9e645afce" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4cfec48-deec-00fc-7000-0ae9e645afce" Name="idx_LawPartnerRepresentatives_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b4cfec48-deec-01fc-4000-0ae9e645afce" />
	</SchemeIndex>
</SchemeTable>