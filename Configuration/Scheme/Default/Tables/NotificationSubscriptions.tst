<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d5b074e2-eaff-4993-b238-1d5d3d248d56" Name="NotificationSubscriptions" Group="System">
	<Description>Таблица с подписками/отписками пользователей по карточкам</Description>
	<SchemePhysicalColumn ID="1c7267a7-6f12-4628-a5d2-bca3cc26968a" Name="ID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="49c2bd5e-6d14-4f17-aa5b-a8cf74be9033" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="49c2bd5e-6d14-0017-4000-08cf74be9033" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="86cf5298-d9fc-4325-b5e9-8825f05cb969" Name="Card" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="86cf5298-d9fc-0025-4000-0825f05cb969" Name="CardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="55c6b1d6-c3e3-44c3-bafa-cdc50940d211" Name="CardDigest" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="00085a0f-594a-4c6b-ac9d-7b50c1b50e8c" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="00085a0f-594a-006b-4000-0b50c1b50e8c" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="97d65931-4816-433d-8345-fa104fef1780" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="495fd235-761f-484b-8506-c5fcfbb73ac1" Name="IsSubscription" Type="Boolean Not Null">
		<Description>Определяет, является ли данная строка подпиской, или отпиской от уведомления</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5fee6ac4-bbca-46ff-b120-16a175f817cf" Name="df_NotificationSubscriptions_IsSubscription" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a2690e73-2cf8-431b-a4b8-8a3ab976ad30" Name="SubscriptionDate" Type="DateTime Not Null" />
	<SchemePrimaryKey ID="d1d0ad05-72b9-4ad0-84d1-450212bb4487" Name="pk_NotificationSubscriptions">
		<SchemeIndexedColumn Column="1c7267a7-6f12-4628-a5d2-bca3cc26968a" />
	</SchemePrimaryKey>
	<SchemeIndex ID="6be04173-db14-4dd5-a974-7c8ea12f917b" Name="ndx_NotificationSubscriptions_CardIDNotificationTypeIDIsSubscriptionUserID">
		<SchemeIndexedColumn Column="86cf5298-d9fc-0025-4000-0825f05cb969" />
		<SchemeIndexedColumn Column="00085a0f-594a-006b-4000-0b50c1b50e8c" />
		<SchemeIndexedColumn Column="495fd235-761f-484b-8506-c5fcfbb73ac1" />
		<SchemeIndexedColumn Column="49c2bd5e-6d14-0017-4000-08cf74be9033" />
	</SchemeIndex>
	<SchemeIndex ID="2d7d305e-665d-427d-a901-644b610fe5b6" Name="ndx_NotificationSubscriptions_UserIDSubscriptionDate">
		<SchemeIndexedColumn Column="49c2bd5e-6d14-0017-4000-08cf74be9033" />
		<SchemeIndexedColumn Column="a2690e73-2cf8-431b-a4b8-8a3ab976ad30" />
	</SchemeIndex>
</SchemeTable>