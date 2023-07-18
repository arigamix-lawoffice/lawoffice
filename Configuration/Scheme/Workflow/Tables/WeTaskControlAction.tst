<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="adcf3458-d724-4411-9059-60bdb353a9b5" Name="WeTaskControlAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для действия Управление заданием</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="adcf3458-d724-0011-2000-00bdb353a9b5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="adcf3458-d724-0111-4000-00bdb353a9b5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c233c267-22e5-4069-b03a-e9fddcb2ddfb" Name="Digest" Type="String(1) Not Null">
		<Description>Описание задания</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4cbeeab1-0012-4115-92d0-4503903e608d" Name="Planned" Type="DateTime Null">
		<Description>Дата завершения</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="1b34b510-e064-4f07-97c9-64bf253d83d8" Name="Role" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Роль ждя задания</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1b34b510-e064-0007-4000-04bf253d83d8" Name="RoleID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="d088848b-e62d-488c-b1b2-00d1d5ed7296" Name="RoleName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ee6f9351-140c-4c8a-ad7d-b414825bb592" Name="Option" Type="Reference(Typified) Null" ReferencedTable="08cf782d-4130-4377-8a49-3e201a05d496">
		<Description>Вариант завершения</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ee6f9351-140c-008a-4000-0414825bb592" Name="OptionID" Type="Guid Null" ReferencedColumn="132dc5f5-ce87-4dd0-acce-b4a02acf7715" />
		<SchemeReferencingColumn ID="bf752d10-984b-4951-8644-bdf46e163261" Name="OptionCaption" Type="String(128) Null" ReferencedColumn="6762309a-b0ff-4b2f-9cce-dd111116e554" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="d0a495ee-3da7-4ffb-91b8-247bbfab3578" Name="ControlType" Type="Reference(Typified) Not Null" ReferencedTable="ab612473-e0a2-4dd7-b05e-d9bbdf06b62f">
		<Description>Тип управления над заданием</Description>
		<SchemeReferencingColumn ID="50b30e66-2b8d-4614-a232-36e72fdc04f4" Name="ControlTypeName" Type="String(128) Not Null" ReferencedColumn="6181923c-bd93-4e41-977c-427f083995e4" />
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d0a495ee-3da7-00fb-4000-047bbfab3578" Name="ControlTypeID" Type="Int32 Not Null" ReferencedColumn="d10c759b-79b0-4a8f-ae83-fbf86debcf7a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="adcf3458-d724-0011-5000-00bdb353a9b5" Name="pk_WeTaskControlAction" IsClustered="true">
		<SchemeIndexedColumn Column="adcf3458-d724-0111-4000-00bdb353a9b5" />
	</SchemePrimaryKey>
</SchemeTable>