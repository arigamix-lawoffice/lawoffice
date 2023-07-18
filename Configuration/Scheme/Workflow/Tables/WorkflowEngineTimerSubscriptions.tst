<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="9c65ad25-7d88-4e7c-b398-26d45d1d7204" Name="WorkflowEngineTimerSubscriptions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица с подписками таймеров</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9c65ad25-7d88-007c-2000-06d45d1d7204" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9c65ad25-7d88-017c-4000-06d45d1d7204" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9c65ad25-7d88-007c-3100-06d45d1d7204" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="aab99489-95af-4757-8a10-e33170335d5c" Name="Node" Type="Reference(Typified) Not Null" ReferencedTable="69f72d3a-97c1-4d67-a348-071ab861b3c7">
		<Description>ID экземпляра узла</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="aab99489-95af-0057-4000-033170335d5c" Name="NodeRowID" Type="Guid Not Null" ReferencedColumn="69f72d3a-97c1-0067-3100-071ab861b3c7" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="35c60e16-d4f6-4f9f-b59a-f54449f92d0e" Name="Period" Type="Int32 Null" />
	<SchemePhysicalColumn ID="ceecbc8f-4e3c-4aad-9e74-331c71499ddd" Name="Cron" Type="String(128) Null" />
	<SchemePhysicalColumn ID="bc85a659-54a9-4003-ad17-db905efa60a4" Name="Date" Type="DateTime Null" />
	<SchemePhysicalColumn ID="0320ff31-0982-4cb2-88b1-692e71180226" Name="RunOnce" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2f370ae8-d459-4aee-893c-2c82dcd46352" Name="df_WorkflowEngineTimerSubscriptions_RunOnce" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3c015396-e0a2-482e-a145-f696966f54a1" Name="Modified" Type="DateTime Not Null">
		<Description>Дата изменения таймера</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9c65ad25-7d88-007c-5000-06d45d1d7204" Name="pk_WorkflowEngineTimerSubscriptions">
		<SchemeIndexedColumn Column="9c65ad25-7d88-007c-3100-06d45d1d7204" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9c65ad25-7d88-007c-7000-06d45d1d7204" Name="idx_WorkflowEngineTimerSubscriptions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9c65ad25-7d88-017c-4000-06d45d1d7204" />
	</SchemeIndex>
	<SchemeIndex ID="5e165188-07ad-4d05-a732-f98a4f75b23c" Name="ndx_WorkflowEngineTimerSubscriptions_Modified">
		<SchemeIndexedColumn Column="3c015396-e0a2-482e-a145-f696966f54a1" />
	</SchemeIndex>
	<SchemeIndex ID="52990720-b824-4f8d-81f5-3dc123e2c9d9" Name="ndx_WorkflowEngineTimerSubscriptions_NodeRowID">
		<SchemeIndexedColumn Column="aab99489-95af-0057-4000-033170335d5c" />
	</SchemeIndex>
</SchemeTable>