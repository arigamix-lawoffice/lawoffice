<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a90baecf-c9ce-4cba-8bb0-150a13666266" Name="KrCardTypesVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная таблица для ссылки из KrPermissions</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a90baecf-c9ce-00ba-2000-050a13666266" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a90baecf-c9ce-01ba-4000-050a13666266" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" Name="Caption" Type="String(128) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a90baecf-c9ce-00ba-5000-050a13666266" Name="pk_KrCardTypesVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="a90baecf-c9ce-01ba-4000-050a13666266" />
	</SchemePrimaryKey>
</SchemeTable>