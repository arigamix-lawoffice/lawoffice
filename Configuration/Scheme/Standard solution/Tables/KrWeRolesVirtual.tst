<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="eea339fd-2e18-415b-b338-418f84ac961e" Name="KrWeRolesVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Роли.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="eea339fd-2e18-005b-2000-018f84ac961e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eea339fd-2e18-015b-4000-018f84ac961e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="eea339fd-2e18-005b-3100-018f84ac961e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="922f38db-cd93-48ab-9ea7-59c642cf6f4f" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="922f38db-cd93-00ab-4000-09c642cf6f4f" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="ab8d6b3b-9b40-4929-90de-0f969a525a00" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8fbed7ee-7d40-44ff-a4b1-0c301a2856c6" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="eea339fd-2e18-005b-5000-018f84ac961e" Name="pk_KrWeRolesVirtual">
		<SchemeIndexedColumn Column="eea339fd-2e18-005b-3100-018f84ac961e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="eea339fd-2e18-005b-7000-018f84ac961e" Name="idx_KrWeRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="eea339fd-2e18-015b-4000-018f84ac961e" />
	</SchemeIndex>
</SchemeTable>