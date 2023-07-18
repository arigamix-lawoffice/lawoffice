<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c6397b27-d2a4-4b67-9450-7bb19a69fbbf" Name="KrForkManagementSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c6397b27-d2a4-0067-2000-0bb19a69fbbf" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c6397b27-d2a4-0167-4000-0bb19a69fbbf" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="5ae8fab2-b309-4c55-bf9a-6fd42f00229f" Name="Mode" Type="Reference(Typified) Not Null" ReferencedTable="75e444ae-a785-4e30-a6e0-15020a31654d">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5ae8fab2-b309-0055-4000-0fd42f00229f" Name="ModeID" Type="Int16 Not Null" ReferencedColumn="f7410455-fcaf-4595-9a1a-d64e6f141769" />
		<SchemeReferencingColumn ID="51c66a1b-17d4-43ad-bbe6-c2a8bce7d03d" Name="ModeName" Type="String(128) Not Null" ReferencedColumn="dcf079bd-8726-45ef-a061-8e6ba0f400bb" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ef67f11a-2d8b-43a5-b5f0-fee67a95296f" Name="ManagePrimaryProcess" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9d11b61b-ecfb-497c-9024-3a577bcadeae" Name="df_KrForkManagementSettingsVirtual_ManagePrimaryProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b8ce95ec-2baa-4336-8ec6-01ec44519248" Name="DirectionAfterInterrupt" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="27f26a8d-f9fb-42fe-bb6c-628c506caaa0" Name="df_KrForkManagementSettingsVirtual_DirectionAfterInterrupt" Value="0" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c6397b27-d2a4-0067-5000-0bb19a69fbbf" Name="pk_KrForkManagementSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="c6397b27-d2a4-0167-4000-0bb19a69fbbf" />
	</SchemePrimaryKey>
</SchemeTable>