<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="a8c71408-d1a3-4dbc-abcb-287dd7b7c648" Name="RoleDeputiesNestedManagementTypesVirtual" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a8c71408-d1a3-00bc-2000-087dd7b7c648" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a8c71408-d1a3-01bc-4000-087dd7b7c648" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a8c71408-d1a3-00bc-3100-087dd7b7c648" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e18c31f6-4fef-4a1e-bb26-ad336e9e69ed" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="3937aa4f-0658-4e8b-a25a-911802f1fa82" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e18c31f6-4fef-001e-4000-0d336e9e69ed" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="3937aa4f-0658-008b-3100-011802f1fa82" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ae4f8272-282d-4e94-b565-bcfe4167f1c6" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ae4f8272-282d-0094-4000-0cfe4167f1c6" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="93bdf0a6-cb57-463a-b35a-4c04cbd86037" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a8c71408-d1a3-00bc-5000-087dd7b7c648" Name="pk_RoleDeputiesNestedManagementTypesVirtual">
		<SchemeIndexedColumn Column="a8c71408-d1a3-00bc-3100-087dd7b7c648" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a8c71408-d1a3-00bc-7000-087dd7b7c648" Name="idx_RoleDeputiesNestedManagementTypesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a8c71408-d1a3-01bc-4000-087dd7b7c648" />
	</SchemeIndex>
</SchemeTable>