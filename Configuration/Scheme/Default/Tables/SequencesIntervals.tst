<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="510bf28c-bccf-4701-b9fa-1081c22c2ef9" Name="SequencesIntervals" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="510bf28c-bccf-0001-2000-0081c22c2ef9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="510bf28c-bccf-0101-4000-0081c22c2ef9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="510bf28c-bccf-0001-3100-0081c22c2ef9" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="9b37f3d7-f1c6-4ed7-878c-bd35ba40de4d" Name="First" Type="Int64 Not Null" />
	<SchemePhysicalColumn ID="61d44727-e78f-4a51-a590-64e54007ad6b" Name="Last" Type="Int64 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="510bf28c-bccf-0001-5000-0081c22c2ef9" Name="pk_SequencesIntervals">
		<SchemeIndexedColumn Column="510bf28c-bccf-0001-3100-0081c22c2ef9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="510bf28c-bccf-0001-7000-0081c22c2ef9" Name="idx_SequencesIntervals_ID" IsClustered="true">
		<SchemeIndexedColumn Column="510bf28c-bccf-0101-4000-0081c22c2ef9" />
	</SchemeIndex>
</SchemeTable>