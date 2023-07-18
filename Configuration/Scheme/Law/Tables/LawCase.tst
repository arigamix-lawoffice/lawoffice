<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="191308f6-820e-408e-b779-cef96b1b09c0" Name="LawCase" Group="Law" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="191308f6-820e-008e-2000-0ef96b1b09c0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="191308f6-820e-018e-4000-0ef96b1b09c0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="704fbf8c-77f2-4aee-a93a-ee54265a88f1" Name="ClassificationPlan" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="704fbf8c-77f2-00ee-4000-0e54265a88f1" Name="ClassificationPlanID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="e9d745fe-cd0e-484b-b375-14f635766393" Name="ClassificationPlanPlan" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="0bcf9aaa-122a-4b51-891c-6d786457602d" Name="ClassificationPlanName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="bdcaf64d-d834-4c13-a4b7-6776f893aca3" Name="ClassificationPlanFullName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="5f5b25fb-3ad4-4d17-9053-c0ee6519c1a0" Name="NumberByCourt" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="02bbe696-3576-4270-bb98-5adff7a9d962" Name="df_LawCase_NumberByCourt" Value="" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="93ae1ba9-9093-43dd-8b5b-ab00bbc185b9" Name="Location" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="93ae1ba9-9093-00dd-4000-0b00bbc185b9" Name="LocationID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="ac4795d3-6ff8-4393-a1f1-392ac826ce09" Name="LocationName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f00c5047-fc39-48c4-97cb-473ca647b0f6" Name="Category" Type="Reference(Abstract) Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f00c5047-fc39-00c4-4000-073ca647b0f6" Name="CategoryID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="32167dc2-9ac4-49fe-9e4c-b389242c205f" Name="CategoryName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d3908bdc-725e-4dfe-943b-6902e08d49e1" Name="Number" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="dad4c667-d9f7-4633-94bc-fcf19a22a5da" Name="Date" Type="Date Not Null" />
	<SchemePhysicalColumn ID="3bb156fc-a2f4-4787-ae5e-52dc028a8922" Name="DecisionDate" Type="Date Null" />
	<SchemePhysicalColumn ID="45902ed1-0721-413b-9943-420c1a12da6e" Name="PCTO" Type="Decimal(18, 2) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e90e2d2a-57a6-431f-b82b-e578918438d2" Name="df_LawCase_PCTO" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="20713868-c886-45d5-b7dd-0ac1d862ad77" Name="IsLimitedAccess" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0aa0dd30-52c3-45e1-a1c6-80725c81c229" Name="df_LawCase_IsLimitedAccess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="700ff3a2-8643-4e1b-b6f4-a08d64fbb927" Name="IsArchive" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9ff0f6e3-f175-4f5d-95f1-37fa5b39fa71" Name="df_LawCase_IsArchive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2241b7f0-7160-4549-bda4-3c3780c269eb" Name="Description" Type="String(400) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d890eba5-5f47-419d-85f2-740bf32ec06e" Name="df_LawCase_Description" Value="" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="191308f6-820e-008e-5000-0ef96b1b09c0" Name="pk_LawCase" IsClustered="true">
		<SchemeIndexedColumn Column="191308f6-820e-018e-4000-0ef96b1b09c0" />
	</SchemePrimaryKey>
</SchemeTable>