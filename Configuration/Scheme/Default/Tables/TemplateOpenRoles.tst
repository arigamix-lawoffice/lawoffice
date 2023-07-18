<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="831ff542-f2b2-4a3e-9295-0695b843567c" Name="TemplateOpenRoles" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Роли, которым доступен просмотр шаблона и создание карточки из шаблона помимо администраторов.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="831ff542-f2b2-003e-2000-0695b843567c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="831ff542-f2b2-013e-4000-0695b843567c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="831ff542-f2b2-003e-3100-0695b843567c" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="8d71d950-d942-44fe-a34f-a444d5b5f97b" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Любая роль, кроме контекстной и временной, для которой предоставляется доступ на открытие шаблона и создания из него карточки. Если пользователь не входит ни в одну роль, указанную в этой таблице, то он может открывать шаблон и создавать из него карточки только в том случае, если он является администратором, или если он входит в список ролей, для которых разрешено редактирование или удаление шаблона.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8d71d950-d942-00fe-4000-0444d5b5f97b" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="23a3504e-3a0f-4d25-bfbc-0de8c79cd57e" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="831ff542-f2b2-003e-5000-0695b843567c" Name="pk_TemplateOpenRoles">
		<SchemeIndexedColumn Column="831ff542-f2b2-003e-3100-0695b843567c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="831ff542-f2b2-003e-7000-0695b843567c" Name="idx_TemplateOpenRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="831ff542-f2b2-013e-4000-0695b843567c" />
	</SchemeIndex>
</SchemeTable>