<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="9b7fc0b0-da06-46df-a5c9-d66ecc386d55" Name="WeTaskActionNotificationRoles" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9b7fc0b0-da06-00df-2000-066ecc386d55" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9b7fc0b0-da06-01df-4000-066ecc386d55" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9b7fc0b0-da06-00df-3100-066ecc386d55" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d879808b-d178-40dc-9c52-1f5c5dbd5eaa" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d879808b-d178-00dc-4000-0f5c5dbd5eaa" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="3392ac99-7913-4cc4-956c-e5bde4a05b49" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="695eacd0-a523-4d98-8398-6d42f2b4be78" Name="TaskOption" Type="Reference(Typified) Null" ReferencedTable="e30dcb0a-2a63-4f52-82f9-a12b0038d70d" IsReferenceToOwner="true" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="695eacd0-a523-0098-4000-0d42f2b4be78" Name="TaskOptionRowID" Type="Guid Null" ReferencedColumn="e30dcb0a-2a63-0052-3100-012b0038d70d" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2220dae5-e9f6-4581-92e6-bfdafe541e21" Name="TaskGroupOption" Type="Reference(Typified) Not Null" ReferencedTable="dee05376-8267-42b9-8cc9-1ff5bb58bb06" IsReferenceToOwner="true" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2220dae5-e9f6-0081-4000-0fdafe541e21" Name="TaskGroupOptionRowID" Type="Guid Not Null" ReferencedColumn="dee05376-8267-00b9-3100-0ff5bb58bb06" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9b7fc0b0-da06-00df-5000-066ecc386d55" Name="pk_WeTaskActionNotificationRoles">
		<SchemeIndexedColumn Column="9b7fc0b0-da06-00df-3100-066ecc386d55" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9b7fc0b0-da06-00df-7000-066ecc386d55" Name="idx_WeTaskActionNotificationRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9b7fc0b0-da06-01df-4000-066ecc386d55" />
	</SchemeIndex>
</SchemeTable>