<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="599f50f0-95c4-48ad-a739-c54fd9b5f829" Name="ReportRolesPassive" Group="Common" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, которых могут смотреть отчёты по текущим заданиям</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="599f50f0-95c4-00ad-2000-054fd9b5f829" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="599f50f0-95c4-01ad-4000-054fd9b5f829" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="599f50f0-95c4-00ad-3100-054fd9b5f829" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="22d87a3f-3685-483d-bdd2-f8f2470ec40e" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="22d87a3f-3685-003d-4000-08f2470ec40e" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="885726ef-c1ef-4c3f-b837-e625e133c9b4" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="599f50f0-95c4-00ad-5000-054fd9b5f829" Name="pk_ReportRolesPassive">
		<SchemeIndexedColumn Column="599f50f0-95c4-00ad-3100-054fd9b5f829" />
	</SchemePrimaryKey>
	<SchemeUniqueKey ID="1edd8140-51cd-45e0-bb87-b8e0db7fcdea" Name="ndx_ReportRolesPassive_IDRoleID">
		<SchemeIndexedColumn Column="599f50f0-95c4-01ad-4000-054fd9b5f829" />
		<SchemeIndexedColumn Column="22d87a3f-3685-003d-4000-08f2470ec40e" />
	</SchemeUniqueKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="599f50f0-95c4-00ad-7000-054fd9b5f829" Name="idx_ReportRolesPassive_ID" IsClustered="true">
		<SchemeIndexedColumn Column="599f50f0-95c4-01ad-4000-054fd9b5f829" />
	</SchemeIndex>
	<SchemeIndex ID="247a8aa3-5330-4af1-9d96-afdf35b9c391" Name="ndx_ReportRolesPassive_RoleID">
		<SchemeIndexedColumn Column="22d87a3f-3685-003d-4000-08f2470ec40e" />
	</SchemeIndex>
</SchemeTable>