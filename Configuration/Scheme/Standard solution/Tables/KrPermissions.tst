<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="1c7406cb-e445-4d1a-bf00-a1116db39bc6" Name="KrPermissions" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для карточки настроек разрешений для бизнес-процесса.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c7406cb-e445-001a-2000-01116db39bc6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1c7406cb-e445-011a-4000-01116db39bc6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="13cd1998-4510-4a19-90fe-7de420df1170" Name="Caption" Type="String(128) Not Null">
		<Description>Название карточки с правами.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="262a15f8-b81c-496a-b582-e9a8657aef32" Name="Description" Type="String(Max) Null">
		<Description>Описание карточки с правами или Null, если описание не задано.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="39cd8f14-e889-4dcf-a391-c9839c8b8540" Name="Types" Type="String(Max) Not Null">
		<Description>Список типов карточек через запятую, включённых в секцию карточки KrPermissionTypes.
Используется для вывода в представлении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9119800f-c874-4d9e-a76c-f25a506ae6b7" Name="States" Type="String(1024) Not Null">
		<Description>Список состояний карточки через запятую, включённых в секцию карточки KrPermissionStates.
Используется для вывода в представлении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8a9aa3e3-dc18-4c51-b6b1-2e46688d38a8" Name="Roles" Type="String(Max) Not Null">
		<Description>Список ролей через запятую, включённых в секцию карточки KrPermissionRoles.
Используется для вывода в представлении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e143f13e-622a-4d00-9d61-d151353bee50" Name="Permissions" Type="String(1024) Not Null">
		<Description>Список разрешений через запятую, заданных в секции карточки KrPermissions.
Используется для вывода в представлении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b1536c72-c128-43e8-8d61-2d79a84789d2" Name="Conditions" Type="BinaryJson Null">
		<Description>Сериализованные данные с условиями к правилу доступа</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e0790931-bb63-4204-ab66-21d0bd12d18b" Name="CanCreateCard" Type="Boolean Not Null">
		<Description>Право на создание карточки.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="beb2e68e-eb06-49a4-b2dd-7b1e5e99415c" Name="df_KrPermissions_CanCreateCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e4126e40-61bd-4d27-950e-b894435c09b8" Name="CanReadCard" Type="Boolean Not Null">
		<Description>Право на чтение карточки.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6a926177-f3d9-4a41-bbd1-7be36257862f" Name="df_KrPermissions_CanReadCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="18db83a9-b8f3-4b64-9c6e-e5c5e0bd055e" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Право на редактирование данных карточки.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c1c49ee5-8e10-47bc-90ef-89ae03ae0cd1" Name="df_KrPermissions_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="10ef9692-8972-41ab-8606-acb48f1d071c" Name="CanEditFiles" Type="Boolean Not Null">
		<Description>Право на редактирование файлов карточки.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="128f063b-b66c-4966-9a5e-fdb75dcde8e2" Name="df_KrPermissions_CanEditFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a9a05d69-13dc-440d-84bf-86df0ffb1f62" Name="CanAddFiles" Type="Boolean Not Null">
		<Description>Право на добавление файлов в карточку.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ca27623d-0051-407d-89c9-7547dca5e706" Name="df_KrPermissions_CanAddFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f669b26d-95f7-4e53-94a8-b10774e006b1" Name="CanEditRoute" Type="Boolean Not Null">
		<Description>Право на редактирование маршрута согласования.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="12b4e162-4849-4e84-a207-c119fe0e1607" Name="df_KrPermissions_CanEditRoute" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="84127fa0-e042-4ab8-a75f-4df5a59cafb0" Name="CanDeleteCard" Type="Boolean Not Null">
		<Description>Право на удаление карточки.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="119fb5b1-4fde-4587-ac98-f9528843e88d" Name="df_KrPermissions_CanDeleteCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="da4a68b1-ce2b-4ae0-b91b-5ff457d0e8e5" Name="CanStartProcess" Type="Boolean Not Null">
		<Description>Право на запуск процесса согласования.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="89aed279-201c-4ff7-8d71-f163d2a1679b" Name="df_KrPermissions_CanStartProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8337c48f-583e-4bc2-bae6-5147b39d052b" Name="CanRejectProcess" Type="Boolean Not Null">
		<Description>Право на отзыв процесса согласования.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="81401e90-b014-43ff-9eee-0c1871978ae2" Name="df_KrPermissions_CanRejectProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c38052c7-fe38-45ea-9430-b9a5784c9306" Name="CanRebuildProcess" Type="Boolean Not Null">
		<Description>Право на возврат карточки на доработку при активном процессе согласования.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="5bb6ac6d-5941-4731-aa40-381510856126" Name="df_KrPermissions_CanRebuildProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5efb39b0-7828-416a-8161-3a3f343dc280" Name="CanCancelProcess" Type="Boolean Not Null">
		<Description>Право на отмену процесса согласования и перевод документа в состояние "Отмена".
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="87b225b5-dd82-4a90-b5c9-2533505e3c73" Name="df_KrPermissions_CanCancelProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb51ec21-69f6-46a1-acf1-40dcf9247338" Name="CanRegisterCard" Type="Boolean Not Null">
		<Description>Право на регистрацию процесса согласования и перевод документа в состояние "Зарегистрирован".
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="16c55559-0999-48f0-9402-b2a1ac3c1184" Name="df_KrPermissions_CanRegisterCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5a2f1dd3-52d9-4ad2-9ee7-d123afb5e7bb" Name="CanSeeInCatalog" Type="Boolean Not Null">
		<Description>Право видеть карточку в представлениях, учитывающих права доступа к карточке.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="23b780ad-f479-4676-b7f3-5acc6dff638a" Name="df_KrPermissions_CanSeeInCatalog" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bf328e4a-2fa1-443f-872e-b106545e659e" Name="CanEditNumber" Type="Boolean Not Null">
		<Description>Возможность редактирования/выделения/освобождения номера
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8d679b80-83a7-4984-b87e-232efb1f8b5e" Name="df_KrPermissions_CanEditNumber" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8ddae2d0-1472-414e-a77e-b0f413277946" Name="CanCreateResolutions" Type="Boolean Not Null">
		<Description>Право на создание резолюций.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3823009f-69bc-4cfb-a44f-0ecbf60bbe21" Name="df_KrPermissions_CanCreateResolutions" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5d1d78a3-52ef-4cb3-85bd-111807c08bb3" Name="CanDeleteFiles" Type="Boolean Not Null">
		<Description>Право на удаление файлов.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a0f03057-a38a-476f-b9be-152140658570" Name="df_KrPermissions_CanDeleteFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8f533af2-2f3c-4670-8250-eef44ddf1bec" Name="CanEditOwnFiles" Type="Boolean Not Null">
		<Description>Право на редактирование собственных файлов.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="865409ae-11d7-46cd-81b7-fc052f61e576" Name="df_KrPermissions_CanEditOwnFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4aaadef7-9a4d-4e44-a0b7-76ef2dab88f3" Name="CanDeleteOwnFiles" Type="Boolean Not Null">
		<Description>Право на удаление собственных файлов.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e10d2d41-db74-47e0-aad6-fc72b1cd2ed2" Name="df_KrPermissions_CanDeleteOwnFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="35cccdae-8e00-4fad-a586-5378a6d0f66b" Name="CanSignFiles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="a42d2746-241f-40ab-b1b8-671cb9e57ad4" Name="df_KrPermissions_CanSignFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9325d23d-0729-4906-8aba-38ab98ee3332" Name="CanAddTopics" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="bd2b2ca9-7614-4157-87a9-a6f1c8000fc7" Name="df_KrPermissions_CanAddTopics" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3f57ffbb-d844-4740-afec-3455139cc8c8" Name="CanSuperModeratorMode" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="53c82520-7d45-4e6f-9557-d3bfdb54d9a5" Name="df_KrPermissions_CanSuperModeratorMode" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1b73dc13-845a-4ee1-9ef6-fc47b9a191dc" Name="CanSubscribeForNotifications" Type="Boolean Not Null">
		<Description>Право на возможность подписываться на уведомления по карточке.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2d592071-34f2-4f3a-9119-e5e57ad427e5" Name="df_KrPermissions_CanSubscribeForNotifications" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="59539cb1-b14b-4738-8168-30af33b0f906" Name="IsExtended" Type="Boolean Not Null">
		<Description>Флаг определяет, что данное правило доступа поддерживает расширенные настройки прав доступа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="44916882-b37f-4e35-851f-6a2f45ffdd63" Name="df_KrPermissions_IsExtended" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="07a2ed5c-57f8-4fa9-a175-7c0b6b43d245" Name="IsRequired" Type="Boolean Not Null">
		<Description>Фоаг определяет, что флаги данного правила доступа всегда проверяются как Required при расчете прав</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e661d5aa-0a0a-4b8c-b41b-d8896cab284e" Name="df_KrPermissions_IsRequired" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a101eabe-bedf-4d03-8028-cab74347b1ac" Name="IsDisabled" Type="Boolean Not Null">
		<Description>Определяет, что правило доступа отключено</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="833a5296-8a13-43e2-9970-abf3e639707a" Name="df_KrPermissions_IsDisabled" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4f081ba0-adb3-48c3-a6a8-f76d7cb46847" Name="CanCreateTemplateAndCopy" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3c397c33-2042-4813-89f7-2b10cc4244cb" Name="df_KrPermissions_CanCreateTemplateAndCopy" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e63ac7ce-f035-4423-9f9d-7daa36378ce8" Name="CanSkipStages" Type="Boolean Not Null">
		<Description>Право на разрешение пропуска этапа.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e6bb0fa5-faef-41fa-8c54-4ba9da84f166" Name="df_KrPermissions_CanSkipStages" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2f0f12f1-dde6-4b3b-b333-e12da324f44f" Name="CanFullRecalcRoute" Type="Boolean Not Null">
		<Description>Право на полный пересчёт маршрута.
!ВАЖНО! не следует менять название этого столбца, т.к. при рассчете прав запрос на выборку флагов формируется по названию флага, в рассчете на то, что название столбца соотв. названию флага + "Can"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f62e44e2-690d-4f1e-ad5f-79572e80f766" Name="df_KrPermissions_CanFullRecalcRoute" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="66baeae0-6c7c-48e1-a201-0d598bed65b4" Name="CanEditMyMessages" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="fb388161-4cb9-4d78-b747-8b698a67e2c9" Name="df_KrPermissions_CanEditMyMessages" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1c1f58ed-5133-4ce9-962c-e95dcdd0ef7e" Name="CanEditAllMessages" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="59c19f8b-ea30-4f3c-be7a-4eebed4c8c3f" Name="df_KrPermissions_CanEditAllMessages" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5a54dfd5-6ab7-45f2-92be-691b66080b1e" Name="CanModifyAllTaskAssignedRoles" Type="Boolean Not Null">
		<Description>Может менять связанные с заданием роли в любом задании</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="293a39b9-431e-4363-9e8a-eaa683d63571" Name="df_KrPermissions_CanModifyAllTaskAssignedRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b5120b99-d848-4d73-bc22-1043c5bae166" Name="Priority" Type="Int32 Not Null">
		<Description>Приоритет правила доступа.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d807e25b-2849-4032-9359-7b679a1a139d" Name="df_KrPermissions_Priority" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="eabfabe0-ebaf-41e2-978b-c7ea38e2bc4c" Name="CanReadAllTopics" Type="Boolean Not Null">
		<Description>Может читать все обсуждения</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0c20fea1-565a-4f43-a91a-203e4cc27c11" Name="df_KrPermissions_CanReadAllTopics" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="86b143a7-e932-4503-9e77-0bfb68956f82" Name="CanReadAndSendMessageInAllTopics" Type="Boolean Not Null">
		<Description>Может читать и отправлять сообщения во всех обсуждениях</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c5dc8728-bd8d-4a7e-aa0b-37e4bda79ed2" Name="df_KrPermissions_CanReadAndSendMessageInAllTopics" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="30410302-fa81-436a-a07d-2b8ee5baccbc" Name="CanModifyOwnTaskAssignedRoles" Type="Boolean Not Null">
		<Description>Может менять связанные с заданием роли в своих заданиях</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a531d791-22e3-4602-bc70-9759f2357dd6" Name="df_KrPermissions_CanModifyOwnTaskAssignedRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="437e27a5-ce4e-4b8d-82d3-2293daa76e56" Name="AclGenerationRules" Type="String(Max) Not Null">
		<Description>Список правил расчёта ACL через запятую, включённых в секцию карточки KrPermissionAclGenerationRules.
Используется для вывода в представлении.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="430a9a45-acc7-4504-817c-cce89f2f03a1" Name="df_KrPermissions_AclGenerationRules" Value="" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1c7406cb-e445-001a-5000-01116db39bc6" Name="pk_KrPermissions" IsClustered="true">
		<SchemeIndexedColumn Column="1c7406cb-e445-011a-4000-01116db39bc6" />
	</SchemePrimaryKey>
</SchemeTable>