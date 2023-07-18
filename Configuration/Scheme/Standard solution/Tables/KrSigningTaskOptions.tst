<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0ad2b029-2f30-4e19-96df-bc3c2dcd9dfe" Name="KrSigningTaskOptions" Group="KrStageTypes" InstanceType="Tasks" ContentType="Entries">
	<Description>Таблица с параметрами задания "Подписание".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0ad2b029-2f30-0019-2000-0c3c2dcd9dfe" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0ad2b029-2f30-0119-4000-0c3c2dcd9dfe" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b3d9cde7-8208-4deb-a91f-7920144bd701" Name="AllowAdditionalApproval" Type="Boolean Not Null">
		<Description>Признак того, что разрешено дополнительное согласование.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0ad2b029-2f30-0019-5000-0c3c2dcd9dfe" Name="pk_KrSigningTaskOptions" IsClustered="true">
		<SchemeIndexedColumn Column="0ad2b029-2f30-0119-4000-0c3c2dcd9dfe" />
	</SchemePrimaryKey>
</SchemeTable>