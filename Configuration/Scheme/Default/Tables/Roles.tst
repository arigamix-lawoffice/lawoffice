<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" ID="81f6010b-9641-4aa5-8897-b8e8603fbf4b" Name="Roles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Roles.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="81f6010b-9641-00a5-2000-08e8603fbf4b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="81f6010b-9641-01a5-4000-08e8603fbf4b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="616d6b2e-35d5-424d-846b-618eb25962d0" Name="Name" Type="String(128) Not Null">
		<Description>Role`s name.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="54a93a9b-cd03-4852-b6f1-7a76bbe2de35" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d">
		<Description>Тип роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="54a93a9b-cd03-0052-4000-0a76bbe2de35" Name="TypeID" Type="Int16 Not Null" ReferencedColumn="c9e1fce6-f27f-4fce-83a0-fadbff72f848">
			<Description>Идентификатор типа роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a6f5c001-a096-49dc-9cc7-985d9d35149e" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Ссылка на родительскую роль, если она включена в иерархию (статических ролей, департаментов), или NULL, если роль корневая или не включена в иерархию.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a6f5c001-a096-00dc-4000-085d9d35149e" Name="ParentID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="c13a099f-24d4-458d-a04f-0a9916ed4880" Name="ParentName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="76725824-fe4f-4fce-9ab2-be0a858967f6" Name="Hidden" Type="Boolean Not Null">
		<Description>Признак что роль будет скрыта при выборе</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ed235510-37a3-4e42-9d3e-37659e5cd306" Name="df_Roles_Hidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="57834e06-f4c2-468d-9957-7b0e6b31dcde" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="998f51ac-ffcb-413a-ac2c-a30bff142fe8" Name="AdSyncID" Type="Guid Null">
		<Description>ID объекта в Active Directory</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="622624d0-7407-4230-af3a-e8ee4e80b551" Name="AdSyncDate" Type="DateTime Null">
		<Description>Дата последней синхронизации с Active Directory</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1963f578-3da3-4bd4-82d4-c7a02d7e62a5" Name="AdSyncDisableUpdate" Type="Boolean Null" />
	<SchemePhysicalColumn ID="363d8307-9f5b-4307-a235-1a6e21d9c6c1" Name="AdSyncIndependent" Type="Boolean Not Null">
		<Description>Флажок, разрешающий синхронизацию объекта AD вне корня синхронизации</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d8baed72-f9a7-4181-bf5d-06530c8b37b7" Name="df_Roles_AdSyncIndependent" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="225e6a6b-2904-4fda-a900-41caedde34b4" Name="AdSyncWhenChanged" Type="DateTime Null" />
	<SchemePhysicalColumn ID="c1b7fb89-29b7-4336-84e9-23f651651552" Name="AdSyncDistinguishedName" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="53bb3083-0dbf-4fd4-b317-405d501375da" Name="AdSyncHash" Type="Binary(32) Null">
		<Description>Хеш синхронизируемого объекта Active Directory</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="266cf427-b85a-4a1d-80fd-18cd22e21968" Name="ExternalID" Type="String(128) Null">
		<Description>Поле для связи с внешними источниками данных для сотрудников и подразделений</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="988e92e6-5188-4061-b105-44915e0f53f7" Name="TimeZone" Type="Reference(Typified) Null" ReferencedTable="984e22bf-78fc-4c69-b1a6-ca73341c36ea" WithForeignKey="false">
		<Description>Временная зона</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="988e92e6-5188-0061-4000-04915e0f53f7" Name="TimeZoneID" Type="Int16 Null" ReferencedColumn="2aa45b0b-2eb1-40c7-85e9-812b59053f63" />
		<SchemeReferencingColumn ID="0065444d-9448-44c9-9186-2f48e01d6ef4" Name="TimeZoneShortName" Type="String(20) Null" ReferencedColumn="05ba6f34-73ea-4d4e-8ce0-8c7ed5ba8598" />
		<SchemeReferencingColumn ID="042c858e-353f-4d73-833b-bab0a59f8848" Name="TimeZoneUtcOffsetMinutes" Type="Int32 Null" ReferencedColumn="d08567f5-3a73-4431-8e15-65b99ec110ec" />
		<SchemeReferencingColumn ID="66ce9310-60e1-4560-952c-9009e25bcbbe" Name="TimeZoneCodeName" Type="String(256) Null" ReferencedColumn="a55a67f8-6b64-4f1c-8c04-4b8b6d3be55b" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1813800b-d336-44a5-825a-fb637f2b9265" Name="InheritTimeZone" Type="Boolean Not Null">
		<Description>Наследовать временную зону от родительской роли</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="188c844c-729d-4022-a879-5a37ecf233c7" Name="df_Roles_InheritTimeZone" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="fd05bfeb-303a-4314-b142-edec61da848b" Name="Calendar" Type="Reference(Typified) Null" ReferencedTable="67b1fd42-0106-4b31-a368-ea3e4d38ac5c">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fd05bfeb-303a-0014-4000-0dec61da848b" Name="CalendarID" Type="Guid Null" ReferencedColumn="67b1fd42-0106-0131-4000-0a3e4d38ac5c" />
		<SchemeReferencingColumn ID="531532f5-c4aa-4d8f-b6df-dc22d6b780de" Name="CalendarName" Type="String(255) Null" ReferencedColumn="a593a14d-d146-4071-aaa7-215307755c58" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4cc896b0-b6fa-49a3-b25e-9c536138d5f9" Name="DeputiesExpired" Type="DateTime Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2177634c-cdb3-4e6b-93ec-f84502e93704" Name="df_Roles_DeputiesExpired" Value="1753-01-01T00:00:00Z" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="03a4c0d2-c420-4b10-aaa2-76775793e42f" Name="DisableDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2cda7d13-8628-4f61-8b70-f14260fb4498" Name="df_Roles_DisableDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="81f6010b-9641-00a5-5000-08e8603fbf4b" Name="pk_Roles" IsClustered="true">
		<SchemeIndexedColumn Column="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemePrimaryKey>
	<SchemeIndex ID="1e94bad8-d54d-4c58-911f-6c71e1a2d241" Name="ndx_Roles_TypeIDHiddenName">
		<Description>Порядок колонок имеет значение, поскольку только так индекс используется и SQL Server'ом, и PostgreSQL. Используется представлением Roles.</Description>
		<Predicate Dbms="SqlServer">[TypeID] &lt;&gt; 6</Predicate>
		<Predicate Dbms="PostgreSql">"TypeID" &lt;&gt; 6</Predicate>
		<SchemeIndexedColumn Column="54a93a9b-cd03-0052-4000-0a76bbe2de35" />
		<SchemeIndexedColumn Column="76725824-fe4f-4fce-9ab2-be0a858967f6" />
		<SchemeIndexedColumn Column="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeIndex>
	<SchemeIndex ID="63da876f-cdc0-4f22-8335-850f7ee9c8ba" Name="ndx_Roles_ParentIDTypeID">
		<SchemeIndexedColumn Column="a6f5c001-a096-00dc-4000-085d9d35149e" />
		<SchemeIndexedColumn Column="54a93a9b-cd03-0052-4000-0a76bbe2de35" />
	</SchemeIndex>
	<SchemeIndex ID="3f93e685-455c-4db1-80a0-1e52b12a8728" Name="ndx_Roles_Name">
		<SchemeIndexedColumn Column="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeIndex>
	<SchemeIndex ID="557981b2-d3b5-49ff-b33f-9abccc4bfcf6" Name="ndx_Roles_Name_557981b2" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Expression Dbms="PostgreSql">lower("Name") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="edd6245d-90b8-4593-8acd-641dad7b9665" Name="ndx_Roles_HiddenName" SupportsSqlServer="false">
		<Description>Порядок колонок имеет значение, поскольку только так индекс используется PostgreSQL. В противном случае он пытается сортировать строки в памяти.</Description>
		<SchemeIndexedColumn Column="76725824-fe4f-4fce-9ab2-be0a858967f6" />
		<SchemeIndexedColumn Column="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeIndex>
	<SchemeIndex ID="c336b441-eb1a-436a-84ef-fd0fde613ccd" Name="ndx_Roles_AdSyncID" IsUnique="true">
		<Predicate Dbms="SqlServer">[AdSyncID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"AdSyncID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="998f51ac-ffcb-413a-ac2c-a30bff142fe8" />
	</SchemeIndex>
	<SchemeIndex ID="b1cc08f7-beb0-470d-91bb-29f82ba353d9" Name="ndx_Roles_ExternalID">
		<SchemeIndexedColumn Column="266cf427-b85a-4a1d-80fd-18cd22e21968" />
	</SchemeIndex>
	<SchemeIndex ID="83a9b1a8-c493-4422-af77-441f579bc164" Name="ndx_Roles_DeputiesExpiredTypeIDID">
		<SchemeIndexedColumn Column="4cc896b0-b6fa-49a3-b25e-9c536138d5f9" />
		<SchemeIndexedColumn Column="54a93a9b-cd03-0052-4000-0a76bbe2de35" />
		<SchemeIndexedColumn Column="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeIndex>
</SchemeTable>