<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f7a6f1e2-a4c2-4f26-9c50-bc7e14dfc8ce" Name="KrSecondaryProcessRoles" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Содержит роли для которых доступен для выполнения вторичный процесс.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7a6f1e2-a4c2-0026-2000-0c7e14dfc8ce" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f7a6f1e2-a4c2-0126-4000-0c7e14dfc8ce" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7a6f1e2-a4c2-0026-3100-0c7e14dfc8ce" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f0dfd35e-e650-4554-a425-c5d1284813ec" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f0dfd35e-e650-0054-4000-05d1284813ec" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="0ee618f1-774e-4574-b9c9-44f4295d7eb6" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="107f9e85-b013-4758-8044-4aeff69ae636" Name="IsContext" Type="Boolean Not Null">
		<Description>Признак того, что роль является контекстной.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="bfc9226f-141f-4983-9602-a0a751e4a83b" Name="df_KrSecondaryProcessRoles_IsContext" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7a6f1e2-a4c2-0026-5000-0c7e14dfc8ce" Name="pk_KrSecondaryProcessRoles">
		<SchemeIndexedColumn Column="f7a6f1e2-a4c2-0026-3100-0c7e14dfc8ce" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7a6f1e2-a4c2-0026-7000-0c7e14dfc8ce" Name="idx_KrSecondaryProcessRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f7a6f1e2-a4c2-0126-4000-0c7e14dfc8ce" />
	</SchemeIndex>
</SchemeTable>