<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2d90b630-c611-4137-8094-18986416c7b9" Name="KrAcquaintanceAction" Group="KrWe" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для действия "Ознакомление"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2d90b630-c611-0037-2000-08986416c7b9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2d90b630-c611-0137-4000-08986416c7b9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="612cb3bb-aed7-4da9-bf9c-5e4f73e96623" Name="ExcludeDeputies" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="7c5842ac-b6df-407a-be02-54199ae77f52" Name="df_KrAcquaintanceAction_ExcludeDeputies" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="12b4bc93-1e93-4bae-8f91-a790ff6a1649" Name="Comment" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="b6e7c2ea-5af5-4109-abc3-cc8e24aebd32" Name="AliasMetadata" Type="String(Max) Null" />
	<SchemeComplexColumn ID="f6839aec-5f93-4018-b50f-8f67d16e618b" Name="Notification" Type="Reference(Typified) Null" ReferencedTable="18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f6839aec-5f93-0018-4000-0f67d16e618b" Name="NotificationID" Type="Guid Null" ReferencedColumn="18145bb5-fd4e-0195-4000-0e1cd9b4ee5a" />
		<SchemeReferencingColumn ID="250c1905-47ea-4052-959d-9779f9e34a60" Name="NotificationName" Type="String(256) Null" ReferencedColumn="265d4336-6764-4db8-8874-0e5fa92cbd5d" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="793bfa99-5e17-436c-8d7d-4a0aeeab6aea" Name="Sender" Type="Reference(Typified) Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="793bfa99-5e17-006c-4000-0a0aeeab6aea" Name="SenderID" Type="Guid Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
		<SchemeReferencingColumn ID="09b81c5f-283c-40a3-889f-2a85f9bb1603" Name="SenderName" Type="String(128) Null" ReferencedColumn="616d6b2e-35d5-424d-846b-618eb25962d0" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2d90b630-c611-0037-5000-08986416c7b9" Name="pk_KrAcquaintanceAction" IsClustered="true">
		<SchemeIndexedColumn Column="2d90b630-c611-0137-4000-08986416c7b9" />
	</SchemePrimaryKey>
</SchemeTable>