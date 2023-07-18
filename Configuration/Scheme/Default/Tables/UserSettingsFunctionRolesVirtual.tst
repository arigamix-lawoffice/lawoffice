<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="0e7d4c80-0a90-40a6-86a7-01ec32c80ba9" Name="UserSettingsFunctionRolesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Таблица с настройками сотрудника, предоставляемыми системой для функциональных ролей заданий.
Такие настройки изменяются в метаинформации динамически, в зависимости от строк в таблице FunctionRoles.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0e7d4c80-0a90-00a6-2000-01ec32c80ba9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0e7d4c80-0a90-01a6-4000-01ec32c80ba9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0e7d4c80-0a90-00a6-5000-01ec32c80ba9" Name="pk_UserSettingsFunctionRolesVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="0e7d4c80-0a90-01a6-4000-01ec32c80ba9" />
	</SchemePrimaryKey>
</SchemeTable>