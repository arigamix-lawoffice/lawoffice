<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="5518f35a-ea30-4968-983d-aec524aeb710" Name="AclGenerationRules" Group="Acl" InstanceType="Cards" ContentType="Entries">
	<Description>Основная таблица для правил расчета ACL.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5518f35a-ea30-0068-2000-0ec524aeb710" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5518f35a-ea30-0168-4000-0ec524aeb710" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="976e584c-c374-428b-82ba-6eb043774c3d" Name="Name" Type="String(128) Not Null">
		<Description>Имя правила расчета.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0cf9c11d-cfea-4684-bda4-4c8f7e8384be" Name="Version" Type="Int32 Not Null">
		<Description>Версия правила генерации.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f9f40130-144a-4641-82dd-9200285f871e" Name="df_AclGenerationRules_Version" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b6f90e6e-1425-4dce-b8dc-2f0c5149bbc9" Name="RolesSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение списка ролей для Acl.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1e01fdcb-7ade-43b6-a4c8-8cbc692462f7" Name="DefaultUpdateAclCardSelectorSql" Type="String(Max) Null">
		<Description>Запрос по умолчанию на получение списка карточек, которые должны быть обновлены при срабатывании триггера правила.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1b62fce6-8111-4ed7-8855-3a4179622141" Name="UseSmartRoles" Type="Boolean Not Null">
		<Description>Флаг определяет, что должны использоваться умные роли.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1da5f1cf-04b7-4e56-8683-c5cf05f23e27" Name="df_AclGenerationRules_UseSmartRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="380efd39-70c8-4b0a-90d9-d4d360e45a2c" Name="CardOwnerSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение владельцев умных ролкй для определения роли Acl. Может быть не задан, если владельцем является сам генератор умных ролей или не используются умные роли.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="70f3868e-a347-4490-9f1c-df732e5a43a5" Name="SmartRoleGenerator" Type="Reference(Typified) Null" ReferencedTable="5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba">
		<Description>Генератор умной роли.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="70f3868e-a347-0090-4000-0f732e5a43a5" Name="SmartRoleGeneratorID" Type="Guid Null" ReferencedColumn="5f3a0dbc-2fc4-0169-4000-0b95f39970ba" />
		<SchemeReferencingColumn ID="34deb989-bc1f-4b1d-ad8a-3f1c7e450579" Name="SmartRoleGeneratorName" Type="String(256) Null" ReferencedColumn="c8001380-3388-48c0-adca-da34e05fad04" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fd1992da-c4bf-4534-9535-6447de9c6973" Name="Diescription" Type="String(Max) Null">
		<Description>Описание правила расчёта.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5aebe9a7-8978-4c06-a3b6-938c2b9f3da8" Name="IsDisabled" Type="Boolean Not Null">
		<Description>Определяет, отключено ли данное правило.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5ea0a725-ca60-4654-bd97-46ca41a99160" Name="df_AclGenerationRules_IsDisabled" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a38588ad-bcc0-4678-94d2-1b4e94595998" Name="EnableErrorLogging" Type="Boolean Not Null">
		<Description>Определяет, должны ли логироваться ошибки, возникающие при обработке данного правила расчёта ACL.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6b05f4c8-9678-4152-b310-9e7e41d933a9" Name="df_AclGenerationRules_EnableErrorLogging" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d782aa82-d6d5-4c7b-86b2-633fdb910929" Name="CardsByOwnerSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение карточек по владельцу умной роли для определения списка карточек на перерасчёт при добавлении умной роли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6a471d35-a5f6-4916-a4d1-3895fde9413c" Name="ExtensionsData" Type="BinaryJson Null">
		<Description>Данные расширений правила расчёта ACL</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5518f35a-ea30-0068-5000-0ec524aeb710" Name="pk_AclGenerationRules" IsClustered="true">
		<SchemeIndexedColumn Column="5518f35a-ea30-0168-4000-0ec524aeb710" />
	</SchemePrimaryKey>
</SchemeTable>