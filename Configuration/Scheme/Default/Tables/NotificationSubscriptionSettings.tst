<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="9ffce865-a6cd-4883-9ed0-0cbeaa1831d1" Name="NotificationSubscriptionSettings" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная секция для виртуальной карточки настроек уведомлений</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9ffce865-a6cd-0083-2000-0cbeaa1831d1" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9ffce865-a6cd-0183-4000-0cbeaa1831d1" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e2b6f04b-4836-49df-94a7-379426d9d254" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки, к которой относятся настройки уведомлений</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e2b6f04b-4836-00df-4000-079426d9d254" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="495e3432-5b66-485c-a65f-2d1c06ad2dcb" Name="CardTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9ffce865-a6cd-0083-5000-0cbeaa1831d1" Name="pk_NotificationSubscriptionSettings" IsClustered="true">
		<SchemeIndexedColumn Column="9ffce865-a6cd-0183-4000-0cbeaa1831d1" />
	</SchemePrimaryKey>
</SchemeTable>