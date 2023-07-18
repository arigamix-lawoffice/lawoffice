<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="fd37a3c0-33e5-4256-98bf-4440402f4116" Name="ReportRolesActive" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, которые могут смотреть отчёты по текущим заданиям</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fd37a3c0-33e5-0056-2000-0440402f4116" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fd37a3c0-33e5-0156-4000-0440402f4116" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="fd37a3c0-33e5-0056-3100-0440402f4116" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="2acfc855-7adf-4fb3-9704-d050fa5b2e96" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2acfc855-7adf-00b3-4000-0050fa5b2e96" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="64fe247d-75ce-4bfa-ac83-e8886a7656fb" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="fd37a3c0-33e5-0056-5000-0440402f4116" Name="pk_ReportRolesActive">
		<SchemeIndexedColumn Column="fd37a3c0-33e5-0056-3100-0440402f4116" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="9ddef98c-381a-42a7-b523-7fc67a79c7f9" Name="ndx_ReportRolesActive_IDRoleID">
		<SchemeIndexedColumn Column="fd37a3c0-33e5-0156-4000-0440402f4116" />
		<SchemeIndexedColumn Column="2acfc855-7adf-00b3-4000-0050fa5b2e96" />
	</SchemeUniqueKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="fd37a3c0-33e5-0056-7000-0440402f4116" Name="idx_ReportRolesActive_ID" IsClustered="true">
		<SchemeIndexedColumn Column="fd37a3c0-33e5-0156-4000-0440402f4116" />
	</SchemeIndex>
	<SchemeIndex ID="0450044b-a96e-4ed3-94f1-29cf199589ed" Name="ndx_ReportRolesActive_RoleID">
		<SchemeIndexedColumn Column="2acfc855-7adf-00b3-4000-0050fa5b2e96" />
	</SchemeIndex>
</SchemeTable>