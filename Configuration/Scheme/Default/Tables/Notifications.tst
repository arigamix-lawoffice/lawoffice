<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a" Name="Notifications" Group="System" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для карточки Уведомление</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="18145bb5-fd4e-0095-2000-0e1cd9b4ee5a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="265d4336-6764-4db8-8874-0e5fa92cbd5d" Name="Name" Type="String(256) Not Null">
		<Description>Название карточки уведомления</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8c67f9a0-3651-475e-99ba-ffb864624a8e" Name="Description" Type="String(Max) Null">
		<Description>Описание карточки уведомления</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="7e0dc976-5fd5-44b3-a870-e50c6c8717eb" Name="NotificationType" Type="Reference(Typified) Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<Description>Тип уведомления</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7e0dc976-5fd5-00b3-4000-050c6c8717eb" Name="NotificationTypeID" Type="Guid Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79">
			<SchemeDefaultConstraint IsPermanent="true" ID="86cee950-62ce-4c56-8564-fa341887c271" Name="df_Notifications_NotificationTypeID" Value="c5a765f4-bd96-44c3-8c5f-5cf5fe43c521" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="9775ecdb-053a-46e6-a343-a3619bafc99c" Name="NotificationTypeName" Type="String(256) Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a">
			<SchemeDefaultConstraint IsPermanent="true" ID="fc955f55-009f-48fe-be70-ef897c7b7176" Name="df_Notifications_NotificationTypeName" Value="$Notifications_Other" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f69de972-cae7-428f-b4df-ecd37d48a08d" Name="AliasMetadata" Type="String(Max) Null">
		<Description>Метаинформация по алиасам плейсхолдеров.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="55829fa0-5448-4e1b-ab39-1edf40051478" Name="Subject" Type="String(Max) Null">
		<Description>Тема письма</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f413fb04-cbd7-47b1-9fae-f1d48597a0c1" Name="Text" Type="String(Max) Null">
		<Description>Текст письма</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="18145bb5-fd4e-0095-5000-0e1cd9b4ee5a" Name="pk_Notifications" IsClustered="true">
		<SchemeIndexedColumn Column="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
	</SchemePrimaryKey>
</SchemeTable>