<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0748a117-8a2a-4198-a994-15d91094d6b7" Name="KrSettingsTaskAuthor" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0748a117-8a2a-0098-2000-05d91094d6b7" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0748a117-8a2a-0198-4000-05d91094d6b7" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0748a117-8a2a-0098-3100-05d91094d6b7" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a239a602-ccf6-4802-a4f7-1ff5cbdd416f" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a239a602-ccf6-0002-4000-0ff5cbdd416f" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6f2418fb-a4f9-4b3a-bf33-eef3c27e2d8b" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e1292ca5-19be-44ab-8977-44e7337556df" Name="Authors" Type="Reference(Typified) Not Null" ReferencedTable="afafd0bc-446e-4adf-8332-16be0b3d1908" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e1292ca5-19be-00ab-4000-04e7337556df" Name="AuthorsRowID" Type="Guid Not Null" ReferencedColumn="afafd0bc-446e-00df-3100-06be0b3d1908" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0748a117-8a2a-0098-5000-05d91094d6b7" Name="pk_KrSettingsTaskAuthor">
		<SchemeIndexedColumn Column="0748a117-8a2a-0098-3100-05d91094d6b7" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0748a117-8a2a-0098-7000-05d91094d6b7" Name="idx_KrSettingsTaskAuthor_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0748a117-8a2a-0198-4000-05d91094d6b7" />
	</SchemeIndex>
</SchemeTable>