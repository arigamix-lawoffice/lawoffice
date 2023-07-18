<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0f717b89-050d-4a3f-97fc-4520eed77540" Name="KrSettingsRouteRoles" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Разрешения по ролям пользователя в маршрутах.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f717b89-050d-003f-2000-0520eed77540" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0f717b89-050d-013f-4000-0520eed77540" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f717b89-050d-003f-3100-0520eed77540" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="01edeaaf-3ae6-43e1-8fef-b1594086e87b" Name="StageRoles" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>ID роли, имеющей права на добавление этапа</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="01edeaaf-3ae6-00e1-4000-01594086e87b" Name="StageRolesID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="6973b729-9a2b-4b00-9d5b-931789a35b66" Name="StageRolesName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a47023f9-45e5-4d02-bd46-adec515e1c0c" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="39e6d38f-4e35-45e9-8c71-42a932dce18c" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a47023f9-45e5-0002-4000-0dec515e1c0c" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="39e6d38f-4e35-00e9-3100-02a932dce18c" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f717b89-050d-003f-5000-0520eed77540" Name="pk_KrSettingsRouteRoles">
		<SchemeIndexedColumn Column="0f717b89-050d-003f-3100-0520eed77540" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f717b89-050d-003f-7000-0520eed77540" Name="idx_KrSettingsRouteRoles_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0f717b89-050d-013f-4000-0520eed77540" />
	</SchemeIndex>
</SchemeTable>