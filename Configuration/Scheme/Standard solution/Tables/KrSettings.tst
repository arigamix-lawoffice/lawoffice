<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4a8403cf-6979-4e21-ad09-6956d681c405" Name="KrSettings" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="4a8403cf-6979-0021-2000-0956d681c405" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4a8403cf-6979-0121-4000-0956d681c405" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d1277623-0ccb-41be-9a80-7db1b5966b4d" Name="AscendingApprovalList" Type="Boolean Not Null">
		<Description>Признак того, что в листе согласования записи выводятся в прямом порядке. По умолчанию записи выводятся в обратном порядке.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d4a3b027-656e-448b-b143-1fc51d2b9a00" Name="df_KrSettings_AscendingApprovalList" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7b3bb967-06ec-4517-880b-585d4bc72713" Name="NotificationsDefaultLanguage" Type="Reference(Typified) Null" ReferencedTable="1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7b3bb967-06ec-0017-4000-085d4bc72713" Name="NotificationsDefaultLanguageID" Type="Int16 Null" ReferencedColumn="f13de4a3-34d7-4e7b-95b6-f34372ed724c" />
		<SchemeReferencingColumn ID="ab186b70-0c1b-4714-a883-aca482c03c39" Name="NotificationsDefaultLanguageCaption" Type="String(256) Null" ReferencedColumn="40a3d47c-40f7-48bd-ab8e-edef2f84094d" />
		<SchemeReferencingColumn ID="427e20ad-5a6d-4f2d-84db-4c81088e11e1" Name="NotificationsDefaultLanguageCode" Type="AnsiString(3) Null" ReferencedColumn="9e7084bb-c1dc-4ace-90c9-800dbcf7f3c2" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5bbc1cb9-03a0-4eb2-a10e-ad2e398c2649" Name="PermissionsExtensionType" Type="Reference(Typified) Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Ссылка на тип карточки, который может расширять систему безопасности</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5bbc1cb9-03a0-00b2-4000-0d2e398c2649" Name="PermissionsExtensionTypeID" Type="Guid Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="e25c572d-60b8-4cea-9fbd-07fbfb000b67" Name="PermissionsExtensionTypeName" Type="String(128) Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22">
			<Description>Name of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="884f8002-b710-4544-8808-8d3cc46c35c1" Name="PermissionsExtensionTypeCaption" Type="String(128) Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0f3deb0e-c3c6-447d-ad41-fc15fa9b06b2" Name="HideCommentForApprove" Type="Boolean Not Null">
		<Description>Признак того, что поле "Комментарий" для варианта завершения "Согласовать" требуется скрыть. При этом для варианта "Не согласовать" поле остаётся.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a8832f61-3909-436c-802e-bd9694b6d4be" Name="df_KrSettings_HideCommentForApprove" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e2b69faf-cef8-4e9b-ac42-3c745c889807" Name="AllowManualInputAndAutoCreatePartners" Type="Boolean Not Null">
		<Description>Разрешить ручной ввод и автоматическое создание контрагентов</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="348d66a6-73e8-43f4-99b4-f54266e82da1" Name="df_KrSettings_AllowManualInputAndAutoCreatePartners" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="71adce96-4869-4325-9a90-03b329f1ff2b" Name="NotificationsDefaultFormat" Type="Reference(Typified) Null" ReferencedTable="a96047e7-3b08-42bd-8455-1032520a608f" WithForeignKey="false">
		<Description>Внешний ключ отсутствует, т.к. настройки импортируются раньше карточек форматов. Важна только колонка Name.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="71adce96-4869-0025-4000-03b329f1ff2b" Name="NotificationsDefaultFormatID" Type="Guid Null" ReferencedColumn="a96047e7-3b08-01bd-4000-0032520a608f" />
		<SchemeReferencingColumn ID="71bc11a3-9e4a-4f21-b74b-cea45ae9f7f5" Name="NotificationsDefaultFormatName" Type="String(32) Null" ReferencedColumn="38480a7b-400d-476c-8aa2-28be9591d798" />
		<SchemeReferencingColumn ID="dbbdc909-6fbc-4d02-b6e6-96eda09d3c4c" Name="NotificationsDefaultFormatCaption" Type="String(128) Null" ReferencedColumn="adc1e27d-7efd-4af6-a21a-263e5290733f" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7966e30e-03c2-42be-95fa-be8713a282cd" Name="HideLanguageSelection" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3fb81e32-f659-41c5-aa35-57317df45c7b" Name="df_KrSettings_HideLanguageSelection" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b618ce99-fcdd-48f8-a1ab-250eaf6a52b9" Name="HideFormattingSelection" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e75c54d8-9889-40c6-8d20-8618590b80a8" Name="df_KrSettings_HideFormattingSelection" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="04ab0213-8a13-4481-84dc-e6c497e98451" Name="AclReadCardAccess" Type="Boolean Not Null">
		<Description>Флаг указывает, что сотрудники, которые добавлены в ACL, всегда имеют доступ на чтение карточки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a3a410a3-7066-4454-bcfc-099bd17184a9" Name="df_KrSettings_AclReadCardAccess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="4a8403cf-6979-0021-5000-0956d681c405" Name="pk_KrSettings" IsClustered="true">
		<SchemeIndexedColumn Column="4a8403cf-6979-0121-4000-0956d681c405" />
	</SchemePrimaryKey>
</SchemeTable>