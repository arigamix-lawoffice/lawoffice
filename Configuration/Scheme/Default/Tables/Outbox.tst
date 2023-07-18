<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b4412a23-3e36-4468-a9cf-6b7b553f9e64" Name="Outbox" Group="System">
	<SchemePhysicalColumn ID="f3d47ea1-9e53-4f31-bc06-31bc78c0e593" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="7c29d303-de5f-4a0e-9882-32e21a9560e7" Name="Created" Type="DateTime Not Null" />
	<SchemePhysicalColumn ID="e11dc479-e453-479d-9f75-6a936f077f09" Name="Email" Type="String(Max) Null">
		<Description>Один или несколько адресов, на которые отправляется Email</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="527996b7-892d-46b7-b91b-0b3f6435ef8d" Name="Subject" Type="String(255) Null" />
	<SchemePhysicalColumn ID="f81a90ee-e7f9-48cb-adca-9862a5ba94df" Name="Body" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="7e68e0d5-c258-4725-bcb3-71aa6067bca6" Name="Attempts" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="3725f329-4c16-4c72-b5ab-dc33d55abbb7" Name="df_Outbox_Attempts" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="898e7ac5-b0d5-4651-8172-a8378916a6e8" Name="LastErrorDate" Type="DateTime Null" />
	<SchemePhysicalColumn ID="ade24d56-b2ff-498f-b2a6-f94d94a8bf5e" Name="LastErrorText" Type="String(256) Null" />
	<SchemePhysicalColumn ID="4b5f7c04-b493-494b-bd66-c633f32a7017" Name="Info" Type="BinaryJson Null">
		<Description>Информация по отправленным сообщениям в текстовой форме типизированного json.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="71beac28-c38d-4794-a1fd-26bb60eea610" Name="pk_Outbox">
		<SchemeIndexedColumn Column="f3d47ea1-9e53-4f31-bc06-31bc78c0e593" />
	</SchemePrimaryKey>
	<SchemeIndex ID="ae8ab85d-6942-4d30-9e84-8f08d0202d92" Name="idx_Outbox_Created" IsClustered="true">
		<SchemeIndexedColumn Column="7c29d303-de5f-4a0e-9882-32e21a9560e7" />
	</SchemeIndex>
</SchemeTable>