<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="43f92881-c875-437a-bf1c-b7793c099d00" Name="FmMessageTypes" Group="Fm">
	<Description>Типы сообщений</Description>
	<SchemePhysicalColumn ID="70da9423-8064-4045-842f-69a3fe1f59e5" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a" Name="Name" Type="String(512) Not Null" />
	<SchemePrimaryKey ID="a4beb052-fb82-45a8-a745-37b999112e4c" Name="pk_FmMessageTypes">
		<SchemeIndexedColumn Column="70da9423-8064-4045-842f-69a3fe1f59e5" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">0</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">Default</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">1</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">AddUser</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">2</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">RemoveUser</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">3</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">AddRoles</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">4</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">RemoveRoles</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="70da9423-8064-4045-842f-69a3fe1f59e5">5</ID>
		<Name ID="5cf6b72d-5c2b-4be7-88bb-6a1792caec8a">Custom</Name>
	</SchemeRecord>
</SchemeTable>