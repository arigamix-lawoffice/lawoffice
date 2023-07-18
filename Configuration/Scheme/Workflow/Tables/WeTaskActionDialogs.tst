<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="7c068441-e9e1-445a-a371-bf9436156428" Name="WeTaskActionDialogs" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с настройками диалогов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c068441-e9e1-005a-2000-0f9436156428" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7c068441-e9e1-015a-4000-0f9436156428" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c068441-e9e1-005a-3100-0f9436156428" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0b8bdde5-75fb-44bd-be8a-fb70b7ce078e" Name="DialogType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0b8bdde5-75fb-00bd-4000-0b70b7ce078e" Name="DialogTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="30fabc2d-1225-48af-975d-481da01e1053" Name="DialogTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="32b4f04b-9949-4241-a8a4-adf1a1e3ccbb" Name="DialogTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ee4eac72-1f92-4266-a7e2-7a88c8b67f99" Name="CardStoreMode" Type="Reference(Typified) Not Null" ReferencedTable="f383bf09-2ec9-4fe5-aa50-f3b14898c976" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ee4eac72-1f92-0066-4000-0a88c8b67f99" Name="CardStoreModeID" Type="Int32 Not Null" ReferencedColumn="c3ebd27e-4fd3-40d9-9bed-13716ba05342" />
		<SchemeReferencingColumn ID="17667869-e0df-456e-bbe1-c33adbab3b28" Name="CardStoreModeName" Type="String(Max) Not Null" ReferencedColumn="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2f2186d7-a675-4655-8e82-7a1cfd35b69b" Name="DialogName" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="a7ea1f8c-7b70-4d78-8f92-26f1a8c7c90d" Name="DialogAlias" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="8509b498-c8ec-4e87-9724-86849f7e629e" Name="SavingScript" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="d8877236-1cc8-4e43-a597-a11ba21b98e3" Name="ActionScript" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="965f2246-bd2d-4e7c-b095-585a0e67bb54" Name="InitScript" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="800c93f2-f43a-4818-9f96-1d9114a0f189" Name="CompletionOption" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="800c93f2-f43a-0018-4000-0d9114a0f189" Name="CompletionOptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="d22c4d2f-34a5-4153-90ed-c42b9b774cfc" Name="CompletionOptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="389628c7-f411-4d38-90bd-a072a1b35706" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="b6e95646-fcf9-4c07-825f-42312a4e5f1e" Name="DisplayValue" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="d93cb777-9e33-4743-8c0e-9a0ab99e05b0" Name="KeepFiles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="339260fb-e200-4d2b-805c-5f773accc7a4" Name="df_WeTaskActionDialogs_KeepFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="3e540811-9e64-41b8-8dfd-8e0bd1eb0a20" Name="Template" Type="Reference(Typified) Null" ReferencedTable="9f15aaf8-032c-4222-9c7c-2cfffeee89ed">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3e540811-9e64-00b8-4000-0e0bd1eb0a20" Name="TemplateID" Type="Guid Null" ReferencedColumn="9f15aaf8-032c-0122-4000-0cfffeee89ed" />
		<SchemeReferencingColumn ID="c2c41dcc-d35f-40e5-adb8-f7a8e411b058" Name="TemplateCaption" Type="String(128) Null" ReferencedColumn="5a28da2d-0d5f-48e1-a626-f9dc69278788" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c068441-e9e1-005a-5000-0f9436156428" Name="pk_WeTaskActionDialogs">
		<SchemeIndexedColumn Column="7c068441-e9e1-005a-3100-0f9436156428" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="7c068441-e9e1-005a-7000-0f9436156428" Name="idx_WeTaskActionDialogs_ID" IsClustered="true">
		<SchemeIndexedColumn Column="7c068441-e9e1-015a-4000-0f9436156428" />
	</SchemeIndex>
</SchemeTable>