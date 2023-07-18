<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="16588bc2-69cf-4a54-bf16-b0bf9507a315" Name="KrPermissionExtendedMandatoryRuleFields" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="16588bc2-69cf-0054-2000-00bf9507a315" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="16588bc2-69cf-0154-4000-00bf9507a315" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="16588bc2-69cf-0054-3100-00bf9507a315" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fc9f5d2b-6323-4e95-9f20-9c2153fda7cb" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="a4b6af05-9147-4335-8bf4-0e7387f77455" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc9f5d2b-6323-0095-4000-0c2153fda7cb" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="a4b6af05-9147-0035-3100-0e7387f77455" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="315fe09a-2bfa-4d2b-90b6-820a98e901b8" Name="Field" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="315fe09a-2bfa-002b-4000-020a98e901b8" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="d90ad3e9-e995-4612-9bf4-7c3f8cac9753" Name="FieldName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="16588bc2-69cf-0054-5000-00bf9507a315" Name="pk_KrPermissionExtendedMandatoryRuleFields">
		<SchemeIndexedColumn Column="16588bc2-69cf-0054-3100-00bf9507a315" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="16588bc2-69cf-0054-7000-00bf9507a315" Name="idx_KrPermissionExtendedMandatoryRuleFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="16588bc2-69cf-0154-4000-00bf9507a315" />
	</SchemeIndex>
	<SchemeIndex ID="cfb0fa50-97a6-4f8d-b1af-5940631a17fa" Name="ndx_KrPermissionExtendedMandatoryRuleFields_RuleRowID">
		<Description>Быстрое удаление правил для FK.</Description>
		<SchemeIndexedColumn Column="fc9f5d2b-6323-0095-4000-0c2153fda7cb" />
	</SchemeIndex>
</SchemeTable>