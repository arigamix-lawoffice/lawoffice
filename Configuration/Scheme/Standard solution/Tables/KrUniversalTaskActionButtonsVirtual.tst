<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e85631c4-0014-4842-86f4-9a6ba66166f3" Name="KrUniversalTaskActionButtonsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Действие "Настраиваемое задание". Параметры настраиваемых вариантов завершения.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e85631c4-0014-0042-2000-0a6ba66166f3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e85631c4-0014-0142-4000-0a6ba66166f3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e85631c4-0014-0042-3100-0a6ba66166f3" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="53af4dde-12f5-41c3-870b-0d76f8db9a65" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер варианта завершения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="bc664124-7e59-456e-8b0e-1a3f44f6da8d" Name="df_KrUniversalTaskActionButtonsVirtual_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="44222488-5921-4ae1-9e34-9a71e275793f" Name="Caption" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="aa69a8a5-a8f5-4c07-8751-86f63d7f0ab8" Name="Digest" Type="String(Max) Not Null">
		<Description>Дайджест задания.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="afd220ad-1034-408a-a9d4-c40e3f7da6a4" Name="IsShowComment" Type="Boolean Not Null">
		<Description>Показывать поле ввода комментария.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="29f079de-3134-4b3c-8b23-765dcd17b0a9" Name="df_KrUniversalTaskActionButtonsVirtual_IsShowComment" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="717604dd-e220-4b21-96fc-34e6515a1890" Name="IsAdditionalOption" Type="Boolean Not Null">
		<Description>Дополнительный вариант завершения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b6bfe764-dc0c-4501-8f53-7955d731dee3" Name="df_KrUniversalTaskActionButtonsVirtual_IsAdditionalOption" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="abf886f0-d8a5-45bc-a579-d79600e4d4fe" Name="Link" Type="Reference(Abstract) Not Null">
		<Description>Связь.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="abf886f0-d8a5-00bc-4000-079600e4d4fe" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="13aacc3a-3987-47f2-a399-c4ef84e8f37e" Name="Script" Type="String(Max) Not Null">
		<Description>Скрипт, который выполняется при завершении задания с данным вариантом завершения.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="2d7483ae-4a82-48e7-95b7-bbedf71f5462" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" WithForeignKey="false">
		<Description>Уведомление отправляемое при завершении задания с указанным вариантом завершения.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2d7483ae-4a82-00e7-4000-0bedf71f5462" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="20dd57f4-4c29-49e3-9186-1b6091338d6b" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b9afe1f5-bc04-4a21-a894-df85e2fdb92e" Name="SendToPerformer" Type="Boolean Not Null">
		<Description>Отправлять исполнителю.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ca8d4005-eb90-4f4b-81f8-7a0bbe6581f8" Name="df_KrUniversalTaskActionButtonsVirtual_SendToPerformer" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c6681950-b62b-4a9a-988d-6673e7599813" Name="SendToAuthor" Type="Boolean Not Null">
		<Description>Отправлять автору.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3ce6e5e3-9d60-4b19-ad0b-6d1a94773f90" Name="df_KrUniversalTaskActionButtonsVirtual_SendToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7ac609c6-0258-4982-bb83-6781b17c7df1" Name="ExcludeDeputies" Type="Boolean Not Null">
		<Description>Не отправлять заместителям.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="235c82df-c63c-452c-9ed7-9651713754ca" Name="df_KrUniversalTaskActionButtonsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a9eb24a5-5253-41f7-9cb7-3cc6373834b2" Name="ExcludeSubscribers" Type="Boolean Not Null">
		<Description>Не отправлять подписчикам.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="14cf0410-6244-48ef-8161-23f635014cbe" Name="df_KrUniversalTaskActionButtonsVirtual_ExcludeSubscribers" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6687e5d4-90af-48bd-9bc0-443e18b9a249" Name="NotificationScript" Type="String(Max) Not Null">
		<Description>Сценарий изменения уведомления.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b62dad82-cfb9-4604-a36a-f7f125d55a3c" Name="OptionID" Type="Guid Not Null">
		<Description>Идентификатор настраиваемого варианта завершения.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e85631c4-0014-0042-5000-0a6ba66166f3" Name="pk_KrUniversalTaskActionButtonsVirtual">
		<SchemeIndexedColumn Column="e85631c4-0014-0042-3100-0a6ba66166f3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e85631c4-0014-0042-7000-0a6ba66166f3" Name="idx_KrUniversalTaskActionButtonsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e85631c4-0014-0142-4000-0a6ba66166f3" />
	</SchemeIndex>
</SchemeTable>