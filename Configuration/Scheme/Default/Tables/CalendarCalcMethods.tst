<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="011f3246-c0f2-4d91-aaee-5129c6b83e15" Name="CalendarCalcMethods" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="011f3246-c0f2-0091-2000-0129c6b83e15" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="011f3246-c0f2-0191-4000-0129c6b83e15" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="bd906dc7-dee2-49f7-99ab-301888285796" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="9196fa9c-63cf-4e9e-b3ce-b8d955811c6a" Name="Description" Type="String(4000) Null" />
	<SchemePhysicalColumn ID="08c2be1d-fa0c-49b3-a955-6423a7ceeae7" Name="Script" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="011f3246-c0f2-0091-5000-0129c6b83e15" Name="pk_CalendarCalcMethods" IsClustered="true">
		<SchemeIndexedColumn Column="011f3246-c0f2-0191-4000-0129c6b83e15" />
	</SchemePrimaryKey>
</SchemeTable>