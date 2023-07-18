<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="15368402-1522-4722-91b7-d27636f3596b" Name="KrCheckStateTileExtension" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Расширение функциональности проверки прав доступа на тайлы в WorkflowEngine по состоянию типового решения</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="15368402-1522-0022-2000-027636f3596b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="15368402-1522-0122-4000-027636f3596b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="15368402-1522-0022-3100-027636f3596b" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fd83e5e0-5d62-4a0b-859b-0e7b478e8fd7" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fd83e5e0-5d62-000b-4000-0e7b478e8fd7" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="d1b7704a-2e6b-4661-9d73-42655679c781" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="15368402-1522-0022-5000-027636f3596b" Name="pk_KrCheckStateTileExtension">
		<SchemeIndexedColumn Column="15368402-1522-0022-3100-027636f3596b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="15368402-1522-0022-7000-027636f3596b" Name="idx_KrCheckStateTileExtension_ID" IsClustered="true">
		<SchemeIndexedColumn Column="15368402-1522-0122-4000-027636f3596b" />
	</SchemeIndex>
	<SchemeIndex ID="3f8782bf-52dc-47b1-928d-0af07d481041" Name="ndx_KrCheckStateTileExtension_StateIDRowID">
		<SchemeIndexedColumn Column="fd83e5e0-5d62-000b-4000-0e7b478e8fd7" />
		<SchemeIndexedColumn Column="15368402-1522-0022-3100-027636f3596b" />
	</SchemeIndex>
</SchemeTable>