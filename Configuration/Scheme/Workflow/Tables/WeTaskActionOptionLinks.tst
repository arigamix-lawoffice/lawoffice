<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="a3d3bf40-b37a-4118-af51-2b555da511b7" Name="WeTaskActionOptionLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3d3bf40-b37a-0018-2000-0b555da511b7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a3d3bf40-b37a-0118-4000-0b555da511b7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3d3bf40-b37a-0018-3100-0b555da511b7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f9dacb10-1314-478f-bf13-3b14bca75613" Name="Link" Type="Reference(Abstract) Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f9dacb10-1314-008f-4000-0b14bca75613" Name="LinkID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="e9e02c7c-e401-489f-997c-bb2b9482a1cf" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="f7cd636e-64b9-4c3b-a139-70477cd635b9" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ff43c5c8-c992-4939-9533-50099722dab5" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="e30dcb0a-2a63-4f52-82f9-a12b0038d70d" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ff43c5c8-c992-0039-4000-00099722dab5" Name="OptionRowID" Type="Guid Not Null" ReferencedColumn="e30dcb0a-2a63-0052-3100-012b0038d70d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3d3bf40-b37a-0018-5000-0b555da511b7" Name="pk_WeTaskActionOptionLinks">
		<SchemeIndexedColumn Column="a3d3bf40-b37a-0018-3100-0b555da511b7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3d3bf40-b37a-0018-7000-0b555da511b7" Name="idx_WeTaskActionOptionLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a3d3bf40-b37a-0118-4000-0b555da511b7" />
	</SchemeIndex>
</SchemeTable>