<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="17dcbbe4-108a-4f15-8716-f7d2718f0953" Name="WfResolutionChildrenVirtual" Group="Wf" IsVirtual="true" InstanceType="Tasks" ContentType="Collections">
	<Description>Таблица с информацией по дочерним резолюциям. Используются в заданиях совместно с таблицей WfResolutions.
Таблица является виртуальной и заполняется автоматически в расширениях.
Колонка RowID содержит идентификатор дочернего задания.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="17dcbbe4-108a-0015-2000-07d2718f0953" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="17dcbbe4-108a-0115-4000-07d2718f0953" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="17dcbbe4-108a-0015-3100-07d2718f0953" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="31fe9c2c-4f6d-4d67-af43-ef88585e93b8" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую назначена резолюция.
Может быть временной ролью, которая удалится после завершения дочернего задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31fe9c2c-4f6d-0067-4000-0f88585e93b8" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="19a2f651-eb7f-421f-9932-ae2e1e7020e3" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a66904c7-3451-4026-a7ec-01a5328706c3" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который взял задание резолюции в работу или завершил его, или Null, если задание было создано, но не было взято в работу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a66904c7-3451-0026-4000-01a5328706c3" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="46356d55-5705-4394-9095-94e35dfad2df" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a7b87396-4d4a-47c6-a9db-e8ee1df27fe6" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения задания или Null, если задание ещё не было завершено.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a7b87396-4d4a-00c6-4000-08ee1df27fe6" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715">
			<Description>Идентификатор варианта завершения.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="ecf75cd7-004b-44a8-8b5a-8cabf5b8f3fa" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554">
			<Description>Отображаемое пользователю имя варианта завершения.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a6fb3d5b-b99d-4dd3-81e1-ccdc75d475db" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к дочерней резолюции. Например, вопрос, который был задан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e27b8f17-d1db-4166-9849-828722db7170" Name="Answer" Type="String(Max) Null">
		<Description>Ответный комментарий на дочернюю резолюцию или Null, если комментарий ещё не был указан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b5c0d72c-468e-48b2-9732-c9e65ebdd5ae" Name="Created" Type="DateTime Not Null">
		<Description>Дата и время, когда отправлено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b1e10431-8ab8-4a21-92e7-c75fbce908c2" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия резолюции в работу или Null, если резолюция ещё не была взята в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9ee42103-d51a-4c15-af12-4237e01ba03c" Name="Planned" Type="DateTime Not Null">
		<Description>Дата и время запланированного завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9f9d1a99-e53b-4c0c-94ce-1ff7409cd94d" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0c6ca3f1-8797-4094-aefd-514ea50c980a" Name="ColumnComment" Type="String(Max) Null">
		<Description>Краткий комментарий к дочерней резолюции.
Выводится в колонке таблицы для дочерних резолюций.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a2c34cdc-84c2-49f3-b61c-65f735cce345" Name="ColumnState" Type="String(Max) Null">
		<Description>Краткая информация по текущему состоянию дочерней резолюции.
Выводится в колонке таблицы для дочерних резолюций.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="17dcbbe4-108a-0015-5000-07d2718f0953" Name="pk_WfResolutionChildrenVirtual">
		<SchemeIndexedColumn Column="17dcbbe4-108a-0015-3100-07d2718f0953" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="17dcbbe4-108a-0015-7000-07d2718f0953" Name="idx_WfResolutionChildrenVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="17dcbbe4-108a-0115-4000-07d2718f0953" />
	</SchemeIndex>
</SchemeTable>