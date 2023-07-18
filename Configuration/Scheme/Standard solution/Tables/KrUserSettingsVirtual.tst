<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="84d45612-8cbb-4b7d-91eb-2796003a365d" Name="KrUserSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="84d45612-8cbb-007d-2000-0796003a365d" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="84d45612-8cbb-017d-4000-0796003a365d" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="66ec0151-bcb3-4d14-b095-4f531a66ee24" Name="DisableTaskPopupNotifications" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="58321e0e-0bd6-409b-bac7-325988ecffdc" Name="df_KrUserSettingsVirtual_DisableTaskPopupNotifications" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="84d45612-8cbb-007d-5000-0796003a365d" Name="pk_KrUserSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="84d45612-8cbb-017d-4000-0796003a365d" />
	</SchemePrimaryKey>
</SchemeTable>