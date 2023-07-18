<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b764f842-5b97-4de7-854a-f61b6b7a71dc" Name="ConditionTypeUsePlaces" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b764f842-5b97-00e7-2000-061b6b7a71dc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b764f842-5b97-01e7-4000-061b6b7a71dc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b764f842-5b97-00e7-3100-061b6b7a71dc" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="e6554172-c0a5-4cd4-af13-561b0e1ee92f" Name="UsePlace" Type="Reference(Typified) Not Null" ReferencedTable="6963c76f-5e8d-49b5-80a3-f2ec342de0bf">
		<SchemeReferencingColumn ID="b0be30b3-4ea9-4d83-aed8-fba60079d119" Name="UsePlaceName" Type="String(Max) Not Null" ReferencedColumn="6aaafc76-c841-45a4-a733-4a94e572f5a5" />
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e6554172-c0a5-00d4-4000-061b0e1ee92f" Name="UsePlaceID" Type="Guid Not Null" ReferencedColumn="67485d1f-6b37-4438-9264-458bdf2ff2e7" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b764f842-5b97-00e7-5000-061b6b7a71dc" Name="pk_ConditionTypeUsePlaces">
		<SchemeIndexedColumn Column="b764f842-5b97-00e7-3100-061b6b7a71dc" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b764f842-5b97-00e7-7000-061b6b7a71dc" Name="idx_ConditionTypeUsePlaces_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b764f842-5b97-01e7-4000-061b6b7a71dc" />
	</SchemeIndex>
</SchemeTable>