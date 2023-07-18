<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="255f542f-3469-4c42-928d-7cf2cfedb644" Name="KrBuildLocalOutputVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция, используемая для вывода результатов сборки текущего объекта подсистемы маршрутов.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="255f542f-3469-0042-2000-0cf2cfedb644" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="255f542f-3469-0142-4000-0cf2cfedb644" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2c0aa0e8-b236-4a4d-aa6a-1122ec4768b1" Name="Output" Type="String(Max) Null">
		<Description>Результаты сборки.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="255f542f-3469-0042-5000-0cf2cfedb644" Name="pk_KrBuildLocalOutputVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="255f542f-3469-0142-4000-0cf2cfedb644" />
	</SchemePrimaryKey>
</SchemeTable>