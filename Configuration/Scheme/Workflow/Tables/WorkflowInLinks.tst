<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="83bf8e43-0292-4fb8-ac1d-6e36c8ba99a6" Name="WorkflowInLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список входящих в узел связей</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="83bf8e43-0292-00b8-2000-0e36c8ba99a6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="83bf8e43-0292-01b8-4000-0e36c8ba99a6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="83bf8e43-0292-00b8-3100-0e36c8ba99a6" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="6966b491-bd8e-4842-a0b4-65106a3f8f7a" Name="Name" Type="String(128) Not Null">
		<Description>Имя связи</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8ef417fb-94b3-4dae-9195-4346668295dd" Name="Caption" Type="String(128) Not Null">
		<Description>Заголовок связи</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e40356df-dbca-4af5-a8cd-d91b95a68c89" Name="Script" Type="String(Max) Null">
		<Description>Скрипт для перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1e0271f4-6191-4526-b19a-73706be99510" Name="HasCondition" Type="Boolean Not Null">
		<Description>Флаг, определяющий, задано ли условие перехода</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2456a548-8bb0-499a-a579-3f127255a027" Name="Description" Type="String(Max) Not Null">
		<Description>Описание</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9bf4c298-0f5a-43dd-8378-2fb55f506125" Name="IsAsync" Type="Boolean Not Null">
		<Description>Флаг определяет, является ли вызов данного перехода асинхронным</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ac1e00ac-b2ae-4eca-b7bd-79a415f86107" Name="LockProcess" Type="Boolean Not Null">
		<Description>Флаг определяет, должен ли процесс блокироваться при выполнении асинхронной операции</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="41fbddf3-b4ce-4a46-93c8-f94f3727e756" Name="df_WorkflowInLinks_LockProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="ccdbb00e-34f5-4b12-854e-9bab1c2e007a" Name="LinkMode" Type="Reference(Typified) Not Null" ReferencedTable="29b2fb61-6880-43de-a40f-6688e1d0e247">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ccdbb00e-34f5-0012-4000-0bab1c2e007a" Name="LinkModeID" Type="Int32 Not Null" ReferencedColumn="1aca9753-e67a-4044-bc5d-656ad20fcc98" />
		<SchemeReferencingColumn ID="f79c948b-1b66-4590-8dc3-720f7db3577f" Name="LinkModeName" Type="String(Max) Not Null" ReferencedColumn="43e1fa98-b92a-40ef-9c5a-43b7332d575f" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="872cefbf-7f2b-4fe8-97e7-0ba152ff4b7a" Name="SignalProcessingMode" Type="Reference(Typified) Not Null" ReferencedTable="67b602c1-ea47-4716-92ba-81f625ba36f1">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="872cefbf-7f2b-00e8-4000-0ba152ff4b7a" Name="SignalProcessingModeID" Type="Int32 Not Null" ReferencedColumn="03a94b31-a856-4bb3-a570-ba6ab6772730" />
		<SchemeReferencingColumn ID="8d0e37ae-ae59-47e0-9a19-761e4e5dedbb" Name="SignalProcessingModeName" Type="String(Max) Not Null" ReferencedColumn="7252edda-77c8-4807-82da-f01e75711c68" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="83bf8e43-0292-00b8-5000-0e36c8ba99a6" Name="pk_WorkflowInLinks">
		<SchemeIndexedColumn Column="83bf8e43-0292-00b8-3100-0e36c8ba99a6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="83bf8e43-0292-00b8-7000-0e36c8ba99a6" Name="idx_WorkflowInLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="83bf8e43-0292-01b8-4000-0e36c8ba99a6" />
	</SchemeIndex>
</SchemeTable>