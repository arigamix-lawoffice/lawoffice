<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="07e8720b-4500-4a7f-b988-7eda3bb8dc38" Name="BusinessProcessExtensions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со списком расширений для карточки шаблона процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="07e8720b-4500-007f-2000-0eda3bb8dc38" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="07e8720b-4500-017f-4000-0eda3bb8dc38" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="07e8720b-4500-007f-3100-0eda3bb8dc38" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="21936f28-c4fb-4314-b901-18c8d2dadc62" Name="Extension" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21936f28-c4fb-0014-4000-08c8d2dadc62" Name="ExtensionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="6e8334ac-b486-47fd-84d1-b35546122a6c" Name="ExtensionName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="07e8720b-4500-007f-5000-0eda3bb8dc38" Name="pk_BusinessProcessExtensions">
		<SchemeIndexedColumn Column="07e8720b-4500-007f-3100-0eda3bb8dc38" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="07e8720b-4500-007f-7000-0eda3bb8dc38" Name="idx_BusinessProcessExtensions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="07e8720b-4500-017f-4000-0eda3bb8dc38" />
	</SchemeIndex>
</SchemeTable>