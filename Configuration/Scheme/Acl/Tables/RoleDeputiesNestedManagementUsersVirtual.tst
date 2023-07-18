<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="6d0cfd99-aa36-4992-9231-eea478138fe6" Name="RoleDeputiesNestedManagementUsersVirtual" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d0cfd99-aa36-0092-2000-0ea478138fe6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6d0cfd99-aa36-0192-4000-0ea478138fe6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d0cfd99-aa36-0092-3100-0ea478138fe6" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="77ce216a-1243-4d7a-afc6-74bb65164acb" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="3937aa4f-0658-4e8b-a25a-911802f1fa82" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="77ce216a-1243-007a-4000-04bb65164acb" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="3937aa4f-0658-008b-3100-011802f1fa82" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="dee2e9bd-0893-4a3c-82a0-5a1f0560c5fa" Name="PersonalRole" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dee2e9bd-0893-003c-4000-0a1f0560c5fa" Name="PersonalRoleID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="b1fe2468-be58-4e33-9517-5f849977b251" Name="PersonalRoleName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d0cfd99-aa36-0092-5000-0ea478138fe6" Name="pk_RoleDeputiesNestedManagementUsersVirtual">
		<SchemeIndexedColumn Column="6d0cfd99-aa36-0092-3100-0ea478138fe6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d0cfd99-aa36-0092-7000-0ea478138fe6" Name="idx_RoleDeputiesNestedManagementUsersVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6d0cfd99-aa36-0192-4000-0ea478138fe6" />
	</SchemeIndex>
</SchemeTable>