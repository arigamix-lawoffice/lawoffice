<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="c0b9d91c-4a9e-4c7a-b7c2-13daa37a1cf9" Name="LawPartnersDialogVirtual" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Виртуальная таблица для редактирования компаний</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c0b9d91c-4a9e-007a-2000-03daa37a1cf9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c0b9d91c-4a9e-017a-4000-03daa37a1cf9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c0b9d91c-4a9e-007a-3100-03daa37a1cf9" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="bc960930-2e6e-49b2-8fdc-3d405b15c324" Name="Name" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="b09aeabc-45b7-4a3f-94e1-ddf2f6214afd" Name="AddressID" Type="Guid Null" />
	<SchemePhysicalColumn ID="0692b32b-3e0f-434a-b756-c6427986d4d4" Name="TaxNumber" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="4eaf2fb9-ec01-41e4-aa3f-3d6c3a4fb6be" Name="RegistrationNumber" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="d06219a7-2d54-49f6-86db-9e2cfd42c043" Name="Contacts" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c30b7702-0b2d-4ba6-b3f2-8637095f0565" Name="Street" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c37e027b-7f54-45a9-8e21-136f4b548ac4" Name="PostalCode" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1b52a832-b8dc-4e72-a9c0-37b217db0ed8" Name="City" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="ca4193ae-114e-49c7-bab4-2032a637efff" Name="Country" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="0167e36f-140b-4bc9-a271-40c815cc8c82" Name="PoBox" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="a0cc4832-30ea-4bc9-8aa6-c6f7aa01adcf" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c0b9d91c-4a9e-007a-5000-03daa37a1cf9" Name="pk_LawPartnersDialogVirtual">
		<SchemeIndexedColumn Column="c0b9d91c-4a9e-007a-3100-03daa37a1cf9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c0b9d91c-4a9e-007a-7000-03daa37a1cf9" Name="idx_LawPartnersDialogVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c0b9d91c-4a9e-017a-4000-03daa37a1cf9" />
	</SchemeIndex>
</SchemeTable>