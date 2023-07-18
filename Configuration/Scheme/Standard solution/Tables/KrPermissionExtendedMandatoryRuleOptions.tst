<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ae17320c-ff1b-45fb-9dd4-f9d99c24d824" Name="KrPermissionExtendedMandatoryRuleOptions" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae17320c-ff1b-00fb-2000-09d99c24d824" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ae17320c-ff1b-01fb-4000-09d99c24d824" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae17320c-ff1b-00fb-3100-09d99c24d824" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f4441b0d-c238-4b11-a3e4-322f2d3a4c3a" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="a4b6af05-9147-4335-8bf4-0e7387f77455" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f4441b0d-c238-0011-4000-022f2d3a4c3a" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="a4b6af05-9147-0035-3100-0e7387f77455" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="86ea6bd1-07ac-471b-9bd3-0e4f4f39b047" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="86ea6bd1-07ac-001b-4000-0e4f4f39b047" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="90ffeb60-a426-4212-97e1-9d9a6a9d77fb" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae17320c-ff1b-00fb-5000-09d99c24d824" Name="pk_KrPermissionExtendedMandatoryRuleOptions">
		<SchemeIndexedColumn Column="ae17320c-ff1b-00fb-3100-09d99c24d824" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ae17320c-ff1b-00fb-7000-09d99c24d824" Name="idx_KrPermissionExtendedMandatoryRuleOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ae17320c-ff1b-01fb-4000-09d99c24d824" />
	</SchemeIndex>
	<SchemeIndex ID="e6390409-9e01-4a1e-9178-3eef771d4379" Name="ndx_KrPermissionExtendedMandatoryRuleOptions_RuleRowID">
		<Description>Быстрое удаление правил для FK.</Description>
		<SchemeIndexedColumn Column="f4441b0d-c238-0011-4000-022f2d3a4c3a" />
	</SchemeIndex>
</SchemeTable>