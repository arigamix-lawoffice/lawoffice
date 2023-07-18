<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ce71fe9f-6ae4-4f76-8311-7ae54686a474" Name="KrSecondaryProcessCommonInfo" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Содержит информацию по вторичному процессу.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ce71fe9f-6ae4-0076-2000-0ae54686a474" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ce71fe9f-6ae4-0176-4000-0ae54686a474" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ee39c0f4-9295-4ba5-8985-089ce818795f" Name="MainCardID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="96012dee-b299-4ab3-92e8-9bd697058cba" Name="CurrentApprovalStage" Type="Reference(Typified) Null" ReferencedTable="92caadca-2409-40ff-b7d8-1d4fd302b1e9" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="96012dee-b299-00b3-4000-0bd697058cba" Name="CurrentApprovalStageRowID" Type="Guid Null" ReferencedColumn="92caadca-2409-00ff-3100-0d4fd302b1e9" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a3ffd868-d6a8-4572-abf8-608134e01343" Name="Info" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="7d5924e7-9d6d-461e-9ff5-ceffd4a64473" Name="SecondaryProcessID" Type="Guid Null" />
	<SchemePhysicalColumn ID="f4606eb7-4113-4425-9c59-441aa57cee98" Name="NestedWorkflowProcesses" Type="BinaryJson Null" />
	<SchemeComplexColumn ID="ef3260f5-5adc-4376-b370-63bc40d31470" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор (инициатор) процесса.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ef3260f5-5adc-0076-4000-03bc40d31470" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d2f0b9d8-3ce2-4467-b952-43362cf31a02" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5b2f31cc-f265-4adf-ac0d-2930b9e7dacc" Name="ProcessOwner" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Владелец процесса.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5b2f31cc-f265-00df-4000-0930b9e7dacc" Name="ProcessOwnerID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="b1a88dbc-2562-4330-9d25-f960c2e10e12" Name="ProcessOwnerName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ce71fe9f-6ae4-0076-5000-0ae54686a474" Name="pk_KrSecondaryProcessCommonInfo" IsClustered="true">
		<SchemeIndexedColumn Column="ce71fe9f-6ae4-0176-4000-0ae54686a474" />
	</SchemePrimaryKey>
	<SchemeIndex ID="9c8d5cf2-5e7c-49ed-8f2f-46b76461820c" Name="ndx_KrSecondaryProcessCommonInfo_MainCardID">
		<SchemeIndexedColumn Column="ee39c0f4-9295-4ba5-8985-089ce818795f" />
	</SchemeIndex>
</SchemeTable>