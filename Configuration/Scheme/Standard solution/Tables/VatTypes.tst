<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="8dd87520-9d83-4d8a-8c60-c1275328c5e8" Name="VatTypes" Group="Common">
	<SchemePhysicalColumn ID="5338f623-353a-4922-ae37-a4a531c7caf1" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="7615e67a-6089-4f1a-95ed-1cfa92ca784a" Name="Name" Type="String(256) Null" />
	<SchemePrimaryKey ID="9d8d94b3-ae97-4cca-9a08-0f34b1f26ee6" Name="pk_VatTypes" IsClustered="true">
		<SchemeIndexedColumn Column="5338f623-353a-4922-ae37-a4a531c7caf1" />
	</SchemePrimaryKey>
	<SchemeIndex ID="43a33ee9-eb62-4eb4-bec4-6cf311c8d573" Name="ndx_VatTypes_Name" IsUnique="true">
		<SchemeIndexedColumn Column="7615e67a-6089-4f1a-95ed-1cfa92ca784a">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="5338f623-353a-4922-ae37-a4a531c7caf1">0</ID>
		<Name ID="7615e67a-6089-4f1a-95ed-1cfa92ca784a">$VatType_WithVAT</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5338f623-353a-4922-ae37-a4a531c7caf1">1</ID>
		<Name ID="7615e67a-6089-4f1a-95ed-1cfa92ca784a">$VatType_ExemptFromVAT</Name>
	</SchemeRecord>
</SchemeTable>