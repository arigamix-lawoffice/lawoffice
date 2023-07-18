<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ef5f3db3-95d9-4654-91a4-87dcd3d2195a" Name="WfTaskCardsVirtual" Group="Wf" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная секция для карточек-сателлитов для задач.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef5f3db3-95d9-0054-2000-07dcd3d2195a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ef5f3db3-95d9-0154-4000-07dcd3d2195a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c611959e-4673-46d9-9325-59f676b0c75b" Name="DocType" Type="Reference(Typified) Null" ReferencedTable="78bfc212-cad5-4d1d-8b91-a9c58562b9d5" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c611959e-4673-00d9-4000-09f676b0c75b" Name="DocTypeID" Type="Guid Null" ReferencedColumn="78bfc212-cad5-011d-4000-09c58562b9d5" />
		<SchemeReferencingColumn ID="16c51078-e467-4f68-9ee9-686c5a2568d2" Name="DocTypeTitle" Type="String(128) Null" ReferencedColumn="2f9f6600-bc8e-491f-b71e-81c8c8ac5987" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c681b377-4f55-44ce-a066-282b3c24acf1" Name="Number" Type="Int64 Null">
		<Description>Порядковый первичный номер документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bcf1adbb-052c-4bd7-91bd-7395c303e79a" Name="FullNumber" Type="String(64) Null">
		<Description>Текстовое отображение первичного номера документа (с префиксами и др.).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="64df3abc-ddad-41a4-a6c3-19667f579876" Name="Sequence" Type="String(128) Null">
		<Description>Последовательность для первичного номера (поля Number и FullNumber)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="11763c82-87b7-4804-8799-284310971c9c" Name="Subject" Type="String(440) Null">
		<Description>Тема документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3a2b659f-b44d-40ed-b472-d173baabf6d3" Name="DocDate" Type="Date Null">
		<Description>Дата документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="34de266b-bc90-471f-b30d-4c3287fef220" Name="CreationDate" Type="DateTime Null">
		<Description>Дата создания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a6f31de7-b3ba-42be-886a-a5ad4bacb0f6" Name="StateModified" Type="DateTime Null">
		<Description></Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b88c07f0-e317-4672-8fa2-0d6fdd1d1a8a" Name="MainCardDigest" Type="String(Max) Null">
		<Description>Digest основной карточки, определяется расширениями.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="91da4ed5-f265-44f5-9ccf-561d989d7f5c" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор документа.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="91da4ed5-f265-00f5-4000-061d989d7f5c" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="9cd44dc2-6ccf-46bf-97af-e89215ab384d" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ff8207a1-a07d-4266-87bf-8a64466b4f92" Name="Registrator" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Регистратор.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ff8207a1-a07d-0066-4000-0a64466b4f92" Name="RegistratorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c4ac78b6-9936-422c-a4b1-1116bcaff8b3" Name="RegistratorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e8dd36e0-5133-4bd8-8990-a71fba6ae2e1" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e8dd36e0-5133-00d8-4000-071fba6ae2e1" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="c904c8ca-7153-4a23-9316-465f915135d0" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef5f3db3-95d9-0054-5000-07dcd3d2195a" Name="pk_WfTaskCardsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="ef5f3db3-95d9-0154-4000-07dcd3d2195a" />
	</SchemePrimaryKey>
</SchemeTable>