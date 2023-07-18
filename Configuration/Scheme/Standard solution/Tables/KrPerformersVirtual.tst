<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b47d668e-7bf0-4165-a10c-6fe22ee10882" Name="KrPerformersVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b47d668e-7bf0-0065-2000-0fe22ee10882" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b47d668e-7bf0-0165-4000-0fe22ee10882" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b47d668e-7bf0-0065-3100-0fe22ee10882" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="31932138-ddd4-465f-aca0-d27afb395516" Name="Performer" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31932138-ddd4-005f-4000-027afb395516" Name="PerformerID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="eb33d30f-a8ee-4f28-aa1d-02a3b0faf2bd" Name="PerformerName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9f79210b-104e-4a99-898f-0df27a23d8f5" Name="Stage" Type="Reference(Typified) Not Null" ReferencedTable="89d78d5c-f8dd-48e7-868c-88bbafe74257" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9f79210b-104e-0099-4000-0df27a23d8f5" Name="StageRowID" Type="Guid Not Null" ReferencedColumn="89d78d5c-f8dd-00e7-3100-08bbafe74257" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="3563cacf-718a-43ed-ad1d-837a4d59f0b2" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0b7f624f-fc47-4e60-8663-9b94d345b493" Name="df_KrPerformersVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3b9c9ce2-6130-47e3-a1b3-128d70d9959f" Name="SQLApprover" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9d5ae088-f082-40e8-a3f9-248e5e0e7a82" Name="df_KrPerformersVirtual_SQLApprover" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b47d668e-7bf0-0065-5000-0fe22ee10882" Name="pk_KrPerformersVirtual">
		<SchemeIndexedColumn Column="b47d668e-7bf0-0065-3100-0fe22ee10882" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b47d668e-7bf0-0065-7000-0fe22ee10882" Name="idx_KrPerformersVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b47d668e-7bf0-0165-4000-0fe22ee10882" />
	</SchemeIndex>
</SchemeTable>