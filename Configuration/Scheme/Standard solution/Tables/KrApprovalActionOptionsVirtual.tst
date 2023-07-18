<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="cea61a5b-0420-41ba-a5f2-e21c21c30f5a" Name="KrApprovalActionOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Согласование". Таблица с обрабатываемыми вариантами завершения задания действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cea61a5b-0420-00ba-2000-021c21c30f5a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cea61a5b-0420-01ba-4000-021c21c30f5a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cea61a5b-0420-00ba-3100-021c21c30f5a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="28369f34-444c-4f63-b840-4d11c874ddaa" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="28369f34-444c-0063-4000-0d11c874ddaa" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="b9e59bc9-7218-446e-abee-8cc48b59ff40" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e670c7df-ea27-4a54-bb8b-c56b77667761" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="17d25651-ab06-46dd-934f-421ec7e9a08f" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер строки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3143a514-f429-4b55-90d0-bc0ff11b9e3c" Name="df_KrApprovalActionOptionsVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="586d3cd7-2b3f-4811-a70e-16d172f01d51" Name="Result" Type="String(Max) Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="1d404461-a860-4809-a1fa-68c857a06b67" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении задания с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1d404461-a860-0009-4000-08c857a06b67" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="adc3e472-ab19-4de6-96f4-f0e28f886349" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="100eaccd-839d-443a-b532-dbd160891a53" Name="SendToPerformer" Type="Boolean Not Null">
		<Description>Отправлять исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="cc7832bd-2bc7-4844-8e66-15a44271d92b" Name="df_KrApprovalActionOptionsVirtual_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3b330299-556c-482d-9837-cb1d1f6b791b" Name="SendToAuthor" Type="Boolean Not Null">
		<Description>Отправлять автору.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3d5ecf51-fde2-43e9-848f-69230a94d5b8" Name="df_KrApprovalActionOptionsVirtual_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="85e48857-bdaf-4e61-afa2-afbc4f8bd4b6" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b06cda2a-3191-4be4-b5e4-b54e58d8064a" Name="df_KrApprovalActionOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="171825da-d633-4d14-86f5-e52dca16eafd" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ca807a77-8568-4977-a12c-0992e4374d3b" Name="df_KrApprovalActionOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c8326aef-cc2c-4c19-8e3a-a7cf5a6e7eec" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="eb968817-b3a3-4860-bccb-4b80ffd6d770" Name="df_KrApprovalActionOptionsVirtual_NotificationScript" Value="" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="afe8a361-dbef-4a3e-98b5-1ac605f20f88" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="afe8a361-dbef-003e-4000-0ac605f20f88" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="88d42bdd-2973-4577-bb89-430186698b12" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
		<SchemeReferencingColumn ID="2734c78b-836c-4dfe-b733-60adc6aafe43" Name="TaskTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cea61a5b-0420-00ba-5000-021c21c30f5a" Name="pk_KrApprovalActionOptionsVirtual">
		<SchemeIndexedColumn Column="cea61a5b-0420-00ba-3100-021c21c30f5a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="cea61a5b-0420-00ba-7000-021c21c30f5a" Name="idx_KrApprovalActionOptionsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="cea61a5b-0420-01ba-4000-021c21c30f5a" />
	</SchemeIndex>
</SchemeTable>