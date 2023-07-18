<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="89599d8b-fa2f-44de-94d5-9687d4a16854" Name="BusinessProcessButtonRoles" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, которым доступная данная кнопка</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="89599d8b-fa2f-00de-2000-0687d4a16854" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="89599d8b-fa2f-01de-4000-0687d4a16854" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="89599d8b-fa2f-00de-3100-0687d4a16854" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="eed6d4c0-03ed-44f8-af73-001540e69855" Name="Button" Type="Reference(Typified) Not Null" ReferencedTable="59bf0d0b-f7fc-41d3-92da-56c673f1e0b3" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eed6d4c0-03ed-00f8-4000-001540e69855" Name="ButtonRowID" Type="Guid Not Null" ReferencedColumn="59bf0d0b-f7fc-00d3-3100-06c673f1e0b3" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="cd16bf2d-56d0-4f09-98ee-39381fd8a8f7" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cd16bf2d-56d0-0009-4000-09381fd8a8f7" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="1ea80dcf-41dc-4868-907d-052ac45239f3" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="89599d8b-fa2f-00de-5000-0687d4a16854" Name="pk_BusinessProcessButtonRoles">
		<SchemeIndexedColumn Column="89599d8b-fa2f-00de-3100-0687d4a16854" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="89599d8b-fa2f-00de-7000-0687d4a16854" Name="idx_BusinessProcessButtonRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="89599d8b-fa2f-01de-4000-0687d4a16854" />
	</SchemeIndex>
	<SchemeIndex ID="86417f06-b506-4ae2-bb0a-e933293cc613" Name="ndx_BusinessProcessButtonRoles_ButtonRowIDRoleID">
		<SchemeIndexedColumn Column="eed6d4c0-03ed-00f8-4000-001540e69855" />
		<SchemeIndexedColumn Column="cd16bf2d-56d0-0009-4000-09381fd8a8f7" />
	</SchemeIndex>
</SchemeTable>