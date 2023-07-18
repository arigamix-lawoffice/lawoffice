<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" ID="57b9e507-d135-4c69-9a94-bf507d499484" Name="Configuration" Group="System">
	<Description>Configuration properties</Description>
	<SchemePhysicalColumn ID="47b91d0b-9866-4832-a04e-973b28b5c9f5" Name="BuildVersion" Type="String(24) Not Null">
		<Description>Platform build version</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="575beac6-5579-49ec-9157-ea6af610b969" Name="BuildName" Type="String(64) Not Null">
		<Description>Platform build version as text</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="acaecb91-b71d-4923-9a68-aa6eb5fdeecf" Name="BuildDate" Type="Date Not Null">
		<Description>Platform build date</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="12519720-b4c0-4d23-bb9f-b6b244beab13" Name="Description" Type="String(512) Null">
		<Description>Configuration description</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="acefad4b-7c0a-40c3-8364-3fd6caa1026d" Name="Modified" Type="DateTime Not Null">
		<Description>Date/time of the last changes</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c584caae-6889-4ade-b348-792181e98b1f" Name="ModifiedByID" Type="Guid Not Null">
		<Description>The ID of the user who made the last change</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="cd03b614-718c-4b7c-a7ad-f649b7ff33a6" Name="df_Configuration_ModifiedByID" Value="11111111-1111-1111-1111-111111111111" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="66d8f577-204e-43d4-b986-fe3b93806984" Name="ModifiedByName" Type="String(128) Not Null">
		<Description>The name of the user who made the last change</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="adedd53e-9e47-4d71-aed4-014242087ad0" Name="df_Configuration_ModifiedByName" Value="System" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c9de3895-668c-4921-88c2-123c4424cdda" Name="Version" Type="Int32 Not Null">
		<Description>Configuration version</Description>
	</SchemePhysicalColumn>
</SchemeTable>