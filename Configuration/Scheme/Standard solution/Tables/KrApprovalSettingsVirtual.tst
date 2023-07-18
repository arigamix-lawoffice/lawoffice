<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5a48521b-e00c-44b6-995e-8f238e9103ff" Name="KrApprovalSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5a48521b-e00c-00b6-2000-0f238e9103ff" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5a48521b-e00c-01b6-4000-0f238e9103ff" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="86cda99c-0448-4a70-87d2-750cd9807c7e" Name="IsParallel" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="458fe42a-f6a6-483f-899d-af2416cd312a" Name="df_KrApprovalSettingsVirtual_IsParallel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="be1baf43-74f4-4af4-8f10-8908729ee7ce" Name="ReturnToAuthor" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="65420b6d-7b99-4247-954d-edba6e4e3d44" Name="df_KrApprovalSettingsVirtual_ReturnToAuthor" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5d3898cd-47c4-480e-8bf0-49c8ef256123" Name="ReturnWhenDisapproved" Type="Boolean Not Null">
		<Description>Признак того, что при отказном согласовании после этапа нужно отправлять задание на редактирование</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6c2f8d43-74a9-4613-8372-60e0a37fa742" Name="df_KrApprovalSettingsVirtual_ReturnWhenDisapproved" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5f3120ab-77f7-4e61-98ab-2c2c3ec60c8c" Name="CanEditCard" Type="Boolean Not Null">
		<Description>Возможность редактировать карточку при наличии задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="921805c3-f24e-40a9-8442-c97642e5dbbd" Name="df_KrApprovalSettingsVirtual_CanEditCard" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5d3fcb88-a6c9-4dc7-87c0-b671299bdedd" Name="CanEditFiles" Type="Boolean Not Null">
		<Description>Возможность редактировать файлы при наличии задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8290cd19-7682-4560-bc76-8d197d07f5d6" Name="df_KrApprovalSettingsVirtual_CanEditFiles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ce0117a9-bbdb-440c-8074-9ffe4d4deef6" Name="Comment" Type="String(440) Null">
		<Description>Комментарий</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="30295c13-1d8b-4ce8-a088-6155e03b3045" Name="DisableAutoApproval" Type="Boolean Not Null">
		<Description>Запретить автоматическое завершение заданий</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="6e68c56c-8f3f-4bcf-bc0e-be3e19cc79d1" Name="df_KrApprovalSettingsVirtual_DisableAutoApproval" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1bfe4f9f-25af-4ec0-80fd-d04a1cabb170" Name="FirstIsResponsible" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5db9980a-34ee-464e-bc02-cb9027f8acc2" Name="df_KrApprovalSettingsVirtual_FirstIsResponsible" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="79504dee-6ca7-49ba-854b-e1f49a4b4d9b" Name="ChangeStateOnStart" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="fc87e071-1e42-4449-9e1a-b9b23eb6c5bb" Name="df_KrApprovalSettingsVirtual_ChangeStateOnStart" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="53fd4675-70b5-41b3-bb64-ded154e5ca50" Name="ChangeStateOnEnd" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="ed265249-87ca-452d-b0f3-dbeb586c0bd7" Name="df_KrApprovalSettingsVirtual_ChangeStateOnEnd" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1ee432da-ceac-45cb-af78-a741222c1ea7" Name="Advisory" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e51949c7-ab3e-4aef-a8fd-2a76b460829b" Name="df_KrApprovalSettingsVirtual_Advisory" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9317d3c4-439c-4905-9f23-7808e1a030b1" Name="NotReturnEdit" Type="Boolean Not Null">
		<Description>Признак того, что при несогласовании или согласовании с установленным флагом "Вернуть после согласования" не будет выполняться переход в начало текущей группы этапов.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="3321b4bc-e539-4482-aa0f-fc27ec1b344e" Name="df_KrApprovalSettingsVirtual_NotReturnEdit" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5a48521b-e00c-00b6-5000-0f238e9103ff" Name="pk_KrApprovalSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="5a48521b-e00c-01b6-4000-0f238e9103ff" />
	</SchemePrimaryKey>
</SchemeTable>