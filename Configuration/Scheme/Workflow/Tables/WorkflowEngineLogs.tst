<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="f3fa0390-6444-4df6-8be5-cbad1fdd153e" Name="WorkflowEngineLogs" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с логами процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f3fa0390-6444-00f6-2000-0bad1fdd153e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f3fa0390-6444-01f6-4000-0bad1fdd153e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f3fa0390-6444-00f6-3100-0bad1fdd153e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1a6bc6d6-4b6e-4a9c-a28a-7859e3055e49" Name="Process" Type="Reference(Typified) Not Null" ReferencedTable="27debe30-ae5f-4f69-89c9-5706e1592540" WithForeignKey="false">
		<Description>ID процесса</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1a6bc6d6-4b6e-009c-4000-0859e3055e49" Name="ProcessRowID" Type="Guid Not Null" ReferencedColumn="27debe30-ae5f-0069-3100-0706e1592540" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9219195c-98e9-4048-8f8f-1574847f384d" Name="LogLevel" Type="Reference(Typified) Not Null" ReferencedTable="9d29f065-3c4b-4209-af8d-10b699895231">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9219195c-98e9-0048-4000-0574847f384d" Name="LogLevelID" Type="Int32 Not Null" ReferencedColumn="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="cae35123-33bf-4fb9-8b26-f4fbeb3c673b" Name="ObjectName" Type="String(Max) Not Null">
		<Description>Имя объекта, отправившего лог</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f3798db7-01ef-44f5-b3ca-63b93dcbf3c4" Name="Text" Type="String(Max) Not Null">
		<Description>Текст лога</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0955dc95-f8f0-4664-ad56-771841f69980" Name="Added" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="4ff174a3-d622-4724-afec-d43333c8e27b" Name="Order" Type="Int64 Not Null" IsIdentity="true" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f3fa0390-6444-00f6-5000-0bad1fdd153e" Name="pk_WorkflowEngineLogs">
		<SchemeIndexedColumn Column="f3fa0390-6444-00f6-3100-0bad1fdd153e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f3fa0390-6444-00f6-7000-0bad1fdd153e" Name="idx_WorkflowEngineLogs_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f3fa0390-6444-01f6-4000-0bad1fdd153e" />
	</SchemeIndex>
	<SchemeIndex ID="cbb2cf2b-7bbb-4c31-b52a-b1737ada70bc" Name="ndx_WorkflowEngineLogs_Added">
		<SchemeIndexedColumn Column="0955dc95-f8f0-4664-ad56-771841f69980" />
	</SchemeIndex>
	<SchemeIndex ID="a5edfd52-cb52-4540-babf-74b041be2113" Name="ndx_WorkflowEngineLogs_ProcessRowIDOrder">
		<SchemeIndexedColumn Column="1a6bc6d6-4b6e-009c-4000-0859e3055e49" />
		<SchemeIndexedColumn Column="4ff174a3-d622-4724-afec-d43333c8e27b" />
	</SchemeIndex>
</SchemeTable>