<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="c4ee86f8-3022-432b-ae77-e1ca4a47c891" Name="TagEditors" Group="Tags" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c4ee86f8-3022-002b-2000-01ca4a47c891" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c4ee86f8-3022-012b-4000-01ca4a47c891" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c4ee86f8-3022-002b-3100-01ca4a47c891" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b3dba782-f7dc-41ba-b190-18f521015944" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль, для которой доступно редактирование тега.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b3dba782-f7dc-00ba-4000-08f521015944" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="8399c133-a484-44cc-a316-c40628e27660" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c4ee86f8-3022-002b-5000-01ca4a47c891" Name="pk_TagEditors">
		<SchemeIndexedColumn Column="c4ee86f8-3022-002b-3100-01ca4a47c891" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c4ee86f8-3022-002b-7000-01ca4a47c891" Name="idx_TagEditors_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c4ee86f8-3022-012b-4000-01ca4a47c891" />
	</SchemeIndex>
	<SchemeIndex ID="8ab51909-6181-4ff7-ba69-3fd4017200e5" Name="ndx_TagEditors_RoleID">
		<SchemeIndexedColumn Column="b3dba782-f7dc-00ba-4000-08f521015944" />
	</SchemeIndex>
</SchemeTable>