<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="57f61e17-bd87-48cb-8efc-7a7dc56f2eef" Name="WeDialogActionButtonLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="57f61e17-bd87-00cb-2000-0a7dc56f2eef" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="57f61e17-bd87-01cb-4000-0a7dc56f2eef" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="57f61e17-bd87-00cb-3100-0a7dc56f2eef" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b0a66418-1695-4efb-ace0-dce675cd8a40" Name="Button" Type="Reference(Typified) Not Null" ReferencedTable="a99b285f-80c3-442a-85a6-2a3bfd645d2b" IsReferenceToOwner="true" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b0a66418-1695-00fb-4000-0ce675cd8a40" Name="ButtonRowID" Type="Guid Not Null" ReferencedColumn="a99b285f-80c3-002a-3100-0a3bfd645d2b" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="809a12cd-5b41-4199-bf12-3aad9023dea3" Name="Link" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="809a12cd-5b41-0099-4000-0aad9023dea3" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="9880d5c8-482a-4549-a5b3-ecf44d2d8d82" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="096c6ff2-3966-466c-9223-e8be88f75456" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="57f61e17-bd87-00cb-5000-0a7dc56f2eef" Name="pk_WeDialogActionButtonLinks">
		<SchemeIndexedColumn Column="57f61e17-bd87-00cb-3100-0a7dc56f2eef" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="57f61e17-bd87-00cb-7000-0a7dc56f2eef" Name="idx_WeDialogActionButtonLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="57f61e17-bd87-01cb-4000-0a7dc56f2eef" />
	</SchemeIndex>
</SchemeTable>