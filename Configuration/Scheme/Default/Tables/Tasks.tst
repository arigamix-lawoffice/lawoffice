<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" ID="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8" Name="Tasks" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Tasks of a card</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5bfa9936-bb5a-008f-2000-080bfd8f75f8" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5bfa9936-bb5a-018f-4000-080bfd8f75f8" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5bfa9936-bb5a-008f-3100-080bfd8f75f8" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a8be2299-1baa-4b8e-b403-36b1c0045cbb" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a8be2299-1baa-008e-4000-06b1c0045cbb" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="46ed8a0b-4e9c-45a7-949d-da787ef47cd8" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8a79816f-084b-4f65-9171-7efaa1060781" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="057a85c8-c20f-430b-bd3b-6ea9f9fb82ee">
		<Description>Состояние задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a79816f-084b-0065-4000-0efaa1060781" Name="StateID" Type="Int16 Not Null" ReferencedColumn="413df3de-fc7a-476d-a604-77ee5135e7bc">
			<Description>Идентификатор состояния.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f2e42329-8a91-4431-b618-99aaedd1220f" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который взял задание в работу и может его завершить.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f2e42329-8a91-0031-4000-09aaedd1220f" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6a26c3e3-d70e-4466-89e0-db8334ca359d" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="80e5b81c-5b53-4dd1-96bb-66afa0385401" Name="Planned" Type="DateTime Not Null">
		<Description>Запланированная дата выполнения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a12b7727-1b0a-485f-902e-bb6eccc88af6" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия задания в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1d02164a-5e91-4b2f-9530-b70bc9cd7104" Name="Created" Type="DateTime Not Null">
		<Description>Дата создания задания.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="0d316ae4-08fb-4297-be9e-a41146467f80" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который создал задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0d316ae4-08fb-0097-4000-041146467f80" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c34c0d7f-1431-4998-a9f1-63ea55f5a379" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d13f1223-d780-43e8-928a-84e48a1cc32e" Name="Modified" Type="DateTime Not Null">
		<Description>Дата изменения задания.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="1402af55-5935-4f30-b97f-a853ac030b88" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который изменил задание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1402af55-5935-0030-4000-0853ac030b88" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6eb0920d-4e35-4da0-82b8-dee7493fcd00" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="653735ee-cb40-402e-a233-ec908031dfe3" Name="Digest" Type="String(Max) Null">
		<Description>Digest задания, т.е. любой текст, описывающий задание и отображаемый пользователям.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="80f49067-a30c-4b15-86a7-2d1d3be80c69" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8" WithForeignKey="false">
		<Description>Из-за отсутствия foreign key возможна ситуация, когда задания ссылаются на уже несуществующего родителя.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="80f49067-a30c-0015-4000-0d1d3be80c69" Name="ParentID" Type="Guid Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b0e62ef4-b07b-4dbb-9564-b109fad18fad" Name="Postponed" Type="DateTime Null">
		<Description>Дата и время, когда было отложено задание, или Null, если задание не было отложено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fe1c519f-91e9-4712-805f-693638886dd7" Name="PostponedTo" Type="DateTime Null">
		<Description>Дата и время, до которого было отложено задание, или Null, если задание не было отложено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e497356-ce4c-4f61-a74b-434501f6e6de" Name="PostponeComment" Type="String(512) Null">
		<Description>Комментарий по откладыванию задания или Null, если задание не было отложено или пользователь не задал комментария.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="ee43652e-be42-4166-8a8f-3c7dd98afb82" Name="TimeZone" Type="Reference(Typified) Not Null" ReferencedTable="984e22bf-78fc-4c69-b1a6-ca73341c36ea" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ee43652e-be42-0066-4000-0c7dd98afb82" Name="TimeZoneID" Type="Int16 Not Null" ReferencedColumn="2aa45b0b-2eb1-40c7-85e9-812b59053f63">
			<SchemeDefaultConstraint IsPermanent="true" ID="327121be-d695-4b6d-9ba6-08f8a4248a72" Name="df_Tasks_TimeZoneID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="835fff18-c652-48c1-aa15-8be4fed6625b" Name="TimeZoneUtcOffsetMinutes" Type="Int32 Not Null" ReferencedColumn="d08567f5-3a73-4431-8e15-65b99ec110ec">
			<SchemeDefaultConstraint IsPermanent="true" ID="493cab84-e704-4360-82d0-f5c07f851ba1" Name="df_Tasks_TimeZoneUtcOffsetMinutes" Value="0" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="dd3c91e5-0dc1-49a2-8837-3e98974061b1" Name="Settings" Type="BinaryJson Null" />
	<SchemeComplexColumn ID="b50bfefd-26ae-46f5-b596-cc4479d26c03" Name="Calendar" Type="Reference(Typified) Null" ReferencedTable="67b1fd42-0106-4b31-a368-ea3e4d38ac5c">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b50bfefd-26ae-00f5-4000-0c4479d26c03" Name="CalendarID" Type="Guid Null" ReferencedColumn="67b1fd42-0106-0131-4000-0a3e4d38ac5c" />
		<SchemeReferencingColumn ID="13d0b231-d392-4f14-b438-b382007275b3" Name="CalendarName" Type="String(255) Null" ReferencedColumn="a593a14d-d146-4071-aaa7-215307755c58" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="29922608-4475-4e9d-bb0b-38f3f24031b3" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, являющийся автором задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="29922608-4475-009d-4000-08f3f24031b3" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="b8dad8a3-fe53-4a54-8eb1-8c689b3ecc77" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5bfa9936-bb5a-008f-5000-080bfd8f75f8" Name="pk_Tasks">
		<SchemeIndexedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5bfa9936-bb5a-008f-7000-080bfd8f75f8" Name="idx_Tasks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5bfa9936-bb5a-018f-4000-080bfd8f75f8" />
	</SchemeIndex>
	<SchemeIndex ID="acd28094-dca3-41cd-9f88-2c27026c2421" Name="ndx_Tasks_PostponedToUserIDIDRowID">
		<SchemeIndexedColumn Column="fe1c519f-91e9-4712-805f-693638886dd7" />
		<SchemeIndexedColumn Column="f2e42329-8a91-0031-4000-09aaedd1220f" />
		<SchemeIndexedColumn Column="5bfa9936-bb5a-018f-4000-080bfd8f75f8" />
		<SchemeIndexedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeIndex>
	<SchemeIndex ID="0e8c4cc4-50e1-4c6c-9d8b-2a5a7b6ddcd7" Name="ndx_Tasks_TypeIDStateID">
		<SchemeIndexedColumn Column="a8be2299-1baa-008e-4000-06b1c0045cbb" />
		<SchemeIndexedColumn Column="8a79816f-084b-0065-4000-0efaa1060781" />
	</SchemeIndex>
	<SchemeIndex ID="8b59a4bc-a491-41a6-85d1-f1a21515ced6" Name="ndx_Tasks_ParentID">
		<SchemeIndexedColumn Column="80f49067-a30c-0015-4000-0d1d3be80c69" />
		<SchemeIncludedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeIndex>
	<SchemeIndex ID="9d9e56e2-e724-410a-bf3f-04b30a7be630" Name="ndx_Tasks_TypeIDParentID">
		<SchemeIndexedColumn Column="a8be2299-1baa-008e-4000-06b1c0045cbb" />
		<SchemeIndexedColumn Column="80f49067-a30c-0015-4000-0d1d3be80c69" />
		<SchemeIncludedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeIndex>
	<SchemeIndex ID="27903bcf-1f1e-4bdd-ae54-82a1c8ceda8b" Name="ndx_Tasks_Modified">
		<SchemeIndexedColumn Column="d13f1223-d780-43e8-928a-84e48a1cc32e" />
		<SchemeIncludedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
		<SchemeIncludedColumn Column="f2e42329-8a91-0031-4000-09aaedd1220f" />
	</SchemeIndex>
	<SchemeIndex ID="4eb20cbe-5ac8-425d-a5ae-b498e004f77c" Name="ndx_Tasks_StateIDCreated">
		<SchemeIndexedColumn Column="8a79816f-084b-0065-4000-0efaa1060781" />
		<SchemeIndexedColumn Column="1d02164a-5e91-4b2f-9530-b70bc9cd7104" />
		<SchemeIncludedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
		<SchemeIncludedColumn Column="f2e42329-8a91-0031-4000-09aaedd1220f" />
	</SchemeIndex>
	<SchemeIndex ID="e02d7600-a9c0-44b6-a42e-2ede0de48d7f" Name="ndx_Tasks_UserIDRowID">
		<FillFactor Dbms="SqlServer">80</FillFactor>
		<SchemeIndexedColumn Column="f2e42329-8a91-0031-4000-09aaedd1220f" />
		<SchemeIndexedColumn Column="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
		<SchemeIncludedColumn Column="d13f1223-d780-43e8-928a-84e48a1cc32e" />
		<SchemeIncludedColumn Column="80e5b81c-5b53-4dd1-96bb-66afa0385401" />
		<SchemeIncludedColumn Column="8a79816f-084b-0065-4000-0efaa1060781" />
		<SchemeIncludedColumn Column="a8be2299-1baa-008e-4000-06b1c0045cbb" />
		<SchemeIncludedColumn Column="1d02164a-5e91-4b2f-9530-b70bc9cd7104" />
	</SchemeIndex>
</SchemeTable>