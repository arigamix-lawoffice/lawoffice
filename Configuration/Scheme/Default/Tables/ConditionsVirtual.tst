<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="6d2ec0d3-4980-45f3-aa64-ab79eb9f4da1" Name="ConditionsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d2ec0d3-4980-00f3-2000-0b79eb9f4da1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6d2ec0d3-4980-01f3-4000-0b79eb9f4da1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d2ec0d3-4980-00f3-3100-0b79eb9f4da1" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1f9e7b70-031a-4c92-a93a-d826cb7a5df1" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="925a75d4-639f-4467-9155-c8e21f5433a9" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1f9e7b70-031a-0092-4000-0826cb7a5df1" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="925a75d4-639f-0067-3100-08e21f5433a9" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="10e03058-de0a-4b0d-b28d-483598e88173" Name="ConditionType" Type="Reference(Typified) Not Null" ReferencedTable="7e0c2c3b-e8f3-4f96-9aa6-eb1c2100d74f">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="10e03058-de0a-000d-4000-083598e88173" Name="ConditionTypeID" Type="Guid Not Null" ReferencedColumn="7e0c2c3b-e8f3-0196-4000-0b1c2100d74f" />
		<SchemeReferencingColumn ID="19d9b15e-a22b-4ad9-8093-2601ac8958c6" Name="ConditionTypeName" Type="String(Max) Not Null" ReferencedColumn="372d7961-0e5f-4045-abe0-651363144d31" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e738f791-137f-42b9-b8ab-fa3f849d1c8f" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="c91d9d83-e9b6-4f51-9a8d-e95e1edd196f" Name="InvertCondition" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="65b6a2a1-2a88-4d4e-bf1a-bc2fb94e8765" Name="df_ConditionsVirtual_InvertCondition" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="52e6d8d9-aeea-4c7c-8e9d-ed8bbbf87340" Name="Settings" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="371c3403-5499-4d4d-a0dd-0c77dbfc3b5f" Name="Description" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="3c566992-06a5-44e6-914a-260a627761bf" Name="InvertConditionString" Type="String(Max) Null">
		<Description>Строка, выводящая текстовое значение для InvertCondition</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d2ec0d3-4980-00f3-5000-0b79eb9f4da1" Name="pk_ConditionsVirtual">
		<SchemeIndexedColumn Column="6d2ec0d3-4980-00f3-3100-0b79eb9f4da1" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6d2ec0d3-4980-00f3-7000-0b79eb9f4da1" Name="idx_ConditionsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6d2ec0d3-4980-01f3-4000-0b79eb9f4da1" />
	</SchemeIndex>
</SchemeTable>