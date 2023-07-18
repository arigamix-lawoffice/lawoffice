<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="9d6186ba-f7ae-4910-8784-6c63a3b13179" Name="KrVirtualFileRoles" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Роли для карточки виртуального файла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9d6186ba-f7ae-0010-2000-0c63a3b13179" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9d6186ba-f7ae-0110-4000-0c63a3b13179" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="9d6186ba-f7ae-0010-3100-0c63a3b13179" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fcefedc6-055a-4253-8429-f7ce28fe8d0b" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fcefedc6-055a-0053-4000-07ce28fe8d0b" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="ebb09dfd-efc2-4cba-ae79-4512799fe81c" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="9d6186ba-f7ae-0010-5000-0c63a3b13179" Name="pk_KrVirtualFileRoles">
		<SchemeIndexedColumn Column="9d6186ba-f7ae-0010-3100-0c63a3b13179" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="9d6186ba-f7ae-0010-7000-0c63a3b13179" Name="idx_KrVirtualFileRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="9d6186ba-f7ae-0110-4000-0c63a3b13179" />
	</SchemeIndex>
	<SchemeIndex ID="1bf3fd57-094a-4119-a5a9-315ede69e422" Name="ndx_KrVirtualFileRoles_RoleIDID">
		<SchemeIndexedColumn Column="fcefedc6-055a-0053-4000-07ce28fe8d0b" />
		<SchemeIndexedColumn Column="9d6186ba-f7ae-0110-4000-0c63a3b13179" />
	</SchemeIndex>
</SchemeTable>