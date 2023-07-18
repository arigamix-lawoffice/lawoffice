<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="61a4ec06-f583-4eaf-8d91-c73de9f61164" Name="KrAcquaintanceSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция настроек этапа Ознакомление</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="61a4ec06-f583-00af-2000-073de9f61164" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="61a4ec06-f583-01af-4000-073de9f61164" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="da92b351-5553-4e17-aa65-58103d374ebb" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="6839f856-edb2-4342-813c-a422cb0858a4" Name="df_KrAcquaintanceSettingsVirtual_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e9bb4994-fe84-452d-a1f7-e6a2e2f02e07" Name="Comment" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="6e18ac9e-3125-48fa-a9e3-cbadd2debc19" Name="AliasMetadata" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="97fe5857-9aea-46d7-8227-4cfbd22a608f" Name="Notification" Type="Reference(Typified) Not Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="97fe5857-9aea-00d7-4000-0cfbd22a608f" Name="NotificationID" Type="Guid Not Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a">
			<SchemeDefaultConstraint IsPermanent="true" ID="84430192-215e-4e0c-8745-b6729e461c6b" Name="df_KrAcquaintanceSettingsVirtual_NotificationID" Value="9e3d20a6-0dff-4667-a29d-30296635c89a" />
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="fdd710ec-6edb-4282-beba-4f71fda03736" Name="NotificationName" Type="String(256) Not Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d">
			<SchemeDefaultConstraint IsPermanent="true" ID="113d0ae8-d0c4-4afc-be8d-4e0ff7ca3581" Name="df_KrAcquaintanceSettingsVirtual_NotificationName" Value="$KrNotification_Acquaintance" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="a456a642-8156-45b6-b54e-4b3a9db5fe1c" Name="Sender" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a456a642-8156-00b6-4000-0b3a9db5fe1c" Name="SenderID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="aeba04e3-98b0-4648-b207-1888cdd30967" Name="SenderName" Type="String(128) Not Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="61a4ec06-f583-00af-5000-073de9f61164" Name="pk_KrAcquaintanceSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="61a4ec06-f583-01af-4000-073de9f61164" />
	</SchemePrimaryKey>
</SchemeTable>