<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="70427d3c-3df8-4efc-8bf7-8e19efa2c20d" Name="KrDepartmentCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция для условия для правил уведомлений, проверяющая Подразделение.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="70427d3c-3df8-00fc-2000-0e19efa2c20d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="70427d3c-3df8-01fc-4000-0e19efa2c20d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="70427d3c-3df8-00fc-3100-0e19efa2c20d" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4783f3f9-fe98-46ab-baf2-2e9d95dca4d3" Name="Department" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4783f3f9-fe98-00ab-4000-0e9d95dca4d3" Name="DepartmentID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="561ce1a5-e2ea-497d-91f8-083b5ca7a80c" Name="DepartmentName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="70427d3c-3df8-00fc-5000-0e19efa2c20d" Name="pk_KrDepartmentCondition">
		<SchemeIndexedColumn Column="70427d3c-3df8-00fc-3100-0e19efa2c20d" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="70427d3c-3df8-00fc-7000-0e19efa2c20d" Name="idx_KrDepartmentCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="70427d3c-3df8-01fc-4000-0e19efa2c20d" />
	</SchemeIndex>
</SchemeTable>