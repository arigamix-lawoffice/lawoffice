<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="cafa4371-0483-4d71-80cd-75d68cd6086f" Name="CustomBlockColorsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cafa4371-0483-0071-2000-05d68cd6086f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cafa4371-0483-0171-4000-05d68cd6086f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="604ed85d-4619-425c-b31b-322f730ce1a8" Name="Color1" Type="Int32 Null" />
	<SchemePhysicalColumn ID="fc2f66fa-c50e-4f04-8961-9ba7379e54b0" Name="Color2" Type="Int32 Null" />
	<SchemePhysicalColumn ID="9da03389-cf52-43d0-ba3e-49a2699c7c90" Name="Color3" Type="Int32 Null" />
	<SchemePhysicalColumn ID="b3681d1b-bbfe-46cd-a4cf-3314fa4910c9" Name="Color4" Type="Int32 Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cafa4371-0483-0071-5000-05d68cd6086f" Name="pk_CustomBlockColorsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="cafa4371-0483-0171-4000-05d68cd6086f" />
	</SchemePrimaryKey>
</SchemeTable>