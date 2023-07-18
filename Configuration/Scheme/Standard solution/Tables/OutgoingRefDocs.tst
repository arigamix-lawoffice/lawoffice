<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="73320234-fc44-4126-a7a6-5dd0bdaa4880" Name="OutgoingRefDocs" Group="Common" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="73320234-fc44-0026-2000-0dd0bdaa4880" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="73320234-fc44-0126-4000-0dd0bdaa4880" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="73320234-fc44-0026-3100-0dd0bdaa4880" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4856d3e1-fda0-42eb-bd87-8c2cea592e9f" Name="Doc" Type="Reference(Abstract) Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4856d3e1-fda0-00eb-4000-0c2cea592e9f" Name="DocID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="a8049896-147e-45ea-ad9a-d225080d46df" Name="DocDescription" Type="String(250) Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f9708dec-9a21-4f0a-85a5-9f90385f44d1" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ed68e7d0-a591-49c4-a170-3a93969d6644" Name="df_OutgoingRefDocs_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="73320234-fc44-0026-5000-0dd0bdaa4880" Name="pk_OutgoingRefDocs">
		<SchemeIndexedColumn Column="73320234-fc44-0026-3100-0dd0bdaa4880" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="73320234-fc44-0026-7000-0dd0bdaa4880" Name="idx_OutgoingRefDocs_ID" IsClustered="true">
		<SchemeIndexedColumn Column="73320234-fc44-0126-4000-0dd0bdaa4880" />
	</SchemeIndex>
	<SchemeIndex ID="7aa59eff-49e8-483c-a000-44e2df806491" Name="ndx_OutgoingRefDocs_DocID">
		<SchemeIndexedColumn Column="4856d3e1-fda0-00eb-4000-0c2cea592e9f" />
	</SchemeIndex>
</SchemeTable>