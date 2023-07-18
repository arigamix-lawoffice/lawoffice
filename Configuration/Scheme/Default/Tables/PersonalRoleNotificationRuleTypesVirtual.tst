<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="16baecaa-9088-4635-af93-4c042893bf1d" Name="PersonalRoleNotificationRuleTypesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="16baecaa-9088-0035-2000-0c042893bf1d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="16baecaa-9088-0135-4000-0c042893bf1d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="16baecaa-9088-0035-3100-0c042893bf1d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4b174d9e-c73d-4e13-b131-bdf21dc5dde5" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4b174d9e-c73d-0013-4000-0df21dc5dde5" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="d80a85ce-3124-491f-b285-417cb6e1e208" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="6e773624-a01e-4101-a5a6-e7ec1fcbe61d" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="925a75d4-639f-4467-9155-c8e21f5433a9" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6e773624-a01e-0001-4000-07ec1fcbe61d" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="925a75d4-639f-0067-3100-08e21f5433a9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="16baecaa-9088-0035-5000-0c042893bf1d" Name="pk_PersonalRoleNotificationRuleTypesVirtual">
		<SchemeIndexedColumn Column="16baecaa-9088-0035-3100-0c042893bf1d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="16baecaa-9088-0035-7000-0c042893bf1d" Name="idx_PersonalRoleNotificationRuleTypesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="16baecaa-9088-0135-4000-0c042893bf1d" />
	</SchemeIndex>
</SchemeTable>