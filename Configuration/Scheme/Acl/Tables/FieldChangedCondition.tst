<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="06245b07-be2a-40de-aec8-bfd367860930" Name="FieldChangedCondition" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условийпроверяющих изменение поля.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="06245b07-be2a-00de-2000-0fd367860930" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="06245b07-be2a-01de-4000-0fd367860930" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="06245b07-be2a-00de-3100-0fd367860930" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="c5f46bdf-3a04-49c6-8e2c-5527bb2ca7c0" Name="Field" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c5f46bdf-3a04-00c6-4000-0527bb2ca7c0" Name="FieldID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="8d2e2b4f-e218-4078-8a23-30cb7a48c572" Name="FieldName" Type="String(256) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="06245b07-be2a-00de-5000-0fd367860930" Name="pk_FieldChangedCondition">
		<SchemeIndexedColumn Column="06245b07-be2a-00de-3100-0fd367860930" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="06245b07-be2a-00de-7000-0fd367860930" Name="idx_FieldChangedCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="06245b07-be2a-01de-4000-0fd367860930" />
	</SchemeIndex>
</SchemeTable>