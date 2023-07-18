<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="925a75d4-639f-4467-9155-c8e21f5433a9" Name="PersonalRoleNotificationRulesVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="925a75d4-639f-0067-2000-08e21f5433a9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="925a75d4-639f-0167-4000-08e21f5433a9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="925a75d4-639f-0067-3100-08e21f5433a9" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="85a19707-7c37-4446-b7fa-acbede851f1d" Name="Name" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="35608988-66a4-4152-9ccf-742e8592c3a3" Name="Disallow" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5f4a807e-166e-4311-b85f-01824baa6224" Name="df_PersonalRoleNotificationRulesVirtual_Disallow" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c4b1cf7c-f9b1-45cc-9aa3-01db54f7b41a" Name="AllowanceType" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="ab50f191-7849-407f-b1df-7876524eb3a0" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="925a75d4-639f-0067-5000-08e21f5433a9" Name="pk_PersonalRoleNotificationRulesVirtual">
		<SchemeIndexedColumn Column="925a75d4-639f-0067-3100-08e21f5433a9" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="925a75d4-639f-0067-7000-08e21f5433a9" Name="idx_PersonalRoleNotificationRulesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="925a75d4-639f-0167-4000-08e21f5433a9" />
	</SchemeIndex>
</SchemeTable>