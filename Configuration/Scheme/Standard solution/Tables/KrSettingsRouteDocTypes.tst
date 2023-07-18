<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9568db07-0f34-48ad-bab8-0d5e43d1846b" Name="KrSettingsRouteDocTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Разрешения по типам карточек или видам документов в маршрутах.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9568db07-0f34-00ad-2000-0d5e43d1846b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9568db07-0f34-01ad-4000-0d5e43d1846b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9568db07-0f34-00ad-3100-0d5e43d1846b" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e954d964-1d16-49b5-b6ad-becbf1319f78" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e954d964-1d16-00b5-4000-0ecbf1319f78" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="5b0a7ec8-b74c-4816-9109-855c15443aab" Name="CardTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="41ab6c8d-24ff-4083-91d5-56d4bdb470ae" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="39e6d38f-4e35-45e9-8c71-42a932dce18c" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="41ab6c8d-24ff-0083-4000-06d4bdb470ae" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="39e6d38f-4e35-00e9-3100-02a932dce18c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9568db07-0f34-00ad-5000-0d5e43d1846b" Name="pk_KrSettingsRouteDocTypes">
		<SchemeIndexedColumn Column="9568db07-0f34-00ad-3100-0d5e43d1846b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9568db07-0f34-00ad-7000-0d5e43d1846b" Name="idx_KrSettingsRouteDocTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9568db07-0f34-01ad-4000-0d5e43d1846b" />
	</SchemeIndex>
</SchemeTable>