<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="bbdef4f8-22b0-4075-83f2-1c6e89d1ba7b" Name="KrRouteInitializationActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры действия "Инициализация маршрута".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="bbdef4f8-22b0-0075-2000-0c6e89d1ba7b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bbdef4f8-22b0-0175-4000-0c6e89d1ba7b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0b1dc4ca-a1be-486b-918e-2c7d6ebad17f" Name="Initiator" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Инициатор процесса.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0b1dc4ca-a1be-006b-4000-0c7d6ebad17f" Name="InitiatorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="3e2ef921-5645-42e3-98a1-2fe6bcacbee9" Name="InitiatorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="736025ab-c92b-4c3c-92ff-f28d8a538d19" Name="InitiatorComment" Type="String(Max) Null">
		<Description>Комментарий инициатора процесса.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="bbdef4f8-22b0-0075-5000-0c6e89d1ba7b" Name="pk_KrRouteInitializationActionVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="bbdef4f8-22b0-0175-4000-0c6e89d1ba7b" />
	</SchemePrimaryKey>
</SchemeTable>