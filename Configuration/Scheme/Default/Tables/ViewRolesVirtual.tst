<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="08fccef5-fe25-4f3b-9a8c-2291b6a60209" Name="ViewRolesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="08fccef5-fe25-003b-2000-0291b6a60209" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="08fccef5-fe25-013b-4000-0291b6a60209" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="08fccef5-fe25-003b-3100-0291b6a60209" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="6d07f616-31b8-45f3-9e67-eea11ed2c63a" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6d07f616-31b8-00f3-4000-0ea11ed2c63a" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="47d9590f-6f99-411e-a5ed-05a7369a4c51" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="08fccef5-fe25-003b-5000-0291b6a60209" Name="pk_ViewRolesVirtual">
		<SchemeIndexedColumn Column="08fccef5-fe25-003b-3100-0291b6a60209" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="08fccef5-fe25-003b-7000-0291b6a60209" Name="idx_ViewRolesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="08fccef5-fe25-013b-4000-0291b6a60209" />
	</SchemeIndex>
</SchemeTable>