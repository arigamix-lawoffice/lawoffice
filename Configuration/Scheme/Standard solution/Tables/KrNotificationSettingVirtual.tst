<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="28204069-f27e-4b4e-b309-5d2f77dbff8e" Name="KrNotificationSettingVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция настроек этапа Уведомление</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="28204069-f27e-004e-2000-0d2f77dbff8e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="28204069-f27e-014e-4000-0d2f77dbff8e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8ed1b330-a620-4200-9082-3c46ddee26a1" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8ed1b330-a620-0000-4000-0c46ddee26a1" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="3874835f-523e-48c7-bbab-5af0b1677934" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="73cde03d-3058-41c3-9990-41c7c5ed16a6" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="fc0d2cc9-058b-4380-aa85-9a04fc9416fd" Name="df_KrNotificationSettingVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="952faeae-b0ae-4b48-8a0b-a4d9f647413a" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a67275d6-a3bf-456a-8140-bf2047d20e57" Name="df_KrNotificationSettingVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d8647451-ab35-4f94-b80c-56e32f255e7e" Name="EmailModificationScript" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="28204069-f27e-004e-5000-0d2f77dbff8e" Name="pk_KrNotificationSettingVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="28204069-f27e-014e-4000-0d2f77dbff8e" />
	</SchemePrimaryKey>
</SchemeTable>