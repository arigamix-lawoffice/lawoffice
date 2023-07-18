<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="44e8b6f2-f7d1-48ff-a3f2-599bf76e5180" Name="TimeZonesSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="44e8b6f2-f7d1-00ff-2000-099bf76e5180" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="44e8b6f2-f7d1-01ff-4000-099bf76e5180" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="937e1686-6a01-4461-b7a4-1fef1bdbab60" Name="AllowToModify" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="681d64e3-d146-431e-867f-965f7641add2" Name="df_TimeZonesSettings_AllowToModify" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="44e8b6f2-f7d1-00ff-5000-099bf76e5180" Name="pk_TimeZonesSettings" IsClustered="true">
		<SchemeIndexedColumn Column="44e8b6f2-f7d1-01ff-4000-099bf76e5180" />
	</SchemePrimaryKey>
</SchemeTable>