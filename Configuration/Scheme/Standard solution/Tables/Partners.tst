<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5d47ef13-b6f4-47ef-9815-3b3d0e6d475a" Name="Partners" Group="Common" InstanceType="Cards" ContentType="Entries">
	<Description>Контрагенты</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5d47ef13-b6f4-00ef-2000-0b3d0e6d475a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5d47ef13-b6f4-01ef-4000-0b3d0e6d475a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f1c960e0-951e-4837-8474-bb61d98f40f0" Name="Name" Type="String(255) Not Null">
		<Description>Краткое название контрагента, например "Василек, ООО". </Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0e8dd598-19ba-4fa1-9e38-78eb6f9ba074" Name="FullName" Type="String(450) Null">
		<Description>Полное название контрагента, например "Общество с ограниченной ответственностью "Василек"</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8eac7508-96eb-466f-8242-8522683a82d4" Name="LegalAddress" Type="String(512) Null">
		<Description>Юридический адрес</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e84d9a1-89a9-407f-9e44-8d2ef606fa3a" Name="Phone" Type="String(128) Null">
		<Description>Телефон контрагента</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="309ff2b8-0f56-44f8-ae70-81144bc61605" Name="Head" Type="String(256) Null">
		<Description>Руководитель</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="692f7173-ebdb-4c1c-9478-2bc05d477571" Name="ChiefAccountant" Type="String(256) Null">
		<Description>Главный бухгалтер</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a49729a6-d025-4eea-b2cd-6f52a035afaf" Name="ContactPerson" Type="String(256) Null">
		<Description>Контактное лицо</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2a531740-ca7a-4af2-89b1-65e4a98f7aa5" Name="Email" Type="String(256) Null" />
	<SchemePhysicalColumn ID="810379cc-09a3-4a28-8ed7-fdc4ce6e8aaa" Name="ContactAddress" Type="String(512) Null">
		<Description>Адрес контактный</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8578c2e0-f294-442b-b9a4-c30bb43f9698" Name="INN" Type="String(256) Null">
		<Description>ИНН</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5034c793-9ab3-4fde-b045-8f2d5cf3286a" Name="KPP" Type="String(256) Null">
		<Description>КПП</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b31abd5e-60c1-406e-822f-39410146db69" Name="OGRN" Type="String(256) Null" />
	<SchemePhysicalColumn ID="200f7ae5-6b1b-4fa5-8bb7-e5622c24d8c8" Name="OKPO" Type="String(256) Null" />
	<SchemePhysicalColumn ID="04d21288-97b0-440c-bdc6-d10a1b1de326" Name="OKVED" Type="String(256) Null" />
	<SchemePhysicalColumn ID="4109eb88-c386-4e80-89df-a4f059f822f0" Name="Comment" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="72a91250-f471-4308-bc87-9410612e2b86" Name="Bank" Type="String(256) Null">
		<Description>Банк</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="51e1f134-83c5-4a82-ad6b-d17c1de7f1d3" Name="SettlementAccount" Type="String(256) Null">
		<Description>Расчетный счёт</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9c48e71b-3c87-4810-8793-30fb0b09bf7b" Name="BIK" Type="String(256) Null">
		<Description>БИК</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb6cff99-c792-4d72-8fda-dca98d725b0c" Name="CorrAccount" Type="String(256) Null">
		<Description>Корр. счёт</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="cde1cde5-de0f-4556-b077-7dd2b8800eb9" Name="Type" Type="Reference(Typified) Null" ReferencedTable="354e4f5a-e50c-4a11-84d0-6e0a98a81ca5">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cde1cde5-de0f-0056-4000-0dd2b8800eb9" Name="TypeID" Type="Int32 Null" ReferencedColumn="876c8cd8-505f-40f4-ba4a-65ae78b22945">
			<SchemeDefaultConstraint IsPermanent="true" ID="20ab56c2-200a-4442-b1ca-835cdb2c4c85" Name="df_Partners_TypeID" Value="1" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="b17de6ab-c715-4f62-b3fb-f7a6bb2a40a8" Name="TypeName" Type="String(256) Null" ReferencedColumn="695e6069-4bde-406a-b880-a0a27c87117e">
			<SchemeDefaultConstraint IsPermanent="true" ID="5fc972b5-70dc-4f82-adb3-efc062276477" Name="df_Partners_TypeName" Value="$PartnerType_LegalEntity" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ece42d34-97cb-4a99-b10c-4e16fcdf86e3" Name="VatType" Type="Reference(Typified) Null" ReferencedTable="8dd87520-9d83-4d8a-8c60-c1275328c5e8">
		<SchemeReferencingColumn IsPermanent="true" ID="910dcc69-6499-4930-92ee-acb25c0eb412" Name="VatTypeID" Type="Int32 Null" ReferencedColumn="5338f623-353a-4922-ae37-a4a531c7caf1" />
		<SchemeReferencingColumn ID="6a918717-99eb-43f6-964a-ac70bf3fc134" Name="VatTypeName" Type="String(256) Null" ReferencedColumn="7615e67a-6089-4f1a-95ed-1cfa92ca784a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5d47ef13-b6f4-00ef-5000-0b3d0e6d475a" Name="pk_Partners" IsClustered="true">
		<SchemeIndexedColumn Column="5d47ef13-b6f4-01ef-4000-0b3d0e6d475a" />
	</SchemePrimaryKey>
	<SchemeIndex ID="4fa00c45-3a61-4342-94d1-f542d4ee3ccc" Name="ndx_Partners_Name">
		<SchemeIndexedColumn Column="f1c960e0-951e-4837-8474-bb61d98f40f0" />
	</SchemeIndex>
	<SchemeIndex ID="449d656e-6c58-47bc-b3e0-46cdff0714ba" Name="ndx_Partners_FullName" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="0e8dd598-19ba-4fa1-9e38-78eb6f9ba074" />
	</SchemeIndex>
	<SchemeIndex ID="1466d678-d816-4858-8c01-dcb6a137aa69" Name="ndx_Partners_INN">
		<SchemeIndexedColumn Column="8578c2e0-f294-442b-b9a4-c30bb43f9698" />
	</SchemeIndex>
	<SchemeIndex ID="632ca806-0b4e-4c98-bfcb-8d6d320bb802" Name="ndx_Partners_KPP">
		<SchemeIndexedColumn Column="5034c793-9ab3-4fde-b045-8f2d5cf3286a" />
	</SchemeIndex>
	<SchemeIndex ID="54b4c71a-8d68-48c7-bd31-09398c91ee23" Name="ndx_Partners_OGRN">
		<SchemeIndexedColumn Column="b31abd5e-60c1-406e-822f-39410146db69" />
	</SchemeIndex>
	<SchemeIndex ID="fdf44b62-b98f-4589-90b3-10e072bd2cda" Name="ndx_Partners_TypeID">
		<SchemeIndexedColumn Column="cde1cde5-de0f-0056-4000-0dd2b8800eb9" />
	</SchemeIndex>
	<SchemeIndex ID="13ad5f87-3657-445d-b9ff-66b114393a8b" Name="ndx_Partners_FullName_13ad5f87" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="0e8dd598-19ba-4fa1-9e38-78eb6f9ba074">
			<Expression Dbms="PostgreSql">lower("FullName") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="ff063e05-d5b7-4b96-ae6d-c0b25e6ae5c9" Name="ndx_Partners_Name_ff063e05" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="f1c960e0-951e-4837-8474-bb61d98f40f0">
			<Expression Dbms="PostgreSql">lower("Name") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>