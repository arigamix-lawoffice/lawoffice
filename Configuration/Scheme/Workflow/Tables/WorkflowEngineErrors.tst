<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="61905471-1e69-4478-946f-772f11152386" Name="WorkflowEngineErrors" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с ошибками обработки</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="61905471-1e69-0078-2000-072f11152386" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="61905471-1e69-0178-4000-072f11152386" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="61905471-1e69-0078-3100-072f11152386" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4aab6b4f-87b9-47d8-8502-31b5aa33e139" Name="ErrorCard" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4aab6b4f-87b9-00d8-4000-01b5aa33e139" Name="ErrorCardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2480105e-78a8-479b-aff7-c37b1cd3f213" Name="Node" Type="Reference(Typified) Not Null" ReferencedTable="69f72d3a-97c1-4d67-a348-071ab861b3c7" WithForeignKey="false">
		<Description>ID экземпляра узла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2480105e-78a8-009b-4000-037b1cd3f213" Name="NodeRowID" Type="Guid Not Null" ReferencedColumn="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d48c533f-edc2-4439-b6ff-eb040e205d3d" Name="Active" Type="Boolean Not Null">
		<Description>Флаг определяет, является ли данная ошибка активной</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7a0ebf5d-d0be-4eab-addd-f9948a7597a2" Name="df_WorkflowEngineErrors_Active" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5b40b728-c184-4583-a303-255bace43d42" Name="ErrorData" Type="BinaryJson Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="313bbba6-13c0-4b4d-867b-9cf459295127" Name="df_WorkflowEngineErrors_ErrorData" Value="{}" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e8468155-bdf2-4d5b-9ef7-447a20cf905e" Name="Added" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="4ee49b32-cc06-4504-af86-e428147f3ae7" Name="IsAsync" Type="Boolean Not Null">
		<Description>Флаг определяет, возникла ли ошибка при асинхронной обработке</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6e5d5a4c-d7a4-4571-b7d6-e083387a148d" Name="df_WorkflowEngineErrors_IsAsync" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b9a43912-7342-43c5-951d-2da99f62f7fe" Name="Resumable" Type="Boolean Not Null">
		<Description>Флаг определяет, процесс по данной ошибке можно возобновить</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0237f836-85d4-4121-a56f-6c3187960c22" Name="df_WorkflowEngineErrors_Resumable" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="61905471-1e69-0078-5000-072f11152386" Name="pk_WorkflowEngineErrors">
		<SchemeIndexedColumn Column="61905471-1e69-0078-3100-072f11152386" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="61905471-1e69-0078-7000-072f11152386" Name="idx_WorkflowEngineErrors_ID" IsClustered="true">
		<SchemeIndexedColumn Column="61905471-1e69-0178-4000-072f11152386" />
	</SchemeIndex>
	<SchemeIndex ID="3cc275fa-d5b9-4820-ad70-c84a75e57e4a" Name="ndx_WorkflowEngineErrors_NodeRowIDActiveResumable">
		<SchemeIndexedColumn Column="2480105e-78a8-009b-4000-037b1cd3f213" />
		<SchemeIndexedColumn Column="d48c533f-edc2-4439-b6ff-eb040e205d3d" />
		<SchemeIndexedColumn Column="b9a43912-7342-43c5-951d-2da99f62f7fe" />
	</SchemeIndex>
	<SchemeIndex ID="c582b2b9-e6ae-4147-9ec8-65ff25200c9c" Name="ndx_WorkflowEngineErrors_Added">
		<SchemeIndexedColumn Column="e8468155-bdf2-4d5b-9ef7-447a20cf905e" />
	</SchemeIndex>
	<SchemeIndex ID="a7ff7929-7f72-4da2-a1a9-f1ac293e0b0c" Name="ndx_WorkflowEngineErrors_ErrorCardID">
		<SchemeIndexedColumn Column="4aab6b4f-87b9-00d8-4000-01b5aa33e139" />
	</SchemeIndex>
</SchemeTable>