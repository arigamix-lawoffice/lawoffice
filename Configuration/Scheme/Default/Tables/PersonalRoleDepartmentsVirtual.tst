<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="21566803-b822-42b2-ab11-2c20e72a0de4" Name="PersonalRoleDepartmentsVirtual" Group="Roles" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица для отображения и редактирования департаментов, в которые входит сотрудник.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="21566803-b822-00b2-2000-0c20e72a0de4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21566803-b822-01b2-4000-0c20e72a0de4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="21566803-b822-00b2-3100-0c20e72a0de4" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="8e5beca9-e479-492e-b4d8-1e3ff242d83d" Name="Department" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Ссылка на департамент, в который включен сотрудник</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8e5beca9-e479-002e-4000-0e3ff242d83d" Name="DepartmentID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="09e698e6-9f8d-42c8-ad94-4ddcf66bbadb" Name="DepartmentName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="21566803-b822-00b2-5000-0c20e72a0de4" Name="pk_PersonalRoleDepartmentsVirtual">
		<SchemeIndexedColumn Column="21566803-b822-00b2-3100-0c20e72a0de4" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="21566803-b822-00b2-7000-0c20e72a0de4" Name="idx_PersonalRoleDepartmentsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="21566803-b822-01b2-4000-0c20e72a0de4" />
	</SchemeIndex>
</SchemeTable>