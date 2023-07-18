<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8f0adc86-8166-4579-9a25-7c3f2921d32d" Name="CustomForegroundColorsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8f0adc86-8166-0079-2000-0c3f2921d32d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8f0adc86-8166-0179-4000-0c3f2921d32d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="540953b1-abc0-4e75-b410-4de7c0f26d60" Name="Color1" Type="Int32 Null" />
	<SchemePhysicalColumn ID="b48f822f-41b2-4c69-bb3f-2b183e2f0181" Name="Color2" Type="Int32 Null" />
	<SchemePhysicalColumn ID="321c68ae-384b-48e1-990d-6c1b1e3bf6bd" Name="Color3" Type="Int32 Null" />
	<SchemePhysicalColumn ID="c7c76719-4c1b-4629-a33c-a97bf6df13f5" Name="Color4" Type="Int32 Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8f0adc86-8166-0079-5000-0c3f2921d32d" Name="pk_CustomForegroundColorsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="8f0adc86-8166-0179-4000-0c3f2921d32d" />
	</SchemePrimaryKey>
</SchemeTable>