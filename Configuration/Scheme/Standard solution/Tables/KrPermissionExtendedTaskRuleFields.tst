<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c30346b8-91a6-4dcd-8324-254e253f0148" Name="KrPermissionExtendedTaskRuleFields" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c30346b8-91a6-00cd-2000-054e253f0148" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c30346b8-91a6-01cd-4000-054e253f0148" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c30346b8-91a6-00cd-3100-054e253f0148" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="952080f4-cb50-4fd9-94a8-28e719b46b8d" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="536f27ed-f1d2-4850-ad9e-eab93f584f1a" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="952080f4-cb50-00d9-4000-08e719b46b8d" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="536f27ed-f1d2-0050-3100-0ab93f584f1a" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9fe7249b-b127-498f-b75e-7e4a3e471bc2" Name="Field" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9fe7249b-b127-008f-4000-0e4a3e471bc2" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="1ba2f5cd-dea2-40bb-9ba4-34154eedc6a5" Name="FieldName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c30346b8-91a6-00cd-5000-054e253f0148" Name="pk_KrPermissionExtendedTaskRuleFields">
		<SchemeIndexedColumn Column="c30346b8-91a6-00cd-3100-054e253f0148" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c30346b8-91a6-00cd-7000-054e253f0148" Name="idx_KrPermissionExtendedTaskRuleFields_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c30346b8-91a6-01cd-4000-054e253f0148" />
	</SchemeIndex>
	<SchemeIndex ID="b60f34d9-8e47-4198-8a4b-7cc5eeed3da0" Name="ndx_KrPermissionExtendedTaskRuleFields_RuleRowID">
		<SchemeIndexedColumn Column="952080f4-cb50-00d9-4000-08e719b46b8d" />
	</SchemeIndex>
</SchemeTable>