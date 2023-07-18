<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a2f0c6b0-32c0-4c2e-97c4-e431ef93fc84" Name="WorkplacesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Рабочие места, отображаемые как виртуальные карточки в клиенте.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2f0c6b0-32c0-002e-2000-0431ef93fc84" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a2f0c6b0-32c0-012e-4000-0431ef93fc84" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="edc66607-d14a-411f-83fd-f9e03464e2ad" Name="Name" Type="String(128) Not Null">
		<Description>Уникальное имя рабочего места.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a2f0c6b0-32c0-002e-5000-0431ef93fc84" Name="pk_WorkplacesVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="a2f0c6b0-32c0-012e-4000-0431ef93fc84" />
	</SchemePrimaryKey>
</SchemeTable>