<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="797022e2-bac4-408c-b529-110943fade63" Name="WeTaskActionEvents" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с обработчиками событий заданий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="797022e2-bac4-008c-2000-010943fade63" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="797022e2-bac4-018c-4000-010943fade63" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="797022e2-bac4-008c-3100-010943fade63" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a88e6d25-ddcc-4b2c-bf5a-795011de2c65" Name="Script" Type="String(Max) Null">
		<Description>Скрипт обработки</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8a822b31-9e26-400c-b47e-8dc7d6958620" Name="Event" Type="Reference(Typified) Not Null" ReferencedTable="857ef2b9-6bdb-4913-bbc2-2cf9d1ae0b55">
		<Description>Обрабатываемое событие</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a822b31-9e26-000c-4000-0dc7d6958620" Name="EventID" Type="Int32 Not Null" ReferencedColumn="3b7d3241-404a-47ff-b077-9309ca66ab85" />
		<SchemeReferencingColumn ID="2a30edf0-1be3-4b88-9670-061813a72db4" Name="EventName" Type="String(128) Not Null" ReferencedColumn="2e22bdfe-2f25-4f96-98a6-a71b02ff8808" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="797022e2-bac4-008c-5000-010943fade63" Name="pk_WeTaskActionEvents">
		<SchemeIndexedColumn Column="797022e2-bac4-008c-3100-010943fade63" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="797022e2-bac4-008c-7000-010943fade63" Name="idx_WeTaskActionEvents_ID" IsClustered="true">
		<SchemeIndexedColumn Column="797022e2-bac4-018c-4000-010943fade63" />
	</SchemeIndex>
</SchemeTable>