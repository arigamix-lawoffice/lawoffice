<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="f7ebd016-ef99-4dfd-ba04-11f428395fe3" Name="WorkflowProcessErrorsVirtual" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7ebd016-ef99-00fd-2000-01f428395fe3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f7ebd016-ef99-01fd-4000-01f428395fe3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7ebd016-ef99-00fd-3100-01f428395fe3" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="4b9f5bf2-41e9-4f56-8b44-d1eaf9bd7e33" Name="Added" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="12658c04-db7d-4e48-9b1f-3ab620e9ef65" Name="Text" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="794ddbf3-bf28-4e81-bf3f-2ceb092b12ea" Name="NodeInstanceID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="4848c90d-b42f-40e3-a57f-f06816ede3ff" Name="IsAsync" Type="Boolean Not Null">
		<Description>Флаг определяет, возникла ли ошибка при асинхронной обработке</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9e670e71-2066-4705-be5a-c6f203744e39" Name="Resumable" Type="Boolean Not Null">
		<Description>Флаг определяет, что процесс по данной ошибке можно возобновить</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b0984d35-ca3a-45e0-8986-5052f81abf68" Name="NodeID" Type="Guid Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7ebd016-ef99-00fd-5000-01f428395fe3" Name="pk_WorkflowProcessErrorsVirtual">
		<SchemeIndexedColumn Column="f7ebd016-ef99-00fd-3100-01f428395fe3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7ebd016-ef99-00fd-7000-01f428395fe3" Name="idx_WorkflowProcessErrorsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f7ebd016-ef99-01fd-4000-01f428395fe3" />
	</SchemeIndex>
</SchemeTable>