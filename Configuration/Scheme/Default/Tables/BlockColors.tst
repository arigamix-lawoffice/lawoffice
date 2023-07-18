<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="c1b59501-4d7f-4884-ac20-715d5d26078b" Name="BlockColors" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c1b59501-4d7f-0084-2000-015d5d26078b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c1b59501-4d7f-0184-4000-015d5d26078b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ae87d64b-5d47-455d-a12e-b3beefee03dd" Name="Color1" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d15b266d-dd12-4571-9e39-aed3d68f1eb7" Name="df_BlockColors_Color1" Value="872408710" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f718055d-12c0-4957-a32f-abf0243e63cd" Name="Color2" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3b586111-912b-4ed9-af11-ceb3b147082d" Name="df_BlockColors_Color2" Value="869791633" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e0c2b9ee-db33-4c3a-9507-f7c8357dd696" Name="Color3" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5f24d912-c44f-468b-9287-cee629c4cfd6" Name="df_BlockColors_Color3" Value="866310143" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5b8b90c3-d5c7-4226-9bca-ba2a1f86c0a1" Name="Color4" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f867a676-4892-4303-bccd-fef53a526efd" Name="df_BlockColors_Color4" Value="872390395" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="94a65e62-d829-4d3f-a87e-7ca36ab338bc" Name="Color5" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0dd0d022-d49d-485c-89ca-b89140df69b2" Name="df_BlockColors_Color5" Value="872374107" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c1b59501-4d7f-0084-5000-015d5d26078b" Name="pk_BlockColors" IsClustered="true">
		<SchemeIndexedColumn Column="c1b59501-4d7f-0184-4000-015d5d26078b" />
	</SchemePrimaryKey>
</SchemeTable>