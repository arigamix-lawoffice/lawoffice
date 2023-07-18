<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="8bd27c6a-16e0-4ac4-a147-8cc2d30dc88b" Name="KrVirtualFileVersions" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8bd27c6a-16e0-00c4-2000-0cc2d30dc88b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8bd27c6a-16e0-01c4-4000-0cc2d30dc88b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8bd27c6a-16e0-00c4-3100-0cc2d30dc88b" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="287664dc-aba2-4ab7-9438-efcbc1cbd40b" Name="FileName" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1e6d6fa9-1fe4-4a29-a98a-13efcbc88126" Name="FileVersionID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7cd05ac8-0714-4de9-8120-828f2cd38671" Name="FileTemplate" Type="Reference(Typified) Not Null" ReferencedTable="98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7cd05ac8-0714-00e9-4000-028f2cd38671" Name="FileTemplateID" Type="Guid Not Null" ReferencedColumn="98e0c3a9-0b9a-01ec-4000-0a077f6ff5f0" />
		<SchemeReferencingColumn ID="b3d2b75e-0337-47d9-bf29-efc31a0430b3" Name="FileTemplateName" Type="String(256) Not Null" ReferencedColumn="db93e6bd-9e6a-4232-bf8c-bfe652e5573c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a437b34f-c29f-47d0-b377-db6657c6c0ab" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8bd27c6a-16e0-00c4-5000-0cc2d30dc88b" Name="pk_KrVirtualFileVersions">
		<SchemeIndexedColumn Column="8bd27c6a-16e0-00c4-3100-0cc2d30dc88b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8bd27c6a-16e0-00c4-7000-0cc2d30dc88b" Name="idx_KrVirtualFileVersions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8bd27c6a-16e0-01c4-4000-0cc2d30dc88b" />
	</SchemeIndex>
	<SchemeIndex ID="150f6205-628f-4dc0-8f59-663c108d7d72" Name="ndx_KrVirtualFileVersions_FileTemplateID">
		<Description>Индекс, чтобы файловые шаблоны удалялись быстро, независимо от количества виртуальных файлов.</Description>
		<SchemeIndexedColumn Column="7cd05ac8-0714-00e9-4000-028f2cd38671" />
	</SchemeIndex>
</SchemeTable>