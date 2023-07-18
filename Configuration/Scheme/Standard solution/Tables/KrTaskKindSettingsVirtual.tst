<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="80ab607a-d43f-435d-a1be-a203bb99c2d3" Name="KrTaskKindSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="80ab607a-d43f-005d-2000-0203bb99c2d3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="80ab607a-d43f-015d-4000-0203bb99c2d3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="858edc9c-50ff-41b6-8c0d-7e4818f93200" Name="Kind" Type="Reference(Typified) Null" ReferencedTable="856068b1-0e78-4aa8-8e7a-4f53d91a7298" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="858edc9c-50ff-00b6-4000-0e4818f93200" Name="KindID" Type="Guid Null" ReferencedColumn="856068b1-0e78-01a8-4000-0f53d91a7298" />
		<SchemeReferencingColumn ID="8cd8c0a9-15aa-4973-b91c-44aedbae8eb1" Name="KindCaption" Type="String(128) Null" ReferencedColumn="63d9110b-7628-4bf9-9dae-750c3035e48d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="80ab607a-d43f-005d-5000-0203bb99c2d3" Name="pk_KrTaskKindSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="80ab607a-d43f-015d-4000-0203bb99c2d3" />
	</SchemePrimaryKey>
</SchemeTable>