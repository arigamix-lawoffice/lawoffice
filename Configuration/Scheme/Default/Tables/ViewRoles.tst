<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="5a5dc5fe-19e1-4c69-b084-d6db36aa5a23" Name="ViewRoles" Group="System">
	<SchemeComplexColumn ID="217a763a-5e2d-4f44-b39f-28f5afb0e5b5" Name="View" Type="Reference(Typified) Not Null" ReferencedTable="3519b63c-eea0-48f4-b70a-544e58ece5fc">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="217a763a-5e2d-0044-4000-08f5afb0e5b5" Name="ViewID" Type="Guid Not Null" ReferencedColumn="8e4c45ad-ca6f-4f0f-be25-9a9e37a4cfd6" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3e8b7f7f-bc1a-4d38-a664-6a5808cd1dd9" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3e8b7f7f-bc1a-0038-4000-0a5808cd1dd9" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="adc90912-ab5a-4676-b7c1-f184fe6195c0" Name="pk_ViewRoles">
		<SchemeIndexedColumn Column="217a763a-5e2d-0044-4000-08f5afb0e5b5" />
		<SchemeIndexedColumn Column="3e8b7f7f-bc1a-0038-4000-0a5808cd1dd9" />
	</SchemePrimaryKey>
</SchemeTable>