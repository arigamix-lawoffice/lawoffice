<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="94b08bf8-6cb2-4a11-a42b-4ff996ac71e5" Name="WeEmailActionOptionalRecipients" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список опциональных получателей письма</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="94b08bf8-6cb2-0011-2000-0ff996ac71e5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="94b08bf8-6cb2-0111-4000-0ff996ac71e5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="94b08bf8-6cb2-0011-3100-0ff996ac71e5" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e1802d1d-231c-497b-8f8f-5819f9dc27b0" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e1802d1d-231c-007b-4000-0819f9dc27b0" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="b99ebe40-0254-4a99-96ad-bf94e6832e76" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="94b08bf8-6cb2-0011-5000-0ff996ac71e5" Name="pk_WeEmailActionOptionalRecipients">
		<SchemeIndexedColumn Column="94b08bf8-6cb2-0011-3100-0ff996ac71e5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="94b08bf8-6cb2-0011-7000-0ff996ac71e5" Name="idx_WeEmailActionOptionalRecipients_ID" IsClustered="true">
		<SchemeIndexedColumn Column="94b08bf8-6cb2-0111-4000-0ff996ac71e5" />
	</SchemeIndex>
</SchemeTable>