<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="93d11813-4967-458e-b3be-f7da367a8872" Name="WeAddFileFromTemplateAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для действия Добавить файл по шаблону</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="93d11813-4967-008e-2000-07da367a8872" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="93d11813-4967-018e-4000-07da367a8872" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b4c48c2b-dc42-4c1a-a228-bc15b7180eb5" Name="Name" Type="String(Max) Null" />
	<SchemeComplexColumn ID="3e8b2145-4bb2-40d2-91d0-e42598ac0474" Name="FileTemplate" Type="Reference(Typified) Not Null" ReferencedTable="98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3e8b2145-4bb2-00d2-4000-042598ac0474" Name="FileTemplateID" Type="Guid Not Null" ReferencedColumn="98e0c3a9-0b9a-01ec-4000-0a077f6ff5f0" />
		<SchemeReferencingColumn ID="c259ebfd-5848-4055-93bc-e6bf9c3353fb" Name="FileTemplateName" Type="String(256) Not Null" ReferencedColumn="db93e6bd-9e6a-4232-bf8c-bfe652e5573c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="93d11813-4967-008e-5000-07da367a8872" Name="pk_WeAddFileFromTemplateAction" IsClustered="true">
		<SchemeIndexedColumn Column="93d11813-4967-018e-4000-07da367a8872" />
	</SchemePrimaryKey>
</SchemeTable>