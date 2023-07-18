<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="42c4f5aa-d0e8-4d26-abb8-a898e736fe35" Name="KrCommentators" Group="KrStageTypes" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="42c4f5aa-d0e8-0026-2000-0898e736fe35" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="42c4f5aa-d0e8-0126-4000-0898e736fe35" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="42c4f5aa-d0e8-0026-3100-0898e736fe35" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0ed53c15-b1d2-4ee7-8cbb-a0cce3d2b0e4" Name="Commentator" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0ed53c15-b1d2-00e7-4000-00cce3d2b0e4" Name="CommentatorID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a00b6ce3-a9af-42d3-ac9d-87669011f01c" Name="CommentatorName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="42c4f5aa-d0e8-0026-5000-0898e736fe35" Name="pk_KrCommentators">
		<SchemeIndexedColumn Column="42c4f5aa-d0e8-0026-3100-0898e736fe35" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="42c4f5aa-d0e8-0026-7000-0898e736fe35" Name="idx_KrCommentators_ID" IsClustered="true">
		<SchemeIndexedColumn Column="42c4f5aa-d0e8-0126-4000-0898e736fe35" />
	</SchemeIndex>
</SchemeTable>