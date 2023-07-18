<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9" Name="KrTaskRegistrationActionOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Задание регистрации". Таблица с обрабатываемыми вариантами завершения задания действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2ba2b1a3-b8ad-0047-2000-0a3fa421c7a9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2ba2b1a3-b8ad-0147-4000-0a3fa421c7a9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2ba2b1a3-b8ad-0047-3100-0a3fa421c7a9" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d83ab961-22a7-4aed-b6d5-c99f7caa8e2e" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d83ab961-22a7-00ed-4000-099f7caa8e2e" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="63aaf9a4-780f-4d13-a1f3-2067cd72ee7c" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9bdfffe4-04db-4567-a792-08534f95a6f7" Name="Link" Type="Reference(Abstract) Not Null">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9bdfffe4-04db-0067-4000-08534f95a6f7" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="198e5c51-057c-4a26-842c-1ab02c20fbf8" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f5fd0011-ef43-4b2a-8496-cbab16bf0465" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер строки.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="662dd8a1-0f93-4361-abc6-3d036b906d09" Name="Result" Type="String(Max) Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="2a90b91b-07b2-4283-a5a6-ca27ff428441" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении задания с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2a90b91b-07b2-0083-4000-0a27ff428441" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="90497d66-b30f-4e05-89a4-a9012bb3eb81" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a029267f-ef3e-4692-9471-0247ccb17945" Name="SendToPerformer" Type="Boolean Not Null">
		<Description>Отправлять исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="09762565-1394-4257-ae3c-8001cde2525e" Name="df_KrTaskRegistrationActionOptionsVirtual_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b33bd244-abe6-4e6d-89a0-17b0dafe9a02" Name="SendToAuthor" Type="Boolean Not Null">
		<Description>Отправлять автору.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="bb714cd4-659d-4ae7-b847-f1bfc209b88f" Name="df_KrTaskRegistrationActionOptionsVirtual_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0248663c-5d5c-483b-b260-c4b53d19a094" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9ff2191d-18ff-4648-885a-723e3a7e5ccc" Name="df_KrTaskRegistrationActionOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="055ed582-5c37-4739-89ba-cea58424f713" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1f7f10f3-6369-49b8-bec3-252f46eb99ef" Name="df_KrTaskRegistrationActionOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8ea5f593-5f4b-4af9-b680-c681b41a4a0b" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2ba2b1a3-b8ad-0047-5000-0a3fa421c7a9" Name="pk_KrTaskRegistrationActionOptionsVirtual">
		<SchemeIndexedColumn Column="2ba2b1a3-b8ad-0047-3100-0a3fa421c7a9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2ba2b1a3-b8ad-0047-7000-0a3fa421c7a9" Name="idx_KrTaskRegistrationActionOptionsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2ba2b1a3-b8ad-0147-4000-0a3fa421c7a9" />
	</SchemeIndex>
</SchemeTable>