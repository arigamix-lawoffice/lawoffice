<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3519b63c-eea0-48f4-b70a-544e58ece5fc" Name="Views" Group="System">
	<Description>Представления.</Description>
	<SchemePhysicalColumn ID="8e4c45ad-ca6f-4f0f-be25-9a9e37a4cfd6" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="827d19f5-a1aa-4e74-92c0-8bb9dcbceb7d" Name="Alias" Type="String(128) Not Null">
		<Description>Алиас представления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1fa10dd2-e0f5-449a-9a96-6cc4e497ef6e" Name="Caption" Type="String(256) Not Null">
		<Description>Отображаемое имя представления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="138ea20a-c807-4da4-adb3-f38d0ae441fc" Name="ModifiedDateTime" Type="DateTime Not Null">
		<Description>Дата изменения представления.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="fb03370c-8b3b-44bc-b567-ecca91fd67c7" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, изменивший представление.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fb03370c-8b3b-00bc-4000-0cca91fd67c7" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="1b7f8e0d-e565-434a-bbe9-4ba46b963711" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5c4b28a6-07aa-4e76-acd5-771985f9e3e6" Name="MetadataSource" Type="String(Max) Not Null">
		<Description>Метаданные представления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d209f71c-e624-4f45-89bc-e8d6f8157b16" Name="MsQuerySource" Type="String(Max) Not Null">
		<Description>Столбец содержит текст запроса для SQL Server</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="508a513b-6ee5-495d-af8c-dfff0e46698c" Name="Description" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="888161fd-6b4e-4631-8791-409ad5a5f54a" Name="df_Views_Description" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d911b4f3-e46b-4671-88ba-c4b2be33230e" Name="GroupName" Type="String(128) Null" />
	<SchemePhysicalColumn ID="ba53dfc1-643f-42d5-b7f3-53b155c4e158" Name="PgQuerySource" Type="String(Max) Not Null">
		<Description>Столбец содержит текст запроса для Postgres</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="54402780-042e-41c2-8958-3fea2f1d322e" Name="df_Views_PgQuerySource" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="72c9eafc-2fd4-456d-b4a5-dc4195265bd7" Name="JsonMetadataSource" Type="BinaryJson Null">
		<Description>Содержит метаданные в формате JSON</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="2ca1a73f-baf1-4441-b91f-79ec1f38e9f3" Name="pk_Views">
		<SchemeIndexedColumn Column="8e4c45ad-ca6f-4f0f-be25-9a9e37a4cfd6" />
	</SchemePrimaryKey>
	<SchemeIndex ID="ef667149-9948-48cf-b829-b7273fe7dc42" Name="ndx_Views_Alias" IsUnique="true">
		<SchemeIndexedColumn Column="827d19f5-a1aa-4e74-92c0-8bb9dcbceb7d">
			<Expression Dbms="PostgreSql">lower("Alias")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>