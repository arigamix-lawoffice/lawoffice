<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ef86e270-047b-4b7c-9c22-dda56e8eef2c" Name="KrEditSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef86e270-047b-007c-2000-0da56e8eef2c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ef86e270-047b-017c-4000-0da56e8eef2c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f090ed43-c823-4e0a-9a69-3243116e4a25" Name="ChangeState" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="b7659c1a-7756-4632-9db0-b6204a5d3dd2" Name="df_KrEditSettingsVirtual_ChangeState" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b823d41a-eb03-4379-a470-6d19efd2bba8" Name="Comment" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="831eaaa5-65cb-49df-b614-86a1cb696894" Name="IncrementCycle" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="0a164932-dc1a-40f4-ab26-9d26d11a36e3" Name="df_KrEditSettingsVirtual_IncrementCycle" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d2657c80-9d4f-471d-911d-e2e9a6c74c38" Name="DoNotSkipStage" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5220cd25-b705-470f-bc10-c1a10fd58dea" Name="df_KrEditSettingsVirtual_DoNotSkipStage" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3a83b5cd-f81f-4679-ba76-12788fe7d5a0" Name="ManageStageVisibility" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="10e528a4-33d9-445d-a477-f99ee230ef15" Name="df_KrEditSettingsVirtual_ManageStageVisibility" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ef86e270-047b-007c-5000-0da56e8eef2c" Name="pk_KrEditSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="ef86e270-047b-017c-4000-0da56e8eef2c" />
	</SchemePrimaryKey>
</SchemeTable>