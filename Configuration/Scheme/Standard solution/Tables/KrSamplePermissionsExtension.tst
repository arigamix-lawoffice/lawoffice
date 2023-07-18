<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f45e2e3f-5559-4800-a9f7-45276924234b" Name="KrSamplePermissionsExtension" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Таблица для примера расширения правил доступа типового решения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f45e2e3f-5559-0000-2000-05276924234b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f45e2e3f-5559-0100-4000-05276924234b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="12191d26-ab6b-4a63-8941-3da878ca4fa1" Name="MinAmount" Type="Decimal(18, 2) Null">
		<Description>Минимальная граница суммы, указанной в документе.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0dfa83e5-b7b3-45ee-91ea-d704847a947f" Name="MaxAmount" Type="Decimal(18, 2) Null">
		<Description>Максимальная граница суммы, указанной в документе.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f45e2e3f-5559-0000-5000-05276924234b" Name="pk_KrSamplePermissionsExtension" IsClustered="true">
		<SchemeIndexedColumn Column="f45e2e3f-5559-0100-4000-05276924234b" />
	</SchemePrimaryKey>
</SchemeTable>