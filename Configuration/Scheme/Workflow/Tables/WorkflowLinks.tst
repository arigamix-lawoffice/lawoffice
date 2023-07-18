<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="9764baef-636c-4558-86cb-0b7e4360f771" Name="WorkflowLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция с параметрами перехода</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9764baef-636c-0058-2000-0b7e4360f771" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9764baef-636c-0158-4000-0b7e4360f771" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="34b5e333-df7e-42e4-8ecf-be1984446b61" Name="InScript" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="edcd9d99-3d8b-414a-a82a-731a3e103432" Name="OutScript" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="a9acea90-f004-4f6d-91fb-cb79d22162b4" Name="InDescription" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="8346fcbd-7841-4427-94a8-549ff2e624b6" Name="OutDescription" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="f2778a11-3468-47e8-8216-a3aba8aadc53" Name="IsAsync" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="002f7fab-cd1f-4ff0-ab1e-fe9053879aa1" Name="LockProcess" Type="Boolean Not Null">
		<Description>Флаг определяет, должен ли процесс блокироваться при выполнении асинхронной операции</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1ac00e80-83b8-4cdb-8628-47913cd656c1" Name="df_WorkflowLinks_LockProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="082c6f27-4e63-4fdb-912d-4b3b5fe0f6f0" Name="LinkMode" Type="Reference(Typified) Not Null" ReferencedTable="29b2fb61-6880-43de-a40f-6688e1d0e247">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="082c6f27-4e63-00db-4000-0b3b5fe0f6f0" Name="LinkModeID" Type="Int32 Not Null" ReferencedColumn="1aca9753-e67a-4044-bc5d-656ad20fcc98">
			<SchemeDefaultConstraint IsPermanent="true" ID="e0e731ee-c74f-4999-a629-262f22cd65ec" Name="df_WorkflowLinks_LinkModeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="e8977f37-7d6b-4aad-9a10-2b1b82a5dbb3" Name="LinkModeName" Type="String(Max) Not Null" ReferencedColumn="43e1fa98-b92a-40ef-9c5a-43b7332d575f">
			<SchemeDefaultConstraint IsPermanent="true" ID="4dfc4d32-7b7e-460a-a829-e064bd9b912e" Name="df_WorkflowLinks_LinkModeName" Value="Создать экземпляр узла, если его нет" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2fdab66d-11df-44c1-9a68-0e410d21d108" Name="SignalProcessingMode" Type="Reference(Typified) Not Null" ReferencedTable="67b602c1-ea47-4716-92ba-81f625ba36f1">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2fdab66d-11df-00c1-4000-0e410d21d108" Name="SignalProcessingModeID" Type="Int32 Not Null" ReferencedColumn="03a94b31-a856-4bb3-a570-ba6ab6772730">
			<SchemeDefaultConstraint IsPermanent="true" ID="fd844227-3023-42ce-912b-256c6f427c65" Name="df_WorkflowLinks_SignalProcessingModeID" Value="0" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="7ec0fec2-8f2c-4668-b361-3d660b8cc9f8" Name="SignalProcessingModeName" Type="String(Max) Not Null" ReferencedColumn="7252edda-77c8-4807-82da-f01e75711c68">
			<SchemeDefaultConstraint IsPermanent="true" ID="4247a04c-9292-408e-84c3-cc4a965f66cd" Name="df_WorkflowLinks_SignalProcessingModeName" Value="$WorkflowEngine_SignalProcessingMode_Default" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9764baef-636c-0058-5000-0b7e4360f771" Name="pk_WorkflowLinks" IsClustered="true">
		<SchemeIndexedColumn Column="9764baef-636c-0158-4000-0b7e4360f771" />
	</SchemePrimaryKey>
</SchemeTable>