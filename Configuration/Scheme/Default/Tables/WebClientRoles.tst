<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="383321f7-c432-42d8-84f9-e4f58e0cb021" Name="WebClientRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, в одну из которых должен входить сотрудник для авторизации в web-клиенте.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="383321f7-c432-00d8-2000-04f58e0cb021" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="383321f7-c432-01d8-4000-04f58e0cb021" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="383321f7-c432-00d8-3100-04f58e0cb021" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="57151c1e-01c1-400b-9910-a8da6a7ceb77" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Ссылка на роль должна быть без FK, т.к. импорт карточки настроек сервера выполняется раньше импорта роли "Все сотрудники" при накатывании базы "с нуля".</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="57151c1e-01c1-000b-4000-08da6a7ceb77" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="f4772be4-a746-481f-9980-cd734eba7745" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="383321f7-c432-00d8-5000-04f58e0cb021" Name="pk_WebClientRoles">
		<SchemeIndexedColumn Column="383321f7-c432-00d8-3100-04f58e0cb021" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="383321f7-c432-00d8-7000-04f58e0cb021" Name="idx_WebClientRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="383321f7-c432-01d8-4000-04f58e0cb021" />
	</SchemeIndex>
</SchemeTable>