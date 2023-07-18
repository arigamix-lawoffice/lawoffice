<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="fbe60ad7-091a-4f09-a57a-f1068088fa38" Name="WeSendSignalAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для действия Отправка сигнала</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fbe60ad7-091a-0009-2000-01068088fa38" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fbe60ad7-091a-0109-4000-01068088fa38" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="533b8d8a-6fd6-4a2c-ada2-87eae62a59fb" Name="Signal" Type="Reference(Typified) Null" ReferencedTable="53dc8c0b-391a-4fbd-86c0-3da697abf065">
		<Description>Тип отправляемого сигнала</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="533b8d8a-6fd6-002c-4000-07eae62a59fb" Name="SignalID" Type="Guid Null" ReferencedColumn="cabbc72d-b093-43be-a645-8503664980d6" />
		<SchemeReferencingColumn ID="e14a5850-ef8e-4676-8374-613ddd585f2a" Name="SignalName" Type="String(128) Null" ReferencedColumn="2e7c413d-0de6-4900-ac97-68ce16e3da11" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="78c19a30-2f79-408c-9f30-ce74140a3d58" Name="PassHash" Type="Boolean Not Null">
		<Description>Определяет, нужно ли передавать параметры из текущего сигнала в отправляемый</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7134d17b-cd6f-4abb-b534-a7d7f22be71e" Name="df_WeSendSignalAction_PassHash" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb249847-16e5-4154-90b3-34e9666e5842" Name="Scenario" Type="String(Max) Null">
		<Description>Сценарий, выполняемый при инициализации сигнала</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fbe60ad7-091a-0009-5000-01068088fa38" Name="pk_WeSendSignalAction" IsClustered="true">
		<SchemeIndexedColumn Column="fbe60ad7-091a-0109-4000-01068088fa38" />
	</SchemePrimaryKey>
</SchemeTable>