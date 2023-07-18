<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="ad21dc6e-c694-4862-ba61-1df6b7506101" Name="WorkplaceRoles" Group="System">
	<Description>Роли, которые могут использовать рабочее место.</Description>
	<SchemeComplexColumn ID="0990bc68-1341-47ce-9b90-3b3f6a28b715" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, для которой включённые в неё пользователи могут использовать рабочее место.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0990bc68-1341-00ce-4000-0b3f6a28b715" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="66e9575a-ce69-4f5e-84da-3b76e3b5aac4" Name="Workplace" Type="Reference(Typified) Not Null" ReferencedTable="21cd7a4f-6930-4746-9a57-72481e951b02">
		<Description>Рабочее место.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="66e9575a-ce69-005e-4000-0b76e3b5aac4" Name="WorkplaceID" Type="Guid Not Null" ReferencedColumn="39f02b29-1a58-4409-a8fa-11756a2870f4">
			<Description>ПК</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="f18fae2d-7078-4595-ace9-b80f4fa299ef" Name="pk_WorkplaceRoles">
		<SchemeIndexedColumn Column="66e9575a-ce69-005e-4000-0b76e3b5aac4" />
		<SchemeIndexedColumn Column="0990bc68-1341-00ce-4000-0b3f6a28b715" />
	</SchemePrimaryKey>
</SchemeTable>