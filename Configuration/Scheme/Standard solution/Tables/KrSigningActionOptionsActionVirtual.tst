<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="b4c6c410-c5cb-40e3-b800-3cd854c94a2c" Name="KrSigningActionOptionsActionVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Подписание". Коллекционная секция содержащая параметры завершения действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4c6c410-c5cb-00e3-2000-0cd854c94a2c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b4c6c410-c5cb-01e3-4000-0cd854c94a2c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4c6c410-c5cb-00e3-3100-0cd854c94a2c" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="30699f4a-8d07-4caf-b5f5-620e4a39526b" Name="Link" Type="Reference(Abstract) Not Null">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="30699f4a-8d07-00af-4000-020e4a39526b" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="01cabf38-b10c-40c7-89ae-3350c929f1fc" Name="ActionOption" Type="Reference(Typified) Not Null" ReferencedTable="6a24d3cd-ec83-4e7a-8815-77b054c69371">
		<Description>Вариант завершения действия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="01cabf38-b10c-00c7-4000-0350c929f1fc" Name="ActionOptionID" Type="Guid Not Null" ReferencedColumn="e256f95b-bb48-4bd7-b626-ac3733f2c638" />
		<SchemeReferencingColumn ID="d4329104-50c2-4890-9b53-4fddbecbcb23" Name="ActionOptionCaption" Type="String(128) Not Null" ReferencedColumn="c475d8d0-f491-4235-a08d-cf1a8f41d72f" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d982b422-27b6-40cf-b4ce-3e87d5f2c2be" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер строки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="917e263e-ecbd-4e6c-9d7e-d19798629bd8" Name="df_KrSigningActionOptionsActionVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d729bf9b-bfdb-4d4a-a4b6-71a98a353236" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="1ce41889-0414-40db-80be-033138bbf2f3" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении действия с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1ce41889-0414-00db-4000-033138bbf2f3" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="faaa932f-c422-4ba2-9925-93fd0c01b6af" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="30af0b86-5b55-446a-972a-44e91d390ee2" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e707ec8d-e7fd-42b4-9568-c29cd92881c8" Name="df_KrSigningActionOptionsActionVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e7e44b4b-622f-4b90-a6ab-d4e4084d349e" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ceb74bde-2765-455b-af07-d8e9edbb89f0" Name="df_KrSigningActionOptionsActionVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4ca2d792-a971-4ee2-828b-2ba770af4a81" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4c6c410-c5cb-00e3-5000-0cd854c94a2c" Name="pk_KrSigningActionOptionsActionVirtual">
		<SchemeIndexedColumn Column="b4c6c410-c5cb-00e3-3100-0cd854c94a2c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b4c6c410-c5cb-00e3-7000-0cd854c94a2c" Name="idx_KrSigningActionOptionsActionVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b4c6c410-c5cb-01e3-4000-0cd854c94a2c" />
	</SchemeIndex>
</SchemeTable>