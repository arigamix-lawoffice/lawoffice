<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e06fa88f-35a2-48fc-8ce4-6e20521b5238" Name="KrTypedTaskSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e06fa88f-35a2-00fc-2000-0e20521b5238" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e06fa88f-35a2-01fc-4000-0e20521b5238" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="34bd7831-7cd2-4e2b-8642-f3c8ae88cd8d" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="34bd7831-7cd2-002b-4000-03c8ae88cd8d" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="b86d0986-0ca0-4b45-ae01-96815dff244c" Name="TaskTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="fe2094c4-b7e5-4f57-858c-a3b5767da3ad" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c7819aa8-cf3d-4066-a548-05166eb3c341" Name="AfterTaskCompletion" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="b438f8bf-335a-41ef-9af7-b6021793e443" Name="TaskDigest" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e06fa88f-35a2-00fc-5000-0e20521b5238" Name="pk_KrTypedTaskSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="e06fa88f-35a2-01fc-4000-0e20521b5238" />
	</SchemePrimaryKey>
</SchemeTable>