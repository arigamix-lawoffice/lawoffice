<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="38c4dd25-e26e-469c-8072-6498f33a0d06" Name="WeTimerControlAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Управление таймером</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="38c4dd25-e26e-009c-2000-0498f33a0d06" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="38c4dd25-e26e-019c-4000-0498f33a0d06" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d9247e51-daf7-4ae8-bc84-130e38577a2d" Name="Period" Type="Int32 Null" />
	<SchemePhysicalColumn ID="3709ed40-83dc-4535-8efb-3f02acec6ca9" Name="Cron" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="23fb5876-93bb-4687-a05e-950796c2dcbb" Name="Stop" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="8fe01786-c28e-402b-8aa2-e78f839adead" Name="df_WeTimerControlAction_Stop" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="424a6bd8-ae81-4c10-b955-c9dede4a1f35" Name="Date" Type="DateTime Null">
		<Description>Конкретная дата запуска таймера</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="38c4dd25-e26e-009c-5000-0498f33a0d06" Name="pk_WeTimerControlAction" IsClustered="true">
		<SchemeIndexedColumn Column="38c4dd25-e26e-019c-4000-0498f33a0d06" />
	</SchemePrimaryKey>
</SchemeTable>