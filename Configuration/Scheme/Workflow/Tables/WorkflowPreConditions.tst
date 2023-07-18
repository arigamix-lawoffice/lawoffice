<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="1290310e-0b81-4560-8996-71f5bcb3a9a3" Name="WorkflowPreConditions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список обрабатываемых типов событий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1290310e-0b81-0060-2000-01f5bcb3a9a3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1290310e-0b81-0160-4000-01f5bcb3a9a3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1290310e-0b81-0060-3100-01f5bcb3a9a3" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0b6d1147-7489-4881-bce6-9e48c20a0c9f" Name="Signal" Type="Reference(Typified) Not Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0b6d1147-7489-0081-4000-0e48c20a0c9f" Name="SignalID" Type="Guid Not Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="58ef1aba-77c0-4965-ae29-0f3b366d4970" Name="SignalName" Type="String(128) Not Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1290310e-0b81-0060-5000-01f5bcb3a9a3" Name="pk_WorkflowPreConditions">
		<SchemeIndexedColumn Column="1290310e-0b81-0060-3100-01f5bcb3a9a3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="1290310e-0b81-0060-7000-01f5bcb3a9a3" Name="idx_WorkflowPreConditions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="1290310e-0b81-0160-4000-01f5bcb3a9a3" />
	</SchemeIndex>
</SchemeTable>