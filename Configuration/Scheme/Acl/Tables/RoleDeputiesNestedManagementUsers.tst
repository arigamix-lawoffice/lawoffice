<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="c9bf7542-de37-4fad-9cda-6b1a5a4964b7" Name="RoleDeputiesNestedManagementUsers" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9bf7542-de37-00ad-2000-0b1a5a4964b7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c9bf7542-de37-01ad-4000-0b1a5a4964b7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9bf7542-de37-00ad-3100-0b1a5a4964b7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="310def44-dcf9-4d32-a169-354e72d12a2c" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="dd329f32-adf0-4336-bd9e-fa084c0fe494" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="310def44-dcf9-0032-4000-054e72d12a2c" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="dd329f32-adf0-0036-3100-0a084c0fe494" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a0dfe997-69e4-4bbe-bd91-ff4f9c50ce88" Name="PersonalRole" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a0dfe997-69e4-00be-4000-0f4f9c50ce88" Name="PersonalRoleID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="2dffba7e-25e0-46d3-af3b-d7b7fa60e9cb" Name="PersonalRoleName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9bf7542-de37-00ad-5000-0b1a5a4964b7" Name="pk_RoleDeputiesNestedManagementUsers">
		<SchemeIndexedColumn Column="c9bf7542-de37-00ad-3100-0b1a5a4964b7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c9bf7542-de37-00ad-7000-0b1a5a4964b7" Name="idx_RoleDeputiesNestedManagementUsers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c9bf7542-de37-01ad-4000-0b1a5a4964b7" />
	</SchemeIndex>
	<SchemeIndex ID="46deb064-b1ef-4a29-b23b-c07825852281" Name="ndx_RoleDeputiesNestedManagementUsers_ParentRowIDPersonalRoleID">
		<SchemeIndexedColumn Column="310def44-dcf9-0032-4000-054e72d12a2c" />
		<SchemeIndexedColumn Column="a0dfe997-69e4-00be-4000-0f4f9c50ce88" />
	</SchemeIndex>
</SchemeTable>