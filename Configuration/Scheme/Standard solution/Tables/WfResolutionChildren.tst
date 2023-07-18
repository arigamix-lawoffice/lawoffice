<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="d4f683a4-a1e9-4fc1-ae84-2c4ab304b7fb" Name="WfResolutionChildren" Group="Wf" InstanceType="Tasks" ContentType="Collections">
	<Description>Записи для дочерних резолюций.
Колонка RowID содержит идентификатор дочернего задания.
Если поле IsCompleted = True, то дочерняя резолюция была завершена и задание больше не существует.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d4f683a4-a1e9-00c1-2000-0c4ab304b7fb" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d4f683a4-a1e9-01c1-4000-0c4ab304b7fb" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d4f683a4-a1e9-00c1-3100-0c4ab304b7fb" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="96ce3875-8150-4610-96c3-447b8509aa51" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую назначена резолюция.
Может быть временной ролью, которая удалится после завершения дочернего задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="96ce3875-8150-0010-4000-047b8509aa51" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="9b5eb47a-ed7e-4db5-a93f-14889ec56ae4" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9f6c8572-3e8b-42f1-abe5-8715717ea91f" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который взял задание резолюции в работу или завершил его, или Null, если задание было создано, но не было взято в работу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9f6c8572-3e8b-00f1-4000-0715717ea91f" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a42d81ea-6208-4a20-a06d-d7e20a1e2ccd" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8d479e55-a805-4f9e-a677-b41babf5bc76" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения задания или Null, если задание ещё не было завершено.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8d479e55-a805-009e-4000-041babf5bc76" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715">
			<Description>Идентификатор варианта завершения.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="0147d545-7853-46eb-b62b-05471d13e8b2" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554">
			<Description>Отображаемое пользователю имя варианта завершения.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b9c5a49e-b2c6-44a8-a873-88f93e36674e" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к дочерней резолюции. Например, вопрос, который был задан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="03c0ee39-b12c-4dbe-9368-a3078ba4b4df" Name="Answer" Type="String(Max) Null">
		<Description>Ответный комментарий на дочернюю резолюцию.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="edd3bb16-f544-4910-a72e-bced2ceed096" Name="Created" Type="DateTime Not Null">
		<Description>Дата и время, когда отправлено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2a668a60-ff5e-448c-ac95-fff74032de89" Name="Planned" Type="DateTime Not Null">
		<Description>Дата и время запланированного завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cf4ce8c1-c17c-4df3-abad-4d9606334519" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия резолюции в работу или Null, если резолюция ещё не была взята в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f4c7f659-9297-4c04-840a-c8868f2837fb" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d4f683a4-a1e9-00c1-5000-0c4ab304b7fb" Name="pk_WfResolutionChildren">
		<SchemeIndexedColumn Column="d4f683a4-a1e9-00c1-3100-0c4ab304b7fb" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d4f683a4-a1e9-00c1-7000-0c4ab304b7fb" Name="idx_WfResolutionChildren_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d4f683a4-a1e9-01c1-4000-0c4ab304b7fb" />
	</SchemeIndex>
	<SchemeIndex ID="67310cd2-394a-4b6c-a3d9-643c70254ea5" Name="ndx_WfResolutionChildren_IDCompleted">
		<SchemeIndexedColumn Column="d4f683a4-a1e9-01c1-4000-0c4ab304b7fb" />
		<SchemeIndexedColumn Column="f4c7f659-9297-4c04-840a-c8868f2837fb" />
		<SchemeIncludedColumn Column="d4f683a4-a1e9-00c1-3100-0c4ab304b7fb" />
	</SchemeIndex>
</SchemeTable>