<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f909d7b8-a840-4864-a2de-fd50c4475519" Name="KrApprovalActionAdditionalPerformersDisplayInfoVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Отображаемые параметры дополнительного согласования для действия "Согласование".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f909d7b8-a840-0064-2000-0d50c4475519" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f909d7b8-a840-0164-4000-0d50c4475519" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f909d7b8-a840-0064-3100-0d50c4475519" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="4c5037e8-fc09-4d32-820a-f53b0e737d61" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Дополнительный согласующий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4c5037e8-fc09-0032-4000-053b0e737d61" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="c6442c3f-d444-4ee9-8f69-b1003c5eaa22" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="914756f5-60ce-4647-b92d-6100abffab99" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="9878088a-ab37-4466-ad33-844fd35139ec" Name="MainApprover" Type="Reference(Typified) Not Null" ReferencedTable="eea339fd-2e18-415b-b338-418f84ac961e">
		<Description>Согласующий для которого настраивается дополнительное согласование.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9878088a-ab37-0066-4000-044fd35139ec" Name="MainApproverRowID" Type="Guid Not Null" ReferencedColumn="eea339fd-2e18-005b-3100-018f84ac961e" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7c95ee52-d1d4-4e91-a0fc-62c560849bc6" Name="IsResponsible" Type="Boolean Not Null">
		<Description>Исполнитель является ответственным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="184682a8-83f3-44db-b7dc-5447fd6b113a" Name="df_KrApprovalActionAdditionalPerformersDisplayInfoVirtual_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f909d7b8-a840-0064-5000-0d50c4475519" Name="pk_KrApprovalActionAdditionalPerformersDisplayInfoVirtual">
		<SchemeIndexedColumn Column="f909d7b8-a840-0064-3100-0d50c4475519" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f909d7b8-a840-0064-7000-0d50c4475519" Name="idx_KrApprovalActionAdditionalPerformersDisplayInfoVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f909d7b8-a840-0164-4000-0d50c4475519" />
	</SchemeIndex>
</SchemeTable>