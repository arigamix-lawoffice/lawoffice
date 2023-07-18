<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="3f87675e-0a60-4ece-a5c9-3c203e2c9ffb" Name="KrSigningActionOptionsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Подписание". Таблица с обрабатываемыми вариантами завершения задания действия.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f87675e-0a60-00ce-2000-0c203e2c9ffb" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3f87675e-0a60-01ce-4000-0c203e2c9ffb" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f87675e-0a60-00ce-3100-0c203e2c9ffb" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="51ee264c-3808-4c05-8d92-02c7d8d9e58a" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="51ee264c-3808-0005-4000-02c7d8d9e58a" Name="OptionID" Type="Guid Not Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="a8076e49-4075-4677-b13e-c0b51950b78e" Name="OptionCaption" Type="String(128) Not Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c41242fd-cdfd-4003-be02-3e7b9e955c12" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d8e682be-7ce0-4959-9c1d-77901bc0e498" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер строки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7571ba9d-685d-4606-8d47-397e4d0485a7" Name="df_KrSigningActionOptionsVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6ad47b7c-79ec-4345-b157-e60f93e169be" Name="Result" Type="String(Max) Null">
		<Description>Результат записываемый в историю заданий.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="6785e141-0982-4f00-8330-eb295c1e1a92" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении задания с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6785e141-0982-0000-4000-0b295c1e1a92" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="22cc3a4b-bbd8-4e55-bcb8-7c45be720278" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="aa285525-eedb-4da6-b25d-1a9f6d02e6bc" Name="SendToPerformer" Type="Boolean Not Null">
		<Description>Отправлять исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="be02ba08-5abb-4c71-9877-fefa08614cb4" Name="df_KrSigningActionOptionsVirtual_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6628a105-edb4-400b-bb7c-3c1363409c8e" Name="SendToAuthor" Type="Boolean Not Null">
		<Description>Отправлять автору.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="546843ea-c35e-46bc-bedf-afab14905556" Name="df_KrSigningActionOptionsVirtual_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6eaafac6-916d-4490-a740-1ec565d963d7" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9f70105f-3013-4575-890d-acfd18051bf3" Name="df_KrSigningActionOptionsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ab3851b2-9555-4468-a99f-504d97fb8596" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9e3514b4-0cfb-4167-bdbc-e7f3af6ad7ff" Name="df_KrSigningActionOptionsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bf72b665-2ed1-4b62-8c2d-c48c549f9df5" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="45f68daa-80f4-44d7-b59e-183b9fa2bf89" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип задания.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="45f68daa-80f4-00d7-4000-083b9fa2bf89" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="5ae51f1d-44cb-4e29-bd1a-2e49b020ab1b" Name="TaskTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="0b84937b-30f0-4eb2-9c6f-5484e9610b66" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f87675e-0a60-00ce-5000-0c203e2c9ffb" Name="pk_KrSigningActionOptionsVirtual">
		<SchemeIndexedColumn Column="3f87675e-0a60-00ce-3100-0c203e2c9ffb" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f87675e-0a60-00ce-7000-0c203e2c9ffb" Name="idx_KrSigningActionOptionsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3f87675e-0a60-01ce-4000-0c203e2c9ffb" />
	</SchemeIndex>
</SchemeTable>