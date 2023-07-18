<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="470ddc9e-4715-4efa-bd25-cbb9f4033162" Name="KrUniversalTaskOptions" Group="KrStageTypes" InstanceType="Tasks" ContentType="Collections">
	<Description>Секция с данными универсального задания</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="470ddc9e-4715-00fa-2000-0bb9f4033162" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="470ddc9e-4715-01fa-4000-0bb9f4033162" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="470ddc9e-4715-00fa-3100-0bb9f4033162" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="b0479bb5-5725-4ed5-80fb-94c28ced9a2b" Name="OptionID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="9534ffe0-1275-4ce7-a9f9-e23702a5cdc7" Name="Caption" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="d87b9391-4b13-4ee4-9c91-87100477d45c" Name="ShowComment" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a819e8c7-ce63-4b5f-8310-122d805bddb8" Name="df_KrUniversalTaskOptions_ShowComment" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="136da94d-d1d7-4c49-9fc4-5648fce32c3e" Name="Additional" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5c87ef77-a1a0-4d69-99c7-19eee9b0784c" Name="df_KrUniversalTaskOptions_Additional" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9fdaecf3-bb82-4500-9c37-bf1dd09e50cb" Name="Order" Type="Int32 Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ddf15429-5082-4f9c-a963-7ed3669eb62e" Name="df_KrUniversalTaskOptions_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5693d398-1a60-4beb-86cc-648abbec7a56" Name="Message" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="470ddc9e-4715-00fa-5000-0bb9f4033162" Name="pk_KrUniversalTaskOptions">
		<SchemeIndexedColumn Column="470ddc9e-4715-00fa-3100-0bb9f4033162" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="470ddc9e-4715-00fa-7000-0bb9f4033162" Name="idx_KrUniversalTaskOptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="470ddc9e-4715-01fa-4000-0bb9f4033162" />
	</SchemeIndex>
</SchemeTable>