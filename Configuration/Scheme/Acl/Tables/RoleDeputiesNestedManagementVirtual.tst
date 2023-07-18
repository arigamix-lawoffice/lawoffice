<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="3937aa4f-0658-4e8b-a25a-911802f1fa82" Name="RoleDeputiesNestedManagementVirtual" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3937aa4f-0658-008b-2000-011802f1fa82" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3937aa4f-0658-018b-4000-011802f1fa82" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3937aa4f-0658-008b-3100-011802f1fa82" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="26817409-6cc0-43f4-8d59-080698e61a63" Name="MinDate" Type="Date Not Null">
		<Description>Начальная дата временного замещения или минимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ebbfc71e-01d0-43c1-8b34-dfd8e3039590" Name="MaxDate" Type="Date Not Null">
		<Description>Конечная дата временного замещения или максимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1e283e65-4325-4f1f-9129-0f87c049962e" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Признак того, что замещение доступно, т.е. может стать активным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="69b679ea-3bba-438e-a655-75286d5c88f7" Name="df_RoleDeputiesNestedManagementVirtual_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e50bb005-a8f5-430c-b436-a5452e970baa" Name="IsPermanent" Type="Boolean Not Null">
		<Description>Признак постоянного замещения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="95d3e1e7-e411-4514-b9fc-bd51bd1e8d5a" Name="df_RoleDeputiesNestedManagementVirtual_IsPermanent" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3937aa4f-0658-008b-5000-011802f1fa82" Name="pk_RoleDeputiesNestedManagementVirtual">
		<SchemeIndexedColumn Column="3937aa4f-0658-008b-3100-011802f1fa82" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3937aa4f-0658-008b-7000-011802f1fa82" Name="idx_RoleDeputiesNestedManagementVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3937aa4f-0658-018b-4000-011802f1fa82" />
	</SchemeIndex>
</SchemeTable>