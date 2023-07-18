<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="1afa15c7-ca17-4fa9-bfe5-3ca066814247" Name="KrChangeStateAction" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия ВСмена состояния</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1afa15c7-ca17-00a9-2000-0ca066814247" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1afa15c7-ca17-01a9-4000-0ca066814247" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a56ce78c-1d1e-44da-8504-26ab1f5476ee" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a56ce78c-1d1e-00da-4000-06ab1f5476ee" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="adbc6c9e-623d-415b-afa2-fad6c5f029b8" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1afa15c7-ca17-00a9-5000-0ca066814247" Name="pk_KrChangeStateAction" IsClustered="true">
		<SchemeIndexedColumn Column="1afa15c7-ca17-01a9-4000-0ca066814247" />
	</SchemePrimaryKey>
</SchemeTable>