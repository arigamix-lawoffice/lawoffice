<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a03f6c5d-e719-43d6-bcc5-d2ea321765ab" Partition="29f90c69-c1ef-4cbf-b9d5-7fc91cd68c67">
	<SchemeIndex Partition="25ef0098-69a9-4454-94c8-c9a7aa0d3f6f" ID="6856b14e-e086-47ff-af8e-4972e0db94ee" Name="ndx_FmMessages_PlainText" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="9a54ff5b-9248-48c4-9917-1708781a2f79">
			<Expression Dbms="PostgreSql">to_tsvector('arigamix', lower("PlainText"))</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>