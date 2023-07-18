<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3d7fe6dc-f80f-4399-83aa-261e4624aaf1" Name="DocLoadBarcodeRead" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d7fe6dc-f80f-0099-2000-061e4624aaf1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3d7fe6dc-f80f-0199-4000-061e4624aaf1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d7fe6dc-f80f-0099-3100-061e4624aaf1" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d72027bd-39c0-49d6-8cb2-9b7f1b4514b8" Name="Barcode" Type="Reference(Typified) Not Null" ReferencedTable="60ad88cc-f913-48ce-96e1-0abf417da790">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d72027bd-39c0-00d6-4000-0b7f1b4514b8" Name="BarcodeID" Type="Int32 Not Null" ReferencedColumn="eee124bb-83cc-496a-af59-cead1dfeaa0b" />
		<SchemeReferencingColumn ID="6ba672e3-7d8c-4aaa-9876-4ac4967a21f1" Name="BarcodeName" Type="String(128) Not Null" ReferencedColumn="872e6a17-18e8-4b20-886d-40730ce2be03" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d7fe6dc-f80f-0099-5000-061e4624aaf1" Name="pk_DocLoadBarcodeRead">
		<SchemeIndexedColumn Column="3d7fe6dc-f80f-0099-3100-061e4624aaf1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d7fe6dc-f80f-0099-7000-061e4624aaf1" Name="idx_DocLoadBarcodeRead_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3d7fe6dc-f80f-0199-4000-061e4624aaf1" />
	</SchemeIndex>
</SchemeTable>