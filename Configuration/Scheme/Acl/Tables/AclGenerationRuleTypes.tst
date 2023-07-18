<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="930de8d2-2496-4523-9ea2-800d229fd808" Name="AclGenerationRuleTypes" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<Description>Типы карточек для правила Acl.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="930de8d2-2496-0023-2000-000d229fd808" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="930de8d2-2496-0123-4000-000d229fd808" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="930de8d2-2496-0023-3100-000d229fd808" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7ded3305-f454-4b20-be38-6cba26a73717" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7ded3305-f454-0020-4000-0cba26a73717" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="341a8a91-0db9-418d-8b26-709a9d7fe094" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="930de8d2-2496-0023-5000-000d229fd808" Name="pk_AclGenerationRuleTypes">
		<SchemeIndexedColumn Column="930de8d2-2496-0023-3100-000d229fd808" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="930de8d2-2496-0023-7000-000d229fd808" Name="idx_AclGenerationRuleTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="930de8d2-2496-0123-4000-000d229fd808" />
	</SchemeIndex>
</SchemeTable>