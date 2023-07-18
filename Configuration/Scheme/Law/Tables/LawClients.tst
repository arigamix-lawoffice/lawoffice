<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="362c9171-6267-42a8-8fd8-7bf39d04533e" Name="LawClients" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="362c9171-6267-00a8-2000-0bf39d04533e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="362c9171-6267-01a8-4000-0bf39d04533e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="362c9171-6267-00a8-3100-0bf39d04533e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="290a3f6f-cb42-4706-b57e-f17f58dbe054" Name="Client" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="290a3f6f-cb42-0006-4000-017f58dbe054" Name="ClientID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="82ddf170-5b94-4850-8ab9-6e61a49899ad" Name="ClientName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="362c9171-6267-00a8-5000-0bf39d04533e" Name="pk_LawClients">
		<SchemeIndexedColumn Column="362c9171-6267-00a8-3100-0bf39d04533e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="362c9171-6267-00a8-7000-0bf39d04533e" Name="idx_LawClients_ID" IsClustered="true">
		<SchemeIndexedColumn Column="362c9171-6267-01a8-4000-0bf39d04533e" />
	</SchemeIndex>
</SchemeTable>