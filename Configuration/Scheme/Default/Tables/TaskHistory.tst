<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="f8deab4c-fa9d-404a-8abc-b570cd81820e" Name="TaskHistory" Group="System" InstanceType="Cards" ContentType="Hierarchies">
	<Description>История завершённых заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f8deab4c-fa9d-004a-2000-0570cd81820e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f8deab4c-fa9d-014a-4000-0570cd81820e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f8deab4c-fa9d-004a-3100-0570cd81820e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f8deab4c-fa9d-004a-2200-0570cd81820e" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="f8deab4c-fa9d-404a-8abc-b570cd81820e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f8deab4c-fa9d-014a-4020-0570cd81820e" Name="ParentRowID" Type="Guid Null" ReferencedColumn="f8deab4c-fa9d-004a-3100-0570cd81820e" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ca656bba-6182-41c1-9b09-0952ca6419a9" Name="Created" Type="DateTime Not Null">
		<Description>Дата создания задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="690c74a3-be21-43db-9816-96a001806e17" Name="Planned" Type="DateTime Not Null">
		<Description>Запланированная дата завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5dccdcec-4148-4813-a989-7d0fe393415c" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия задания в работу или Null, если задание не взято в работу или такая дата неизвестна.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1f7ab913-0831-4bd8-9989-0a6564840925" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0ef138b2-d5e3-4a67-b704-e474e6bb221f" Name="Result" Type="String(Max) Null">
		<Description>Текстовое описание результата завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="0ceb5e1e-d6e1-49c6-a18b-8c7800195067" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<Description>Тип задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0ceb5e1e-d6e1-00c6-4000-0c7800195067" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="e2422226-0adf-450e-a75a-e19eac145b32" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bbc0e1bc-de43-4fd5-bc5d-6de164dea0fd" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496" WithForeignKey="false">
		<Description>Вариант завершения задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bbc0e1bc-de43-00d5-4000-0de164dea0fd" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715">
			<Description>Идентификатор варианта завершения.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="dfbcf101-4def-475c-aece-45b82e09e820" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554">
			<Description>Отображаемое пользователю имя варианта завершения.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="945d746b-3306-4fdf-bdb1-159b09402fcb" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который завершил задание или взял его в работу</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="945d746b-3306-00df-4000-059b09402fcb" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="777c863f-28f1-40d6-8656-88df140d0679" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="71270c13-fe87-4449-8841-cbb93f909941" Name="UserDepartment" Type="String(512) Null" />
		<SchemePhysicalColumn ID="5c0d4963-e14f-4290-bb5c-c4d804afc3f1" Name="UserPosition" Type="String(512) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="63b34371-7cbc-4b25-9636-02fc85495a09" Name="CompletedBy" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который завершил задание. Заполняется только при завершении задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="63b34371-7cbc-0025-4000-02fc85495a09" Name="CompletedByID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="10237eae-133c-431a-b306-7b999e2bdeb0" Name="CompletedByName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
		<SchemePhysicalColumn ID="b5198dc2-e156-4ce6-8b8f-679d22141af3" Name="CompletedByRole" Type="String(256) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bd901e07-4268-42ca-a98b-691a1de98a7d" Name="TimeZone" Type="Reference(Typified) Not Null" ReferencedTable="984e22bf-78fc-4c69-b1a6-ca73341c36ea" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bd901e07-4268-00ca-4000-091a1de98a7d" Name="TimeZoneID" Type="Int16 Not Null" ReferencedColumn="2aa45b0b-2eb1-40c7-85e9-812b59053f63">
			<SchemeDefaultConstraint IsPermanent="true" ID="e948684b-5342-4d19-a5ea-b98fa21b0fda" Name="df_TaskHistory_TimeZoneID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="9bc7e87b-2ca4-4500-988f-ec55f40b274f" Name="TimeZoneUtcOffsetMinutes" Type="Int32 Not Null" ReferencedColumn="d08567f5-3a73-4431-8e15-65b99ec110ec">
			<SchemeDefaultConstraint IsPermanent="true" ID="53420a83-aa1e-404f-811c-a03230334890" Name="df_TaskHistory_TimeZoneUtcOffsetMinutes" Value="0" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9b8acee1-065d-4fa7-a72b-14fe64148cb6" Name="Group" Type="Reference(Typified) Null" ReferencedTable="31644536-fba1-456c-881c-7dae73b7182c" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9b8acee1-065d-00a7-4000-04fe64148cb6" Name="GroupRowID" Type="Guid Null" ReferencedColumn="31644536-fba1-006c-3100-0dae73b7182c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f8d85729-a6b9-4f19-91a4-91c849ce527e" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f8d85729-a6b9-0019-4000-01c849ce527e" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="a72ba732-dba0-4fd9-8c6a-02cd15429b2a" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="437c21e9-052d-4632-847c-e03c6dfd45bb" Name="Calendar" Type="Reference(Typified) Null" ReferencedTable="67b1fd42-0106-4b31-a368-ea3e4d38ac5c" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="437c21e9-052d-0032-4000-003c6dfd45bb" Name="CalendarID" Type="Guid Null" ReferencedColumn="67b1fd42-0106-0131-4000-0a3e4d38ac5c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="76ce2670-1eb5-4c7b-9fec-a1edd1c4300f" Name="Settings" Type="BinaryJson Null" />
	<SchemeComplexColumn ID="3cb7ab8c-04a5-4938-b34d-29934f57fbdf" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3cb7ab8c-04a5-0038-4000-09934f57fbdf" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="9ce1164d-5791-467d-8ef4-9e0f8510d74d" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4a984fc6-f13c-4887-bd72-fe33097aa20e" Name="AssignedOnRole" Type="String(256) Null">
		<Description>Роли на которые было назначено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f8deab4c-fa9d-004a-5000-0570cd81820e" Name="pk_TaskHistory">
		<SchemeIndexedColumn Column="f8deab4c-fa9d-004a-3100-0570cd81820e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f8deab4c-fa9d-004a-7000-0570cd81820e" Name="idx_TaskHistory_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f8deab4c-fa9d-014a-4000-0570cd81820e" />
	</SchemeIndex>
	<SchemeIndex ID="9ea4c81c-90ee-4d1d-9376-6cff9931b7da" Name="ndx_TaskHistory_ParentRowID">
		<SchemeIndexedColumn Column="f8deab4c-fa9d-014a-4020-0570cd81820e" />
	</SchemeIndex>
	<SchemeIndex ID="1392d378-83dc-4fad-b5ad-10babc836e38" Name="ndx_TaskHistory_UserIDCompleted">
		<SchemeIndexedColumn Column="945d746b-3306-00df-4000-059b09402fcb" />
		<SchemeIndexedColumn Column="1f7ab913-0831-4bd8-9989-0a6564840925" />
		<SchemeIncludedColumn Column="f8deab4c-fa9d-004a-3100-0570cd81820e" />
	</SchemeIndex>
	<SchemeIndex ID="3497959f-7e0b-4eaf-a84e-f65907ed8be4" Name="ndx_TaskHistory_RowID">
		<SchemeIndexedColumn Column="f8deab4c-fa9d-004a-3100-0570cd81820e" />
		<SchemeIncludedColumn Column="f8deab4c-fa9d-014a-4020-0570cd81820e" />
	</SchemeIndex>
	<SchemeIndex ID="44862a3f-977f-4dc0-9b25-998badca852e" Name="ndx_TaskHistory_CompletedOptionID">
		<SchemeIndexedColumn Column="1f7ab913-0831-4bd8-9989-0a6564840925" />
		<SchemeIndexedColumn Column="bbc0e1bc-de43-00d5-4000-0de164dea0fd" />
		<SchemeIncludedColumn Column="f8deab4c-fa9d-004a-3100-0570cd81820e" />
		<SchemeIncludedColumn Column="945d746b-3306-00df-4000-059b09402fcb" />
		<SchemeIncludedColumn Column="690c74a3-be21-43db-9816-96a001806e17" />
		<SchemeIncludedColumn Column="dfbcf101-4def-475c-aece-45b82e09e820" />
	</SchemeIndex>
</SchemeTable>