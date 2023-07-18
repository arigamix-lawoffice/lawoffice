<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="41c02d34-dd86-4115-a485-1bf5e32d2074" Name="KrCardTasksEditorDialogVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="41c02d34-dd86-0015-2000-0bf5e32d2074" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="41c02d34-dd86-0115-4000-0bf5e32d2074" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8a12b366-c1ad-4e0d-b45b-50a095ef627c" Name="KrToken" Type="String(Max) Null" />
	<SchemeComplexColumn ID="9d5fd3c2-63e9-4c37-b545-8fe71f3ef34e" Name="MainCard" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<Description>Ссылка на основную карточку</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9d5fd3c2-63e9-0037-4000-0fe71f3ef34e" Name="MainCardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="41c02d34-dd86-0015-5000-0bf5e32d2074" Name="pk_KrCardTasksEditorDialogVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="41c02d34-dd86-0115-4000-0bf5e32d2074" />
	</SchemePrimaryKey>
</SchemeTable>