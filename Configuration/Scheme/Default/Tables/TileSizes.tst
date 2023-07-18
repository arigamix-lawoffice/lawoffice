<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9d1fb4ee-fa51-4926-8abb-c464ca91e450" Name="TileSizes" Group="System">
	<Description>Размеры плиток</Description>
	<SchemePhysicalColumn ID="47a9e2f0-0216-4b02-b8d4-e65b166d689f" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="7594adc2-eeb7-4d62-a6bd-2ae7040013fb" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="40a790ef-9dec-412c-9280-fc06042c8783" Name="pk_TileSizes">
		<SchemeIndexedColumn Column="47a9e2f0-0216-4b02-b8d4-e65b166d689f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="47a9e2f0-0216-4b02-b8d4-e65b166d689f">1</ID>
		<Name ID="7594adc2-eeb7-4d62-a6bd-2ae7040013fb">$Enum_TileSize_Half</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="47a9e2f0-0216-4b02-b8d4-e65b166d689f">2</ID>
		<Name ID="7594adc2-eeb7-4d62-a6bd-2ae7040013fb">$Enum_TileSize_Quarter</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="47a9e2f0-0216-4b02-b8d4-e65b166d689f">0</ID>
		<Name ID="7594adc2-eeb7-4d62-a6bd-2ae7040013fb">$Enum_TileSize_Full</Name>
	</SchemeRecord>
</SchemeTable>