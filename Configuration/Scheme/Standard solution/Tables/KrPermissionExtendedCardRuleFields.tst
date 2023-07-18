<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a40f2a59-e858-499d-a24a-0f18aab6cbd0" Name="KrPermissionExtendedCardRuleFields" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Набор полей для расширенных настроек доступа</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a40f2a59-e858-009d-2000-0f18aab6cbd0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a40f2a59-e858-019d-4000-0f18aab6cbd0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a40f2a59-e858-009d-3100-0f18aab6cbd0" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e5561cd1-6d3a-49a1-b102-c0e9bf34bb2e" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="24c7c7fa-0c39-44c5-aa8d-0199ab79606e" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e5561cd1-6d3a-00a1-4000-00e9bf34bb2e" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="24c7c7fa-0c39-00c5-3100-0199ab79606e" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="21128cb9-6717-4baa-bfc3-c017532e45b1" Name="Field" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21128cb9-6717-00aa-4000-0017532e45b1" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="c9044b55-feb9-491b-be17-edf2d350afcf" Name="FieldName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a40f2a59-e858-009d-5000-0f18aab6cbd0" Name="pk_KrPermissionExtendedCardRuleFields">
		<SchemeIndexedColumn Column="a40f2a59-e858-009d-3100-0f18aab6cbd0" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a40f2a59-e858-009d-7000-0f18aab6cbd0" Name="idx_KrPermissionExtendedCardRuleFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a40f2a59-e858-019d-4000-0f18aab6cbd0" />
	</SchemeIndex>
	<SchemeIndex ID="eb5ef078-d439-47c4-a664-e973732cd87a" Name="ndx_KrPermissionExtendedCardRuleFields_RuleRowID">
		<Description>Быстрое удаление правил для FK.</Description>
		<SchemeIndexedColumn Column="e5561cd1-6d3a-00a1-4000-00e9bf34bb2e" />
	</SchemeIndex>
</SchemeTable>