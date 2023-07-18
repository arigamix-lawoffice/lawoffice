<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="7e0c2c3b-e8f3-4f96-9aa6-eb1c2100d74f" Name="ConditionTypes" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Тип условия</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="7e0c2c3b-e8f3-0096-2000-0b1c2100d74f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7e0c2c3b-e8f3-0196-4000-0b1c2100d74f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="372d7961-0e5f-4045-abe0-651363144d31" Name="Name" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="72d2c9ba-51f9-43aa-ad00-daa6ee878f7a" Name="SettingsCardType" Type="Reference(Typified) Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="72d2c9ba-51f9-00aa-4000-0aa6ee878f7a" Name="SettingsCardTypeID" Type="Guid Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="472c485d-a692-4e6b-8f26-fd1b31d3b68d" Name="SettingsCardTypeCaption" Type="String(128) Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a1b799ac-57d1-48bc-ac54-70cce8a7a458" Name="ConditionText" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="0e84555c-56ae-4716-a3c6-418d5c109c7f" Name="Condition" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="5cad8a5b-fd6a-4f44-bd32-fcca7004ff1e" Name="Description" Type="String(Max) Null">
		<Description>Описание.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="7e0c2c3b-e8f3-0096-5000-0b1c2100d74f" Name="pk_ConditionTypes" IsClustered="true">
		<SchemeIndexedColumn Column="7e0c2c3b-e8f3-0196-4000-0b1c2100d74f" />
	</SchemePrimaryKey>
</SchemeTable>