<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="78bfc212-cad5-4d1d-8b91-a9c58562b9d5" Name="KrDocType" Group="Kr" InstanceType="Cards" ContentType="Entries">
	<Description>Тип документа</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="78bfc212-cad5-001d-2000-09c58562b9d5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="78bfc212-cad5-011d-4000-09c58562b9d5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2f9f6600-bc8e-491f-b71e-81c8c8ac5987" Name="Title" Type="String(128) Not Null">
		<Description>Отображаемое название типа документа</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a59f219b-dd4e-4199-a7f8-aa45d1c79548" Name="Description" Type="String(1024) Null">
		<Description>Описание типа документа</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="727257d6-d00e-4f9c-8bc7-755d4594112d" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<Description>Тип карточки, к которому применяется тип документа</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="727257d6-d00e-009c-4000-055d4594112d" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="77692b48-fb32-4f49-b3b7-a0d91d719960" Name="CardTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="0dfa4950-50e3-44fe-92ef-d5863a6ea3ea" Name="CardTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22">
			<Description>Name of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="68549b27-da46-46a8-b087-eb411da6b6b8" Name="UseApproving" Type="Boolean Not Null">
		<Description>Использовать согласование.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e36700ef-6d93-4246-80e7-c53ba7d61277" Name="df_KrDocType_UseApproving" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6e63969b-88c2-4b32-a6d0-0fbe454b7ec4" Name="UseRegistration" Type="Boolean Not Null">
		<Description>Использовать регистрацию</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f7180bfb-c7ca-4874-b324-8de2d02a5ac4" Name="df_KrDocType_UseRegistration" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7f5795a6-0ec0-4875-aed8-c5649fcc9924" Name="DocNumberRegularSequence" Type="String(Max) Null">
		<Description>Последовательность для автоматического выделения номера</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e7020079-cc7a-4c85-ab5a-000f7c26c33c" Name="DocNumberRegularFormat" Type="String(Max) Null">
		<Description>Формат полного номера.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="43012a87-07b3-4db9-bc6e-e1d23c3122c5" Name="AllowManualRegularDocNumberAssignment" Type="Boolean Not Null">
		<Description>Разрешить выделять вручную</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c9a30e0e-aaea-4b1a-8252-7c837b2b1e50" Name="df_KrDocType_AllowManualRegularDocNumberAssignment" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8f9e300d-008d-472e-9186-8e3fe7913074" Name="DocNumberRegistrationSequence" Type="String(Max) Null">
		<Description>Имя последовательности для выделения номера при регистрации</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f6e1e16d-eee8-4fe6-94d7-2f5f5efa00d3" Name="DocNumberRegistrationFormat" Type="String(Max) Null">
		<Description>Формат полного номера при регистрации</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="143d56b6-e419-4ef8-97cc-3c72783e3963" Name="AllowManualRegistrationDocNumberAssignment" Type="Boolean Not Null">
		<Description>Разрешить выделять номер вручную для зарегестрированной карточки</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f3b936ea-96fa-4911-a2f3-ee2822d3f581" Name="df_KrDocType_AllowManualRegistrationDocNumberAssignment" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b5e4da68-558e-46a7-9225-7ec93ed7ae63" Name="DocNumberRegistrationAutoAssignment" Type="Reference(Typified) Not Null" ReferencedTable="b965332c-296b-48e3-b16f-21a0cd8a6a25" WithForeignKey="false">
		<SchemeReferencingColumn IsPermanent="true" ID="034c138c-ce2d-4623-8eb2-d610b8983c40" Name="DocNumberRegistrationAutoAssignmentID" Type="Int32 Not Null" ReferencedColumn="dd4b2d82-5ed5-4765-9f07-37ae3ab7eb3f" />
		<SchemeReferencingColumn ID="a0cc2a23-517b-4154-aeff-af54793cfcd7" Name="DocNumberRegistrationAutoAssignmentDescription" Type="String(128) Not Null" ReferencedColumn="4ef823f7-c84d-42dd-8689-ed3571b19c3c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9d178c09-302b-47cf-915a-aaf5ff62cf81" Name="DocNumberRegularAutoAssignment" Type="Reference(Typified) Not Null" ReferencedTable="83b4c03f-fdb8-4e11-bca4-02177dd4b3dc" WithForeignKey="false">
		<SchemeReferencingColumn IsPermanent="true" ID="76d1881b-068e-4fb0-bc91-65e999c1f86d" Name="DocNumberRegularAutoAssignmentID" Type="Int32 Not Null" ReferencedColumn="7ef0f81c-6121-447c-9a2c-21bbdcaf3707" />
		<SchemeReferencingColumn ID="39676ed5-78d2-4d9f-9b1c-c6d5a83df495" Name="DocNumberRegularAutoAssignmentDescription" Type="String(128) Not Null" ReferencedColumn="a769e235-b237-4f3a-be39-f1e7602fe9da" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="61803c98-dee2-45f5-a9a0-dff3cc8bebad" Name="ReleaseRegularNumberOnFinalDeletion" Type="Boolean Not Null">
		<Description>Флаг освобождения вторичного номера при окончательном удалении</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9e915cdf-0543-4e0f-ba29-4f5e949aeb2b" Name="df_KrDocType_ReleaseRegularNumberOnFinalDeletion" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="516baff8-d991-4eab-9076-b3be1c22ae26" Name="ReleaseRegistrationNumberOnFinalDeletion" Type="Boolean Not Null">
		<Description>Флаг освобождения первичного номера при окончательном удалении</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="7900021b-8a4f-4df0-9b30-074e2fa80c41" Name="df_KrDocType_ReleaseRegistrationNumberOnFinalDeletion" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="02020d4c-1c83-49df-9076-edf3209a782e" Name="UseResolutions" Type="Boolean Not Null">
		<Description>Использовать резолюции.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a90541da-5356-4705-bf2e-2dfc12d54af6" Name="df_KrDocType_UseResolutions" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f0a37e2b-4804-4d38-8044-f7addd1811be" Name="DisableChildResolutionDateCheck" Type="Boolean Not Null">
		<Description>Признак того, что в резолюциях отключается проверка на соответствие даты запланированного завершения дочерней резолюции к дате запланированного завершения родительской резолюции.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d7afedc1-05c7-4284-ad59-8c0b89e3933d" Name="df_KrDocType_DisableChildResolutionDateCheck" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ac81f58b-a61c-47aa-8ad9-8822778034fc" Name="UseAutoApprove" Type="Boolean Not Null">
		<Description>Автоматически согласовывать просроченные задания согласования</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="e0dbe361-1dbe-4d4e-912e-d321cf26af98" Name="df_KrDocType_UseAutoApprove" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3ae31e82-e33f-427f-b42e-b40e59da1131" Name="ExceededDays" Type="Double Not Null">
		<Description>Завершать при превышении срока более чем (рабочих дней)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a5e71b92-79bb-4f8e-afe5-871ab830e3e7" Name="df_KrDocType_ExceededDays" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8f24419c-e24b-4d4d-a8fb-83ac8a201fa2" Name="NotifyBefore" Type="Double Null">
		<Description>Уведомлять за N дней до завершения</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="1bf20b7a-16aa-4324-b2f2-31878c09e88d" Name="df_KrDocType_NotifyBefore" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="45e7cf04-2eee-4f2d-891c-4081ea67f5e3" Name="AutoApproveComment" Type="String(Max) Null">
		<Description>Комментарий при завершении</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b20ab8c7-7b82-493f-b4b4-80671324a891" Name="HideCreationButton" Type="Boolean Not Null">
		<Description>Скрытие кнопки создания карточки</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="b5a209c7-a6cf-4c8a-b531-8e137e536d2a" Name="df_KrDocType_HideCreationButton" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="24a066ec-ffbe-4a8b-a462-1b158d558af0" Name="HideRouteTab" Type="Boolean Not Null">
		<Description>Скрытие вкладки маршрут</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="9c236ef1-beea-4202-96e8-00148dfac511" Name="df_KrDocType_HideRouteTab" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0c5cc612-43da-4977-aeaf-efe6dd4a48da" Name="UseForum" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5d867f64-18ab-4bfb-9043-4364ff3cfdaf" Name="df_KrDocType_UseForum" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f32886a5-61ba-42e0-aded-d6fa8da4b594" Name="UseDefaultDiscussionTab" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3723f293-248b-49de-891d-f80a29de0e9f" Name="df_KrDocType_UseDefaultDiscussionTab" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3eeb3539-2f1b-4851-bc3f-135674fecc49" Name="UseRoutesInWorkflowEngine" Type="Boolean Not Null">
		<Description>Использовать маршруты в бизнес-процессах.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="55e5462d-6884-4a3a-a353-54874e5a9539" Name="df_KrDocType_UseRoutesInWorkflowEngine" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="78bfc212-cad5-001d-5000-09c58562b9d5" Name="pk_KrDocType" IsClustered="true">
		<SchemeIndexedColumn Column="78bfc212-cad5-011d-4000-09c58562b9d5" />
	</SchemePrimaryKey>
	<SchemeIndex ID="eaad5b3e-bf38-4cc4-a371-8988fd5b15f6" Name="ndx_KrDocType_CardTypeID">
		<SchemeIndexedColumn Column="727257d6-d00e-009c-4000-055d4594112d" />
	</SchemeIndex>
</SchemeTable>