<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="24e6a4b4-7e51-4429-8bb7-648a840e026b" Name="AclGenerationRuleTriggers" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<Description>Триггеры правила генерации Acl.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="24e6a4b4-7e51-0029-2000-048a840e026b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="24e6a4b4-7e51-0129-4000-048a840e026b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="24e6a4b4-7e51-0029-3100-048a840e026b" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="12c740f7-cbf1-4b8c-ac10-dab75f6a8afd" Name="UpdateAclCardSelectorSql" Type="String(Max) Null">
		<Description>Запрос на получение списка карточек, которые должны быть обновлены при срабатывании триггера правила.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bc71e214-5651-4161-a3d4-ed6986e4a129" Name="OnlySelfUpdate" Type="Boolean Not Null">
		<Description>Определяет, что при срабатывании триггера должна обновиться только карточка, которая вызвала запустила этот триггер.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="240245f5-dbc7-4406-8a39-a5183b7c0e78" Name="df_AclGenerationRuleTriggers_OnlySelfUpdate" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0bf09dd3-280c-4080-b1c1-e9fda435627c" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="cd19ce01-bccd-4435-9647-9631561c98c4" Name="Conditions" Type="String(Max) Null">
		<Description>Сериализованные условия для триггера.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="68999dae-6dab-4a1d-bc5f-ab7f0923ee04" Name="Name" Type="String(256) Null" />
	<SchemePhysicalColumn ID="9e8d00c6-d501-4e3c-99dc-086426215308" Name="UpdateAsync" Type="Boolean Not Null">
		<Description>Флаг определяет, должно ли обновление происходить только асинхронно.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="151dcb32-280f-4738-9f66-4e458e16db43" Name="df_AclGenerationRuleTriggers_UpdateAsync" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5ed03d0b-92e4-4c70-846f-69356045c5c7" Name="ConditionsAndMode" Type="Boolean Not Null">
		<Description>Флаг опредеяет, что условия должна проверяться с оператором "И"</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d078bd16-3a87-472b-9a75-115596d2e527" Name="df_AclGenerationRuleTriggers_ConditionsAndMode" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="61607950-fd40-4ca2-a6a9-d00b048dc170" Name="UseRuleCardTypes" Type="Boolean Not Null">
		<Description>Определяет, должен ли триггер исползовать типы карточек, указанные в правиле расчёте ACL.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3f2b25da-66f7-4b8b-aeee-583d384268c9" Name="df_AclGenerationRuleTriggers_UseRuleCardTypes" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="24e6a4b4-7e51-0029-5000-048a840e026b" Name="pk_AclGenerationRuleTriggers">
		<SchemeIndexedColumn Column="24e6a4b4-7e51-0029-3100-048a840e026b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="24e6a4b4-7e51-0029-7000-048a840e026b" Name="idx_AclGenerationRuleTriggers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="24e6a4b4-7e51-0129-4000-048a840e026b" />
	</SchemeIndex>
</SchemeTable>