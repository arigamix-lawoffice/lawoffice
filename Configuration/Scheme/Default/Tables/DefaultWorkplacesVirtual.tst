<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="dd42ee04-02c5-407b-b596-07aa830a9b80" Name="DefaultWorkplacesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список рабочих мест открываемых по умолчанию</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd42ee04-02c5-007b-2000-07aa830a9b80" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dd42ee04-02c5-017b-4000-07aa830a9b80" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd42ee04-02c5-007b-3100-07aa830a9b80" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="1ba42a4e-900e-4c4a-9a3c-13515e7be401" Name="Workplace" Type="Reference(Typified) Not Null" ReferencedTable="21cd7a4f-6930-4746-9a57-72481e951b02">
		<Description>Рабочее место</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1ba42a4e-900e-004a-4000-03515e7be401" Name="WorkplaceID" Type="Guid Not Null" ReferencedColumn="39f02b29-1a58-4409-a8fa-11756a2870f4" />
		<SchemeReferencingColumn ID="8193dd66-7bf9-413a-8ac1-2107787ee6ac" Name="WorkplaceName" Type="String(128) Not Null" ReferencedColumn="60d1e6ca-06ac-4881-8048-8cc1c4459b95" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a2aa3fef-1e04-4960-821a-e3c3beb969b8" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер РМ</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd42ee04-02c5-007b-5000-07aa830a9b80" Name="pk_DefaultWorkplacesVirtual">
		<SchemeIndexedColumn Column="dd42ee04-02c5-007b-3100-07aa830a9b80" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dd42ee04-02c5-007b-7000-07aa830a9b80" Name="idx_DefaultWorkplacesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dd42ee04-02c5-017b-4000-07aa830a9b80" />
	</SchemeIndex>
</SchemeTable>