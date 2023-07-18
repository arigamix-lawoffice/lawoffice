<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="48dcb84f-1518-4de4-8995-86c4e75a1d03" Name="TaskConditionSettings" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Настройки для условий проверки задания.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="48dcb84f-1518-00e4-2000-06c4e75a1d03" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="48dcb84f-1518-01e4-4000-06c4e75a1d03" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7c429cb3-5630-4c68-9294-596ce409230b" Name="CheckTaskCreation" Type="Boolean Not Null">
		<Description>Определяет, нужно ли проверить создаваемые задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a200297c-5a70-45b1-98d5-5ddbcb944c41" Name="df_TaskConditionSettings_CheckTaskCreation" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c3baed60-ed66-41b5-b429-168e21de4e52" Name="CheckTaskCompletion" Type="Boolean Not Null">
		<Description>Определяет, нужно ли проверять завешаемые задания.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="051d2df7-07d5-4138-ba06-e23addb0f607" Name="df_TaskConditionSettings_CheckTaskCompletion" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="369ce492-c2be-483d-9dbc-df66a2d188c4" Name="CheckTaskFunctionRolesChanges" Type="Boolean Not Null">
		<Description>Определяет, нужно ли проверять изменение функциональных ролей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="501c28fa-05ec-41d1-88ad-f42cc0feda64" Name="df_TaskConditionSettings_CheckTaskFunctionRolesChanges" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="48dcb84f-1518-00e4-5000-06c4e75a1d03" Name="pk_TaskConditionSettings" IsClustered="true">
		<SchemeIndexedColumn Column="48dcb84f-1518-01e4-4000-06c4e75a1d03" />
	</SchemePrimaryKey>
</SchemeTable>