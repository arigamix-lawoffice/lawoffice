<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba" Name="SmartRoleGenerators" Group="Acl" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для генераторов умных ролей.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f3a0dbc-2fc4-0069-2000-0b95f39970ba" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5f3a0dbc-2fc4-0169-4000-0b95f39970ba" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c8001380-3388-48c0-adca-da34e05fad04" Name="Name" Type="String(256) Not Null">
		<Description>Имя генератора умной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="881396b4-7c8f-4a39-b5aa-38ef1dc4167a" Name="RolesSelectorSql" Type="String(Max) Not Null">
		<Description>Запрос на получение списка ролей для умной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="738c006b-3621-404d-8fc1-717a78633895" Name="OwnersSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение списка всех владельцев умных ролей. Может быть не задан, если владельцем умной роли является само правило.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b5152123-d9b7-4cad-8b57-4011f7c3bc88" Name="RoleNameTemplate" Type="String(Max) Not Null">
		<Description>Шаблон имени умной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cfacf9f9-e060-4510-b13d-0baad638ba5f" Name="HideRoles" Type="Boolean Not Null">
		<Description>Определяет, должны ли умные роли, создаваемые данным правилом, быть скрытыми по умолчанию.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f86915ab-cc29-40f1-b2ba-99b7abdd617e" Name="df_SmartRoleGenerators_HideRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="62cf8224-933e-46f6-88b3-370db472dc82" Name="InitRoles" Type="Boolean Not Null">
		<Description>Определяет, что роли должны быть сгенерированы автоматически при создании или изменении генератора.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2b8d2658-3878-4c57-b471-b5bc78bf121b" Name="df_SmartRoleGenerators_InitRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2c79e52f-30e4-4e3e-aa70-aa6021907810" Name="OwnerDataSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение данных владельца роли по его идентификатору.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="79eab8e4-e850-4e71-a092-ca38efc3c6d1" Name="Description" Type="String(Max) Null">
		<Description>Описание генератора умной роли</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d6564ca7-41ee-42e4-a571-192c85fccd5d" Name="IsDisabled" Type="Boolean Not Null">
		<Description>Определяет, отключен ли данной генератор.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="4b9d8d07-d683-4aa3-8408-0e91649a813a" Name="df_SmartRoleGenerators_IsDisabled" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9bcd0800-b75e-428a-84b4-41315371b970" Name="EnableErrorLogging" Type="Boolean Not Null">
		<Description>Определяет, должны ли логироваться ошибки, возникающие при обработке данного генератора умных ролей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c3805260-ed58-47ee-aad4-379629cc80e3" Name="df_SmartRoleGenerators_EnableErrorLogging" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7fb7cc34-0d1a-4dc9-bb5d-9269bfb4ddb1" Name="Version" Type="Int32 Not Null">
		<Description>Версия генератора умных ролей.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="906bd266-89f6-4021-b0b8-59a61365238a" Name="df_SmartRoleGenerators_Version" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c2d92643-5f0e-46a5-a84f-a111cc76dc8e" Name="DisableDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ae221570-f9a8-42a0-9425-0f0de3324168" Name="df_SmartRoleGenerators_DisableDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5f3a0dbc-2fc4-0069-5000-0b95f39970ba" Name="pk_SmartRoleGenerators" IsClustered="true">
		<SchemeIndexedColumn Column="5f3a0dbc-2fc4-0169-4000-0b95f39970ba" />
	</SchemePrimaryKey>
</SchemeTable>