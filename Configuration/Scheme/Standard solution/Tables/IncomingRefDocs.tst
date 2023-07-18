<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="83785076-d844-4ea4-9e84-0a389c951ef4" Name="IncomingRefDocs" Group="Common" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="83785076-d844-00a4-2000-0a389c951ef4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="83785076-d844-01a4-4000-0a389c951ef4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="83785076-d844-00a4-3100-0a389c951ef4" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e53b1a76-83f6-4c8f-a362-74529b358028" Name="Doc" Type="Reference(Abstract) Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e53b1a76-83f6-008f-4000-04529b358028" Name="DocID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="0bffe920-fd7a-4232-a076-a0d63c1da398" Name="DocDescription" Type="String(250) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="83785076-d844-00a4-5000-0a389c951ef4" Name="pk_IncomingRefDocs">
		<SchemeIndexedColumn Column="83785076-d844-00a4-3100-0a389c951ef4" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="83785076-d844-00a4-7000-0a389c951ef4" Name="idx_IncomingRefDocs_ID" IsClustered="true">
		<SchemeIndexedColumn Column="83785076-d844-01a4-4000-0a389c951ef4" />
	</SchemeIndex>
</SchemeTable>