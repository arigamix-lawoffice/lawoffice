<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="3612e150-032f-4a68-bf8e-8e094e5a3a73" Name="Currencies" Group="Common" InstanceType="Cards" ContentType="Entries">
	<Description>Валюты.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3612e150-032f-0068-2000-0e094e5a3a73" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3612e150-032f-0168-4000-0e094e5a3a73" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="60b11ca9-a5b7-48f7-a5c6-6233d166b19a" Name="Name" Type="String(128) Not Null">
		<Description>Название валюты.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bc67fa23-0c21-4f13-bb57-3a89a4fb6a38" Name="Caption" Type="String(128) Null">
		<Description>Локализуемое название валюты.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d307679e-6f6b-4429-83ba-16d0d8b8ecc2" Name="Code" Type="String(64) Null">
		<Description>Код валюты. Обычно это трёхзначный цифровой код по стандарту ISO 4217, но может быть и любой другой код.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3612e150-032f-0068-5000-0e094e5a3a73" Name="pk_Currencies" IsClustered="true">
		<SchemeIndexedColumn Column="3612e150-032f-0168-4000-0e094e5a3a73" />
	</SchemePrimaryKey>
	<SchemeIndex ID="7c1d50a2-e4cc-471b-944d-0de6b4ae9749" Name="ndx_Currencies_Name" IsUnique="true">
		<SchemeIndexedColumn Column="60b11ca9-a5b7-48f7-a5c6-6233d166b19a">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>