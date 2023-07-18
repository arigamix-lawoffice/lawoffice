<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="844013f9-7faa-422a-b583-2b04ae46f0be" Name="SmartRoles" Group="Acl" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для записей настроек умных ролей.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="844013f9-7faa-002a-2000-0b04ae46f0be" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="844013f9-7faa-012a-4000-0b04ae46f0be" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1e0e85f9-823c-4ebc-8383-4b3276ce286e" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="5518f35a-ea30-4968-983d-aec524aeb710" WithForeignKey="false">
		<Description>Правило, которое добавило эту запись.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1e0e85f9-823c-00bc-4000-0b3276ce286e" Name="RuleID" Type="Guid Not Null" ReferencedColumn="5518f35a-ea30-0168-4000-0ec524aeb710" />
		<SchemeReferencingColumn ID="5eec675b-5484-4e7a-bece-d60b78327bf6" Name="RuleName" Type="String(128) Not Null" ReferencedColumn="976e584c-c374-428b-82ba-6eb043774c3d" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="0db2bc34-0d95-4726-b156-e3856cb18f20" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="844013f9-7faa-422a-b583-2b04ae46f0be" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0db2bc34-0d95-0026-4000-03856cb18f20" Name="ParentID" Type="Guid Null" ReferencedColumn="844013f9-7faa-012a-4000-0b04ae46f0be" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="965ce68c-991c-43f0-980b-fd805c41458c" Name="Owner" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="965ce68c-991c-00f0-4000-0d805c41458c" Name="OwnerID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="f20c87e4-8d29-46c6-a40f-ac5637678b28" Name="OwnerName" Type="String(256) Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="844013f9-7faa-002a-5000-0b04ae46f0be" Name="pk_SmartRoles" IsClustered="true">
		<SchemeIndexedColumn Column="844013f9-7faa-012a-4000-0b04ae46f0be" />
	</SchemePrimaryKey>
</SchemeTable>