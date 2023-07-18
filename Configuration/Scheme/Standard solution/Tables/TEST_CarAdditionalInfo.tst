<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9cbc0f98-571a-4822-a290-3e36b2f2f2e6" Name="TEST_CarAdditionalInfo" Group="Test" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9cbc0f98-571a-0022-2000-0e36b2f2f2e6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9cbc0f98-571a-0122-4000-0e36b2f2f2e6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1575dadf-b693-4c96-93d6-61c7a895f52f" Name="Color" Type="String(50) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f6d962a7-47bf-42ab-b13c-e6ee52b79dcf" Name="df_TEST_CarAdditionalInfo_Color" Value="White" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b6435ab1-392e-4ba1-af9b-8f35ddf5d46f" Name="IsBaseColor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="46f9fbfc-81bc-4d5d-a2bc-f323c4cb874a" Name="df_TEST_CarAdditionalInfo_IsBaseColor" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9cbc0f98-571a-0022-5000-0e36b2f2f2e6" Name="pk_TEST_CarAdditionalInfo" IsClustered="true">
		<SchemeIndexedColumn Column="9cbc0f98-571a-0122-4000-0e36b2f2f2e6" />
	</SchemePrimaryKey>
</SchemeTable>