<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="dd36aad9-d17a-41ad-b854-22f886819d28" Name="KrSigningActionOptionLinksVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Подписание". Коллекционная секция объединяющая связи и вырианты завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd36aad9-d17a-00ad-2000-02f886819d28" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dd36aad9-d17a-01ad-4000-02f886819d28" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd36aad9-d17a-00ad-3100-02f886819d28" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5962fd03-9687-4241-b2a5-9c377f384319" Name="Link" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5962fd03-9687-0041-4000-0c377f384319" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="99b1b43f-1990-4f24-a132-b5b83aef3053" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="c55eb354-458f-4dfa-9972-6e880b4a306b" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8b1a0985-d4cb-43bd-9ce6-52643c8c45f2" Name="ActionOption" Type="Reference(Typified) Not Null" ReferencedTable="b4c6c410-c5cb-40e3-b800-3cd854c94a2c" IsReferenceToOwner="true">
		<Description>Параметры обработки завершения действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8b1a0985-d4cb-00bd-4000-02643c8c45f2" Name="ActionOptionRowID" Type="Guid Not Null" ReferencedColumn="b4c6c410-c5cb-00e3-3100-0cd854c94a2c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd36aad9-d17a-00ad-5000-02f886819d28" Name="pk_KrSigningActionOptionLinksVirtual">
		<SchemeIndexedColumn Column="dd36aad9-d17a-00ad-3100-02f886819d28" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd36aad9-d17a-00ad-7000-02f886819d28" Name="idx_KrSigningActionOptionLinksVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dd36aad9-d17a-01ad-4000-02f886819d28" />
	</SchemeIndex>
</SchemeTable>