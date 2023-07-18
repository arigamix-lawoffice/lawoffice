<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="fc4566ea-029b-4d37-b3f0-4ca62a4cb500" Name="PersonalRoleUnsubscibedTypesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со всеми глобальными типами уведомлений, от которых отписался пользователь.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc4566ea-029b-0037-2000-0ca62a4cb500" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc4566ea-029b-0137-4000-0ca62a4cb500" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc4566ea-029b-0037-3100-0ca62a4cb500" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0ec7b39b-c0c2-4d27-9d7a-6775cc398292" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0ec7b39b-c0c2-0027-4000-0775cc398292" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="fd2d8bb8-736c-43ff-8dd3-3595b23dae68" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc4566ea-029b-0037-5000-0ca62a4cb500" Name="pk_PersonalRoleUnsubscibedTypesVirtual">
		<SchemeIndexedColumn Column="fc4566ea-029b-0037-3100-0ca62a4cb500" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="fc4566ea-029b-0037-7000-0ca62a4cb500" Name="idx_PersonalRoleUnsubscibedTypesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="fc4566ea-029b-0137-4000-0ca62a4cb500" />
	</SchemeIndex>
</SchemeTable>