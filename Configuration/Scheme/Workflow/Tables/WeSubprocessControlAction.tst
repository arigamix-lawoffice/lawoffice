<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="2f0a4a5d-6601-4cd9-9c2d-09d193d33352" Name="WeSubprocessControlAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Управление подпроцессом</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2f0a4a5d-6601-00d9-2000-09d193d33352" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2f0a4a5d-6601-01d9-4000-09d193d33352" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c21fb288-e09e-4a5c-a8ab-77065749fb8d" Name="Signal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<Description>Сигнал управления</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c21fb288-e09e-005c-4000-07065749fb8d" Name="SignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="da1bd240-4abe-4b1d-aed6-209e72b2855f" Name="SignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2f0a4a5d-6601-00d9-5000-09d193d33352" Name="pk_WeSubprocessControlAction" IsClustered="true">
		<SchemeIndexedColumn Column="2f0a4a5d-6601-01d9-4000-09d193d33352" />
	</SchemePrimaryKey>
</SchemeTable>