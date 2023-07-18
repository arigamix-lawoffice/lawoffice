<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0eabfebc-fa8b-41b9-9aa3-6db9626a8ac6" Name="FileTemplateRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0eabfebc-fa8b-00b9-2000-0db9626a8ac6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0eabfebc-fa8b-01b9-4000-0db9626a8ac6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0eabfebc-fa8b-00b9-3100-0db9626a8ac6" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f71fc38a-e932-46dd-90d4-919a63e4e301" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f71fc38a-e932-00dd-4000-019a63e4e301" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="212c5b56-6da6-4428-915b-1bdd4f1ad8da" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0eabfebc-fa8b-00b9-5000-0db9626a8ac6" Name="pk_FileTemplateRoles">
		<SchemeIndexedColumn Column="0eabfebc-fa8b-00b9-3100-0db9626a8ac6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0eabfebc-fa8b-00b9-7000-0db9626a8ac6" Name="idx_FileTemplateRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0eabfebc-fa8b-01b9-4000-0db9626a8ac6" />
	</SchemeIndex>
</SchemeTable>