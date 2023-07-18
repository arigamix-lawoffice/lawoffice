<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="fde6b6e3-f7b6-467f-96e1-e2df41a22f05" Name="KrStageGroups" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fde6b6e3-f7b6-007f-2000-02df41a22f05" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fde6b6e3-f7b6-017f-4000-02df41a22f05" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fc8faabd-cc86-44b3-8430-1a0e816cea27" Name="Name" Type="String(255) Not Null" />
	<SchemePhysicalColumn ID="2adc13ed-565d-4506-a412-8831c326a119" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="c0efde58-5361-41ce-b772-5cd81c1d099c" Name="IsGroupReadonly" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="f14e43cf-b12a-4198-bb02-29d3feee825f" Name="df_KrStageGroups_IsGroupReadonly" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e8739583-d3d3-4181-b14e-e3261fc97cc9" Name="SourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="70105515-26b1-4987-82fc-08ace17f14d6" Name="SourceBefore" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="9d34a271-a3de-46f2-ba0b-83f265e25887" Name="SourceAfter" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="188fdcbc-3bf6-4b30-928d-1b09a315f519" Name="RuntimeSourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="680aa8d5-9061-4bfd-974a-9b5d0da08bd5" Name="RuntimeSourceBefore" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c03055fb-2682-4a12-b330-b4b990322bdd" Name="RuntimeSourceAfter" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="3b41c23b-980a-46c5-8897-2ddf0ca2d744" Name="SqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="228c04f1-0a3e-4a93-9dc9-f7527895e137" Name="RuntimeSqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="c97f1870-c197-414b-a01f-837442881b23" Name="Description" Type="String(Max) Null" />
	<SchemeComplexColumn ID="8cc354ef-ea10-4408-9b62-ae7da619fb99" Name="KrSecondaryProcess" Type="Reference(Typified) Null" ReferencedTable="caac66aa-0cbb-4e2b-83fd-7c368e814d64" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8cc354ef-ea10-0008-4000-0e7da619fb99" Name="KrSecondaryProcessID" Type="Guid Null" ReferencedColumn="caac66aa-0cbb-012b-4000-0c368e814d64" />
		<SchemeReferencingColumn ID="e3f19840-ca41-400c-9579-c66a06f30704" Name="KrSecondaryProcessName" Type="String(255) Null" ReferencedColumn="444b8925-572a-449b-901e-8660ddeb3b6c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="81836aa7-65fd-44eb-9ddf-ecc5acab7fef" Name="Ignore" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2e1a623b-9d09-485d-983a-61c54188e7d5" Name="df_KrStageGroups_Ignore" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fde6b6e3-f7b6-007f-5000-02df41a22f05" Name="pk_KrStageGroups" IsClustered="true">
		<SchemeIndexedColumn Column="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
	</SchemePrimaryKey>
	<SchemeIndex ID="914135e6-3e1b-4a38-b401-dfd131c317c4" Name="ndx_KrStageGroups_KrProcessButtonIDID" IsUnique="true">
		<SchemeIndexedColumn Column="8cc354ef-ea10-0008-4000-0e7da619fb99" />
		<SchemeIndexedColumn Column="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
	</SchemeIndex>
	<SchemeIndex ID="bd04c01d-81f5-4826-83f8-55b2635d8aeb" Name="ndx_KrStageGroups_Name" IsUnique="true">
		<SchemeIndexedColumn Column="fc8faabd-cc86-44b3-8430-1a0e816cea27">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeIndex ID="878af1a3-5981-4ce9-8c3b-3b2ae57f6b36" Name="ndx_KrStageGroups_Order">
		<SchemeIndexedColumn Column="2adc13ed-565d-4506-a412-8831c326a119" SortOrder="Ascending" />
	</SchemeIndex>
</SchemeTable>