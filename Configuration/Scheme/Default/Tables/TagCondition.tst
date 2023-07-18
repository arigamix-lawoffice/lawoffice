<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8a9a0383-b94a-40c0-aa3e-7b461f25b598" Name="TagCondition" Group="Tags" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условия для правил уведомлений, проверяющая Тег.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a9a0383-b94a-00c0-2000-0b461f25b598" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a9a0383-b94a-01c0-4000-0b461f25b598" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a9a0383-b94a-00c0-3100-0b461f25b598" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f566b7f2-5a3a-4df5-b6ab-a8cbd517fbb4" Name="Tag" Type="Reference(Typified) Not Null" ReferencedTable="0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f566b7f2-5a3a-00f5-4000-08cbd517fbb4" Name="TagID" Type="Guid Not Null" ReferencedColumn="0bf4050e-d7d4-01da-4000-0a4f0148dd7f" />
		<SchemeReferencingColumn ID="02ccad4a-e6cc-468c-a1c7-ffa35a432b7e" Name="TagName" Type="String(256) Not Null" ReferencedColumn="7ea91bae-9150-4593-a2eb-1971c0ba653f" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a9a0383-b94a-00c0-5000-0b461f25b598" Name="pk_TagCondition">
		<SchemeIndexedColumn Column="8a9a0383-b94a-00c0-3100-0b461f25b598" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a9a0383-b94a-00c0-7000-0b461f25b598" Name="idx_TagCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8a9a0383-b94a-01c0-4000-0b461f25b598" />
	</SchemeIndex>
</SchemeTable>