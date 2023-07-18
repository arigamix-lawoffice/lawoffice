<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="91c272de-462d-4076-8f64-592885a4abd4" Name="ProtocolDecisions" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Решения по протоколу.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="91c272de-462d-0076-2000-092885a4abd4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="91c272de-462d-0176-4000-092885a4abd4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="91c272de-462d-0076-3100-092885a4abd4" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="37a3141c-1793-4245-b13f-b620e6787607" Name="Question" Type="String(Max) Not Null">
		<Description>Вопрос</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f619ae7e-e63c-48c9-85e6-6402fb1f7dd4" Name="Planned" Type="Date Null">
		<Description>Срок</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6baf26e7-c359-4e8b-85af-39e317972fc3" Name="Order" Type="Int32 Not Null">
		<Description>Порядок решений в списке</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f7c22a64-34fa-444c-a1eb-e4f9dd3e8a83" Name="df_ProtocolDecisions_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="91c272de-462d-0076-5000-092885a4abd4" Name="pk_ProtocolDecisions">
		<SchemeIndexedColumn Column="91c272de-462d-0076-3100-092885a4abd4" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="91c272de-462d-0076-7000-092885a4abd4" Name="idx_ProtocolDecisions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="91c272de-462d-0176-4000-092885a4abd4" />
	</SchemeIndex>
</SchemeTable>