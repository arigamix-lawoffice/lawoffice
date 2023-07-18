<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="62fd7bdd-0fc1-4370-afd6-54ac7e5320b4" Name="PersonalRoleSatellite" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Сателлит с настройками пользователя.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="62fd7bdd-0fc1-0070-2000-04ac7e5320b4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="62fd7bdd-0fc1-0170-4000-04ac7e5320b4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a636e615-e0c3-4b7d-928b-18e7f9bc18c4" Name="Language" Type="Reference(Typified) Null" ReferencedTable="1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a636e615-e0c3-007d-4000-08e7f9bc18c4" Name="LanguageID" Type="Int16 Null" ReferencedColumn="f13de4a3-34d7-4e7b-95b6-f34372ed724c" />
		<SchemeReferencingColumn ID="5f7fe43c-0977-4e18-9043-fae90cab3295" Name="LanguageCaption" Type="String(256) Null" ReferencedColumn="40a3d47c-40f7-48bd-ab8e-edef2f84094d" />
		<SchemeReferencingColumn ID="f9e78c02-9c3d-49e0-aa81-0de54f60d4a3" Name="LanguageCode" Type="AnsiString(3) Null" ReferencedColumn="9e7084bb-c1dc-4ace-90c9-800dbcf7f3c2" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="20449660-749a-4b50-9209-15da1fb705e4" Name="FormatName" Type="String(32) Null">
		<Description>FormatSettings.Name</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="045da3e0-df9b-4c8e-9307-ec34eba27529" Name="Settings" Type="BinaryJson Null">
		<Description>Настройки сотрудника, сериализованные в JSON. Настройки могут быть добавлены в типовом и в проектном решении.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5a701f61-69af-4a12-8c42-94837772be6a" Name="FilePreviewPosition" Type="Int16 Not Null">
		<Description>Местоположение области предпросмотра файлов в карточке.
См. перечисление CardFilePreviewPosition в API.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="a5537ef6-f50a-449b-bcfe-b7d68dc0d6dd" Name="df_PersonalRoleSatellite_FilePreviewPosition" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b169a62e-2a79-4163-9132-a38e595ce717" Name="FilePreviewIsHidden" Type="Boolean Not Null">
		<Description>Признак того, что область предпросмотра файлов скрыта.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8cd643c3-70ad-4ba1-933f-42c706a1503a" Name="df_PersonalRoleSatellite_FilePreviewIsHidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2940ad4e-35f3-4951-8710-d6c941e061a8" Name="FilePreviewWidthRatio" Type="Double Not Null">
		<Description>Отношение ширины области предпросмотра файлов к суммарной ширине области предпросмотра и области карточки.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8c331f3c-0084-48b7-8ca4-de922fa916be" Name="df_PersonalRoleSatellite_FilePreviewWidthRatio" Value="0.5" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3fe647b6-52d9-41ec-9576-105f3d14de5c" Name="TaskAreaWidth" Type="Double Not Null">
		<Description>Ширина области с заданиями в карточке.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="2357847f-d503-43ab-97ae-4ec6ee5d8cb5" Name="df_PersonalRoleSatellite_TaskAreaWidth" Value="450" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1483c8ae-67da-4e81-9a6e-e9108e15d472" Name="ContentWidthRatio" Type="Double Not Null">
		<Description>Отношение ширины области содержимого карточки (с заданиями) к суммарной ширине области редактора (вместе с пустым местом справа от карточки).</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="4d671a9d-5064-48c3-9ee0-47405fee09b6" Name="df_PersonalRoleSatellite_ContentWidthRatio" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6d0411f7-e6e6-4b54-9144-d4be04b9a242" Name="WebTheme" Type="String(Max) Null">
		<Description>Название css-файла темы для веб-клиента</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b4023dbd-23eb-4497-8cf9-a88c9aab3126" Name="WebWallpaper" Type="String(Max) Null">
		<Description>Названия файла с фоном для веб-клиента</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="78735a93-e5de-41b0-9494-6f273ac27b59" Name="WorkplaceExtensions" Type="BinaryJson Null">
		<Description>Настройки рабочих мест пользователя.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1e2afb47-9a07-484b-b4b9-f75a6ff17a30" Name="NotificationSettings" Type="BinaryJson Null">
		<Description>Настройки правил уведомлений сотрудника, сериализованные в JSON.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="9e792df9-67f4-4115-8bfc-85452078c57e" Name="UserSettingsLastUpdate" Type="DateTime Null" />
	<SchemePhysicalColumn ID="209ca01e-30d3-4d03-b459-e56c355a5fbe" Name="ForumSettings" Type="BinaryJson Null">
		<Description>Настройки системы форумов, сериализованные в JSON.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="cd3865af-2860-4690-9222-a8b9d7b384f0" Name="WebDefaultWallpaper" Type="String(Max) Null">
		<Description>Дефолтный корпоративный фон для ЛК</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fb17f8b4-93a5-49db-8a78-1e0eae08268f" Name="CardSettings" Type="BinaryJson Null">
		<Description>Настройки элементов карточки (контролов, блоков и др.), применимые к текущему пользователю и сериализованные в JSON.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="62fd7bdd-0fc1-0070-5000-04ac7e5320b4" Name="pk_PersonalRoleSatellite" IsClustered="true">
		<SchemeIndexedColumn Column="62fd7bdd-0fc1-0170-4000-04ac7e5320b4" />
	</SchemePrimaryKey>
</SchemeTable>