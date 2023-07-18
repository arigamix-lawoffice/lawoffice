<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="3c8a5e77-c4da-45f5-b974-170af387ce26" Name="UserSettingsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Таблица с настройками сотрудника, предоставляемыми системой.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3c8a5e77-c4da-00f5-2000-070af387ce26" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3c8a5e77-c4da-01f5-4000-070af387ce26" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fc736fe0-25c9-4019-8508-f7c569e7c980" Name="LeftPanelOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что левая боковая панель открывается не при наведении мыши на полосу слева, а при клике по этой полосе.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f8af440d-9b1d-4e94-96c6-1602a84f574d" Name="df_UserSettingsVirtual_LeftPanelOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="85b393d3-db11-492a-8c8a-85fd85c07f9a" Name="LeftPanelTopAreaOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что левая боковая панель открывается не при наведении мыши на левый верхний угол экрана, а при клике по этой области.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="671eea59-9fcf-4460-a7f3-99379ce4af9a" Name="df_UserSettingsVirtual_LeftPanelTopAreaOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e0af2e5c-12da-40af-a728-a509e2b06e14" Name="LeftPanelBottomAreaOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что левая боковая панель открывается не при наведении мыши на левый нижний угол экрана, а при клике по этой области.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="59805ea7-1a5d-43d5-baa1-3f3fa2ab5fea" Name="df_UserSettingsVirtual_LeftPanelBottomAreaOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fc8394cb-543b-48bd-a596-6bdb94650b72" Name="RightPanelOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что правая боковая панель открывается не при наведении мыши на полосу справа, а при клике по этой полосе.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="87474d0c-be23-44c5-94bf-e4702b309c3d" Name="df_UserSettingsVirtual_RightPanelOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="81c2a8f8-341e-4bc0-936d-536b379cbb45" Name="RightPanelTopAreaOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что правая боковая панель открывается не при наведении мыши на правый верхний угол экрана, а при клике по этой области.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d31cd94a-0d47-4258-8614-1c4ef7c4efdf" Name="df_UserSettingsVirtual_RightPanelTopAreaOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="f28b6507-2066-4290-8f18-52929bcd1e4c" Name="RightPanelBottomAreaOpenOnClick" Type="Boolean Not Null">
		<Description>Признак того, что правая боковая панель открывается не при наведении мыши на правый нижний угол экрана, а при клике по этой области.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0900cd78-afbb-4a77-8166-cf8968016f11" Name="df_UserSettingsVirtual_RightPanelBottomAreaOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="440abf93-f19e-478a-b524-05003b0bd899" Name="DisableWindowFading" Type="Boolean Not Null">
		<Description>Признак того, что запрещено затемнение окна, когда оно не в фокусе.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="84f21f55-8ff7-4d68-a560-72c33f328c9f" Name="df_UserSettingsVirtual_DisableWindowFading" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5e94af85-b645-464a-b365-32dc83ba6a8c" Name="DisablePdfEmbeddedPreview" Type="Boolean Not Null">
		<Description>Признак того, что требуется отключить встроенный предпросмотр PDF и использовать внешнюю программу.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="952db566-e200-4206-89ad-1785dcafe228" Name="df_UserSettingsVirtual_DisablePdfEmbeddedPreview" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="513c2dcc-e677-4781-86b6-fa7412536722" Name="PreferPdfPagingPreview" Type="Boolean Not Null">
		<Description>Признак того, что для встроенного предпросмотра PDF предпочитается использование постраничного просмотра.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="91d58fdf-e49f-4c16-b0d2-99f946132e63" Name="df_UserSettingsVirtual_PreferPdfPagingPreview" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e0e4794-a24e-488d-ab82-ab961d332a6c" Name="DisablePopupNotifications" Type="Boolean Not Null">
		<Description>Признак того, что всплывающие уведомления в приложениях будут отключены.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="0981ef7b-0eb8-466a-a803-2f35a2ff06de" Name="df_UserSettingsVirtual_DisablePopupNotifications" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b52d3c1d-0b16-4877-b2bc-0a13f3b397a6" Name="WebLeftPanelOpenOnClick" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="e82006b6-7763-4040-8ae8-d680b5dba41c" Name="df_UserSettingsVirtual_WebLeftPanelOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2778e8db-5227-42c3-9606-919273622239" Name="WebRightPanelOpenOnClick" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="2f9dec54-8840-4d72-a85c-aaaf6147f46f" Name="df_UserSettingsVirtual_WebRightPanelOpenOnClick" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1208e0cb-2c2a-447a-8a7a-96d689c796e7" Name="AllowMultipleExternalPreview" Type="Boolean Not Null">
		<Description>Признак того, что требуется разрешить предпросмотр одновременно через несколько внешних программ (на разных вкладках).</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="46bdbf48-19a4-41b9-afa9-ae63eb225a11" Name="df_UserSettingsVirtual_AllowMultipleExternalPreview" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="73d4ccb7-7ceb-4a23-a75b-7ba291060653" Name="TaskColor" Type="Int32 Null">
		<Description>Цвет заданий по умолчанию, которые не подходят по функциональным ролям. NULL, если используется цвет из темы.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="53843190-2b9f-449d-a0f0-71fd893729db" Name="TopicItemColor" Type="Int32 Null">
		<Description>Цвет сообщений форумов в области с заданиями по умолчанию. NULL, если используется цвет из темы.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e6f6ce2f-44be-4e2b-8e5c-4de63eb99b04" Name="FrequentlyUsedEmoji" Type="String(2048) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3c8a5e77-c4da-00f5-5000-070af387ce26" Name="pk_UserSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="3c8a5e77-c4da-01f5-4000-070af387ce26" />
	</SchemePrimaryKey>
</SchemeTable>