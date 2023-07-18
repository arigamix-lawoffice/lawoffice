<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b92e97c0-4557-4d43-874a-e9a75173cbf8" Name="LocalizationEntries" Group="System">
	<SchemeComplexColumn ID="b92e97c0-4557-0043-2000-09a75173cbf8" Name="Library" Type="Reference(Typified) Not Null" ReferencedTable="3e31b54e-1a4c-4e9c-bcf5-26e4780d6419" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b92e97c0-4557-0143-4000-09a75173cbf8" Name="LibraryID" Type="Guid Not Null" ReferencedColumn="84abc23e-1f9b-446f-93ba-4fab68dd841c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a1fcd6b6-eba9-4619-9f95-34e84e7b931e" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="0b6e11e0-971a-4a91-b180-c2734217fcda" Name="Name" Type="String(128) Not Null">
		<Collation Dbms="SqlServer">Latin1_General_BIN2</Collation>
		<Collation Dbms="PostgreSql">POSIX</Collation>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="21bc9043-2b4c-4867-a63c-c75f72036fd5" Name="Comment" Type="String(512) Null" />
	<SchemePhysicalColumn ID="56ecfdbc-bd8d-4ca7-a385-ef1f1110ee6e" Name="Overridden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" IsSealed="true" ID="488af45f-c150-4237-8aa7-f40a9e744128" Name="df_LocalizationEntries_Overridden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="0bdb6ad7-0952-49f0-91c6-634623b8ad8e" Name="pk_LocalizationEntries">
		<SchemeIndexedColumn Column="a1fcd6b6-eba9-4619-9f95-34e84e7b931e" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="31724b2b-647d-4c8a-847e-7d7e0bfbd29e" Name="ndx_LocalizationEntries_LibraryIDName">
		<SchemeIndexedColumn Column="b92e97c0-4557-0143-4000-09a75173cbf8" />
		<SchemeIndexedColumn Column="0b6e11e0-971a-4a91-b180-c2734217fcda" />
	</SchemeUniqueKey>
	<SchemeIndex ID="b95f8aa1-cab4-4e9e-8c78-afe7215d2706" Name="idx_LocalizationEntries_OverriddenName" IsClustered="true">
		<SchemeIndexedColumn Column="56ecfdbc-bd8d-4ca7-a385-ef1f1110ee6e" />
		<SchemeIndexedColumn Column="0b6e11e0-971a-4a91-b180-c2734217fcda" />
	</SchemeIndex>
</SchemeTable>