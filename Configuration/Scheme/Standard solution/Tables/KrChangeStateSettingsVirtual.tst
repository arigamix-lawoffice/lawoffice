<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="bc1450c4-0ddd-4efd-9636-f2ec5d013979" Name="KrChangeStateSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="bc1450c4-0ddd-00fd-2000-02ec5d013979" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bc1450c4-0ddd-01fd-4000-02ec5d013979" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0f2bf298-6c8f-45fe-a0bc-c6965f963e12" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0f2bf298-6c8f-00fe-4000-06965f963e12" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="70ba4105-8f0e-40e3-8dcf-6d5637cf2f9a" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="bc1450c4-0ddd-00fd-5000-02ec5d013979" Name="pk_KrChangeStateSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="bc1450c4-0ddd-01fd-4000-02ec5d013979" />
	</SchemePrimaryKey>
</SchemeTable>