<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="df057f7e-f59a-4857-b615-19abb650442a" Name="TemplateEditRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, которым шаблон доступен для редактирования и удаления помимо администраторов.
Указанным ролям автоматически доступно создание шаблона или создание карточки из шаблона.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df057f7e-f59a-0057-2000-09abb650442a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="df057f7e-f59a-0157-4000-09abb650442a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df057f7e-f59a-0057-3100-09abb650442a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="a64a8ad1-0b33-4671-8fe3-42390a22b690" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Любая роль, кроме контекстной и временной, для которой предоставляется доступ на редактирование или удаление шаблона. Если пользователь не входит ни в одну роль, указанную в этой таблице, то он может редактировать и удалять шаблон только в том случае, если он является администратором.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a64a8ad1-0b33-0071-4000-02390a22b690" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="a32b25d8-7352-456f-bc40-ba390f4c05d6" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="df057f7e-f59a-0057-5000-09abb650442a" Name="pk_TemplateEditRoles">
		<SchemeIndexedColumn Column="df057f7e-f59a-0057-3100-09abb650442a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="df057f7e-f59a-0057-7000-09abb650442a" Name="idx_TemplateEditRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="df057f7e-f59a-0157-4000-09abb650442a" />
	</SchemeIndex>
	<SchemeIndex ID="badd3372-0876-48fd-bbfb-5b0e8c03de4e" Name="ndx_TemplateEditRoles_IDRoleID" IsUnique="true">
		<SchemeIndexedColumn Column="df057f7e-f59a-0157-4000-09abb650442a" />
		<SchemeIndexedColumn Column="a64a8ad1-0b33-0071-4000-02390a22b690" />
	</SchemeIndex>
</SchemeTable>