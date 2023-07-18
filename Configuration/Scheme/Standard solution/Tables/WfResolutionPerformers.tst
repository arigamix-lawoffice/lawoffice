<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0f62f90e-6b94-4301-866d-0138fb147939" Name="WfResolutionPerformers" Group="Wf" InstanceType="Tasks" ContentType="Collections">
	<Description>Исполнители создаваемой резолюции. Используются в заданиях совместно с таблицей WfResolutions.
В качестве исполнителя могут выступать несколько контекстных или обычных ролей.
Если указано более одной роли, то резолюция назначается на роль задания "Исполнители задания".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f62f90e-6b94-0001-2000-0138fb147939" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0f62f90e-6b94-0101-4000-0138fb147939" Name="ID" Type="Guid Not Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f62f90e-6b94-0001-3100-0138fb147939" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f7b25eea-d7a7-4d48-a8a9-cc6c7ed5b956" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль исполнителя. Может быть как контекстной, так и обычной ролью.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f7b25eea-d7a7-0048-4000-0c6c7ed5b956" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="20a39378-73de-41bd-aa84-317162253c20" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0">
			<Description>Отображаемое имя роли.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="879c610b-74db-4562-a238-d4e075ecad2d" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер исполнителя.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f421310f-dbac-490d-9d5b-4e30348ec57e" Name="df_WfResolutionPerformers_Order" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f62f90e-6b94-0001-5000-0138fb147939" Name="pk_WfResolutionPerformers">
		<SchemeIndexedColumn Column="0f62f90e-6b94-0001-3100-0138fb147939" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0f62f90e-6b94-0001-7000-0138fb147939" Name="idx_WfResolutionPerformers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0f62f90e-6b94-0101-4000-0138fb147939" />
	</SchemeIndex>
</SchemeTable>