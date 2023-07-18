<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="204bfce7-5a88-4586-90e5-36d69e5b39fa" Name="KrDocStateCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условия для правил уведомлений, првоеряющая состояния.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="204bfce7-5a88-0086-2000-06d69e5b39fa" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="204bfce7-5a88-0186-4000-06d69e5b39fa" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="204bfce7-5a88-0086-3100-06d69e5b39fa" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="43381874-c47e-4b4c-87c6-a8676f877014" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="43381874-c47e-004c-4000-08676f877014" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="d82d0ba1-5111-4271-bb7e-bbcc54148698" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="204bfce7-5a88-0086-5000-06d69e5b39fa" Name="pk_KrDocStateCondition">
		<SchemeIndexedColumn Column="204bfce7-5a88-0086-3100-06d69e5b39fa" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="204bfce7-5a88-0086-7000-06d69e5b39fa" Name="idx_KrDocStateCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="204bfce7-5a88-0186-4000-06d69e5b39fa" />
	</SchemeIndex>
</SchemeTable>