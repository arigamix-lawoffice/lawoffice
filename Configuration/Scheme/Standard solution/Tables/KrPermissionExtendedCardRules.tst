<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="24c7c7fa-0c39-44c5-aa8d-0199ab79606e" Name="KrPermissionExtendedCardRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с расширенными настройками доступа к карточке</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="24c7c7fa-0c39-00c5-2000-0199ab79606e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="24c7c7fa-0c39-01c5-4000-0199ab79606e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="24c7c7fa-0c39-00c5-3100-0199ab79606e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="037de00a-b2bd-4e47-93f0-a6b705353f86" Name="Section" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Секция, к которой применяется правило</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="037de00a-b2bd-0047-4000-06b705353f86" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="d9831498-e0c6-41bb-9fe6-35d04fc23c7f" Name="SectionName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="0d911b97-4386-4650-8340-1df61e2eee6d" Name="SectionTypeID" Type="Int32 Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="299529d7-31e4-4196-862a-7afd73086615" Name="AccessSetting" Type="Reference(Typified) Null" ReferencedTable="4c274eda-ab9a-403f-9e5b-0b933283b5a3">
		<Description>Настройка доступа</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="299529d7-31e4-0096-4000-0afd73086615" Name="AccessSettingID" Type="Int32 Null" ReferencedColumn="0f1e3ccd-ef3b-4c4a-b3be-dbf802f9278c" />
		<SchemeReferencingColumn ID="7e5f4a56-52ef-4cba-a8c0-2372801e8764" Name="AccessSettingName" Type="String(128) Null" ReferencedColumn="daa3bd22-b7ad-469f-8ffb-41afa4fa2e58" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e2cb39c7-633b-42d9-b0c0-80bb7ea6fc1c" Name="IsHidden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9e3721b2-24d9-42bd-9bd7-0677435479f6" Name="df_KrPermissionExtendedCardRules_IsHidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8f20a444-c162-4900-95f8-677788a68bbf" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="d89b9639-ad9b-4bf1-8734-51241e668d45" Name="Mask" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="24c7c7fa-0c39-00c5-5000-0199ab79606e" Name="pk_KrPermissionExtendedCardRules">
		<SchemeIndexedColumn Column="24c7c7fa-0c39-00c5-3100-0199ab79606e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="24c7c7fa-0c39-00c5-7000-0199ab79606e" Name="idx_KrPermissionExtendedCardRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="24c7c7fa-0c39-01c5-4000-0199ab79606e" />
	</SchemeIndex>
</SchemeTable>