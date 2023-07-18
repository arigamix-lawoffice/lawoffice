<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="741301fd-f38a-4cca-bab9-df1328d53b53" Partition="29f90c69-c1ef-4cbf-b9d5-7fc91cd68c67">
	<SchemeIndex Partition="25ef0098-69a9-4454-94c8-c9a7aa0d3f6f" ID="e0c9a574-85bd-474d-97ab-52df8cc79ac1" Name="ndx_HelpSections_PlainText" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="6cfe8eb4-82de-4094-8864-58b00edc8bbc">
			<Expression Dbms="PostgreSql">to_tsvector('arigamix', lower("PlainText"))</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>