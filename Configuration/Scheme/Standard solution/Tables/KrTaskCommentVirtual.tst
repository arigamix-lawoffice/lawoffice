<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="344fa4e8-cdfc-4cb0-8634-9155c49fd21a" Name="KrTaskCommentVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Tasks" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="344fa4e8-cdfc-00b0-2000-0155c49fd21a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="344fa4e8-cdfc-01b0-4000-0155c49fd21a" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="05aa8ff9-010c-499d-b59f-358c6b84485b" Name="Comment" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="344fa4e8-cdfc-00b0-5000-0155c49fd21a" Name="pk_KrTaskCommentVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="344fa4e8-cdfc-01b0-4000-0155c49fd21a" />
	</SchemePrimaryKey>
</SchemeTable>