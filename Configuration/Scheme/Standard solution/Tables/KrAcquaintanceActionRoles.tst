<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4c90a850-8ea9-4b07-8c8e-96145f624a3a" Name="KrAcquaintanceActionRoles" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей для действия "Ознакомление"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4c90a850-8ea9-0007-2000-06145f624a3a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4c90a850-8ea9-0107-4000-06145f624a3a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4c90a850-8ea9-0007-3100-06145f624a3a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5bbafcc0-4971-474c-a29b-52a79c8f15ad" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5bbafcc0-4971-004c-4000-02a79c8f15ad" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="611587b8-c7a6-40fd-a5d2-49e4e46144b3" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4c90a850-8ea9-0007-5000-06145f624a3a" Name="pk_KrAcquaintanceActionRoles">
		<SchemeIndexedColumn Column="4c90a850-8ea9-0007-3100-06145f624a3a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="4c90a850-8ea9-0007-7000-06145f624a3a" Name="idx_KrAcquaintanceActionRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="4c90a850-8ea9-0107-4000-06145f624a3a" />
	</SchemeIndex>
</SchemeTable>