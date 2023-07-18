<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0f489948-bc16-42a6-8953-b92100807296" Name="RoleDeputiesManagement" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Основные записи секции "Мои замещения"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f489948-bc16-00a6-2000-092100807296" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0f489948-bc16-01a6-4000-092100807296" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f489948-bc16-00a6-3100-092100807296" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a0e909bd-8bed-485c-a8fd-cdae3a26e3f2" Name="MinDate" Type="Date Not Null">
		<Description>Начальная дата временного замещения или минимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="196a16fc-8a98-4d80-9d4d-5e533db012d8" Name="MaxDate" Type="Date Not Null">
		<Description>Конечная дата временного замещения или максимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d4ae267c-ded8-41b3-9cd5-70f05f0e21c5" Name="IsActive" Type="Boolean Not Null">
		<Description>Признак того, что замещение активно.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2f9738e9-578d-477f-abee-6795eb613e87" Name="df_RoleDeputiesManagement_IsActive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e7309100-7651-4064-9a68-fb25fdc86924" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Признак того, что замещение доступно, т.е. может стать активным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="75f0e2d9-f055-4f97-85ca-b631c4ff1f94" Name="df_RoleDeputiesManagement_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f489948-bc16-00a6-5000-092100807296" Name="pk_RoleDeputiesManagement">
		<SchemeIndexedColumn Column="0f489948-bc16-00a6-3100-092100807296" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f489948-bc16-00a6-7000-092100807296" Name="idx_RoleDeputiesManagement_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0f489948-bc16-01a6-4000-092100807296" />
	</SchemeIndex>
</SchemeTable>