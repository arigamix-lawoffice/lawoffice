<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="46f88520-33d0-45c9-bd27-20cae8fa58dc" Name="WeScriptAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Скрипт</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="46f88520-33d0-00c9-2000-00cae8fa58dc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="46f88520-33d0-01c9-4000-00cae8fa58dc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="46b9c2d2-8d82-459d-a167-97c0109ab743" Name="Script" Type="String(Max) Not Null">
		<Description>Текст скрипта</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5480e91d-8b02-4842-aedb-830acf728eca" Name="ProcessAnySignal" Type="Boolean Not Null">
		<Description>Флаг определяет, должен ли скрипт выполняться только по дефолтному сигналу, или по любому.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="46f88520-33d0-00c9-5000-00cae8fa58dc" Name="pk_WeScriptAction" IsClustered="true">
		<SchemeIndexedColumn Column="46f88520-33d0-01c9-4000-00cae8fa58dc" />
	</SchemePrimaryKey>
</SchemeTable>