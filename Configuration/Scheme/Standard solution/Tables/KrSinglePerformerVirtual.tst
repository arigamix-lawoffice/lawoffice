<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="52b86f8c-bc19-4dee-8e53-54236bf951a6" Name="KrSinglePerformerVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="52b86f8c-bc19-00ee-2000-04236bf951a6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="52b86f8c-bc19-01ee-4000-04236bf951a6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="696d6497-fce8-494e-9262-0e74d9712d93" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="696d6497-fce8-004e-4000-0e74d9712d93" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="97512b1a-fcbb-47d3-bb92-3bbdab06cdbc" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="52b86f8c-bc19-00ee-5000-04236bf951a6" Name="pk_KrSinglePerformerVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="52b86f8c-bc19-01ee-4000-04236bf951a6" />
	</SchemePrimaryKey>
</SchemeTable>