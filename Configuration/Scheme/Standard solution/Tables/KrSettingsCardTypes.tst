<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="949c3849-eb4b-4d64-9676-f14f9c40dbcf" Name="KrSettingsCardTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="949c3849-eb4b-0064-2000-014f9c40dbcf" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="949c3849-eb4b-0164-4000-014f9c40dbcf" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="949c3849-eb4b-0064-3100-014f9c40dbcf" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d3a27eeb-e273-4b7c-8524-818d228d5fb7" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки, для которого может использоваться процесс согласования.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d3a27eeb-e273-007c-4000-018d228d5fb7" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="ee102138-61b7-4971-84ff-71e92b6efddf" Name="CardTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="40862546-cc52-46db-a101-d5c0f9770ada" Name="UseDocTypes" Type="Boolean Not Null">
		<Description>Используются ли типы документов для типа карточки</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0393ba5e-7c69-4dc1-9652-1f0d25c85748" Name="df_KrSettingsCardTypes_UseDocTypes" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="065ee4ce-87c2-4186-865f-2b7a21097e6d" Name="UseApproving" Type="Boolean Not Null">
		<Description>Использовать согласование.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8ded49e9-e76c-47b3-9bbf-88e5e58fc755" Name="df_KrSettingsCardTypes_UseApproving" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="529b5f7c-7639-447c-b781-9709f358f70b" Name="DocNumberRegularAutoAssignment" Type="Reference(Typified) Not Null" ReferencedTable="83b4c03f-fdb8-4e11-bca4-02177dd4b3dc" WithForeignKey="false">
		<SchemeReferencingColumn IsPermanent="true" ID="deb16f20-1690-4811-b247-cb70d409f0ca" Name="DocNumberRegularAutoAssignmentID" Type="Int32 Not Null" ReferencedColumn="7ef0f81c-6121-447c-9a2c-21bbdcaf3707" />
		<SchemeReferencingColumn ID="136b16d8-f91b-41e3-9773-7f6ce687e795" Name="DocNumberRegularAutoAssignmentDescription" Type="String(128) Not Null" ReferencedColumn="a769e235-b237-4f3a-be39-f1e7602fe9da" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="24438706-f615-44f5-8ce8-f776d5f73ecd" Name="DocNumberRegularSequence" Type="String(Max) Null">
		<Description>Последовательность для автоматического выделения номера</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="52b56d76-0555-4ea2-9e4c-b5f3a122df46" Name="DocNumberRegularFormat" Type="String(Max) Null">
		<Description>Формат полного номера.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2d677fb2-a540-4704-b1d3-15880bc7b23a" Name="AllowManualRegularDocNumberAssignment" Type="Boolean Not Null">
		<Description>Разрешить выделять вручную</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e00743f3-8a06-4933-9d2c-a22fd87c602d" Name="df_KrSettingsCardTypes_AllowManualRegularDocNumberAssignment" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b17fcec6-a516-475f-aa0c-39e722883726" Name="DocNumberRegistrationAutoAssignment" Type="Reference(Typified) Not Null" ReferencedTable="b965332c-296b-48e3-b16f-21a0cd8a6a25" WithForeignKey="false">
		<SchemeReferencingColumn IsPermanent="true" ID="d4cc4987-166c-4375-92ce-c8e67448bf99" Name="DocNumberRegistrationAutoAssignmentID" Type="Int32 Not Null" ReferencedColumn="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f" />
		<SchemeReferencingColumn ID="f2fda14e-e2c8-4954-8f89-e56a5f9d73cd" Name="DocNumberRegistrationAutoAssignmentDescription" Type="String(128) Not Null" ReferencedColumn="4ef823f7-c84d-42dd-8689-ed3571b19c3c" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a8b3636b-301f-4de6-9ffe-6628a019c8b8" Name="DocNumberRegistrationSequence" Type="String(Max) Null">
		<Description>Имя последовательности для выделения номера при регистрации</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bb36d722-b855-4746-8432-35b03adf6067" Name="DocNumberRegistrationFormat" Type="String(Max) Null">
		<Description>Формат полного номера при регистрации</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3b387651-9552-4e5a-958f-562d10c9fa90" Name="AllowManualRegistrationDocNumberAssignment" Type="Boolean Not Null">
		<Description>Разрешить выделять вручную</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="30b41f80-1be9-427c-bed4-5a467c83ba55" Name="df_KrSettingsCardTypes_AllowManualRegistrationDocNumberAssignment" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3e674e95-c603-4890-85db-d0b155b2d9a2" Name="UseRegistration" Type="Boolean Not Null">
		<Description>Использовать регистрацию</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="97a387a0-842e-4a51-8135-b78a3b8aaa2b" Name="df_KrSettingsCardTypes_UseRegistration" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="85230b57-1263-4739-8330-d2aa42a43fed" Name="ReleaseRegularNumberOnFinalDeletion" Type="Boolean Not Null">
		<Description>Флаг освобождения вторичного номера при окончательном удалении</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="18f72c42-d4d7-4293-acf4-f7b98e8835a6" Name="df_KrSettingsCardTypes_ReleaseRegularNumberOnFinalDeletion" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cad2c095-f536-43df-b7eb-ccedb6e6dcc9" Name="ReleaseRegistrationNumberOnFinalDeletion" Type="Boolean Not Null">
		<Description>Флаг освобождения первичного номера при окончательном удалении</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="68dc96c5-9e15-4e6b-9f88-b5ccc4d457ec" Name="df_KrSettingsCardTypes_ReleaseRegistrationNumberOnFinalDeletion" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b00b9dfa-4a0c-4380-9d86-d339fe9f28a4" Name="UseResolutions" Type="Boolean Not Null">
		<Description>Использовать резолюции.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e832bc7b-58df-417c-aeea-e3485b854373" Name="df_KrSettingsCardTypes_UseResolutions" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="34434d32-02a9-4ff9-9436-0cbc1283b62f" Name="DisableChildResolutionDateCheck" Type="Boolean Not Null">
		<Description>Признак того, что в резолюциях отключается проверка на соответствие даты запланированного завершения дочерней резолюции к дате запланированного завершения родительской резолюции.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f27dfdae-8c12-4731-b735-b80e425e2f7d" Name="df_KrSettingsCardTypes_DisableChildResolutionDateCheck" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4094eb1a-bbbe-4043-b9c5-3deed0861b8f" Name="UseAutoApprove" Type="Boolean Not Null">
		<Description>Автоматически согласовывать просроченные задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="96b6f79a-5404-4f65-be6d-2546e09f7947" Name="df_KrSettingsCardTypes_UseAutoApprove" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e525f3a6-3bb8-4843-a23e-d5feca55ab46" Name="ExceededDays" Type="Double Not Null">
		<Description>Завершать при превышении срока более чем (рабочих дней)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f8c288a8-4c6c-497d-a088-ee2b93cf6f92" Name="df_KrSettingsCardTypes_ExceededDays" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cedb1ede-3e11-4183-86d5-46264dedbcf7" Name="NotifyBefore" Type="Double Null">
		<Description>Уведомлять за N дней до завершения</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="bb2ccd05-32e2-4bd2-b6e8-af8c07545bb6" Name="df_KrSettingsCardTypes_NotifyBefore" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="56a1681f-a04b-4ebc-85a7-30943a482818" Name="AutoApproveComment" Type="String(Max) Null">
		<Description>Комментарий при завершении</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="39c5f2d2-df81-4beb-867b-042a6f7d1468" Name="HideCreationButton" Type="Boolean Not Null">
		<Description>Скрытие кнопки создания карточки</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8b9e452c-fe5c-4b1c-91e8-be81e3bc7ab4" Name="df_KrSettingsCardTypes_HideCreationButton" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6e1378fd-9539-4afd-b88e-94ebb2100fd1" Name="HideRouteTab" Type="Boolean Not Null">
		<Description>Скрытие вкладки маршрут</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ad3cda6c-0271-4eb2-b143-16763c506c62" Name="df_KrSettingsCardTypes_HideRouteTab" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b47e9c90-3afb-4158-a053-748e17a52004" Name="UseForum" Type="Boolean Not Null">
		<Description>Использовать систему обсуждений.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="51f641af-ceb5-48b6-9b7b-fe1394fdd82e" Name="df_KrSettingsCardTypes_UseForum" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e40b284-da90-4789-842c-bf4c234a6ba8" Name="UseDefaultDiscussionTab" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5add0523-325a-473b-9bda-59c6d2848a4d" Name="df_KrSettingsCardTypes_UseDefaultDiscussionTab" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="13e3c742-2d2b-4ffa-872f-371e16f816d5" Name="UseRoutesInWorkflowEngine" Type="Boolean Not Null">
		<Description>Использовать маршруты в бизнес-процессах.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="37867ba7-99d6-441c-911a-e2079896f457" Name="df_KrSettingsCardTypes_UseRoutesInWorkflowEngine" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="949c3849-eb4b-0064-5000-014f9c40dbcf" Name="pk_KrSettingsCardTypes">
		<SchemeIndexedColumn Column="949c3849-eb4b-0064-3100-014f9c40dbcf" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="949c3849-eb4b-0064-7000-014f9c40dbcf" Name="idx_KrSettingsCardTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="949c3849-eb4b-0164-4000-014f9c40dbcf" />
	</SchemeIndex>
	<SchemeIndex ID="b014d6c2-93ad-41cf-ace5-6c5685381e90" Name="ndx_KrSettingsCardTypes_CardTypeID" IsUnique="true">
		<SchemeIndexedColumn Column="d3a27eeb-e273-007c-4000-018d228d5fb7" />
	</SchemeIndex>
</SchemeTable>