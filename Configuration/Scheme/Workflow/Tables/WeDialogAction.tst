<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="5aac25fd-de4f-450d-9fd5-a1a9168a795c" Name="WeDialogAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция действия Диалог</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5aac25fd-de4f-000d-2000-01a9168a795c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5aac25fd-de4f-010d-4000-01a9168a795c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="02c0a518-27b5-48b3-9ae9-68588694c54e" Name="DialogType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="02c0a518-27b5-00b3-4000-08588694c54e" Name="DialogTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="5435f0e6-1244-4495-be8e-d3543b44aed2" Name="DialogTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="9efd5961-3516-4bab-ad79-cd7329098faf" Name="DialogTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="09f78afd-5d59-49b3-9cbb-40ea71002bbe" Name="CardStoreMode" Type="Reference(Typified) Not Null" ReferencedTable="f383bf09-2ec9-4fe5-aa50-f3b14898c976">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="09f78afd-5d59-00b3-4000-00ea71002bbe" Name="CardStoreModeID" Type="Int32 Not Null" ReferencedColumn="c3ebd27e-4fd3-40d9-9bed-13716ba05342" />
		<SchemeReferencingColumn ID="c99da335-3d31-42fb-a053-cb0a9c3ef764" Name="CardStoreModeName" Type="String(Max) Not Null" ReferencedColumn="a0c0f93e-a43c-4949-9216-e3b1f8de1b3a" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e2a5f751-a7c2-430b-b29d-638c5931a30a" Name="ButtonName" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="2be4304d-cffc-4026-8bea-54ee47796161" Name="DialogName" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="7fc32286-1229-4e1d-bb41-d12826372a29" Name="DialogAlias" Type="String(Max) Null" />
	<SchemeComplexColumn ID="7204a5ef-a2ae-49d5-a711-70becbdd12d9" Name="OpenMode" Type="Reference(Typified) Not Null" ReferencedTable="b1827f66-89bd-4269-b2ce-ea27337616fd">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7204a5ef-a2ae-00d5-4000-00becbdd12d9" Name="OpenModeID" Type="Int32 Not Null" ReferencedColumn="fca3e61d-c404-4dd1-8980-e069f10512ac" />
		<SchemeReferencingColumn ID="8b5d4072-d2dc-465d-b237-41e409980708" Name="OpenModeName" Type="String(Max) Not Null" ReferencedColumn="915af02d-d52b-40b6-9d35-34ce36491731" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5d9b37b6-e0dc-4987-8452-416a1328323e" Name="TaskDigest" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="a8b01ad8-2328-4173-86af-c142d1a635c1" Name="SavingScript" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="13502cb8-0957-404a-84ac-690f38a9e064" Name="ActionScript" Type="String(Max) Null" />
	<SchemeComplexColumn ID="ded642b9-9b2a-47c7-8f54-a1b43410a5db" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ded642b9-9b2a-00c7-4000-01b43410a5db" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="777485c5-2fe7-4f72-a915-0ede3edd944b" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b1c564e2-cc91-45ea-854e-dda766cc5807" Name="TaskKind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b1c564e2-cc91-00ea-4000-0da766cc5807" Name="TaskKindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="ce70bcb6-195b-4ed0-ac34-2e7d3a99b2cf" Name="TaskKindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4425ca06-3e2b-4abd-8960-4c80698f63e8" Name="Planned" Type="DateTime Null" />
	<SchemePhysicalColumn ID="51164ea8-5cb4-4c3c-96ee-823d7f9870c5" Name="Period" Type="Double Null" />
	<SchemePhysicalColumn ID="816e74dc-0007-4f0d-b002-abf11fda4101" Name="InitScript" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="9a9a472d-1274-4a55-8c30-1af0908492fa" Name="DisplayValue" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="eebfc6d8-5913-48fd-8608-ec2e8b9ff65b" Name="KeepFiles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="4b52838a-bd78-4e0d-8037-e7fde3c5d784" Name="df_WeDialogAction_KeepFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="433e2bf1-6e05-4fc8-8510-67b78d629b9d" Name="WithoutTask" Type="Boolean Not Null">
		<Description>Определяет, что диалог должен создаваться без задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="19edaf68-dba4-4c95-a9c0-3dea030acfd9" Name="df_WeDialogAction_WithoutTask" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="bb3b5351-77c7-4d9d-b3a4-903d2aef49e8" Name="Template" Type="Reference(Typified) Null" ReferencedTable="9f15aaf8-032c-4222-9c7c-2cfffeee89ed">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bb3b5351-77c7-009d-4000-003d2aef49e8" Name="TemplateID" Type="Guid Null" ReferencedColumn="9f15aaf8-032c-0122-4000-0cfffeee89ed" />
		<SchemeReferencingColumn ID="c8d49338-0f3e-421a-8a7b-2035fad2aebd" Name="TemplateCaption" Type="String(128) Null" ReferencedColumn="5a28da2d-0d5f-48e1-a626-f9dc69278788" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="6352e494-adc0-45a1-b25a-078222d3fff1" Name="IsCloseWithoutConfirmation" Type="Boolean Not Null">
		<Description>Флаг, отключающий предупреждение при закрытии диалога без изменений.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f4f7bad7-4d18-4e60-9be7-45756b70aabf" Name="df_WeDialogAction_IsCloseWithoutConfirmation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5aac25fd-de4f-000d-5000-01a9168a795c" Name="pk_WeDialogAction" IsClustered="true">
		<SchemeIndexedColumn Column="5aac25fd-de4f-010d-4000-01a9168a795c" />
	</SchemePrimaryKey>
</SchemeTable>