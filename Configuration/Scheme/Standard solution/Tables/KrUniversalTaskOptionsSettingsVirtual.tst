<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="49f11daf-636a-4342-aa2b-ea798bed7263" Name="KrUniversalTaskOptionsSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с вариантами завершения этапа универсального задания</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="49f11daf-636a-0042-2000-0a798bed7263" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="49f11daf-636a-0142-4000-0a798bed7263" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="49f11daf-636a-0042-3100-0a798bed7263" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="6d076d3d-e96e-40f9-aacd-0418b6fa2eaf" Name="OptionID" Type="Guid Not Null">
		<Description>Идентификатор варианта завершения. Должен быть в формате Guid</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b82fc1ca-b360-4961-8acc-415620204856" Name="Caption" Type="String(128) Not Null">
		<Description>Заголовок варианта завершения</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="27f26266-d7b7-4f7f-a1c9-73207890296a" Name="ShowComment" Type="Boolean Not Null">
		<Description>Флаг определяет, нужен ли ввод комментария</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="38caae6a-2d2e-483b-a371-47ac09b2f939" Name="df_KrUniversalTaskOptionsSettingsVirtual_ShowComment" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1e06c8a4-fedb-495b-86c4-d4e1a94a449b" Name="Additional" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="845be8b5-3442-40bf-a30d-308fa7d5782a" Name="df_KrUniversalTaskOptionsSettingsVirtual_Additional" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="46135e6b-7848-4749-9b3f-5ad2bfa8e52e" Name="Order" Type="Int32 Null" />
	<SchemePhysicalColumn ID="8a505123-7693-4fd4-ae06-ba354414d421" Name="Message" Type="String(Max) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="49f11daf-636a-0042-5000-0a798bed7263" Name="pk_KrUniversalTaskOptionsSettingsVirtual">
		<SchemeIndexedColumn Column="49f11daf-636a-0042-3100-0a798bed7263" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="49f11daf-636a-0042-7000-0a798bed7263" Name="idx_KrUniversalTaskOptionsSettingsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="49f11daf-636a-0142-4000-0a798bed7263" />
	</SchemeIndex>
</SchemeTable>