<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e8fe8b2a-428d-44b6-8328-ee2a7bb4d323" Name="FmUserSettingsVirtual" Group="Fm" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Виртуальная таблица для формы с настройками</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8fe8b2a-428d-00b6-2000-0e2a7bb4d323" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e8fe8b2a-428d-01b6-4000-0e2a7bb4d323" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="9dcef99a-dbbe-4803-838b-5a0be5259033" Name="IsNotShowMsgIndicatorOnStartup" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="91219f93-22f9-4f2c-970a-5eb6525ad1c7" Name="df_FmUserSettingsVirtual_IsNotShowMsgIndicatorOnStartup" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="640cd55d-1782-4582-b504-d982f6ce85d6" Name="EnableMessageIndicator" Type="Boolean Not Null">
		<Description>Признак, что индикатор уведомлений включен (будет показывать новые сообщения в определенный период)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="8ab88f0d-84ea-4b61-a776-63db9666098e" Name="df_FmUserSettingsVirtual_EnableMessageIndicator" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e8fe8b2a-428d-00b6-5000-0e2a7bb4d323" Name="pk_FmUserSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="e8fe8b2a-428d-01b6-4000-0e2a7bb4d323" />
	</SchemePrimaryKey>
</SchemeTable>