<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="5f428478-eaf5-4180-bde9-499483c3f80c" Name="FileSignatures" Group="System" InstanceType="Files" ContentType="Collections">
	<Description>Подписи файла.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f428478-eaf5-0080-2000-099483c3f80c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="dd716146-b177-4920-bc90-b1196b16347c">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5f428478-eaf5-0180-4000-099483c3f80c" Name="ID" Type="Guid Not Null" ReferencedColumn="dd716146-b177-0020-3100-01196b16347c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f428478-eaf5-0080-3100-099483c3f80c" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="cad100e7-ea20-49dd-9707-fa89bfd2b6f3" Name="Version" Type="Reference(Typified) Not Null" ReferencedTable="e17fd270-5c61-49af-955d-ed6bb983f0d8">
		<Description>Версия файла, которой принадлежит подпись.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cad100e7-ea20-00dd-4000-0a89bfd2b6f3" Name="VersionRowID" Type="Guid Not Null" ReferencedColumn="e17fd270-5c61-00af-3100-0d6bb983f0d8" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="edfd5e3d-027f-4f5c-ad08-c8ef5ff78756" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Пользователь, который зарегистрировал подпись в системе, т.е. добавил строку в таблицу.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="edfd5e3d-027f-005c-4000-08ef5ff78756" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="b2fe6eee-e962-4048-87d5-39c926aa1f29" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8d4b4bda-d720-4ebc-be77-b99a69da0cbc" Name="Event" Type="Reference(Typified) Not Null" ReferencedTable="5a8e7767-cd46-4ace-9da3-e3ea6f38cff2">
		<Description>Событие, в результате которого подпись была добавлена.
0 - произвольная логика, определяемая в расширениях.
1 - подпись была импортирована из файла, User - это пользователь, который импортировал подпись.
2 - подпись была создана в системе, User - пользователь, который осуществлял подписывание.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8d4b4bda-d720-00bc-4000-099a69da0cbc" Name="EventID" Type="Int16 Not Null" ReferencedColumn="5c07267f-b144-4691-8334-df3d75c898da">
			<Description>Идентификатор события.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a585f1b0-0570-42d7-88a0-ad531911bb64" Name="Comment" Type="String(512) Null">
		<Description>Произвольный комментарий к подписи, который может использоваться для указания источника подписи и др.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9c3b2c0d-7427-42af-b1d1-dd91cfe4d203" Name="SubjectName" Type="String(256) Null">
		<Description>Получатель сертификата, указанный в файле подписи.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b874dade-5347-4a3f-ab42-0fcab6d19858" Name="Company" Type="String(256) Null">
		<Description>Название компании, указанное в файле подписи.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="32db8007-a52f-43d0-8170-fb5c018ab14b" Name="Signed" Type="DateTime Not Null">
		<Description>Дата и время подписи, указанная в файле подписи.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3dd4c432-b77e-4cd8-91c2-ff2e671975d6" Name="SerialNumber" Type="String(128) Null">
		<Description>Серийный номер сертификата, указанный в файле с подписью.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="528dc58a-9ca9-4370-b03e-40606ed0aac6" Name="IssuerName" Type="String(256) Null">
		<Description>Издатель сертификата, указанный в файле с подписью.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ebfba9d2-8f45-4425-aa15-b61c5b6b40a2" Name="Data" Type="Binary(Max) Null">
		<Description>Бинарные данные файла с подписью.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="143df88e-0672-4e30-8f83-44e72db00f44" Name="SignatureType" Type="Reference(Typified) Not Null" ReferencedTable="577baaea-6832-4eb7-9333-60661367720e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="143df88e-0672-0030-4000-04e72db00f44" Name="SignatureTypeID" Type="Int16 Not Null" ReferencedColumn="dfe71de9-ef54-4eac-8f54-64d5311db556">
			<SchemeDefaultConstraint IsPermanent="true" ID="c4301354-b377-4aec-8f9d-bcc82604971a" Name="df_FileSignatures_SignatureTypeID" Value="1" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="291e4ce9-879a-4c18-a6ea-49e013bad56d" Name="SignatureProfile" Type="Reference(Typified) Not Null" ReferencedTable="eca29bb9-3085-4556-b19a-6015cbc8fb25">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="291e4ce9-879a-0018-4000-09e013bad56d" Name="SignatureProfileID" Type="Int16 Not Null" ReferencedColumn="8c01d076-b862-4d75-852c-453efccfe590">
			<SchemeDefaultConstraint IsPermanent="true" ID="dc9c9a2f-7ee6-4d0b-b880-d9a8bcb60b01" Name="df_FileSignatures_SignatureProfileID" Value="1" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f428478-eaf5-0080-5000-099483c3f80c" Name="pk_FileSignatures">
		<SchemeIndexedColumn Column="5f428478-eaf5-0080-3100-099483c3f80c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f428478-eaf5-0080-7000-099483c3f80c" Name="idx_FileSignatures_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5f428478-eaf5-0180-4000-099483c3f80c" />
	</SchemeIndex>
	<SchemeIndex ID="7088b142-8905-444a-ab47-eb0035072a4c" Name="ndx_FileSignatures_VersionRowID">
		<Description>Индекс для загрузки всех подписей для заданной версии файла без бинарных данных подписи.</Description>
		<SchemeIndexedColumn Column="cad100e7-ea20-00dd-4000-0a89bfd2b6f3" />
		<SchemeIncludedColumn Column="5f428478-eaf5-0080-3100-099483c3f80c" />
		<SchemeIncludedColumn Column="edfd5e3d-027f-005c-4000-08ef5ff78756" />
		<SchemeIncludedColumn Column="b2fe6eee-e962-4048-87d5-39c926aa1f29" />
		<SchemeIncludedColumn Column="8d4b4bda-d720-00bc-4000-099a69da0cbc" />
		<SchemeIncludedColumn Column="a585f1b0-0570-42d7-88a0-ad531911bb64" />
		<SchemeIncludedColumn Column="9c3b2c0d-7427-42af-b1d1-dd91cfe4d203" />
		<SchemeIncludedColumn Column="b874dade-5347-4a3f-ab42-0fcab6d19858" />
		<SchemeIncludedColumn Column="32db8007-a52f-43d0-8170-fb5c018ab14b" />
		<SchemeIncludedColumn Column="3dd4c432-b77e-4cd8-91c2-ff2e671975d6" />
		<SchemeIncludedColumn Column="528dc58a-9ca9-4370-b03e-40606ed0aac6" />
	</SchemeIndex>
</SchemeTable>