<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="e30dcb0a-2a63-4f52-82f9-a12b0038d70d" Name="WeTaskActionOptions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица с вариантами завершения в действии задания</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e30dcb0a-2a63-0052-2000-012b0038d70d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e30dcb0a-2a63-0152-4000-012b0038d70d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e30dcb0a-2a63-0052-3100-012b0038d70d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="916a50cf-cf9a-4c84-9fae-d1746e41a862" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="916a50cf-cf9a-0084-4000-01746e41a862" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="9e1b2334-c376-4558-a587-90ba605d1f5b" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3067f0ce-5274-4438-8a25-fa648f985c78" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3067f0ce-5274-0038-4000-0a648f985c78" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="37d43d6f-98b0-4899-8a9f-22f4d1f68dbe" Name="LinkName" Type="String(128) Not Null" />
		<SchemePhysicalColumn ID="fbf1b7a6-0dd1-4f5e-8875-489188d10396" Name="LinkCaption" Type="String(128) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e6161fa5-2f59-4d96-b36b-f73406935ad4" Name="Script" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="12662c31-8f98-465f-b8d4-809888ebe8a6" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="35fbbd64-431c-4747-a852-c2cda8665da2" Name="Result" Type="String(Max) Null" />
	<SchemeComplexColumn ID="8123349e-2390-4942-895c-82571e8e4798" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8123349e-2390-0042-4000-02571e8e4798" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="7adb3d91-1dd8-437d-b67d-725543e699fc" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="dc0920ae-90bc-41bd-b033-485e2aa26f3a" Name="SendToPerformer" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="dc361003-2c6b-481e-a199-e4bbfb4d7c6d" Name="df_WeTaskActionOptions_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="265243ba-4c62-452c-b519-90cab2b3da49" Name="SendToAuthor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3a71ca22-b5d1-4c30-bebf-bd21a05dd814" Name="df_WeTaskActionOptions_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="af3d53ee-5c6e-4197-9196-52a0b6021393" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="180f823f-4e74-4075-8d41-98eeedc61828" Name="df_WeTaskActionOptions_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5faaff3c-c194-4318-ba0b-a497d93c0dbe" Name="ExcludeSubscribers" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="ef2735e9-abdd-4e76-9f87-f5c86df885e2" Name="NotificationScript" Type="String(Max) Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e30dcb0a-2a63-0052-5000-012b0038d70d" Name="pk_WeTaskActionOptions">
		<SchemeIndexedColumn Column="e30dcb0a-2a63-0052-3100-012b0038d70d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e30dcb0a-2a63-0052-7000-012b0038d70d" Name="idx_WeTaskActionOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e30dcb0a-2a63-0152-4000-012b0038d70d" />
	</SchemeIndex>
</SchemeTable>