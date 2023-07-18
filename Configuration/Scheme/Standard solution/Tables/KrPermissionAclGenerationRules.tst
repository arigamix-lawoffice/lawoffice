<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="04cb0b04-b5c2-477c-ae4a-3d1e19f9530a" Name="KrPermissionAclGenerationRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="04cb0b04-b5c2-007c-2000-0d1e19f9530a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="04cb0b04-b5c2-017c-4000-0d1e19f9530a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="04cb0b04-b5c2-007c-3100-0d1e19f9530a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="3829646d-6ede-4e4b-900c-6ae9b36544b2" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="5518f35a-ea30-4968-983d-aec524aeb710" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3829646d-6ede-004b-4000-0ae9b36544b2" Name="RuleID" Type="Guid Not Null" ReferencedColumn="5518f35a-ea30-0168-4000-0ec524aeb710" />
		<SchemeReferencingColumn ID="d94e4323-8c03-4e9e-8201-dbba7522d918" Name="RuleName" Type="String(128) Not Null" ReferencedColumn="976e584c-c374-428b-82ba-6eb043774c3d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="04cb0b04-b5c2-007c-5000-0d1e19f9530a" Name="pk_KrPermissionAclGenerationRules">
		<SchemeIndexedColumn Column="04cb0b04-b5c2-007c-3100-0d1e19f9530a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="04cb0b04-b5c2-007c-7000-0d1e19f9530a" Name="idx_KrPermissionAclGenerationRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="04cb0b04-b5c2-017c-4000-0d1e19f9530a" />
	</SchemeIndex>
</SchemeTable>