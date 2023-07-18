<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a4c58948-fe22-4e9c-9cfe-5535a4c13990" Name="KrAdditionalApprovalUsersCardVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4c58948-fe22-009c-2000-0535a4c13990" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a4c58948-fe22-019c-4000-0535a4c13990" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4c58948-fe22-009c-3100-0535a4c13990" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c4260eda-4aab-4386-b260-de5f779a7617" Name="MainApprover" Type="Reference(Typified) Not Null" ReferencedTable="b47d668e-7bf0-4165-a10c-6fe22ee10882" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c4260eda-4aab-0086-4000-0e5f779a7617" Name="MainApproverRowID" Type="Guid Not Null" ReferencedColumn="b47d668e-7bf0-0065-3100-0fe22ee10882" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3d29bd35-89b4-40a1-bea1-9bfd64595d95" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3d29bd35-89b4-00a1-4000-0bfd64595d95" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="e3f8ae34-64ba-4895-a1a8-435df3be5873" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f94f49a2-24c4-40da-b8c8-ab9452dabb37" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f1790eab-1c13-47c9-a16a-0b500e076550" Name="df_KrAdditionalApprovalUsersCardVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8e1d3ed7-ae91-4e36-9b2a-54bfdca60b44" Name="IsResponsible" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="48f63b10-8ae3-487b-82f4-ee3cfc62bdd9" Name="df_KrAdditionalApprovalUsersCardVirtual_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7a3a3ab7-5cf4-4b71-a194-79c31afa206b" Name="BasedOnTemplateAdditionalApproval" Type="Reference(Typified) Null" ReferencedTable="a4c58948-fe22-4e9c-9cfe-5535a4c13990" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7a3a3ab7-5cf4-0071-4000-09c31afa206b" Name="BasedOnTemplateAdditionalApprovalRowID" Type="Guid Null" ReferencedColumn="a4c58948-fe22-009c-3100-0535a4c13990" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4c58948-fe22-009c-5000-0535a4c13990" Name="pk_KrAdditionalApprovalUsersCardVirtual">
		<SchemeIndexedColumn Column="a4c58948-fe22-009c-3100-0535a4c13990" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4c58948-fe22-009c-7000-0535a4c13990" Name="idx_KrAdditionalApprovalUsersCardVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a4c58948-fe22-019c-4000-0535a4c13990" />
	</SchemeIndex>
</SchemeTable>