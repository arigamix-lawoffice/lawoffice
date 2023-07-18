<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="79dca225-d99c-4dfd-94d9-27ed3ab15046" Name="RoleDeputiesManagementVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="79dca225-d99c-00fd-2000-07ed3ab15046" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="79dca225-d99c-01fd-4000-07ed3ab15046" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="79dca225-d99c-00fd-3100-07ed3ab15046" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="f2d0c4af-b21e-4290-ae30-a10198b45a41" Name="MinDate" Type="Date Not Null">
		<Description>Начальная дата временного замещения или минимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="db20084b-baf8-452b-a5fa-bc95c34b8fe5" Name="MaxDate" Type="Date Not Null">
		<Description>Конечная дата временного замещения или максимальное значение, если замещение постоянное.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="af4632ee-a8bb-4718-a080-394c2e61c2e0" Name="IsActive" Type="Boolean Not Null">
		<Description>Признак того, что замещение активно.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="54cee6b4-be82-4f47-9eb8-d8bb09dc1d90" Name="df_RoleDeputiesManagementVirtual_IsActive" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c774b850-1bb0-46b6-8891-ec057efda4b8" Name="IsEnabled" Type="Boolean Not Null">
		<Description>Признак того, что замещение доступно, т.е. может стать активным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9bfe7770-8002-4e71-9ea0-10c6df183d78" Name="df_RoleDeputiesManagementVirtual_IsEnabled" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3ee11741-80b2-41ce-a611-95e730f684d6" Name="IsPermanent" Type="Boolean Not Null">
		<Description>Признак постоянного замещения.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="fd0aa32c-296d-4bac-8632-343d09f585be" Name="df_RoleDeputiesManagementVirtual_IsPermanent" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="79dca225-d99c-00fd-5000-07ed3ab15046" Name="pk_RoleDeputiesManagementVirtual">
		<SchemeIndexedColumn Column="79dca225-d99c-00fd-3100-07ed3ab15046" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="79dca225-d99c-00fd-7000-07ed3ab15046" Name="idx_RoleDeputiesManagementVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="79dca225-d99c-01fd-4000-07ed3ab15046" />
	</SchemeIndex>
</SchemeTable>