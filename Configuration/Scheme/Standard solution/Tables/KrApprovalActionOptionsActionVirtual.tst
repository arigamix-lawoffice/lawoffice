<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="244719bf-4d4a-4df6-b2fe-a00b1bf6d173" Name="KrApprovalActionOptionsActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Коллекционная секция содержащая параметры завершения действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="244719bf-4d4a-00f6-2000-000b1bf6d173" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="244719bf-4d4a-01f6-4000-000b1bf6d173" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="244719bf-4d4a-00f6-3100-000b1bf6d173" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="436d2eb6-dd38-4a55-8b23-7be6d7589d0e" Name="Link" Type="Reference(Abstract) Not Null">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="436d2eb6-dd38-0055-4000-0be6d7589d0e" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="360fca9e-eca4-45ff-b1a6-9fdf9e59117b" Name="ActionOption" Type="Reference(Typified) Not Null" ReferencedTable="6a24d3cd-ec83-4e7a-8815-77b054c69371">
		<Description>Вариант завершения действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="360fca9e-eca4-00ff-4000-0fdf9e59117b" Name="ActionOptionID" Type="Guid Not Null" ReferencedColumn="e256f95b-bb48-4bd7-b626-ac3733f2c638" />
		<SchemeReferencingColumn ID="0b096003-1eeb-4514-8509-fe03ff0744ff" Name="ActionOptionCaption" Type="String(128) Not Null" ReferencedColumn="c475d8d0-f491-4235-a08d-cf1a8f41d72f" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="21a2be3e-28f2-4aab-82fe-25987ad463ed" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер строки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8ab1bc70-6c32-4320-8e1a-297b1fb48b35" Name="df_KrApprovalActionOptionsActionVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c30f0fd0-2181-49eb-a560-f01ab907b4f0" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="e89e5123-9bfc-4e32-aea1-f113f4253b3a" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении действия с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e89e5123-9bfc-0032-4000-0113f4253b3a" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="82de9669-14e9-4ba1-aba6-1cfa8ecc4839" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="81d7d2af-4125-4867-81fa-30e9e12baa21" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3c0639e1-9070-4028-8ae0-3809a421c0ce" Name="df_KrApprovalActionOptionsActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="103c91db-e3d2-4ce9-a35d-11400ff5fc0e" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3866e9a0-758e-4523-ae18-730b876c56a7" Name="df_KrApprovalActionOptionsActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c1a15db7-4cc2-47eb-a08f-125c65df070f" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="244719bf-4d4a-00f6-5000-000b1bf6d173" Name="pk_KrApprovalActionOptionsActionVirtual">
		<SchemeIndexedColumn Column="244719bf-4d4a-00f6-3100-000b1bf6d173" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="244719bf-4d4a-00f6-7000-000b1bf6d173" Name="idx_KrApprovalActionOptionsActionVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="244719bf-4d4a-01f6-4000-000b1bf6d173" />
	</SchemeIndex>
</SchemeTable>