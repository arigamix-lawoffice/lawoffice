<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a707b171-d676-45fd-8386-3bd3f20b7a1a" Name="KrPermissionExtendedMandatoryRuleTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a707b171-d676-00fd-2000-0bd3f20b7a1a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a707b171-d676-01fd-4000-0bd3f20b7a1a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a707b171-d676-00fd-3100-0bd3f20b7a1a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="56cf595c-f32e-4527-b333-8674d40a80e4" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="a4b6af05-9147-4335-8bf4-0e7387f77455" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="56cf595c-f32e-0027-4000-0674d40a80e4" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="a4b6af05-9147-0035-3100-0e7387f77455" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="24cdedd5-651e-43db-9cc7-f9d38b86aac7" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="24cdedd5-651e-00db-4000-09d38b86aac7" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="74b4a8f0-6a72-464f-b72f-4bb5c764787d" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a707b171-d676-00fd-5000-0bd3f20b7a1a" Name="pk_KrPermissionExtendedMandatoryRuleTypes">
		<SchemeIndexedColumn Column="a707b171-d676-00fd-3100-0bd3f20b7a1a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a707b171-d676-00fd-7000-0bd3f20b7a1a" Name="idx_KrPermissionExtendedMandatoryRuleTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a707b171-d676-01fd-4000-0bd3f20b7a1a" />
	</SchemeIndex>
	<SchemeIndex ID="5b243fde-0fe5-4923-a2d4-aa17b69a953c" Name="ndx_KrPermissionExtendedMandatoryRuleTypes_RuleRowID">
		<Description>Быстрое удаление правил для FK.</Description>
		<SchemeIndexedColumn Column="56cf595c-f32e-0027-4000-0674d40a80e4" />
	</SchemeIndex>
</SchemeTable>