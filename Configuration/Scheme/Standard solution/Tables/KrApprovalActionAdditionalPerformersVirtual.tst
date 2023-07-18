<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="94a86f8e-ff0f-44fd-933b-9c7af3f35a13" Name="KrApprovalActionAdditionalPerformersVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Параметры дополнительного согласования для действия "Согласование".</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="94a86f8e-ff0f-00fd-2000-0c7af3f35a13" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="94a86f8e-ff0f-01fd-4000-0c7af3f35a13" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="94a86f8e-ff0f-00fd-3100-0c7af3f35a13" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fc6be46e-a3ec-4b96-b183-f4adea743bc7" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<Description>Дополнительный согласующий.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fc6be46e-a3ec-0096-4000-04adea743bc7" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="76c3ef24-3993-4f0e-bee4-e0ba34ba1ec5" Name="RoleName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b107676c-2557-4145-87ae-fbc1d7a7260e" Name="Order" Type="Int32 Not Null">
		<Description>Порядковый номер.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8c5a9e5a-165e-4401-9b2c-a290ab3bd2f7" Name="MainApprover" Type="Reference(Typified) Not Null" ReferencedTable="eea339fd-2e18-415b-b338-418f84ac961e">
		<Description>Согласующий для которого настраивается дополнительное согласование.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8c5a9e5a-165e-0001-4000-0290ab3bd2f7" Name="MainApproverRowID" Type="Guid Not Null" ReferencedColumn="eea339fd-2e18-005b-3100-018f84ac961e" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="cd12ebe2-39a0-41ec-b392-294f04bdb5f9" Name="IsResponsible" Type="Boolean Not Null">
		<Description>Исполнитель является ответственным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="923e87c2-ce76-44e7-b5f1-9368f88cac4c" Name="df_KrApprovalActionAdditionalPerformersVirtual_IsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="94a86f8e-ff0f-00fd-5000-0c7af3f35a13" Name="pk_KrApprovalActionAdditionalPerformersVirtual">
		<SchemeIndexedColumn Column="94a86f8e-ff0f-00fd-3100-0c7af3f35a13" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="94a86f8e-ff0f-00fd-7000-0c7af3f35a13" Name="idx_KrApprovalActionAdditionalPerformersVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="94a86f8e-ff0f-01fd-4000-0c7af3f35a13" />
	</SchemeIndex>
</SchemeTable>