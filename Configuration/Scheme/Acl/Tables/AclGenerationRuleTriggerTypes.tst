<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="59827979-5949-4bb2-896d-dc8b5a238a32" Name="AclGenerationRuleTriggerTypes" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<Description>Типы карточек, при изменении которых проверяется триггер.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="59827979-5949-00b2-2000-0c8b5a238a32" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="59827979-5949-01b2-4000-0c8b5a238a32" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="59827979-5949-00b2-3100-0c8b5a238a32" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="21b5a1d8-8625-4c3e-9ce2-5fbd35baa492" Name="Trigger" Type="Reference(Typified) Not Null" ReferencedTable="24e6a4b4-7e51-4429-8bb7-648a840e026b" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21b5a1d8-8625-003e-4000-0fbd35baa492" Name="TriggerRowID" Type="Guid Not Null" ReferencedColumn="24e6a4b4-7e51-0029-3100-048a840e026b" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e7069ce7-367e-47f5-a877-4f5661f00f1a" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e7069ce7-367e-00f5-4000-0f5661f00f1a" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="1d0c2121-7ac9-4e38-8ebe-bde520c34e1c" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="59827979-5949-00b2-5000-0c8b5a238a32" Name="pk_AclGenerationRuleTriggerTypes">
		<SchemeIndexedColumn Column="59827979-5949-00b2-3100-0c8b5a238a32" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="59827979-5949-00b2-7000-0c8b5a238a32" Name="idx_AclGenerationRuleTriggerTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="59827979-5949-01b2-4000-0c8b5a238a32" />
	</SchemeIndex>
</SchemeTable>