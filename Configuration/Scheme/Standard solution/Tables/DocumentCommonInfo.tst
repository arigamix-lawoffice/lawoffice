<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a161e289-2f99-4699-9e95-6e3336be8527" Name="DocumentCommonInfo" Group="Common" InstanceType="Cards" ContentType="Entries">
	<Description>Общая секция для всех видов документов.
Должна использоваться для всех типов карточек, использующих такие поля, как номер, контрагент, тема и т.п.
Помимо всего прочего, активно используется в поиске и представлениях.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a161e289-2f99-0099-2000-0e3336be8527" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a161e289-2f99-0199-4000-0e3336be8527" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="99037f7d-c376-4cb5-a06a-006fb96548d5" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="99037f7d-c376-00b5-4000-006fb96548d5" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
			<SchemeDefaultConstraint IsPermanent="true" ID="a196f380-dafd-43bc-8dfc-7e2e90cf2993" Name="df_DocumentCommonInfo_CardTypeID" Value="6d06c5a0-9687-4f6b-9bed-d3a081d84d9a" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="d713fa35-af02-4520-bc91-47f5a04e44fa" Name="DocType" Type="Reference(Typified) Null" ReferencedTable="78bfc212-cad5-4d1d-8b91-a9c58562b9d5">
		<Description>Тип документа для карточки, использующей типы документов</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d713fa35-af02-0020-4000-07f5a04e44fa" Name="DocTypeID" Type="Guid Null" ReferencedColumn="78bfc212-cad5-011d-4000-09c58562b9d5" />
		<SchemeReferencingColumn ID="c8d7a11d-0db3-4e6c-9ce1-85cecf7ee20e" Name="DocTypeTitle" Type="String(128) Null" ReferencedColumn="2f9f6600-bc8e-491f-b71e-81c8c8ac5987">
			<Description>Отображаемое название типа документа</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="434a798f-bd1f-4c5c-b291-80490cd0dccf" Name="Number" Type="Int64 Null">
		<Description>Порядковый первичный номер документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="eeb023b5-f473-4a8b-bef2-88e07b0d0688" Name="FullNumber" Type="String(64) Null">
		<Description>Текстовое отображение первичного номера документа (с префиксами и др.).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5a78f218-dae8-41de-92aa-6746d44468c3" Name="Sequence" Type="String(128) Null">
		<Description>Последовательность для первичного номера (поля Number и FullNumber)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="46115821-4126-40ab-a14f-64f33d17d271" Name="SecondaryNumber" Type="Int64 Null">
		<Description>Порядковый вторичный номер документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e43c836d-63d7-460d-ab81-b5823383e150" Name="SecondaryFullNumber" Type="String(64) Null">
		<Description>Текстовое отображение вторичный номера документа (с префиксами и др.).</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dadb1130-034d-493c-b7a0-a7c74eae078b" Name="SecondarySequence" Type="String(128) Null">
		<Description>Последовательность для вторичного номера (поля SecondaryNumber и SecondaryFullNumber)</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2246998b-e4f7-45f1-8a96-211848466450" Name="Subject" Type="String(440) Null">
		<Description>Тема документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b452a4ce-ae1e-43fd-a44f-cc74464a6cd3" Name="DocDate" Type="Date Null">
		<Description>Дата документа.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d804eb6c-9f50-4299-b31c-2fef064fae9e" Name="CreationDate" Type="DateTime Null">
		<Description>Дата создания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="11fce288-91a8-4ddf-a647-f1acbfa81959" Name="OutgoingNumber" Type="String(60) Null">
		<Description>Исходящий (внешний) номер документа / Номер контрагента</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ed01aef8-43f4-441d-8b0d-1cb249164e9b" Name="Amount" Type="Decimal(18, 2) Null">
		<Description>Денежная сумма в документе.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f1abff1c-cb65-4f22-9de1-c6ef302b1a60" Name="Barcode" Type="String(128) Null" />
	<SchemeComplexColumn ID="1f65d492-78e7-4818-b098-8d7a3c524934" Name="Currency" Type="Reference(Typified) Null" ReferencedTable="3612e150-032f-4a68-bf8e-8e094e5a3a73">
		<Description>Карточка валюты документа. Может использоваться для денежной суммы в поле Amount.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1f65d492-78e7-0018-4000-0d7a3c524934" Name="CurrencyID" Type="Guid Null" ReferencedColumn="3612e150-032f-0168-4000-0e094e5a3a73" />
		<SchemeReferencingColumn ID="a11f7fc6-222a-4802-b13a-7a52f1339224" Name="CurrencyName" Type="String(128) Null" ReferencedColumn="60b11ca9-a5b7-48f7-a5c6-6233d166b19a">
			<Description>Название валюты.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="aa152ba8-dc1f-4efa-8c68-03ba804ef6f1" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор документа.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aa152ba8-dc1f-00fa-4000-03ba804ef6f1" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="71be0eb1-3c16-453d-af8f-133cd9a290db" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3089d333-8647-49e7-b2ef-d4f8958da086" Name="Registrator" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Регистратор.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3089d333-8647-00e7-4000-04f8958da086" Name="RegistratorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d750725c-270c-4339-87f0-76be259e1903" Name="RegistratorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1b5c91ec-7cc6-45d0-b520-8ae4f2dc0a75" Name="SignedBy" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Подписано</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1b5c91ec-7cc6-00d0-4000-0ae4f2dc0a75" Name="SignedByID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="de897147-3250-40a3-a71f-bb9502380b76" Name="SignedByName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="36be79af-fff7-417e-997e-4fe3f51aae56" Name="Department" Type="Reference(Typified) Null" ReferencedTable="d43dace1-536f-4c9f-af15-49a8892a7427">
		<Description>Подразделение</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="36be79af-fff7-007e-4000-0fe3f51aae56" Name="DepartmentID" Type="Guid Null" ReferencedColumn="d43dace1-536f-019f-4000-09a8892a7427" />
		<SchemePhysicalColumn ID="f47e37a5-8181-438a-a7c6-fb79ffe63680" Name="DepartmentName" Type="String(128) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="22ed59ec-4939-4d96-a7a8-cbf0bda55ec0" Name="Partner" Type="Reference(Typified) Null" ReferencedTable="5d47ef13-b6f4-47ef-9815-3b3d0e6d475a">
		<Description>Контрагент.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="22ed59ec-4939-0096-4000-0bf0bda55ec0" Name="PartnerID" Type="Guid Null" ReferencedColumn="5d47ef13-b6f4-01ef-4000-0b3d0e6d475a" />
		<SchemeReferencingColumn ID="b74eac03-637d-4a69-a57d-674c8e231967" Name="PartnerName" Type="String(255) Null" ReferencedColumn="f1c960e0-951e-4837-8474-bb61d98f40f0">
			<Description>Краткое название контрагента, например "Василек, ООО". </Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="adff2ccc-f918-4b5d-8e66-4d7a6fbd2a24" Name="RefDoc" Type="Reference(Typified) Null" ReferencedTable="a161e289-2f99-4699-9e95-6e3336be8527">
		<Description>Ссылка на связанный документ. Например, ссылка от исходящего документа на входящий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="adff2ccc-f918-005d-4000-0d7a6fbd2a24" Name="RefDocID" Type="Guid Null" IsSparse="true" ReferencedColumn="a161e289-2f99-0199-4000-0e3336be8527" />
		<SchemePhysicalColumn ID="191a00e2-33e8-48d9-8a64-1125cb8706ee" Name="RefDocDescription" Type="String(250) Null" IsSparse="true" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="6f6c57be-aee6-4799-bffe-c7b91002472a" Name="Receiver" Type="Reference(Typified) Null" ReferencedTable="c57f5563-6673-4ca0-83a1-2896dbd090e1">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6f6c57be-aee6-0099-4000-07b91002472a" Name="ReceiverRowID" Type="Guid Null" IsSparse="true" ReferencedColumn="c57f5563-6673-00a0-3100-0896dbd090e1" />
		<SchemeReferencingColumn ID="eb2dc0b3-0499-47d0-8f91-6cc065eba3f8" Name="ReceiverName" Type="String(255) Null" IsSparse="true" ReferencedColumn="0ffa1a47-5e34-47e2-b00b-92fb928f1148">
			<Description>ФИО</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e4c2fb7d-b232-4654-ae85-9e601055c234" Name="Category" Type="Reference(Typified) Null" ReferencedTable="f939aa52-dc1a-40b2-af4a-cb2757e8390a">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e4c2fb7d-b232-0054-4000-0e601055c234" Name="CategoryID" Type="Guid Null" IsSparse="true" ReferencedColumn="f939aa52-dc1a-01b2-4000-0b2757e8390a" />
		<SchemeReferencingColumn ID="2691939d-093a-4b24-9dc4-ade2902350d1" Name="CategoryName" Type="String(128) Null" IsSparse="true" ReferencedColumn="3dd39fa6-b8bd-4084-8aeb-f129f796f450" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="6409c86c-9200-4ef9-83cd-603bcc17377d" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6409c86c-9200-00f9-4000-003bcc17377d" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c">
			<SchemeDefaultConstraint IsPermanent="true" ID="f103ae50-a8ba-4bf6-a8d9-f15bd330641a" Name="df_DocumentCommonInfo_StateID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="daf5af14-8e3f-4711-ab35-bb8fcb1ee3a9" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9">
			<SchemeDefaultConstraint IsPermanent="true" ID="2b23346a-00be-4aaa-a41c-aa19a99f3e73" Name="df_DocumentCommonInfo_StateName" Value="$KrStates_Doc_Draft" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a161e289-2f99-0099-5000-0e3336be8527" Name="pk_DocumentCommonInfo" IsClustered="true">
		<SchemeIndexedColumn Column="a161e289-2f99-0199-4000-0e3336be8527" />
	</SchemePrimaryKey>
	<SchemeIndex ID="20345ec7-e79b-4f48-8371-8b73b8dc120a" Name="ndx_DocumentCommonInfo_Number">
		<SchemeIndexedColumn Column="434a798f-bd1f-4c5c-b291-80490cd0dccf" SortOrder="Descending" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
		<SchemeIncludedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" />
		<SchemeIncludedColumn Column="22ed59ec-4939-0096-4000-0bf0bda55ec0" />
		<SchemeIncludedColumn Column="b74eac03-637d-4a69-a57d-674c8e231967" />
		<SchemeIncludedColumn Column="aa152ba8-dc1f-00fa-4000-03ba804ef6f1" />
		<SchemeIncludedColumn Column="3089d333-8647-00e7-4000-04f8958da086" />
		<SchemeIncludedColumn Column="ed01aef8-43f4-441d-8b0d-1cb249164e9b" />
		<SchemeIncludedColumn Column="1f65d492-78e7-0018-4000-0d7a3c524934" />
		<SchemeIncludedColumn Column="eeb023b5-f473-4a8b-bef2-88e07b0d0688" />
		<SchemeIncludedColumn Column="2246998b-e4f7-45f1-8a96-211848466450" />
		<SchemeIncludedColumn Column="b452a4ce-ae1e-43fd-a44f-cc74464a6cd3" />
	</SchemeIndex>
	<SchemeIndex ID="f3578bb9-ba61-4b0f-aa3d-58c09f49581f" Name="ndx_DocumentCommonInfo_DocTypeIDCardTypeIDNumber">
		<SchemeIndexedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" />
		<SchemeIndexedColumn Column="434a798f-bd1f-4c5c-b291-80490cd0dccf" />
		<SchemeIncludedColumn Column="22ed59ec-4939-0096-4000-0bf0bda55ec0" />
		<SchemeIncludedColumn Column="b74eac03-637d-4a69-a57d-674c8e231967" />
		<SchemeIncludedColumn Column="aa152ba8-dc1f-00fa-4000-03ba804ef6f1" />
		<SchemeIncludedColumn Column="3089d333-8647-00e7-4000-04f8958da086" />
		<SchemeIncludedColumn Column="ed01aef8-43f4-441d-8b0d-1cb249164e9b" />
		<SchemeIncludedColumn Column="eeb023b5-f473-4a8b-bef2-88e07b0d0688" />
		<SchemeIncludedColumn Column="2246998b-e4f7-45f1-8a96-211848466450" />
		<SchemeIncludedColumn Column="b452a4ce-ae1e-43fd-a44f-cc74464a6cd3" />
	</SchemeIndex>
	<SchemeIndex ID="0b684fac-c714-4036-9704-53c3a1c4e526" Name="ndx_DocumentCommonInfo_CardTypeIDNumber">
		<Description>Индекс для сабсета по типу документа.</Description>
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" />
		<SchemeIndexedColumn Column="434a798f-bd1f-4c5c-b291-80490cd0dccf" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="8899d641-05e7-419c-bfb2-c66ed8d8226c" Name="ndx_DocumentCommonInfo_PartnerID">
		<SchemeIndexedColumn Column="22ed59ec-4939-0096-4000-0bf0bda55ec0" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="07616808-26f4-4e45-8afb-3944be0ff0ff" Name="ndx_DocumentCommonInfo_PartnerName">
		<SchemeIndexedColumn Column="b74eac03-637d-4a69-a57d-674c8e231967" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="b9b24b1c-5c69-4fcb-9332-7c1ce89d6c55" Name="ndx_DocumentCommonInfo_AuthorID">
		<SchemeIndexedColumn Column="aa152ba8-dc1f-00fa-4000-03ba804ef6f1" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="f838e516-ac8c-433e-a678-94589621cbef" Name="ndx_DocumentCommonInfo_RegistratorID">
		<SchemeIndexedColumn Column="3089d333-8647-00e7-4000-04f8958da086" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="efdf0197-d4c2-4c49-a18e-a53515a852c2" Name="ndx_DocumentCommonInfo_Amount">
		<SchemeIndexedColumn Column="ed01aef8-43f4-441d-8b0d-1cb249164e9b" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
		<SchemeIncludedColumn Column="1f65d492-78e7-0018-4000-0d7a3c524934" />
	</SchemeIndex>
	<SchemeIndex ID="42123215-3aee-409a-ad11-93119583777e" Name="ndx_DocumentCommonInfo_FullNumber">
		<SchemeIndexedColumn Column="eeb023b5-f473-4a8b-bef2-88e07b0d0688" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="02efa9a1-dbf6-4b04-a63b-d517e619b111" Name="ndx_DocumentCommonInfo_Subject">
		<SchemeIndexedColumn Column="2246998b-e4f7-45f1-8a96-211848466450" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="6abf5ffd-1dfd-4d56-bde4-3a91df154a32" Name="ndx_DocumentCommonInfo_DocDate">
		<SchemeIndexedColumn Column="b452a4ce-ae1e-43fd-a44f-cc74464a6cd3" />
		<SchemeIncludedColumn Column="d713fa35-af02-0020-4000-07f5a04e44fa" />
	</SchemeIndex>
	<SchemeIndex ID="65e4c8a6-ecd2-49ec-992f-e5c71200b18e" Name="ndx_DocumentCommonInfo_DocTypeTitle">
		<SchemeIndexedColumn Column="c8d7a11d-0db3-4e6c-9ce1-85cecf7ee20e" />
		<SchemeIncludedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" />
	</SchemeIndex>
	<SchemeIndex ID="0d0d3510-80c2-4e62-93dc-28be894b4f12" Name="ndx_DocumentCommonInfo_CardTypeIDAuthorID">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="aa152ba8-dc1f-00fa-4000-03ba804ef6f1" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="f5b72977-6752-48bc-bd4c-4112e4ad7a27" Name="ndx_DocumentCommonInfo_CardTypeIDAuthorName">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="71be0eb1-3c16-453d-af8f-133cd9a290db" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="208ef4b3-561c-431b-8c45-6803d11a1cbc" Name="ndx_DocumentCommonInfo_CardTypeIDCreationDate">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="d804eb6c-9f50-4299-b31c-2fef064fae9e" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="d4324e3d-7091-4c0d-b4ab-a9fbc2d6b7ac" Name="ndx_DocumentCommonInfo_CardTypeID">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="e287fb4f-4e4c-4d92-a3c6-5287db70c2a4" Name="ndx_DocumentCommonInfo_CardTypeIDDocDate">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="b452a4ce-ae1e-43fd-a44f-cc74464a6cd3" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="b573a2b5-102a-4651-8048-ccabab174fcb" Name="ndx_DocumentCommonInfo_CardTypeIDDocTypeTitle">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="c8d7a11d-0db3-4e6c-9ce1-85cecf7ee20e" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="4b45506f-f093-49a6-b6f3-223a57eb5436" Name="ndx_DocumentCommonInfo_CardTypeIDFullNumber" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="eeb023b5-f473-4a8b-bef2-88e07b0d0688" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="cb50623e-e073-4d35-883c-ebd1d757bdc7" Name="ndx_DocumentCommonInfo_CardTypeIDPartnerID">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="22ed59ec-4939-0096-4000-0bf0bda55ec0" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="25b2248e-8e29-4b0b-b238-8cba29aeba92" Name="ndx_DocumentCommonInfo_CardTypeIDPartnerName">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="b74eac03-637d-4a69-a57d-674c8e231967" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="1e50f347-4432-4577-aab1-ad143954e7e1" Name="ndx_DocumentCommonInfo_CardTypeIDRegistratorID">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="3089d333-8647-00e7-4000-04f8958da086" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="ec7bc08d-4cd7-4bd3-9fd4-af943c5a5b1b" Name="ndx_DocumentCommonInfo_CardTypeIDRegistratorName">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" SortOrder="Ascending" />
		<SchemeIndexedColumn Column="d750725c-270c-4339-87f0-76be259e1903" SortOrder="Ascending" />
	</SchemeIndex>
	<SchemeIndex ID="3bc2a377-dd8e-410e-b59a-84b8c52a95b3" Name="ndx_DocumentCommonInfo_CardTypeIDSubject" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="99037f7d-c376-00b5-4000-006fb96548d5" />
		<SchemeIndexedColumn Column="2246998b-e4f7-45f1-8a96-211848466450" />
	</SchemeIndex>
	<SchemeIndex ID="9cc803cf-7a4e-4f6a-b0cd-08633e025f73" Name="ndx_DocumentCommonInfo_Barcode">
		<SchemeIndexedColumn Column="f1abff1c-cb65-4f22-9de1-c6ef302b1a60" />
	</SchemeIndex>
	<SchemeIndex ID="540a7bba-1b72-4877-9109-33f0a038b538" Name="ndx_DocumentCommonInfo_ID">
		<SchemeIndexedColumn Column="a161e289-2f99-0199-4000-0e3336be8527" />
		<SchemeIncludedColumn Column="d804eb6c-9f50-4299-b31c-2fef064fae9e" />
	</SchemeIndex>
	<SchemeIndex ID="892c9244-58c8-454a-9caa-fccba872221c" Name="ndx_DocumentCommonInfo_FullNumber_892c9244" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="eeb023b5-f473-4a8b-bef2-88e07b0d0688">
			<Expression Dbms="PostgreSql">lower("FullNumber") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="d6d8dd63-af6d-4bee-95b6-b6d9038fca84" Name="ndx_DocumentCommonInfo_Subject_d6d8dd63" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="2246998b-e4f7-45f1-8a96-211848466450">
			<Expression Dbms="PostgreSql">lower("Subject") gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="9e2bcf0b-46ae-4cb4-b66d-d8c2f42f9c21" Name="ndx_DocumentCommonInfo_RefDocID">
		<Description>Ссылка Ref имеет FK, поэтому нужен индекс для быстрого удаления документов.</Description>
		<Predicate Dbms="SqlServer">[RefDocID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"RefDocID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="adff2ccc-f918-005d-4000-0d7a6fbd2a24" />
	</SchemeIndex>
	<SchemeIndex ID="1e928459-ae87-4ee5-91d8-846dd165517d" Name="ndx_DocumentCommonInfo_ReceiverRowID">
		<Description>Ссылка Receiver имеет FK, поэтому нужен индекс для быстрого удаления получателей в протоколах.</Description>
		<Predicate Dbms="SqlServer">[ReceiverRowID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"ReceiverRowID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="6f6c57be-aee6-0099-4000-07b91002472a" />
	</SchemeIndex>
	<SchemeIndex ID="49022bd6-f73f-457d-b918-cb8cfb136b4a" Name="ndx_DocumentCommonInfo_CategoryID">
		<Description>Ссылка Category имеет FK, поэтому нужен индекс для быстрого удаления категорий.</Description>
		<Predicate Dbms="SqlServer">[CategoryID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"CategoryID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="e4c2fb7d-b232-0054-4000-0e601055c234" />
	</SchemeIndex>
</SchemeTable>