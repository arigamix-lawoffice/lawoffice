<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="bae37ba2-7a39-49a1-8cc8-64f032ba3f79" Name="NotificationTypes" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для карточки Тип уведомления</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="bae37ba2-7a39-00a1-2000-04f032ba3f79" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bae37ba2-7a39-01a1-4000-04f032ba3f79" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fe686962-4e72-4a67-8dc8-9afa19da3f6a" Name="Name" Type="String(256) Not Null">
		<Description>Название типа уведомления</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="15de4239-db15-48b6-bbab-ae8327901733" Name="IsGlobal" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="acf14d59-e8de-476f-b586-34842d823699" Name="df_NotificationTypes_IsGlobal" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2f24e237-c9e4-42c5-9279-94810773784f" Name="CanSubscribe" Type="Boolean Not Null">
		<Description>Определяет, что на данный тип уведомления можно подписаться</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9c0ef199-3871-4ae8-abb3-9b647d302f87" Name="df_NotificationTypes_CanSubscribe" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="568ff0d6-9ac5-4c17-bdda-497a153e62a2" Name="Hidden" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ef1a8f61-58ec-4b90-8a28-6afc5b9846d5" Name="df_NotificationTypes_Hidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="bae37ba2-7a39-00a1-5000-04f032ba3f79" Name="pk_NotificationTypes" IsClustered="true">
		<SchemeIndexedColumn Column="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
	</SchemePrimaryKey>
</SchemeTable>