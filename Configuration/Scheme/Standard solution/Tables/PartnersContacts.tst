<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c57f5563-6673-4ca0-83a1-2896dbd090e1" Name="PartnersContacts" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Контактные лица</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c57f5563-6673-00a0-2000-0896dbd090e1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c57f5563-6673-01a0-4000-0896dbd090e1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c57f5563-6673-00a0-3100-0896dbd090e1" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="0ffa1a47-5e34-47e2-b00b-92fb928f1148" Name="Name" Type="String(255) Null">
		<Description>ФИО</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1b6bce8d-1f47-456b-b419-96a20cd0eb2b" Name="Department" Type="String(255) Null">
		<Description>Подразделение</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ab87f004-c4cb-4745-b5b1-528c79b20d43" Name="Position" Type="String(255) Null">
		<Description>Должность </Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b04443b9-cf4d-4ee0-a38f-3cf074c654a8" Name="PhoneNumber" Type="String(64) Null">
		<Description>Телефон</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7684b306-91d1-42f0-9d78-15bde92b84a3" Name="Email" Type="String(255) Null">
		<Description>Email</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4cf22a76-aea5-4517-a56f-5625f44a499c" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c57f5563-6673-00a0-5000-0896dbd090e1" Name="pk_PartnersContacts">
		<SchemeIndexedColumn Column="c57f5563-6673-00a0-3100-0896dbd090e1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c57f5563-6673-00a0-7000-0896dbd090e1" Name="idx_PartnersContacts_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c57f5563-6673-01a0-4000-0896dbd090e1" />
	</SchemeIndex>
	<SchemeIndex ID="a72208ca-65d1-4e8c-81e8-38c1bdaf9a96" Name="ndx_PartnersContacts_Department" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="1b6bce8d-1f47-456b-b419-96a20cd0eb2b" />
	</SchemeIndex>
	<SchemeIndex ID="7a0720e2-c1ea-463d-97d7-3a563dbca582" Name="ndx_PartnersContacts_Department_7a0720e2" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="1b6bce8d-1f47-456b-b419-96a20cd0eb2b">
			<Expression Dbms="PostgreSql">lower("Department") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="91e9fff2-ee71-4e04-bddc-b324485bfd93" Name="ndx_PartnersContacts_Name" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="0ffa1a47-5e34-47e2-b00b-92fb928f1148" />
	</SchemeIndex>
	<SchemeIndex ID="5961a074-20bf-4830-9a24-27c30e19268d" Name="ndx_PartnersContacts_Name_5961a074" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="0ffa1a47-5e34-47e2-b00b-92fb928f1148">
			<Expression Dbms="PostgreSql">lower("Name") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>