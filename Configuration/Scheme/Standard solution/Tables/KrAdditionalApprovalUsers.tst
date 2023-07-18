<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="72544086-2776-418a-a867-516ef7aad325" Name="KrAdditionalApprovalUsers" Group="KrStageTypes" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="72544086-2776-008a-2000-016ef7aad325" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="72544086-2776-018a-4000-016ef7aad325" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="72544086-2776-008a-3100-016ef7aad325" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7d4b7325-4847-45ea-b802-5012999ae45c" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7d4b7325-4847-00ea-4000-0012999ae45c" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="75aaa6dc-0412-400b-abe3-13e73a76c09a" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="38ca4071-a22d-4ecb-a2cc-4f270c884399" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="93da675c-a461-4aa7-ad55-92c963c6478a" Name="df_KrAdditionalApprovalUsers_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="72544086-2776-008a-5000-016ef7aad325" Name="pk_KrAdditionalApprovalUsers">
		<SchemeIndexedColumn Column="72544086-2776-008a-3100-016ef7aad325" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="72544086-2776-008a-7000-016ef7aad325" Name="idx_KrAdditionalApprovalUsers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="72544086-2776-018a-4000-016ef7aad325" />
	</SchemeIndex>
</SchemeTable>