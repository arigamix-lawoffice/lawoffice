<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="82f81a44-b515-4187-88c9-03a59e086031" Name="KrPartnerCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условия для правил уведомлений, првоеряющая контрагента.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="82f81a44-b515-0087-2000-03a59e086031" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="82f81a44-b515-0187-4000-03a59e086031" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="82f81a44-b515-0087-3100-03a59e086031" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1860a47a-8dfa-44d1-985b-4e6abf723965" Name="Partner" Type="Reference(Typified) Not Null" ReferencedTable="5d47ef13-b6f4-47ef-9815-3b3d0e6d475a">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1860a47a-8dfa-00d1-4000-0e6abf723965" Name="PartnerID" Type="Guid Not Null" ReferencedColumn="5d47ef13-b6f4-01ef-4000-0b3d0e6d475a" />
		<SchemeReferencingColumn ID="e365ab9b-372b-4b50-84b0-2214f2b869e6" Name="PartnerName" Type="String(255) Not Null" ReferencedColumn="f1c960e0-951e-4837-8474-bb61d98f40f0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="82f81a44-b515-0087-5000-03a59e086031" Name="pk_KrPartnerCondition">
		<SchemeIndexedColumn Column="82f81a44-b515-0087-3100-03a59e086031" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="82f81a44-b515-0087-7000-03a59e086031" Name="idx_KrPartnerCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="82f81a44-b515-0187-4000-03a59e086031" />
	</SchemeIndex>
</SchemeTable>