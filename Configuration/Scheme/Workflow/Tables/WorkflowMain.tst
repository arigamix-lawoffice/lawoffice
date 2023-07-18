<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="87f7e0c3-2d97-4e36-bb14-1aeec6e67a94" Name="WorkflowMain" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основноя таблица для объектов WorkflowEngine</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="87f7e0c3-2d97-0036-2000-0aeec6e67a94" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="87f7e0c3-2d97-0136-4000-0aeec6e67a94" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="3113bddf-32cc-439a-8cde-4773ee2d35d8" Name="Name" Type="String(128) Not Null">
		<Description>Имя объекта</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c58e9c8e-572d-4e79-8ab1-441bf2e6bdba" Name="Caption" Type="String(128) Not Null">
		<Description>Заголовок объекта</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="14d126ff-42d5-4b3d-b347-585319fab159" Name="PreScript" Type="String(Max) Null">
		<Description>Прескрипт</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="34dd4c4d-29ef-41ce-a599-cd767a3bb831" Name="PostScript" Type="String(Max) Null">
		<Description>Постскрипт</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="583a5edd-cbb6-4dff-b250-4b793d012bc7" Name="Icon" Type="String(128) Null">
		<Description>Ресурс икконки для отображения в узле</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="26800514-22d2-43d2-896f-2b7550c93db7" Name="Group" Type="String(128) Not Null">
		<Description>Группа</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f81d70e4-aaad-4f10-a483-42824c6f0d88" Name="GlobalScript" Type="String(Max) Not Null">
		<Description>Глобальный скрипт процесса</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="926db15a-5a47-473c-9b46-901caf3317af" Name="Description" Type="String(Max) Null">
		<Description>Описание объекта</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e84ff10c-893a-4557-8744-b3cece0331a3" Name="ParentType" Type="Reference(Abstract) Null" WithForeignKey="false">
		<Description>Родительский тип процесса</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e84ff10c-893a-0057-4000-03cece0331a3" Name="ParentTypeID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="7033d05b-02af-4392-8c8f-a03b90fbb1ab" Name="ParentTypeName" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f5d0fb37-7367-4297-8dcb-4f7ed8c76965" Name="LogLevel" Type="Reference(Typified) Not Null" ReferencedTable="9d29f065-3c4b-4209-af8d-10b699895231">
		<Description>Уровень логирования</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f5d0fb37-7367-0097-4000-0f7ed8c76965" Name="LogLevelID" Type="Int32 Not Null" ReferencedColumn="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f" />
		<SchemeReferencingColumn ID="39c066a3-459f-4cff-a8be-0f723bf464b9" Name="LogLevelName" Type="String(128) Not Null" ReferencedColumn="13cfc11d-4b9b-429e-a7d6-0688c814d3fc" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="be51ff48-30e6-4174-aa44-839fb81d7bfb" Name="PreScriptProcessAnySignal" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3d2021e6-3b0f-47f0-8671-3823b621a508" Name="df_WorkflowMain_PreScriptProcessAnySignal" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="27138b40-af63-4aa9-a062-18a214242545" Name="PostScriptProcessAnySignal" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="fdb32c25-3aca-4377-8426-343be457bcbd" Name="df_WorkflowMain_PostScriptProcessAnySignal" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="98e7a1d1-c6e2-45b1-9c4d-93c13ed1e9c9" Name="ProjectPath" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="87f7e0c3-2d97-0036-5000-0aeec6e67a94" Name="pk_WorkflowMain" IsClustered="true">
		<SchemeIndexedColumn Column="87f7e0c3-2d97-0136-4000-0aeec6e67a94" />
	</SchemePrimaryKey>
</SchemeTable>