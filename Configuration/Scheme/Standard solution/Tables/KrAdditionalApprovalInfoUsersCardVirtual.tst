<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="fed14580-062d-4f30-a344-23c8d2a427d4" Name="KrAdditionalApprovalInfoUsersCardVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Табличка для контрола с доп. согласантами на вкладке настроек этапа</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fed14580-062d-0030-2000-03c8d2a427d4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fed14580-062d-0130-4000-03c8d2a427d4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fed14580-062d-0030-3100-03c8d2a427d4" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="39ffe571-3a86-4bfd-9692-bdaea6f215d1" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="39ffe571-3a86-00fd-4000-0daea6f215d1" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="429d9dd1-ef87-4dc6-92fc-615eaa30ebf4" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5fdbcd5a-3cf5-4e8a-aa8b-95d42d4d55b5" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="6dc58d24-6853-4674-ba68-8dc1e9e577aa" Name="df_KrAdditionalApprovalInfoUsersCardVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="5c4c6f6d-bee8-42a9-8e6a-be6faf13b7e7" Name="MainApprover" Type="Reference(Typified) Not Null" ReferencedTable="b47d668e-7bf0-4165-a10c-6fe22ee10882" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5c4c6f6d-bee8-00a9-4000-0e6faf13b7e7" Name="MainApproverRowID" Type="Guid Not Null" ReferencedColumn="b47d668e-7bf0-0065-3100-0fe22ee10882" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c216e77a-6d60-4cc2-897c-1240bef09212" Name="IsResponsible" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="99a12ead-f071-4262-89ca-48ab6571134b" Name="df_KrAdditionalApprovalInfoUsersCardVirtual_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fed14580-062d-0030-5000-03c8d2a427d4" Name="pk_KrAdditionalApprovalInfoUsersCardVirtual">
		<SchemeIndexedColumn Column="fed14580-062d-0030-3100-03c8d2a427d4" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="fed14580-062d-0030-7000-03c8d2a427d4" Name="idx_KrAdditionalApprovalInfoUsersCardVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="fed14580-062d-0130-4000-03c8d2a427d4" />
	</SchemeIndex>
</SchemeTable>