<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0ebd80aa-360b-473b-8327-90e10035c000" Name="CompiledViews" Group="System">
	<SchemeComplexColumn ID="78df185a-40d3-4f9a-8226-66f68b8b651c" Name="View" Type="Reference(Typified) Null" ReferencedTable="3519b63c-eea0-48f4-b70a-544e58ece5fc" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="78df185a-40d3-009a-4000-06f68b8b651c" Name="ViewID" Type="Guid Null" ReferencedColumn="8e4c45ad-ca6f-4f0f-be25-9a9e37a4cfd6" />
		<SchemeReferencingColumn ID="8ad2b47c-b06d-4718-bff1-85047e900275" Name="ViewAlias" Type="String(128) Null" ReferencedColumn="827d19f5-a1aa-4e74-92c0-8bb9dcbceb7d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d889d912-e995-4d16-817c-de470d95751e" Name="FunctionName" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="b77ea953-6b17-4287-9171-5d299b8d578c" Name="LastUsed" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="cb40a9a4-a626-4f4e-9297-fa178c587afd" Name="ViewModifiedDateTime" Type="DateTime Not Null" />
	<SchemePrimaryKey ID="74ecc6d0-97bb-43b8-a9a3-6292e21baf64" Name="pk_CompiledViews" IsClustered="true">
		<SchemeIndexedColumn Column="d889d912-e995-4d16-817c-de470d95751e" />
	</SchemePrimaryKey>
</SchemeTable>