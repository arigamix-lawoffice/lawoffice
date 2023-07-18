<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="c993000f-40d8-4639-a25d-e9a25d47e19c" Name="AdSyncSettingsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c993000f-40d8-0039-2000-09a25d47e19c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c993000f-40d8-0139-4000-09a25d47e19c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1413f99e-d07a-4139-ad89-3e2832e7f7cd" Name="SyncUsers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="6534062e-4bb4-46ba-966d-c2551dcec705" Name="df_AdSyncSettingsVirtual_SyncUsers" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="90e03b40-e9c8-4c6a-8b57-0b1d755bc7aa" Name="SyncDepartments" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="26ce10eb-e65d-40de-9c6e-4d814338fce7" Name="df_AdSyncSettingsVirtual_SyncDepartments" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e728e9da-b31e-4a49-9c8b-e6f030cb9bae" Name="SyncStaticRoles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="00679b28-0c7a-4f76-9306-c4cffa914e74" Name="df_AdSyncSettingsVirtual_SyncStaticRoles" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c993000f-40d8-0039-5000-09a25d47e19c" Name="pk_AdSyncSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="c993000f-40d8-0139-4000-09a25d47e19c" />
	</SchemePrimaryKey>
</SchemeTable>