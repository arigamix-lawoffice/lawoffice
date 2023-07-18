<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a53d9011-97c3-4890-97b8-c19c91ae8948" Name="KrSigningStageSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a53d9011-97c3-0090-2000-019c91ae8948" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a53d9011-97c3-0190-4000-019c91ae8948" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a10aba71-a43b-4caa-a493-dadb74f04dc3" Name="IsParallel" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="7a747c66-d214-43d3-82f7-b92bb6ff163b" Name="df_KrSigningStageSettingsVirtual_IsParallel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="62b10cf2-f439-47d6-9dec-f13aa23bc2fa" Name="ReturnToAuthor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="88d53e07-b220-48d5-ad49-b8f3d851c54a" Name="df_KrSigningStageSettingsVirtual_ReturnToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="dc34edae-2539-4451-97eb-972850dcf7dd" Name="ReturnWhenDeclined" Type="Boolean Not Null">
		<Description>Признак того, что при отказном подписании после этапа нужно отправлять задание на редактирование</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f67ebcce-a59d-42bb-a5d7-89f9ab468fc8" Name="df_KrSigningStageSettingsVirtual_ReturnWhenDeclined" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bb503af3-5343-42c2-83a1-b9a1b881755c" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Возможность редактировать карточку при наличии задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="21508238-0f3e-470c-a915-3c37e51e3558" Name="df_KrSigningStageSettingsVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bdf372da-183d-44ce-befa-5130616573cd" Name="CanEditFiles" Type="Boolean Not Null">
		<Description>Возможность редактировать файлы при наличии задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="08f0a135-ac01-4003-89e2-11eef89f22a4" Name="df_KrSigningStageSettingsVirtual_CanEditFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8b9ce502-cbab-47ca-8356-1384ed4006fb" Name="Comment" Type="String(440) Null">
		<Description>Комментарий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d74306a5-08de-49de-9e33-13e158e42a82" Name="ChangeStateOnStart" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d69d68ac-5408-4a1e-b643-c8acefe9aa5a" Name="df_KrSigningStageSettingsVirtual_ChangeStateOnStart" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f47ed329-e9df-46a3-84e8-8f07ea87308e" Name="ChangeStateOnEnd" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="82c8e41a-c602-4f2e-bbf5-df6b16457a88" Name="df_KrSigningStageSettingsVirtual_ChangeStateOnEnd" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="45f0c8f8-f132-4b25-9688-82c856c8484a" Name="NotReturnEdit" Type="Boolean Not Null">
		<Description>Признак того, что при несогласовании или согласовании с установленным флагом "Вернуть после согласования" не будет выполняться переход в начало текущей группы этапов.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a89e365a-b5eb-4375-98e2-cbac42253137" Name="df_KrSigningStageSettingsVirtual_NotReturnEdit" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ba679703-57d0-4626-9135-0f3aaef19aa2" Name="AllowAdditionalApproval" Type="Boolean Not Null">
		<Description>Признак того, что разрешено дополнительное согласование.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c3cacf7b-36e6-4ac7-a57b-6de4e506aca2" Name="df_KrSigningStageSettingsVirtual_AllowAdditionalApproval" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a53d9011-97c3-0090-5000-019c91ae8948" Name="pk_KrSigningStageSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="a53d9011-97c3-0190-4000-019c91ae8948" />
	</SchemePrimaryKey>
</SchemeTable>