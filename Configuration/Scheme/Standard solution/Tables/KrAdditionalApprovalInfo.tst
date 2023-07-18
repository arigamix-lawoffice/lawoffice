<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5f83de75-7485-4785-9528-06ca0e41c5ba" Name="KrAdditionalApprovalInfo" Group="KrStageTypes" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f83de75-7485-0085-2000-06ca0e41c5ba" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5f83de75-7485-0185-4000-06ca0e41c5ba" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f83de75-7485-0085-3100-06ca0e41c5ba" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="42e16c8b-cba5-45fe-aa9a-77c2efff4512" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую назначено задание.
Может быть временной ролью, которая удалится после завершения задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="42e16c8b-cba5-00fe-4000-07c2efff4512" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="29c29b09-5689-462e-a493-8291a95a5071" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b2d424f8-cf28-4520-9231-16f6230ea3e3" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который взял задание в работу или завершил его, или Null, если задание было создано, но не было взято в работу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b2d424f8-cf28-0020-4000-06f6230ea3e3" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="26d0220c-e5ff-4e97-b8ae-d240ec7d54a3" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="06cf9431-424e-4c44-a12e-c3649fa9a806" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения задания или Null, если задание ещё не было завершено.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="06cf9431-424e-0044-4000-03649fa9a806" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715">
			<Description>Идентификатор варианта завершения.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="d0c47b42-d626-4568-aeb9-039217ac543f" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554">
			<Description>Отображаемое пользователю имя варианта завершения.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="8200ddee-59a4-42a0-ad28-985f9c397f1a" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к заданию доп. согласования. Например, вопрос, который был задан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6fb49e05-4d5b-4373-9a88-100f20023be9" Name="Answer" Type="String(Max) Null">
		<Description>Ответный комментарий на задание доп. согласования или Null, если комментарий ещё не был указан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="44536ef1-184b-4cfc-b9d4-41eb7187a8f1" Name="Created" Type="DateTime Not Null">
		<Description>Дата отправки задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7932fbad-e5f9-4789-af47-f3e52c769045" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия задания в работу или Null, если резолюция ещё не была взята в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="68ecd23d-71e8-4c81-938d-4924aa263869" Name="Planned" Type="DateTime Not Null">
		<Description>Дата и время запланированного завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f0482e2a-fc21-4adc-886a-297ab0049c50" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b5f58718-a457-4816-ac6f-704e73c22747" Name="IsResponsible" Type="Boolean Not Null">
		<Description>Признак, что исполнитель установлен, как "Первый - ответсвенный".</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="df9f35f1-3697-4382-8b97-c7c14c3eb12f" Name="df_KrAdditionalApprovalInfo_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f83de75-7485-0085-5000-06ca0e41c5ba" Name="pk_KrAdditionalApprovalInfo">
		<SchemeIndexedColumn Column="5f83de75-7485-0085-3100-06ca0e41c5ba" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f83de75-7485-0085-7000-06ca0e41c5ba" Name="idx_KrAdditionalApprovalInfo_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5f83de75-7485-0185-4000-06ca0e41c5ba" />
	</SchemeIndex>
	<SchemeIndex ID="5841f24a-e907-4d09-b795-6d0be1cf0801" Name="ndx_KrAdditionalApprovalInfo_IDCompleted">
		<SchemeIndexedColumn Column="5f83de75-7485-0185-4000-06ca0e41c5ba" />
		<SchemeIndexedColumn Column="f0482e2a-fc21-4adc-886a-297ab0049c50" />
		<SchemeIncludedColumn Column="5f83de75-7485-0085-3100-06ca0e41c5ba" />
	</SchemeIndex>
</SchemeTable>