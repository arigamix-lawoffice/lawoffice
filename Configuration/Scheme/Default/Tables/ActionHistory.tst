<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="5089ca1c-27af-46e4-a2c2-af01bfd42e81" Name="ActionHistory" Group="System">
	<SchemePhysicalColumn ID="eaf0940b-c56d-4001-aa85-e93bc6ab6dc3" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор карточки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="046f8da0-b1eb-4182-9a25-e77777b16d9f" Name="RowID" Type="Guid Not Null">
		<Description>Идентификатор записи об истории карточки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d8cbdc3e-b224-44a8-ad9f-a668ebc54a5a" Name="Action" Type="Reference(Typified) Not Null" ReferencedTable="420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc" WithForeignKey="false">
		<Description>Тип действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d8cbdc3e-b224-00a8-4000-0668ebc54a5a" Name="ActionID" Type="Int16 Not Null" ReferencedColumn="e017b3c3-aa04-4a5a-8bab-d66a0496b1f6">
			<Description>Идентификатор типа действия с карточкой.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0bfbe64f-9ad9-4605-9af9-a057a04e58e9" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<Description>Тип карточки.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0bfbe64f-9ad9-0005-4000-0057a04e58e9" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="c67573a0-73e4-4543-ab0e-ceb16ae9263c" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="169221b4-b10f-460e-af39-eb4d0c4b6cd7" Name="Digest" Type="String(128) Null">
		<Description>Краткое описание карточки или действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4963e4e1-907f-4e4a-9eb5-f26589c5c681" Name="Request" Type="BinaryJson Not Null">
		<Description>Сериализованный запрос на выполнение действий с карточкой.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0d53104f-25b8-4f8e-a1f9-4bcd78987a5f" Name="df_ActionHistory_Request" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="24b1e479-4368-405e-8da6-15897caa9020" Name="Modified" Type="DateTime Not Null">
		<Description>Дата и время действия с карточкой.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7e248939-8b75-41a4-80e0-1513da49b6e4" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Пользователь, который произвёл действие с карточкой.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7e248939-8b75-00a4-4000-0513da49b6e4" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="f5b91d85-26cd-46db-bfcb-72824cbaf9bd" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="68b685f7-da03-4b8b-b527-a31bf6594064" Name="Session" Type="Reference(Typified) Null" ReferencedTable="bbd3d574-a33e-49fb-867d-db3c6811365e" WithForeignKey="false">
		<Description>Сессия, в рамках которой выполнялось действие, или Null, если действие было выполнено вне пределов сессии
или в старых сборках платформы, не поддерживавших сессию в истории действий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="68b685f7-da03-008b-4000-031bf6594064" Name="SessionID" Type="Guid Null" ReferencedColumn="5100aae0-3958-4b1a-b135-57b6640ced19" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2a42fb79-37af-49b5-a7cb-c94584e7b781" Name="Application" Type="Reference(Typified) Null" ReferencedTable="b939817b-bc1f-4a9d-87ef-694336870eed" WithForeignKey="false">
		<Description>Приложение, от имени которого выполняется действие. Заполняется для всех записей, начиная с версии 3.6, но для ранее залогированных действий будет равно NULL.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2a42fb79-37af-00b5-4000-094584e7b781" Name="ApplicationID" Type="Guid Null" ReferencedColumn="ac166b37-85ea-4bef-b0d2-ad3b95f3af69" />
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="b299e585-0b9b-44c0-b9d9-ba339aa6c1c8" Name="pk_ActionHistory" IsClustered="true">
		<SchemeIndexedColumn Column="046f8da0-b1eb-4182-9a25-e77777b16d9f" />
	</SchemePrimaryKey>
	<SchemeIndex ID="e2a3fda5-a5c1-4f35-8e50-dca4abc4b983" Name="ndx_ActionHistory_Modified">
		<Description>Индекс для быстрого отображения истории действий по всем карточкам.</Description>
		<SchemeIndexedColumn Column="24b1e479-4368-405e-8da6-15897caa9020" SortOrder="Descending" />
		<SchemeIncludedColumn Column="046f8da0-b1eb-4182-9a25-e77777b16d9f" />
		<SchemeIncludedColumn Column="7e248939-8b75-00a4-4000-0513da49b6e4" />
	</SchemeIndex>
	<SchemeIndex ID="f48196d6-9a10-43d4-ac6a-0e15f1cd496a" Name="ndx_ActionHistory_IDModified">
		<Description>Индекс для быстрого отображения истории действий по карточке, выбранной по ID (плитка "История действий" для карточки).</Description>
		<SchemeIndexedColumn Column="eaf0940b-c56d-4001-aa85-e93bc6ab6dc3" />
		<SchemeIndexedColumn Column="24b1e479-4368-405e-8da6-15897caa9020" SortOrder="Descending" />
		<SchemeIncludedColumn Column="046f8da0-b1eb-4182-9a25-e77777b16d9f" />
		<SchemeIncludedColumn Column="7e248939-8b75-00a4-4000-0513da49b6e4" />
	</SchemeIndex>
	<SchemeIndex ID="07270203-c37f-41ac-8484-ffecb21d7a8f" Name="ndx_ActionHistory_SessionIDModified">
		<Description>Индекс для быстрого отображения истории действий по всем карточкам для заданной сессии.</Description>
		<SchemeIndexedColumn Column="68b685f7-da03-008b-4000-031bf6594064" />
		<SchemeIndexedColumn Column="24b1e479-4368-405e-8da6-15897caa9020" />
		<SchemeIncludedColumn Column="046f8da0-b1eb-4182-9a25-e77777b16d9f" />
		<SchemeIncludedColumn Column="7e248939-8b75-00a4-4000-0513da49b6e4" />
	</SchemeIndex>
	<SchemeIndex ID="741c32bd-7c17-456a-bcce-1df62c2608c5" Name="ndx_ActionHistory_ApplicationIDModified">
		<Description>Индекс для быстрого отображения истории действий по всем карточкам для заданного приложения.</Description>
		<SchemeIndexedColumn Column="2a42fb79-37af-00b5-4000-094584e7b781" />
		<SchemeIndexedColumn Column="24b1e479-4368-405e-8da6-15897caa9020" />
		<SchemeIncludedColumn Column="046f8da0-b1eb-4182-9a25-e77777b16d9f" />
		<SchemeIncludedColumn Column="7e248939-8b75-00a4-4000-0513da49b6e4" />
	</SchemeIndex>
</SchemeTable>