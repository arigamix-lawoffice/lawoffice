<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="830b89cf-862c-4f6a-b564-d538d0bbec90" Name="WorkflowNodeInstanceSubprocesses" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с отображением подпроцессов, привязанных у экземпляру узла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="830b89cf-862c-006a-2000-0538d0bbec90" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="830b89cf-862c-016a-4000-0538d0bbec90" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="830b89cf-862c-006a-3100-0538d0bbec90" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="120cecf8-6e1b-4ed8-b46c-1da49d34581e" Name="Created" Type="DateTime Not Null">
		<Description>Дата создания подпроцесса</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d0123d10-c823-420b-9087-871936d775ab" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя подпроцесса</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="830b89cf-862c-006a-5000-0538d0bbec90" Name="pk_WorkflowNodeInstanceSubprocesses">
		<SchemeIndexedColumn Column="830b89cf-862c-006a-3100-0538d0bbec90" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="830b89cf-862c-006a-7000-0538d0bbec90" Name="idx_WorkflowNodeInstanceSubprocesses_ID" IsClustered="true">
		<SchemeIndexedColumn Column="830b89cf-862c-016a-4000-0538d0bbec90" />
	</SchemeIndex>
</SchemeTable>