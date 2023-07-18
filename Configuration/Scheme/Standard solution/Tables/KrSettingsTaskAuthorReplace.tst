<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="cfe4678d-369f-43a4-b103-32aecd9858a6" Name="KrSettingsTaskAuthorReplace" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfe4678d-369f-00a4-2000-02aecd9858a6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cfe4678d-369f-01a4-4000-02aecd9858a6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfe4678d-369f-00a4-3100-02aecd9858a6" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="169054c6-fae6-456e-a180-450678c8ca62" Name="AuthorReplace" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="169054c6-fae6-006e-4000-050678c8ca62" Name="AuthorReplaceID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="038c1808-7641-45c9-a606-156c01da20e9" Name="AuthorReplaceName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="fc435512-14d5-4208-8ae0-fee064530e74" Name="Authors" Type="Reference(Typified) Not Null" ReferencedTable="afafd0bc-446e-4adf-8332-16be0b3d1908" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc435512-14d5-0008-4000-0ee064530e74" Name="AuthorsRowID" Type="Guid Not Null" ReferencedColumn="afafd0bc-446e-00df-3100-06be0b3d1908" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfe4678d-369f-00a4-5000-02aecd9858a6" Name="pk_KrSettingsTaskAuthorReplace">
		<SchemeIndexedColumn Column="cfe4678d-369f-00a4-3100-02aecd9858a6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="cfe4678d-369f-00a4-7000-02aecd9858a6" Name="idx_KrSettingsTaskAuthorReplace_ID" IsClustered="true">
		<SchemeIndexedColumn Column="cfe4678d-369f-01a4-4000-02aecd9858a6" />
	</SchemeIndex>
</SchemeTable>