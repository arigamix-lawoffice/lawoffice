<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="afafd0bc-446e-4adf-8332-16be0b3d1908" Name="KrSettingsTaskAuthors" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="afafd0bc-446e-00df-2000-06be0b3d1908" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="afafd0bc-446e-01df-4000-06be0b3d1908" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="afafd0bc-446e-00df-3100-06be0b3d1908" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="f4c3a114-17f5-4234-864c-22d4fb98eb52" Name="Description" Type="String(255) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="afafd0bc-446e-00df-5000-06be0b3d1908" Name="pk_KrSettingsTaskAuthors">
		<SchemeIndexedColumn Column="afafd0bc-446e-00df-3100-06be0b3d1908" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="afafd0bc-446e-00df-7000-06be0b3d1908" Name="idx_KrSettingsTaskAuthors_ID" IsClustered="true">
		<SchemeIndexedColumn Column="afafd0bc-446e-01df-4000-06be0b3d1908" />
	</SchemeIndex>
</SchemeTable>