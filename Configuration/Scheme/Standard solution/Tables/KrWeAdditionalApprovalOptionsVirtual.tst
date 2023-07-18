<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="54829879-8b8e-4d47-a27b-0346e93e6e45" Name="KrWeAdditionalApprovalOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры дополнительного согласования.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="54829879-8b8e-0047-2000-0346e93e6e45" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="54829879-8b8e-0147-4000-0346e93e6e45" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f951576c-661d-44cb-8a17-4c5d713f4461" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление о задании. Тип уведомления.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f951576c-661d-00cb-4000-0c5d713f4461" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="bef415df-0345-47d3-830a-15ce23af3699" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="73073275-1045-45b2-986e-fd8dbf0b6a0e" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="30892ff6-637b-4327-aa8f-2c0d75aa3959" Name="df_KrWeAdditionalApprovalOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6c29ba43-46c5-4f5d-8f7f-4c0677495a03" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Уведомление о задании. Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6412d51d-0d7c-49b9-b6d4-a0dba289b942" Name="df_KrWeAdditionalApprovalOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bae4fc0a-b01a-4f5d-8f75-4a0465398ece" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Уведомление о задании. Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="09e3c4cd-32ae-41bf-ac61-1cb26d9bb731" Name="InitTaskScript" Type="String(Max) Not Null">
		<Description>Сценарий инициализации задания.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="54829879-8b8e-0047-5000-0346e93e6e45" Name="pk_KrWeAdditionalApprovalOptionsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="54829879-8b8e-0147-4000-0346e93e6e45" />
	</SchemePrimaryKey>
</SchemeTable>