<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="d96c77c3-dec7-4332-b427-bf77ad09c546" Name="KrApprovalActionAdditionalPerformersSettingsVirtual" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Параметры дополнительного согласования для действия "Согласование" являющиеся едиными для всех доп. согласующих.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d96c77c3-dec7-0032-2000-0f77ad09c546" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d96c77c3-dec7-0132-4000-0f77ad09c546" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="37709cf6-56c2-4a4b-be33-20f01d29397f" Name="IsAdditionalApprovalFirstResponsible" Type="Boolean Not Null">
		<Description>Первый исполнитель в дополнительном согласовании является ответственным.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8255e34c-ba01-4205-ac8c-9ce08636be8d" Name="df_KrApprovalActionAdditionalPerformersSettingsVirtual_IsAdditionalApprovalFirstResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d96c77c3-dec7-0032-5000-0f77ad09c546" Name="pk_KrApprovalActionAdditionalPerformersSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="d96c77c3-dec7-0132-4000-0f77ad09c546" />
	</SchemePrimaryKey>
</SchemeTable>