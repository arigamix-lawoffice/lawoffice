<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="89d78d5c-f8dd-48e7-868c-88bbafe74257" Name="KrStagesVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="89d78d5c-f8dd-00e7-2000-08bbafe74257" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="89d78d5c-f8dd-01e7-4000-08bbafe74257" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="89d78d5c-f8dd-00e7-3100-08bbafe74257" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="c75e99b9-0520-494b-a059-385c279a46d4" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="fe3e48ce-4c4a-40fb-bc22-ebea2b84fe9a" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3bb158f9-2460-4327-803c-aa093d45ef0c" Name="df_KrStagesVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="37023800-2213-4400-aed9-e590955d6276" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="beee4f3d-a385-4fc8-884f-bc1ccf55fc5b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="37023800-2213-0000-4000-0590955d6276" Name="StateID" Type="Int16 Not Null" ReferencedColumn="a4844cd9-4328-48d8-8f37-acdd6bce5ffe" />
		<SchemeReferencingColumn ID="3afb99e2-b0ef-4366-a29a-b0df112bb030" Name="StateName" Type="String(128) Not Null" ReferencedColumn="fed52724-582f-49a9-8a3a-9cc2af1c5109" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c9665a0f-132b-4bb0-a6a5-742ce77595c8" Name="TimeLimit" Type="Double Not Null">
		<Description>Время на одно задание</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="fc237640-ec4c-4498-a604-18f1fbc581db" Name="df_KrStagesVirtual_TimeLimit" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cdf57551-e911-4033-bd32-05e72ac7e3fe" Name="SqlApproverRole" Type="String(Max) Null">
		<Description>SQL запрос для шаблона этапов, необходим для расчета ролей при пересчете этапов</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="05237ee6-09fb-4016-ba58-72bd8f2716b3" Name="RowChanged" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="664c6235-9676-4e06-961e-eafd9b22977e" Name="df_KrStagesVirtual_RowChanged" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2fcc9653-c1c7-42e7-aa3e-d8d92cb960c5" Name="OrderChanged" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="724aa8ab-37ae-4537-a7e1-60a46182f7f2" Name="df_KrStagesVirtual_OrderChanged" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="eb75dc63-c502-4378-8360-263047e37e15" Name="BasedOnStage" Type="Reference(Typified) Null" ReferencedTable="92caadca-2409-40ff-b7d8-1d4fd302b1e9" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="eb75dc63-c502-0078-4000-063047e37e15" Name="BasedOnStageRowID" Type="Guid Null" ReferencedColumn="92caadca-2409-00ff-3100-0d4fd302b1e9">
			<Description>RowID этапа в шаблоне этапов. По нему можно восстановить весь шаблон этапов целиком</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3930aa32-0f4f-4053-9b25-57c6bc750b9c" Name="BasedOnStageTemplate" Type="Reference(Typified) Null" ReferencedTable="5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3930aa32-0f4f-0053-4000-07c6bc750b9c" Name="BasedOnStageTemplateID" Type="Guid Null" ReferencedColumn="5a33ac72-f6f5-015a-4000-0a94ed7bf324" />
		<SchemeReferencingColumn ID="e6a50547-c940-4979-9b45-ed3a716bc8d1" Name="BasedOnStageTemplateName" Type="String(255) Null" ReferencedColumn="65776ea1-97aa-48db-a170-8cb5d4eed2bc" />
		<SchemePhysicalColumn ID="63143193-cc7c-4c15-a562-e97154378870" Name="BasedOnStageTemplateOrder" Type="Int32 Null" />
		<SchemePhysicalColumn ID="da4bf34a-2b35-4d40-9124-95d4f27e12cd" Name="BasedOnStageTemplateGroupPositionID" Type="Int16 Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d23b7889-cbfd-4558-b922-1fd6041eeb79" Name="StageTypeID" Type="Guid Null" />
	<SchemePhysicalColumn ID="60af4ff3-5cd3-42e9-ae55-07e0bf2f02c8" Name="StageTypeCaption" Type="String(255) Null" />
	<SchemePhysicalColumn ID="4b263a15-13a6-4707-9edf-ef0038a26264" Name="DisplayTimeLimit" Type="String(Max) Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1fe7fb4e-b4de-4da4-8478-3bdff20b6c83" Name="df_KrStagesVirtual_DisplayTimeLimit" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7741a2e9-d1f6-45a6-abd9-38b1b54030c4" Name="DisplayParticipants" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="24b3c271-4a84-4574-84c5-feb430d10d49" Name="df_KrStagesVirtual_DisplayParticipants" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="77e4b130-e328-44f5-b852-405703c153b2" Name="DisplaySettings" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0a2f2546-650f-4c57-9fe9-cc801d71895d" Name="df_KrStagesVirtual_DisplaySettings" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b9de66b3-0d23-4dd3-922d-43a9e3efbd92" Name="RuntimeSourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="6f0af8b1-8895-4759-9898-b7e77779d883" Name="RuntimeSourceBefore" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="5a338a8b-0691-4310-92ec-4f9bd4d0a2eb" Name="RuntimeSourceAfter" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="c7cfe026-a509-4a25-a588-c8ec827733e2" Name="StageGroup" Type="Reference(Typified) Null" ReferencedTable="fde6b6e3-f7b6-467f-96e1-e2df41a22f05">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c7cfe026-a509-0025-4000-08ec827733e2" Name="StageGroupID" Type="Guid Null" ReferencedColumn="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
		<SchemeReferencingColumn ID="e1889fc8-1d77-470b-8945-5f4a6f82be64" Name="StageGroupName" Type="String(255) Null" ReferencedColumn="fc8faabd-cc86-44b3-8430-1a0e816cea27" />
		<SchemeReferencingColumn ID="edf5fa11-3293-4e17-ab4f-2af7288b2d57" Name="StageGroupOrder" Type="Int32 Null" ReferencedColumn="2adc13ed-565d-4506-a412-8831c326a119" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f2e5ffc2-30b3-466b-a65b-007ea43164ed" Name="RuntimeSqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="472220ae-2e4f-4686-9ada-1a922a7c6cae" Name="Hidden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e8468583-b5a3-4564-92cc-82ce88c6a879" Name="df_KrStagesVirtual_Hidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="440c0fc4-cda2-4155-885a-13ed88bb00c1" Name="Planned" Type="DateTime Null" />
	<SchemePhysicalColumn ID="f49be836-2564-4551-a5b0-4be739cf41a1" Name="Skip" Type="Boolean Not Null">
		<Description>Признак пропуска этапа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2dd757d9-5a43-4cdc-955c-f973131524e5" Name="df_KrStagesVirtual_Skip" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f03abe87-d82c-44bc-a689-348c6f27b310" Name="CanBeSkipped" Type="Boolean Not Null">
		<Description>Флаг, показывающий, разрешен ли пропуск этапа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="702bc3b0-acdf-440f-ad5a-2426ba6e98f6" Name="df_KrStagesVirtual_CanBeSkipped" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="89d78d5c-f8dd-00e7-5000-08bbafe74257" Name="pk_KrStagesVirtual">
		<SchemeIndexedColumn Column="89d78d5c-f8dd-00e7-3100-08bbafe74257" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="89d78d5c-f8dd-00e7-7000-08bbafe74257" Name="idx_KrStagesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="89d78d5c-f8dd-01e7-4000-08bbafe74257" />
	</SchemeIndex>
</SchemeTable>