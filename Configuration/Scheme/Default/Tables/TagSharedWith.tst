<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3b4e5980-82d8-4bce-adb3-4fbc88c3e03a" Name="TagSharedWith" Group="Tags" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей, которым доступен тег.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b4e5980-82d8-00ce-2000-0fbc88c3e03a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3b4e5980-82d8-01ce-4000-0fbc88c3e03a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b4e5980-82d8-00ce-3100-0fbc88c3e03a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="184fbcd8-c270-445a-894a-4094377ab0c6" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, для которой доступен тег.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="184fbcd8-c270-005a-4000-0094377ab0c6" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="303ea2b1-ef18-448f-a479-b12f1bb12dcb" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b4e5980-82d8-00ce-5000-0fbc88c3e03a" Name="pk_TagSharedWith">
		<SchemeIndexedColumn Column="3b4e5980-82d8-00ce-3100-0fbc88c3e03a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b4e5980-82d8-00ce-7000-0fbc88c3e03a" Name="idx_TagSharedWith_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3b4e5980-82d8-01ce-4000-0fbc88c3e03a" />
	</SchemeIndex>
	<SchemeIndex ID="06db683c-cfcd-4e23-8591-e4176c88e16d" Name="ndx_TagSharedWith_RoleID">
		<SchemeIndexedColumn Column="184fbcd8-c270-005a-4000-0094377ab0c6" />
	</SchemeIndex>
</SchemeTable>