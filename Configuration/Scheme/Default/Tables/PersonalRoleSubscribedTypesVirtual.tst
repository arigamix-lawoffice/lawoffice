<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="62859264-a143-4ec0-a86b-bba80f6f61ac" Name="PersonalRoleSubscribedTypesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со всеми глобальными типами уведомлений, на которые подписался пользователь.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="62859264-a143-00c0-2000-0ba80f6f61ac" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="62859264-a143-01c0-4000-0ba80f6f61ac" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="62859264-a143-00c0-3100-0ba80f6f61ac" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="6317c44e-6d96-48e7-bde9-4f04ae779b2d" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6317c44e-6d96-00e7-4000-0f04ae779b2d" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="a84696e2-9425-4f83-a863-6a3812d34570" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="62859264-a143-00c0-5000-0ba80f6f61ac" Name="pk_PersonalRoleSubscribedTypesVirtual">
		<SchemeIndexedColumn Column="62859264-a143-00c0-3100-0ba80f6f61ac" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="62859264-a143-00c0-7000-0ba80f6f61ac" Name="idx_PersonalRoleSubscribedTypesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="62859264-a143-01c0-4000-0ba80f6f61ac" />
	</SchemeIndex>
</SchemeTable>