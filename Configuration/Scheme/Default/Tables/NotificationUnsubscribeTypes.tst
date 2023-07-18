<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d845a7f8-9873-47c1-a160-370f66dc852e" Name="NotificationUnsubscribeTypes" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d845a7f8-9873-00c1-2000-070f66dc852e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d845a7f8-9873-01c1-4000-070f66dc852e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d845a7f8-9873-00c1-3100-070f66dc852e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="9eba84dd-5613-4a2d-a695-8acd9d2cf51f" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9eba84dd-5613-002d-4000-0acd9d2cf51f" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="a460dfa4-bfe2-4d22-bc3a-690abf811d1e" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d845a7f8-9873-00c1-5000-070f66dc852e" Name="pk_NotificationUnsubscribeTypes">
		<SchemeIndexedColumn Column="d845a7f8-9873-00c1-3100-070f66dc852e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="d845a7f8-9873-00c1-7000-070f66dc852e" Name="idx_NotificationUnsubscribeTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="d845a7f8-9873-01c1-4000-070f66dc852e" />
	</SchemeIndex>
</SchemeTable>