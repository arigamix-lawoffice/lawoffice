<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="6b7f7b41-7ba8-4549-b965-f3a2aa9a168b" Name="AdSyncSettings" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6b7f7b41-7ba8-0049-2000-03a2aa9a168b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6b7f7b41-7ba8-0149-4000-03a2aa9a168b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="6e475a70-01ee-4368-bdcc-a0ad197bc951" Name="SyncUsers" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="91e7bb28-cb4f-4fb6-a0d0-95595ed885c1" Name="df_AdSyncSettings_SyncUsers" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="041c7e20-15ec-4ad4-9c41-394ed74737d1" Name="SyncDepartments" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1e5c647f-b6d1-44b1-9088-0376c21a846e" Name="df_AdSyncSettings_SyncDepartments" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c79406d5-07de-4760-b069-2e6e40713a09" Name="SyncUsersGroup" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="5ee15b92-b087-47e9-a314-e88a2f91d5d9" Name="SyncStaticRoles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5ffb781a-78b9-4120-96a3-c76e6bc1cb5d" Name="df_AdSyncSettings_SyncStaticRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="7fe9ace1-0f85-4f5b-a349-c44ae7376500" Name="DisableStaticRoleRename" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="5d5368dc-3fc9-4846-84ed-f5a44e622e84" Name="df_AdSyncSettings_DisableStaticRoleRename" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6b7f7b41-7ba8-0049-5000-03a2aa9a168b" Name="pk_AdSyncSettings" IsClustered="true">
		<SchemeIndexedColumn Column="6b7f7b41-7ba8-0149-4000-03a2aa9a168b" />
	</SchemePrimaryKey>
</SchemeTable>