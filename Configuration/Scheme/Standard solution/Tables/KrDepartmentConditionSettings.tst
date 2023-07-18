<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="f753b988-0c00-471b-869d-0ac361af0d83" Name="KrDepartmentConditionSettings" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для условия для правил уведомлений, првоеряющая дополнительные настройки подразделения</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f753b988-0c00-001b-2000-0ac361af0d83" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f753b988-0c00-011b-4000-0ac361af0d83" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d3e9a960-3a0f-4c23-9287-1e09220dda74" Name="CheckAuthor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="21873555-15d0-4bb0-bfd2-0e33d8e22f81" Name="df_KrDepartmentConditionSettings_CheckAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0af86832-0852-42c3-aa22-a7c1b93a3eab" Name="CheckInitiator" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="4a6c0b31-6124-4082-b356-4f52f6b09f07" Name="df_KrDepartmentConditionSettings_CheckInitiator" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="23bd1678-604f-4d0d-8a3e-1797cbbf7fbd" Name="CheckCard" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="30fd6560-dc10-49b7-a169-cd47e84e6d4d" Name="df_KrDepartmentConditionSettings_CheckCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f753b988-0c00-001b-5000-0ac361af0d83" Name="pk_KrDepartmentConditionSettings" IsClustered="true">
		<SchemeIndexedColumn Column="f753b988-0c00-011b-4000-0ac361af0d83" />
	</SchemePrimaryKey>
</SchemeTable>