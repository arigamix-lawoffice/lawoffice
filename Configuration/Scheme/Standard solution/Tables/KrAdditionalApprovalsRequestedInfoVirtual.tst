<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c5d3a740-794c-4904-b9b1-e0a697a7dd80" Name="KrAdditionalApprovalsRequestedInfoVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Tasks" ContentType="Collections">
	<Description>Запрошенные дополнительные согласования.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c5d3a740-794c-0004-2000-00a697a7dd80" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c5d3a740-794c-0104-4000-00a697a7dd80" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c5d3a740-794c-0004-3100-00a697a7dd80" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f4ca99e6-362c-46fa-b673-f07576d06746" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, на которую назначено задание.
Может быть временной ролью, которая удалится после завершения задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f4ca99e6-362c-00fa-4000-007576d06746" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="d979c7c9-c6b0-4fb0-a891-bcc36959d17c" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2d86f63b-fd3f-4217-a669-d39d96b82dda" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который взял задание в работу или завершил его, или Null, если задание было создано, но не было взято в работу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2d86f63b-fd3f-0017-4000-039d96b82dda" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c5e855f3-08c2-4d1a-bd46-8c85fef24a61" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5d1b67b2-010b-4457-bb1f-5e5ccc9c3bdd" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения задания или Null, если задание ещё не было завершено.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5d1b67b2-010b-0057-4000-0e5ccc9c3bdd" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="fac684f5-0712-492f-815f-e43f03cfb455" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="522342b6-9a17-4046-923d-adc8065b5dc5" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий к заданию доп. согласования. Например, вопрос, который был задан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="66358c5b-a4b6-451b-8365-415e50dd3652" Name="Answer" Type="String(Max) Null">
		<Description>Ответный комментарий на задание доп. согласования или Null, если комментарий ещё не был указан.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="26e41f61-4713-4c35-8b55-a4e00590a87c" Name="Created" Type="DateTime Not Null">
		<Description>Дата и время, когда отправлено задание.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2d1c9a1f-8106-4613-bfdd-a14206768d55" Name="InProgress" Type="DateTime Null">
		<Description>Дата взятия задания в работу или Null, если резолюция ещё не была взята в работу.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d9283e6e-c017-4280-b9bf-ee4af313f403" Name="Planned" Type="DateTime Not Null">
		<Description>Дата и время запланированного завершения задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d0b2d4c1-205c-4ceb-a99e-3bb1f3c6e06c" Name="Completed" Type="DateTime Null">
		<Description>Дата завершения задания или Null, если задание ещё не было завершено.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="47b42cd3-aade-4693-930c-659361d6466c" Name="ColumnComment" Type="String(Max) Null">
		<Description>Краткий комментарий к заданию доп. согласования.
Выводится в колонке таблицы для заданий доп. согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ccf08396-7eef-43f3-bb4f-3f6dda9e4bbb" Name="ColumnState" Type="String(Max) Null">
		<Description>Краткая информация по текущему заданию доп. согласования.
Выводится в колонке таблицы для заданий доп. согласования.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c5d3a740-794c-0004-5000-00a697a7dd80" Name="pk_KrAdditionalApprovalsRequestedInfoVirtual">
		<SchemeIndexedColumn Column="c5d3a740-794c-0004-3100-00a697a7dd80" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c5d3a740-794c-0004-7000-00a697a7dd80" Name="idx_KrAdditionalApprovalsRequestedInfoVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c5d3a740-794c-0104-4000-00a697a7dd80" />
	</SchemeIndex>
</SchemeTable>