<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="e36e23ae-2276-494a-a3f1-5f3cd5c56f9d" Name="WeEndAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Конец процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e36e23ae-2276-004a-2000-0f3cd5c56f9d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e36e23ae-2276-014a-4000-0f3cd5c56f9d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d1e55255-5d16-4faf-a885-025f5e86e82c" Name="FinishProcess" Type="Boolean Not Null">
		<Description>Определяет, должен ли данный процесс завершить свое выполнение</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7ba32853-f200-4770-b4c4-5ed21f60550b" Name="df_WeEndAction_FinishProcess" Value="true" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e9740799-bae4-4023-82f0-4f6dcfccc23a" Name="EndSignal" Type="Reference(Typified) Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e9740799-bae4-0023-4000-0f6dcfccc23a" Name="EndSignalID" Type="Guid Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="e25893a4-d211-4c0d-9710-2ba7f9ba951a" Name="EndSignalName" Type="String(128) Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e36e23ae-2276-004a-5000-0f3cd5c56f9d" Name="pk_WeEndAction" IsClustered="true">
		<SchemeIndexedColumn Column="e36e23ae-2276-014a-4000-0f3cd5c56f9d" />
	</SchemePrimaryKey>
</SchemeTable>