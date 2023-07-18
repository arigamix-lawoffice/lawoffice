<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="3482fa35-9558-4a7c-832f-3ac94c73f2f9" Name="WeEmailAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия отправки уведомления</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3482fa35-9558-007c-2000-0ac94c73f2f9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3482fa35-9558-017c-4000-0ac94c73f2f9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7b0874fd-eeec-4ef7-8be6-c5c26f34c1e2" Name="Body" Type="String(Max) Not Null">
		<Description>Тело письма</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0b2eba9f-da2d-4a51-8412-6104c1e15da1" Name="Header" Type="String(Max) Not Null">
		<Description>Заголовок письма</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8e53a5f1-cf0c-41ca-8efb-114e7c4a1210" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Карточка уведомления</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8e53a5f1-cf0c-00ca-4000-014e7c4a1210" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="f23133a2-ecc8-427e-b721-982a7fc7ea98" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="722c7ef4-398a-4685-b129-0033ca7c26e6" Name="ExcludeDeputies" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="3f2e3c39-fc99-4719-8267-d7923097fdca" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт модификации уведомления</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="915d64c1-87fe-422b-bf8a-ae0db3972648" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="915d64c1-87fe-002b-4000-0e0db3972648" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="f29ca3d2-8b04-41ea-a95b-57aa5c326396" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="af785ecb-a16e-4cd0-bb88-6307a2cf8497" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Флаг определяет, что из списка получателей нужно будет искючить подписчиков на уведомление.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="fd08a799-6ffd-409e-81b5-9031fadc1f04" Name="df_WeEmailAction_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3482fa35-9558-007c-5000-0ac94c73f2f9" Name="pk_WeEmailAction" IsClustered="true">
		<SchemeIndexedColumn Column="3482fa35-9558-017c-4000-0ac94c73f2f9" />
	</SchemePrimaryKey>
</SchemeTable>