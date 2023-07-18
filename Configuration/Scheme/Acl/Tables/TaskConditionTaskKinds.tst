<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="a3777728-9f01-449a-b94c-953a1e205c5b" Name="TaskConditionTaskKinds" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список видов заданий для условий проверки заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3777728-9f01-009a-2000-053a1e205c5b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a3777728-9f01-019a-4000-053a1e205c5b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3777728-9f01-009a-3100-053a1e205c5b" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="2cda82d7-da37-4c50-8570-c2da83f03101" Name="TaskKind" Type="Reference(Typified) Not Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2cda82d7-da37-0050-4000-02da83f03101" Name="TaskKindID" Type="Guid Not Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="2f2afa5d-7b2b-45cf-a432-0b3c8e26d306" Name="TaskKindCaption" Type="String(128) Not Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3777728-9f01-009a-5000-053a1e205c5b" Name="pk_TaskConditionTaskKinds">
		<SchemeIndexedColumn Column="a3777728-9f01-009a-3100-053a1e205c5b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3777728-9f01-009a-7000-053a1e205c5b" Name="idx_TaskConditionTaskKinds_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a3777728-9f01-019a-4000-053a1e205c5b" />
	</SchemeIndex>
</SchemeTable>