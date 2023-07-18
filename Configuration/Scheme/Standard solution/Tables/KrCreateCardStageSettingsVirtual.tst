<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="644515d1-8e3f-419e-b938-f59c5ec07fae" Name="KrCreateCardStageSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="644515d1-8e3f-009e-2000-059c5ec07fae" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="644515d1-8e3f-019e-4000-059c5ec07fae" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="221bfa7c-9cfd-472b-8d05-76b50069d367" Name="Template" Type="Reference(Typified) Null" ReferencedTable="9f15aaf8-032c-4222-9c7c-2cfffeee89ed">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="221bfa7c-9cfd-002b-4000-06b50069d367" Name="TemplateID" Type="Guid Null" ReferencedColumn="9f15aaf8-032c-0122-4000-0cfffeee89ed" />
		<SchemeReferencingColumn ID="9222e6f8-ff62-4682-acdf-3fcc7ec1daba" Name="TemplateCaption" Type="String(128) Null" ReferencedColumn="5a28da2d-0d5f-48e1-a626-f9dc69278788" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0fad1e89-2473-415a-a98e-93929f12056d" Name="Mode" Type="Reference(Typified) Not Null" ReferencedTable="ebf6257e-c0c6-4f84-b913-7a66fc196418">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0fad1e89-2473-005a-4000-03929f12056d" Name="ModeID" Type="Int16 Not Null" ReferencedColumn="81204cd6-3332-4448-bceb-47e51c9049e9" />
		<SchemeReferencingColumn ID="eba78054-ec87-4981-b7f3-485ad6f91e02" Name="ModeName" Type="String(256) Not Null" ReferencedColumn="b73e6dca-ef1a-45ff-aef0-03021bbbbee1" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="82780db1-b412-428e-9017-7f835dcee807" Name="Type" Type="Reference(Typified) Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="82780db1-b412-008e-4000-0f835dcee807" Name="TypeID" Type="Guid Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="230ce587-ae79-4bd6-bea8-4c35a01a9511" Name="TypeCaption" Type="String(128) Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="644515d1-8e3f-009e-5000-059c5ec07fae" Name="pk_KrCreateCardStageSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="644515d1-8e3f-019e-4000-059c5ec07fae" />
	</SchemePrimaryKey>
</SchemeTable>