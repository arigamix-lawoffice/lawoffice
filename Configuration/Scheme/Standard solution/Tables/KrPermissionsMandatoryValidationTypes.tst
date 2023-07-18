<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="4439a1f6-c747-442b-b315-caae1c934058" Name="KrPermissionsMandatoryValidationTypes" Group="Kr">
	<Description>Список типов проверки обязательности</Description>
	<SchemePhysicalColumn ID="5d57253a-2583-479d-86df-e00c72b335a4" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="1a620d37-9710-4302-8ab8-17c7d5f0b96d" Name="Name" Type="String(Max) Not Null" />
	<SchemePrimaryKey ID="0d7fcec0-770e-4c51-84bf-eff1d120cf21" Name="pk_KrPermissionsMandatoryValidationTypes">
		<SchemeIndexedColumn Column="5d57253a-2583-479d-86df-e00c72b335a4" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="5d57253a-2583-479d-86df-e00c72b335a4">0</ID>
		<Name ID="1a620d37-9710-4302-8ab8-17c7d5f0b96d">$KrPermissions_MandatoryValidationType_Always</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5d57253a-2583-479d-86df-e00c72b335a4">1</ID>
		<Name ID="1a620d37-9710-4302-8ab8-17c7d5f0b96d">$KrPermissions_MandatoryValidationType_OnTaskCompletion</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5d57253a-2583-479d-86df-e00c72b335a4">2</ID>
		<Name ID="1a620d37-9710-4302-8ab8-17c7d5f0b96d">$KrPermissions_MandatoryValidationType_WhenOneFieldFilled</Name>
	</SchemeRecord>
</SchemeTable>