<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a3a271db-3ce6-47c7-b75e-87dcc9dc052a" Name="RoleUsers" Group="Roles" InstanceType="Cards" ContentType="Collections">
	<Description>Состав роли (список пользователей, включённых в роль).</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3a271db-3ce6-00c7-2000-07dcc9dc052a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a3a271db-3ce6-01c7-4000-07dcc9dc052a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3a271db-3ce6-00c7-3100-07dcc9dc052a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="adbdbd82-9c4f-4265-b061-2dd1ede5b277" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d">
		<Description>Тип роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="adbdbd82-9c4f-0065-4000-0dd1ede5b277" Name="TypeID" Type="Int16 Not Null" ReferencedColumn="c9e1fce6-f27f-4fce-83a0-fadbff72f848">
			<Description>Идентификатор типа роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f9cfc275-c4c0-4f8a-bbe6-d1e1ab56ba64" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Персональная роль пользователя, включённого в состав роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f9cfc275-c4c0-008a-4000-01e1ab56ba64" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c90009f3-7c77-4327-af37-1982ff34c50a" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1657490d-4041-443d-9e74-1673ea0b68de" Name="IsDeputy" Type="Boolean Not Null">
		<Description>Признак того, что пользователь является заместителем (true, если он включён в состав роли как заместитель, или false, если он включён непосредственно).</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9fb403cc-33bf-4d8a-8441-4e9e41cdbce2" Name="df_RoleUsers_IsDeputy" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3a271db-3ce6-00c7-5000-07dcc9dc052a" Name="pk_RoleUsers">
		<SchemeIndexedColumn Column="a3a271db-3ce6-00c7-3100-07dcc9dc052a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a3a271db-3ce6-00c7-7000-07dcc9dc052a" Name="idx_RoleUsers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a3a271db-3ce6-01c7-4000-07dcc9dc052a" />
	</SchemeIndex>
	<SchemeIndex ID="819b8b72-d4c2-46c8-b36d-8f0cc334ef77" Name="ndx_RoleUsers_IDUserID" IsUnique="true">
		<SchemeIndexedColumn Column="a3a271db-3ce6-01c7-4000-07dcc9dc052a" />
		<SchemeIndexedColumn Column="f9cfc275-c4c0-008a-4000-01e1ab56ba64" />
	</SchemeIndex>
	<SchemeIndex ID="85261e78-ef58-44e0-8973-3da5673545b6" Name="ndx_RoleUsers_TypeIDUserID">
		<SchemeIndexedColumn Column="adbdbd82-9c4f-0065-4000-0dd1ede5b277" />
		<SchemeIndexedColumn Column="f9cfc275-c4c0-008a-4000-01e1ab56ba64" />
	</SchemeIndex>
	<SchemeIndex ID="6ac90ee5-6e5d-4f27-b395-8e73c9b5bfa3" Name="ndx_RoleUsers_UserIDIsDeputy" SupportsPostgreSql="false">
		<FillFactor Dbms="SqlServer">80</FillFactor>
		<SchemeIndexedColumn Column="f9cfc275-c4c0-008a-4000-01e1ab56ba64" />
		<SchemeIndexedColumn Column="1657490d-4041-443d-9e74-1673ea0b68de" />
		<SchemeIncludedColumn Column="a3a271db-3ce6-00c7-3100-07dcc9dc052a" />
	</SchemeIndex>
	<SchemeIndex ID="59050b4f-1cc6-462f-adbe-15d4d9811edc" Name="ndx_RoleUsers_UserIDIsDeputyRowIDID" SupportsSqlServer="false">
		<FillFactor Dbms="PostgreSql">80</FillFactor>
		<SchemeIndexedColumn Column="f9cfc275-c4c0-008a-4000-01e1ab56ba64" />
		<SchemeIndexedColumn Column="1657490d-4041-443d-9e74-1673ea0b68de" />
		<SchemeIndexedColumn Column="a3a271db-3ce6-00c7-3100-07dcc9dc052a" />
		<SchemeIndexedColumn Column="a3a271db-3ce6-01c7-4000-07dcc9dc052a" />
	</SchemeIndex>
</SchemeTable>