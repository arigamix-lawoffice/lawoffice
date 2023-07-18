<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c7f6a799-0dd6-4389-9122-bafc68f35c9e" Name="KrPermissionExtendedTaskRuleTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с типами заданий для расширенных настроек доступа к заданиям</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c7f6a799-0dd6-0089-2000-0afc68f35c9e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c7f6a799-0dd6-0189-4000-0afc68f35c9e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c7f6a799-0dd6-0089-3100-0afc68f35c9e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="2c142254-32a3-4c26-827a-ba31937c405b" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="536f27ed-f1d2-4850-ad9e-eab93f584f1a" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2c142254-32a3-0026-4000-0a31937c405b" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="536f27ed-f1d2-0050-3100-0ab93f584f1a" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8968f1d2-3aee-4dc2-a84c-7bd5fe5d1e6c" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8968f1d2-3aee-00c2-4000-0bd5fe5d1e6c" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="a89efa9e-ffa1-4bc9-9a8b-5ed2b5e39a07" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c7f6a799-0dd6-0089-5000-0afc68f35c9e" Name="pk_KrPermissionExtendedTaskRuleTypes">
		<SchemeIndexedColumn Column="c7f6a799-0dd6-0089-3100-0afc68f35c9e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c7f6a799-0dd6-0089-7000-0afc68f35c9e" Name="idx_KrPermissionExtendedTaskRuleTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c7f6a799-0dd6-0189-4000-0afc68f35c9e" />
	</SchemeIndex>
	<SchemeIndex ID="eaa70ab2-c0c5-4eea-ad9b-5ec315c6b1af" Name="ndx_KrPermissionExtendedTaskRuleTypes_RuleRowID">
		<SchemeIndexedColumn Column="2c142254-32a3-0026-4000-0a31937c405b" />
	</SchemeIndex>
</SchemeTable>