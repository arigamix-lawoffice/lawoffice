<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="53a54dce-29d9-4f2c-8522-73ca60a4dbb5" Name="Dialogs" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная таблица для диалогов в системных карточках.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="53a54dce-29d9-002c-2000-03ca60a4dbb5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="53a54dce-29d9-012c-4000-03ca60a4dbb5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="665e3346-cde8-4d0b-a027-0ac42451054f" Name="CardCount" Type="Int32 Not Null">
		<Description>Количество карточек, которые, например, будут созданы по шаблону.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6830a50d-fde3-4dd5-8e14-328205dc1ab5" Name="ChangePartner" Type="Boolean Not Null">
		<Description>Признак того, что в создаваемых карточках контрагент будет изменяться на произвольного контрагента из справочника.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b903a3cd-1d11-4766-b9d2-4ef59d4ae4f5" Name="df_Dialogs_ChangePartner" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb0c2cf4-9893-4d58-8242-32c41cf923f3" Name="ChangeAuthor" Type="Boolean Not Null">
		<Description>Признак того, что в создаваемых карточках автор будет изменяться на произвольного сотрудника из справочника, кроме System.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="56068024-5054-414e-8e74-ae4ac9556aac" Name="df_Dialogs_ChangeAuthor" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0a80212c-aae8-4630-b52f-c02d3e79a28a" Name="Comment" Type="String(Max) Null">
		<Description>Комментарий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b7fd213c-c8fd-4fa8-a849-66057f06360c" Name="OldPassword" Type="String(128) Not Null">
		<Description>Текущий пароль сотрудника, для которого нужно изменить пароль.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9d644ae5-a81e-4259-82ed-eb3da32e961f" Name="Password" Type="String(128) Not Null">
		<Description>Пароль сотрудника, который требуется установить.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6c4e5759-502b-479a-a796-d238578fd506" Name="PasswordRepeat" Type="String(128) Not Null">
		<Description>Повтор пароля сотрудника, который требуется установить.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="48f5f573-779e-4f93-94c1-673251e7d53a" Name="App" Type="Reference(Typified) Not Null" ReferencedTable="610d8253-e293-4676-abcb-e7a0ac1a084d">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="48f5f573-779e-0093-4000-073251e7d53a" Name="AppID" Type="Guid Not Null" ReferencedColumn="610d8253-e293-0176-4000-07a0ac1a084d" />
		<SchemeReferencingColumn ID="d4afa320-7bdc-4eed-915f-3259f9c6cea9" Name="AppName" Type="String(256) Not Null" ReferencedColumn="fd10c072-8630-4b38-b83c-7c9b4bbb280e" />
		<SchemePhysicalColumn ID="a42ae6ca-e134-40e0-9825-60d8549a4d4b" Name="AppVersion" Type="String(128) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="53a54dce-29d9-002c-5000-03ca60a4dbb5" Name="pk_Dialogs" IsClustered="true">
		<SchemeIndexedColumn Column="53a54dce-29d9-012c-4000-03ca60a4dbb5" />
	</SchemePrimaryKey>
</SchemeTable>