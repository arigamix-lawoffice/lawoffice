<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="8a5782c7-06df-4c0b-9088-2efa46642e8e" Name="KrAdditionalApprovalInfoVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a5782c7-06df-000b-2000-0efa46642e8e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a5782c7-06df-010b-4000-0efa46642e8e" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a5782c7-06df-000b-3100-0efa46642e8e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a503d004-9c8c-44f3-b975-902399622552" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую назначено задание.
Может быть временной ролью, которая удалится после завершения задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a503d004-9c8c-00f3-4000-002399622552" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="c5ad5336-9bea-4f84-be89-6bc0723e0532" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="02796e05-085d-4d2f-a810-b2005c6b973f" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который взял задание в работу или завершил его, или Null, если задание было создано, но не было взято в работу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="02796e05-085d-002f-4000-02005c6b973f" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="04538e6e-7a7d-4efa-8159-49f739fc6b71" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="bb533697-2c78-4f88-a4e9-028a58393408" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения задания или Null, если задание ещё не было завершено.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bb533697-2c78-0088-4000-028a58393408" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715">
			<Description>Идентификатор варианта завершения.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="7354df9c-599b-493d-a6f2-95a8a8a2b051" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554">
			<Description>Отображаемое пользователю имя варианта завершения.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a1155323-937d-4d8c-bfe9-46390da3a328" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к заданию доп. согласования. Например, вопрос, который был задан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0f771839-5493-482e-8b88-62319184b006" Name="Answer" Type="String(Max) Null">
		<Description>Ответный комментарий на задание доп. согласования или Null, если комментарий ещё не был указан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="56130181-8373-4520-b648-9783566856bc" Name="Created" Type="DateTime Not Null">
		<Description>Дата и время, когда отправлено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b44a65ee-fd6c-4ffb-94ca-b3e6623e28db" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия задания в работу или Null, если резолюция ещё не была взята в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5d058c6c-d1b7-4c2f-be28-c18791a23b27" Name="Planned" Type="DateTime Not Null">
		<Description>Дата и время запланированного завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="155cbe26-6940-4ad1-ad6c-9a64d17d5be6" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="eddad571-a555-4b16-856a-e0530a528d99" Name="ColumnComment" Type="String(Max) Null">
		<Description>Краткий комментарий к заданию доп. согласования.
Выводится в колонке таблицы для заданий доп. согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="992ff918-db44-44e4-a212-41dae80d4faf" Name="ColumnState" Type="String(Max) Null">
		<Description>Краткая информация по текущему заданию доп. согласования.
Выводится в колонке таблицы для заданий доп. согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="df4cb20a-3488-4ac9-9823-fd6d674cd267" Name="IsResponsible" Type="Boolean Not Null">
		<Description>Признак, что исполнитель установлен, как "Первый - ответсвенный".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d7083c2f-8d5a-4271-bac6-9ee2cccf1607" Name="df_KrAdditionalApprovalInfoVirtual_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a5782c7-06df-000b-5000-0efa46642e8e" Name="pk_KrAdditionalApprovalInfoVirtual">
		<SchemeIndexedColumn Column="8a5782c7-06df-000b-3100-0efa46642e8e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a5782c7-06df-000b-7000-0efa46642e8e" Name="idx_KrAdditionalApprovalInfoVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8a5782c7-06df-010b-4000-0efa46642e8e" />
	</SchemeIndex>
</SchemeTable>