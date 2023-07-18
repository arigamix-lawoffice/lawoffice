<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="2539f630-3898-457e-9e49-e1f87552caaf" Name="TaskAssignedRoles" Group="System" InstanceType="Tasks" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2539f630-3898-007e-2000-01f87552caaf" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2539f630-3898-017e-4000-01f87552caaf" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8">
			<Description>Идентификатор задания</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2539f630-3898-007e-3100-01f87552caaf" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d43de0d1-655d-4e07-a053-1a59c68e4c4c" Name="TaskRole" Type="Reference(Typified) Not Null" ReferencedTable="a59078ce-8acf-4c45-a49a-503fa88a0580">
		<Description>Ссылка на ФРЗ</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d43de0d1-655d-0007-4000-0a59c68e4c4c" Name="TaskRoleID" Type="Guid Not Null" ReferencedColumn="bd4fdcea-8042-488a-94c9-770b49357cfe" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8be82616-c2b6-40f6-b687-4274983daf92" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Ссылка на роль</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8be82616-c2b6-00f6-4000-0274983daf92" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="be798618-e56f-44cc-b937-9482f373fe0d" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="51cb9824-cb51-4d5a-9e1b-0128de975c05" Name="RoleType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки для роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="51cb9824-cb51-005a-4000-0128de975c05" Name="RoleTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9772fb18-d8d8-4511-a96a-38478f807756" Name="Position" Type="String(256) Null">
		<Description>Должность.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="f12f7f18-9c72-4f09-99a6-7cef825614d4" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="2539f630-3898-457e-9e49-e1f87552caaf">
		<Description>В некоторых случаях требуется иерархия ФРЗ, например для работы вложенных умных ролей модуля ACL.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f12f7f18-9c72-0009-4000-0cef825614d4" Name="ParentRowID" Type="Guid Null" ReferencedColumn="2539f630-3898-007e-3100-01f87552caaf" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="3252ab88-be70-42eb-8fc2-8cfa6b813158" Name="Master" Type="Boolean Not Null">
		<Description>Основная запись. Опираясь на неё берётся временная зона и календарь.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8c8ee447-05a7-4a4e-ae32-b837af0f90f0" Name="df_TaskAssignedRoles_Master" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ce6f3002-ac6b-4bab-b023-42c26f4db034" Name="ShowInTaskDetails" Type="Boolean Not Null">
		<Description>Показывать запись в информации о задании.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6cbf5660-efc0-407e-a2de-d06491b6a0e2" Name="df_TaskAssignedRoles_ShowInTaskDetails" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2539f630-3898-007e-5000-01f87552caaf" Name="pk_TaskAssignedRoles">
		<SchemeIndexedColumn Column="2539f630-3898-007e-3100-01f87552caaf" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2539f630-3898-007e-7000-01f87552caaf" Name="idx_TaskAssignedRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2539f630-3898-017e-4000-01f87552caaf" />
	</SchemeIndex>
	<SchemeIndex ID="dfb4908e-b143-4838-b285-fed38279b384" Name="ndx_TaskAssignedRoles_IDRowID">
		<SchemeIndexedColumn Column="2539f630-3898-017e-4000-01f87552caaf" />
		<SchemeIndexedColumn Column="2539f630-3898-007e-3100-01f87552caaf" />
	</SchemeIndex>
	<SchemeIndex ID="e8b09925-f87d-479c-b582-fce7c5b7c0f9" Name="ndx_TaskAssignedRoles_RoleIDTaskRoleID" SupportsPostgreSql="false">
		<FillFactor Dbms="SqlServer">80</FillFactor>
		<SchemeIndexedColumn Column="8be82616-c2b6-00f6-4000-0274983daf92" />
		<SchemeIndexedColumn Column="d43de0d1-655d-0007-4000-0a59c68e4c4c" />
	</SchemeIndex>
	<SchemeIndex ID="4e6cd7fb-e818-43c7-bed1-795213ef427e" Name="ndx_TaskAssignedRoles_RoleIDTaskRoleIDID" SupportsSqlServer="false">
		<FillFactor Dbms="PostgreSql">80</FillFactor>
		<SchemeIndexedColumn Column="8be82616-c2b6-00f6-4000-0274983daf92" />
		<SchemeIndexedColumn Column="d43de0d1-655d-0007-4000-0a59c68e4c4c" />
		<SchemeIndexedColumn Column="2539f630-3898-017e-4000-01f87552caaf" />
	</SchemeIndex>
	<SchemeIndex ID="cfb5a083-b102-425e-a559-a4e646799f80" Name="ndx_TaskAssignedRoles_ParentRowID">
		<SchemeIndexedColumn Column="f12f7f18-9c72-0009-4000-0cef825614d4" />
	</SchemeIndex>
</SchemeTable>