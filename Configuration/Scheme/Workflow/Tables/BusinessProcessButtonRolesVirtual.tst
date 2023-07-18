<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="803fee29-9750-46f5-950d-77a44ff8b2af" Name="BusinessProcessButtonRolesVirtual" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, которым доступная данная кнопка</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="803fee29-9750-00f5-2000-07a44ff8b2af" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="803fee29-9750-01f5-4000-07a44ff8b2af" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="803fee29-9750-00f5-3100-07a44ff8b2af" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a8bef474-bc04-4903-9187-41be9a67131d" Name="Button" Type="Reference(Typified) Not Null" ReferencedTable="033a363a-e183-4084-83cb-4672841a2a90" IsReferenceToOwner="true" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a8bef474-bc04-0003-4000-01be9a67131d" Name="ButtonRowID" Type="Guid Not Null" ReferencedColumn="033a363a-e183-0084-3100-0672841a2a90" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b745bdc1-a863-45e1-9dd5-a4b8297fa443" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b745bdc1-a863-00e1-4000-04b8297fa443" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="4915b346-a46e-4ffe-8207-fb30ebcc7b0b" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="803fee29-9750-00f5-5000-07a44ff8b2af" Name="pk_BusinessProcessButtonRolesVirtual">
		<SchemeIndexedColumn Column="803fee29-9750-00f5-3100-07a44ff8b2af" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="803fee29-9750-00f5-7000-07a44ff8b2af" Name="idx_BusinessProcessButtonRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="803fee29-9750-01f5-4000-07a44ff8b2af" />
	</SchemeIndex>
</SchemeTable>