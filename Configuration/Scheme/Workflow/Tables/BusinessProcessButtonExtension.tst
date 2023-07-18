<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="b4d0da55-0e6e-4835-bb71-1df3c5b5e695" Name="BusinessProcessButtonExtension" Group="WorkflowEngine" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для карточки-расширения тайла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4d0da55-0e6e-0035-2000-0df3c5b5e695" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4d0da55-0e6e-0135-4000-0df3c5b5e695" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="4c48fad5-6d4b-451a-b055-46b5cb362a9b" Name="Button" Type="Reference(Typified) Not Null" ReferencedTable="59bf0d0b-f7fc-41d3-92da-56c673f1e0b3" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4c48fad5-6d4b-001a-4000-06b5cb362a9b" Name="ButtonRowID" Type="Guid Not Null" ReferencedColumn="59bf0d0b-f7fc-00d3-3100-06c673f1e0b3" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4d0da55-0e6e-0035-5000-0df3c5b5e695" Name="pk_BusinessProcessButtonExtension" IsClustered="true">
		<SchemeIndexedColumn Column="b4d0da55-0e6e-0135-4000-0df3c5b5e695" />
	</SchemePrimaryKey>
	<SchemeIndex ID="63247cc1-6a0a-4ba6-b0e2-96c2dc64751f" Name="ndx_BusinessProcessButtonExtension_ButtonRowID">
		<SchemeIndexedColumn Column="4c48fad5-6d4b-001a-4000-06b5cb362a9b" />
	</SchemeIndex>
</SchemeTable>