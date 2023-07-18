<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b939817b-bc1f-4a9d-87ef-694336870eed" Name="ApplicationNames" Group="System">
	<Description>Имена стандартных приложений.</Description>
	<SchemePhysicalColumn ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор приложения. Соответствует идентификатору в сессии, а также идентификаторам в классе Applications.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя приложения. Может быть строкой локализации.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="25f96a82-3a13-4604-b427-72789024f32e" Name="IsHidden" Type="Boolean Not Null">
		<Description>Установка флага определяет, что элемент нужно скрыть. Если флаг снят, элемент нужно показать.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="14c4fec6-450d-4768-b09c-530bef84e93a" Name="df_ApplicationNames_IsHidden" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="d321c875-a298-4f5f-aab7-e3431f6e2e79" Name="pk_ApplicationNames">
		<SchemeIndexedColumn Column="ac166b37-85ea-4bef-b0d2-ad3b95f3af69" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">00000000-0000-0000-0000-000000000000</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_Other</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">3bc38194-a881-4955-85e7-0c6be3031f45</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_TessaClient</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">35a85591-a7cf-4b33-8319-891207587af9</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_TessaAdmin</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">9b7d9877-2017-4a35-b612-5f83bec39df9</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_WebClient</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">0468baaf-3a52-43bb-8efb-40bf1757776d</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_TessaAppManager</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">fdd842ad-8318-42b8-b2bb-f8233b37199e</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_Chronos</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">6eb1fdba-7eac-4b70-9612-161dd9fbd511</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_TessaAdminConsole</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">false</IsHidden>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="ac166b37-85ea-4bef-b0d2-ad3b95f3af69">1e3386c4-4baa-4bb6-b6c9-64b699410372</ID>
		<Name ID="4b6a03c2-56c1-43b8-b46d-6f37f11fb979">$Enum_ApplicationNames_TessaClientNotifications</Name>
		<IsHidden ID="25f96a82-3a13-4604-b427-72789024f32e">true</IsHidden>
	</SchemeRecord>
</SchemeTable>