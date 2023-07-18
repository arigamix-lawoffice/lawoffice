<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="3d947708-6196-443f-a4e3-a1e1a5315d9d" Name="WeSubprocessAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция с данными действия Подпроцесс</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d947708-6196-003f-2000-01e1a5315d9d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3d947708-6196-013f-4000-01e1a5315d9d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c1aae0be-49b0-4f36-8b57-27887b26baf9" Name="StartSignal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c1aae0be-49b0-0036-4000-07887b26baf9" Name="StartSignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="4f6e6f89-92dd-44dc-abf4-ea701796295b" Name="StartSignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5738e7d1-2a25-493a-89ca-09894b29eb47" Name="Process" Type="Reference(Typified) Not Null" ReferencedTable="5640ffb9-ef7c-4584-8793-57da90e82fa0">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5738e7d1-2a25-003a-4000-09894b29eb47" Name="ProcessID" Type="Guid Not Null" ReferencedColumn="5640ffb9-ef7c-0184-4000-07da90e82fa0" />
		<SchemeReferencingColumn ID="34e42c24-f861-4b3b-9620-b546aa460bcb" Name="ProcessName" Type="String(128) Not Null" ReferencedColumn="08d8a253-0ba9-416b-943b-1699364c7d53" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3d947708-6196-003f-5000-01e1a5315d9d" Name="pk_WeSubprocessAction" IsClustered="true">
		<SchemeIndexedColumn Column="3d947708-6196-013f-4000-01e1a5315d9d" />
	</SchemePrimaryKey>
</SchemeTable>