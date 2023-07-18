<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="92caadca-2409-40ff-b7d8-1d4fd302b1e9" Name="KrStages" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="92caadca-2409-00ff-2000-0d4fd302b1e9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="92caadca-2409-01ff-4000-0d4fd302b1e9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="92caadca-2409-00ff-3100-0d4fd302b1e9" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="95ac1ae3-f232-47d5-b7bf-2d012f6117db" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="57befa39-9e72-49ed-a4ff-059ae26c42bb" Name="Order" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e55056d4-c33b-428c-9bf7-1e1720c2cf17" Name="df_KrStages_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="de80d9b2-cc8e-47b7-b76e-d589b92e193d" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="beee4f3d-a385-4fc8-884f-bc1ccf55fc5b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de80d9b2-cc8e-00b7-4000-0589b92e193d" Name="StateID" Type="Int16 Not Null" ReferencedColumn="a4844cd9-4328-48d8-8f37-acdd6bce5ffe" />
		<SchemeReferencingColumn ID="ce72d426-3f63-4653-bc0b-4f759ad6032f" Name="StateName" Type="String(128) Not Null" ReferencedColumn="fed52724-582f-49a9-8a3a-9cc2af1c5109" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e8579807-f880-4ca9-bf69-ac02247c6631" Name="TimeLimit" Type="Double Null">
		<Description>Время на одно задание</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7e95c220-cd46-43a3-bd85-b7f995b683cd" Name="df_KrStages_TimeLimit" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="449c8a3e-d7a2-45c2-91dc-bd1e9e3ad8fe" Name="SqlApproverRole" Type="String(Max) Null" IsSparse="true">
		<Description>Роль, вычисляемая при подстановке шаблона. Актуально только для KrStageTemplate</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b5a520e0-11e1-483b-8d65-1459dee52e3f" Name="RowChanged" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="170e870c-ac57-497c-b6d2-b60de04ce672" Name="df_KrStages_RowChanged" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="54082ea9-1782-44ce-8765-7ae2c2add533" Name="OrderChanged" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="499e7dc2-5ece-4d8a-b749-7c1e40797767" Name="df_KrStages_OrderChanged" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d8842207-6b81-438d-9d3d-d552ea94afe9" Name="BasedOnStage" Type="Reference(Typified) Null" ReferencedTable="92caadca-2409-40ff-b7d8-1d4fd302b1e9" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d8842207-6b81-008d-4000-0552ea94afe9" Name="BasedOnStageRowID" Type="Guid Null" ReferencedColumn="92caadca-2409-00ff-3100-0d4fd302b1e9">
			<Description>RowID этапа в шаблоне этапов. По нему можно восстановить весь шаблон этапов целиком</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="67485a00-3665-44c3-a114-33002c668821" Name="BasedOnStageTemplate" Type="Reference(Typified) Null" ReferencedTable="5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="67485a00-3665-00c3-4000-03002c668821" Name="BasedOnStageTemplateID" Type="Guid Null" ReferencedColumn="5a33ac72-f6f5-015a-4000-0a94ed7bf324" />
		<SchemeReferencingColumn ID="2633467d-d494-4675-8217-3e477d66b43f" Name="BasedOnStageTemplateName" Type="String(255) Null" ReferencedColumn="65776ea1-97aa-48db-a170-8cb5d4eed2bc" />
		<SchemePhysicalColumn ID="504ee764-3306-427b-8e68-9f4dd1462b65" Name="BasedOnStageTemplateOrder" Type="Int32 Null" />
		<SchemePhysicalColumn ID="26afdf25-1025-4a36-b113-7e0a210a6aec" Name="BasedOnStageTemplateGroupPositionID" Type="Int16 Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="15525fce-0321-4801-a613-d28d6d1334b3" Name="StageTypeID" Type="Guid Null" />
	<SchemePhysicalColumn ID="f45c39e1-c651-4bb8-94a7-bdeb845525de" Name="StageTypeCaption" Type="String(255) Null" />
	<SchemePhysicalColumn ID="d29eec6b-b54f-41d8-82e1-79df0a4d6f53" Name="DisplayTimeLimit" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1009ad7b-03b2-45b2-bce0-242e372f8f4f" Name="df_KrStages_DisplayTimeLimit" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="105d6779-1ba2-4fc7-9e88-2ad471a5d27e" Name="DisplayParticipants" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="255e3ab0-301c-436f-8eea-515cc981bc11" Name="df_KrStages_DisplayParticipants" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6619ed46-67b0-426c-b70a-6970e9fc8a3d" Name="DisplaySettings" Type="String(Max) Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="592d8124-d7ec-400b-b427-493a733f7e33" Name="df_KrStages_DisplaySettings" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bcfd96e2-4d32-4531-87a5-a8eb1efaa44e" Name="Settings" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="ee0aba25-4456-4fcc-80fd-dbfdf36b97ca" Name="Info" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="d8f69dec-346e-449b-8766-18e5137ea1bf" Name="RuntimeSourceCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="2bdd89c1-f88b-4070-ae90-d99362f8bcdc" Name="RuntimeSourceBefore" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="ea4e71e8-a652-44c4-9522-220bff3537af" Name="RuntimeSourceAfter" Type="String(Max) Null" />
	<SchemeComplexColumn ID="708d5117-455f-483b-be52-0457b764e4c0" Name="StageGroup" Type="Reference(Typified) Null" ReferencedTable="fde6b6e3-f7b6-467f-96e1-e2df41a22f05" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="708d5117-455f-003b-4000-0457b764e4c0" Name="StageGroupID" Type="Guid Null" ReferencedColumn="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
		<SchemeReferencingColumn ID="2dc38596-0788-4a56-8002-d162253db3f5" Name="StageGroupOrder" Type="Int32 Null" ReferencedColumn="2adc13ed-565d-4506-a412-8831c326a119" />
		<SchemeReferencingColumn ID="25a50e0e-2d36-49d6-a691-7673db661fa7" Name="StageGroupName" Type="String(255) Null" ReferencedColumn="fc8faabd-cc86-44b3-8430-1a0e816cea27" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a97d683d-e372-45be-8266-9dc1a9570c96" Name="RuntimeSqlCondition" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="86cd1cc2-10b0-4292-a554-b3547d6b4f4e" Name="Hidden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="30470806-09f6-44ad-9c96-c164d4461f81" Name="df_KrStages_Hidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="80f21c7e-f163-4ad0-834c-4a68929f358a" Name="NestedProcessID" Type="Guid Null" />
	<SchemePhysicalColumn ID="8b57ae05-3f7a-49f7-8d63-dd379c9f02ef" Name="ParentStageRowID" Type="Guid Null" />
	<SchemePhysicalColumn ID="2ca5b633-2268-4650-895e-9b3b2f0cec3e" Name="NestedOrder" Type="Int32 Null" />
	<SchemePhysicalColumn ID="f82da432-3f7f-4ed8-9ab2-4811f78f08f1" Name="ExtraSources" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="573656b2-0bc7-4b5e-ae68-c5f1da720bf3" Name="Planned" Type="DateTime Null" />
	<SchemePhysicalColumn ID="5a83c6f8-8f68-41ca-8c90-8eef318a20f4" Name="Skip" Type="Boolean Not Null">
		<Description>Признак пропуска этапа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ab990b5f-87ca-4e7a-8f46-572a6ee07ced" Name="df_KrStages_Skip" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="41f11b47-0c29-4dab-ad80-59aab1083488" Name="CanBeSkipped" Type="Boolean Not Null">
		<Description>Флаг, показывающий, разрешен ли пропуск этапа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e5ee7f82-6ed5-42a5-9606-132f851748b9" Name="df_KrStages_CanBeSkipped" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="92caadca-2409-00ff-5000-0d4fd302b1e9" Name="pk_KrStages">
		<SchemeIndexedColumn Column="92caadca-2409-00ff-3100-0d4fd302b1e9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="92caadca-2409-00ff-7000-0d4fd302b1e9" Name="idx_KrStages_ID" IsClustered="true">
		<SchemeIndexedColumn Column="92caadca-2409-01ff-4000-0d4fd302b1e9" />
	</SchemeIndex>
	<SchemeIndex ID="9f0311bc-7d32-4547-ba2a-706955a6b0ff" Name="ndx_KrStages_RowID">
		<SchemeIndexedColumn Column="92caadca-2409-00ff-3100-0d4fd302b1e9" />
	</SchemeIndex>
</SchemeTable>