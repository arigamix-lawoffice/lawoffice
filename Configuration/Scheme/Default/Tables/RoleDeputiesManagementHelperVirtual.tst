<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="81f28cf8-709b-4dde-8c9e-505d3d7870e0" Name="RoleDeputiesManagementHelperVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="81f28cf8-709b-00de-2000-005d3d7870e0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="81f28cf8-709b-01de-4000-005d3d7870e0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="4f9c20f4-5bcb-4687-980b-da71b32b79fc" Name="UserID" Type="Guid Not Null">
		<Description>UserID связан с представлением AvailableDeputyRoles для пробрасывания параметра User. Параметр User маппится в карточках «Мои замещения» (RoleDeputiesManagement)</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="81f28cf8-709b-00de-5000-005d3d7870e0" Name="pk_RoleDeputiesManagementHelperVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="81f28cf8-709b-01de-4000-005d3d7870e0" />
	</SchemePrimaryKey>
</SchemeTable>