<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="428f3b30-561c-446e-b676-4ec84ba8e03a" Name="WeSubprocessActionOptions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с настройками переходов при получении сигналов из под-процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="428f3b30-561c-006e-2000-0ec84ba8e03a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="428f3b30-561c-016e-4000-0ec84ba8e03a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="428f3b30-561c-006e-3100-0ec84ba8e03a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="9cb25ccb-51cb-4712-8569-fc57aa37ff3e" Name="Signal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<Description>Сигнал</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9cb25ccb-51cb-0012-4000-0c57aa37ff3e" Name="SignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="b21bcdf0-82f8-45ad-b88f-ce6bbc2f799f" Name="SignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0897e5b8-9adb-4609-bb1f-e342fd431dda" Name="Link" Type="Reference(Abstract) Not Null">
		<Description>Переход</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0897e5b8-9adb-0009-4000-0342fd431dda" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="19690d23-1974-434a-9051-42083127589f" Name="LinkName" Type="String(128) Not Null" />
		<SchemePhysicalColumn ID="a3cb6e0b-b89a-4f69-a325-88b709d0fcf3" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1f85291d-f9ff-4427-a52a-81e188eb021b" Name="Script" Type="String(Max) Not Null">
		<Description>Сценарий, выполняющийся после выполнения перихода</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="428f3b30-561c-006e-5000-0ec84ba8e03a" Name="pk_WeSubprocessActionOptions">
		<SchemeIndexedColumn Column="428f3b30-561c-006e-3100-0ec84ba8e03a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="428f3b30-561c-006e-7000-0ec84ba8e03a" Name="idx_WeSubprocessActionOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="428f3b30-561c-016e-4000-0ec84ba8e03a" />
	</SchemeIndex>
</SchemeTable>