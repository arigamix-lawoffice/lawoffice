<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="476425ca-8284-4c41-b11b-dd215042ee6a" Name="KrAdditionalApproval" Group="KrStageTypes" InstanceType="Tasks" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="476425ca-8284-0041-2000-0d215042ee6a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="476425ca-8284-0141-4000-0d215042ee6a" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4c4db3f6-c137-415f-a291-e913592207cf" Name="TimeLimitation" Type="Double Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="75388693-5189-4175-b4e4-50edc3f22290" Name="df_KrAdditionalApproval_TimeLimitation" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b4a61726-ef41-48b3-a9dc-8bef6b4d9bbf" Name="FirstIsResponsible" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="4ea4c115-5a2c-432a-9ac1-170c9bc29830" Name="df_KrAdditionalApproval_FirstIsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="13f99856-c2b2-486a-874a-68ed00b0cadb" Name="Comment" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="476425ca-8284-0041-5000-0d215042ee6a" Name="pk_KrAdditionalApproval" IsClustered="true">
		<SchemeIndexedColumn Column="476425ca-8284-0141-4000-0d215042ee6a" />
	</SchemePrimaryKey>
</SchemeTable>