<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="27debe30-ae5f-4f69-89c9-5706e1592540" Name="WorkflowEngineProcesses" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Содержит состояния активных процессов</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="27debe30-ae5f-0069-2000-0706e1592540" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="27debe30-ae5f-0169-4000-0706e1592540" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="27debe30-ae5f-0069-3100-0706e1592540" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="05f039be-82e9-43b0-a058-b65c0f3bbce0" Name="ProcessData" Type="BinaryJson Not Null">
		<Description>Состояние экземпляра процесса</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ddff2287-4736-419f-b0fc-05e3930b7d43" Name="df_WorkflowEngineProcesses_ProcessData" Value="{}" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="af8d90d3-c57f-48b3-8a89-b3afedee9479" Name="ProcessTemplate" Type="Reference(Typified) Not Null" ReferencedTable="dcd38c54-ed18-4503-b435-3dee1c6c2c62" WithForeignKey="false">
		<Description>Ссылка на версию шаблона бизнес-процесса</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="af8d90d3-c57f-00b3-4000-03afedee9479" Name="ProcessTemplateRowID" Type="Guid Not Null" ReferencedColumn="dcd38c54-ed18-0003-3100-0dee1c6c2c62" />
		<SchemeReferencingColumn ID="ed21168b-f6d0-4009-983c-8141dbcb7867" Name="ProcessTemplateID" Type="Guid Not Null" ReferencedColumn="dcd38c54-ed18-0103-4000-0dee1c6c2c62">
			<SchemeDefaultConstraint IsPermanent="true" ID="cb7c4291-8ae9-4fb3-ab32-9e0b4e467133" Name="df_WorkflowEngineProcesses_ProcessTemplateID" Value="00000000-0000-0000-0000-000000000000" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8eca65dd-07e2-442d-a034-5f9cc311360e" Name="Card" Type="Reference(Abstract) Null" WithForeignKey="false">
		<Description>Идентификатор карточки, для которой выполняется данный процесс.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8eca65dd-07e2-002d-4000-0f9cc311360e" Name="CardID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="5745d573-5015-4b1a-a0a6-2846cd788844" Name="CardDigest" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="07baa873-59c5-471e-b3e0-5bbe182f5054" Name="Created" Type="DateTime Not Null">
		<Description>Дата создания процесса</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4ffa80f8-6cbb-42e2-baad-389af0ee78fa" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="27debe30-ae5f-4f69-89c9-5706e1592540" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4ffa80f8-6cbb-00e2-4000-089af0ee78fa" Name="ParentRowID" Type="Guid Null" ReferencedColumn="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="0698dd4b-97fe-4ecc-b5cc-d8fd9e640f1c" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="d1b748ac-8a7a-460a-9272-e470b120039d" Name="LastActivity" Type="DateTime Not Null">
		<Description>Дата последней активности процесса.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b717c095-166a-4318-af5e-10c6cb9c9bd8" Name="df_WorkflowEngineProcesses_LastActivity" Value="1900-01-01T00:00:00Z" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="27debe30-ae5f-0069-5000-0706e1592540" Name="pk_WorkflowEngineProcesses">
		<SchemeIndexedColumn Column="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="27debe30-ae5f-0069-7000-0706e1592540" Name="idx_WorkflowEngineProcesses_ID" IsClustered="true">
		<SchemeIndexedColumn Column="27debe30-ae5f-0169-4000-0706e1592540" />
	</SchemeIndex>
	<SchemeIndex ID="687846bc-e3e4-422c-b941-8a48233e208b" Name="ndx_WorkflowEngineProcesses_ProcessTemplateRowIDCardID">
		<SchemeIndexedColumn Column="af8d90d3-c57f-00b3-4000-03afedee9479" />
		<SchemeIndexedColumn Column="8eca65dd-07e2-002d-4000-0f9cc311360e" />
		<SchemeIncludedColumn Column="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeIndex>
	<SchemeIndex ID="5a176e1c-933b-4d4d-870d-e6057f5b8bf6" Name="ndx_WorkflowEngineProcesses_Created">
		<SchemeIndexedColumn Column="07baa873-59c5-471e-b3e0-5bbe182f5054" />
	</SchemeIndex>
	<SchemeIndex ID="9d7cae30-b45b-44b1-b418-bcbb1218b705" Name="ndx_WorkflowEngineProcesses_CardID">
		<SchemeIndexedColumn Column="8eca65dd-07e2-002d-4000-0f9cc311360e" />
	</SchemeIndex>
	<SchemeIndex ID="47c9fe5c-909f-452f-9cf7-e0afb025bc3b" Name="ndx_WorkflowEngineProcesses_LastActivity">
		<SchemeIndexedColumn Column="d1b748ac-8a7a-460a-9272-e470b120039d" />
	</SchemeIndex>
	<SchemeIndex ID="a9275eba-9218-446c-9d5f-7b38b48b5b17" Name="ndx_WorkflowEngineProcesses_ID" SupportsPostgreSql="false">
		<FillFactor Dbms="SqlServer">80</FillFactor>
		<SchemeIndexedColumn Column="27debe30-ae5f-0169-4000-0706e1592540" />
	</SchemeIndex>
</SchemeTable>