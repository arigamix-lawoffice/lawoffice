<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="5d65177e-590c-4422-9120-1a202a534640" Name="CustomBackgroundColorsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5d65177e-590c-0022-2000-0a202a534640" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5d65177e-590c-0122-4000-0a202a534640" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="15669518-0f4b-45d6-9d48-8910f64a8f97" Name="Color1" Type="Int32 Null" />
	<SchemePhysicalColumn ID="f565622e-cadf-4870-a157-f68e22bd16fb" Name="Color2" Type="Int32 Null" />
	<SchemePhysicalColumn ID="a2a8bc86-1b6f-46e3-be91-75b995e2bd74" Name="Color3" Type="Int32 Null" />
	<SchemePhysicalColumn ID="1034ffae-2f19-4809-9143-86255fdcee60" Name="Color4" Type="Int32 Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5d65177e-590c-0022-5000-0a202a534640" Name="pk_CustomBackgroundColorsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="5d65177e-590c-0122-4000-0a202a534640" />
	</SchemePrimaryKey>
</SchemeTable>