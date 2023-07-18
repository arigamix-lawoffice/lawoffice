<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="354e4f5a-e50c-4a11-84d0-6e0a98a81ca5" Name="PartnersTypes" Group="Common">
	<SchemePhysicalColumn ID="876c8cd8-505f-40f4-ba4a-65ae78b22945" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="695e6069-4bde-406a-b880-a0a27c87117e" Name="Name" Type="String(256) Null" />
	<SchemePrimaryKey ID="8d428245-85d9-4bd7-bdfb-b567d50c71df" Name="pk_PartnersTypes" IsClustered="true">
		<SchemeIndexedColumn Column="876c8cd8-505f-40f4-ba4a-65ae78b22945" />
	</SchemePrimaryKey>
	<SchemeIndex ID="6644855a-cd08-43ab-84ab-a2eff1ce5a2e" Name="ndx_PartnersTypes_Name" IsUnique="true">
		<SchemeIndexedColumn Column="695e6069-4bde-406a-b880-a0a27c87117e">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="876c8cd8-505f-40f4-ba4a-65ae78b22945">1</ID>
		<Name ID="695e6069-4bde-406a-b880-a0a27c87117e">$PartnerType_LegalEntity</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="876c8cd8-505f-40f4-ba4a-65ae78b22945">2</ID>
		<Name ID="695e6069-4bde-406a-b880-a0a27c87117e">$PartnerType_Individual</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="876c8cd8-505f-40f4-ba4a-65ae78b22945">3</ID>
		<Name ID="695e6069-4bde-406a-b880-a0a27c87117e">$PartnerType_SoleTrader</Name>
	</SchemeRecord>
</SchemeTable>