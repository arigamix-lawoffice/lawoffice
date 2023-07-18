<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="fc3ec595-313c-4b5f-aada-07d7d2f34ff2" Name="KrApprovalActionOptionLinksVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Коллекционная секция объединяющая связи и вырианты завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc3ec595-313c-005f-2000-07d7d2f34ff2" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc3ec595-313c-015f-4000-07d7d2f34ff2" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc3ec595-313c-005f-3100-07d7d2f34ff2" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="43442d6e-d9f9-4d3b-bbbf-2fd364c95e22" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="43442d6e-d9f9-003b-4000-0fd364c95e22" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="9692fb06-7729-4793-836a-6eed00679000" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="d4aac66a-e29f-4427-a9c9-af55f7897f61" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="91944b59-308b-46b7-beef-ff729cfb034c" Name="ActionOption" Type="Reference(Typified) Not Null" ReferencedTable="244719bf-4d4a-4df6-b2fe-a00b1bf6d173" IsReferenceToOwner="true">
		<Description>Параметры обработки завершения действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="91944b59-308b-00b7-4000-0f729cfb034c" Name="ActionOptionRowID" Type="Guid Not Null" ReferencedColumn="244719bf-4d4a-00f6-3100-000b1bf6d173" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc3ec595-313c-005f-5000-07d7d2f34ff2" Name="pk_KrApprovalActionOptionLinksVirtual">
		<SchemeIndexedColumn Column="fc3ec595-313c-005f-3100-07d7d2f34ff2" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc3ec595-313c-005f-7000-07d7d2f34ff2" Name="idx_KrApprovalActionOptionLinksVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="fc3ec595-313c-015f-4000-07d7d2f34ff2" />
	</SchemeIndex>
</SchemeTable>