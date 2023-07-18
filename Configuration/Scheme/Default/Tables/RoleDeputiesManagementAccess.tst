<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="edbc91b1-dd36-43c2-867a-67c74ed7f403" Name="RoleDeputiesManagementAccess" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Сотрудники, которые могут редактировать секции "Мои замещения"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="edbc91b1-dd36-00c2-2000-07c74ed7f403" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="edbc91b1-dd36-01c2-4000-07c74ed7f403" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="edbc91b1-dd36-00c2-3100-07c74ed7f403" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5e8ffd83-3bd5-463a-9bf1-3179ac39719d" Name="PersonalRole" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Сотрудник, который может редактировать секции "Мои замещения" другого пользователя</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5e8ffd83-3bd5-003a-4000-0179ac39719d" Name="PersonalRoleID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d5b3e09f-7b43-4e9e-aa8b-2b8162b52ca8" Name="PersonalRoleName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="edbc91b1-dd36-00c2-5000-07c74ed7f403" Name="pk_RoleDeputiesManagementAccess">
		<SchemeIndexedColumn Column="edbc91b1-dd36-00c2-3100-07c74ed7f403" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="edbc91b1-dd36-00c2-7000-07c74ed7f403" Name="idx_RoleDeputiesManagementAccess_ID" IsClustered="true">
		<SchemeIndexedColumn Column="edbc91b1-dd36-01c2-4000-07c74ed7f403" />
	</SchemeIndex>
	<SchemeIndex ID="376d36a0-97bd-40bc-8398-9c8a675057f2" Name="ndx_RoleDeputiesManagementAccess_PersonalRoleID">
		<SchemeIndexedColumn Column="5e8ffd83-3bd5-003a-4000-0179ac39719d" />
	</SchemeIndex>
</SchemeTable>