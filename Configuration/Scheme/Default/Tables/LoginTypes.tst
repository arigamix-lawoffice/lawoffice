<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="44a94501-a954-4ab1-a7f8-47eebb2f869b" Name="LoginTypes" Group="System">
	<Description>Типы входа пользователей в систему</Description>
	<SchemePhysicalColumn ID="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="df05a434-d285-4608-b1c8-361ef4356773" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="068d7815-53a4-4bfa-9086-f3c5369f6adf" Name="pk_LoginTypes">
		<SchemeIndexedColumn Column="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e">0</ID>
		<Name ID="df05a434-d285-4608-b1c8-361ef4356773">$Enum_LoginTypes_None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e">1</ID>
		<Name ID="df05a434-d285-4608-b1c8-361ef4356773">$Enum_LoginTypes_Tessa</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e">2</ID>
		<Name ID="df05a434-d285-4608-b1c8-361ef4356773">$Enum_LoginTypes_Windows</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="19e48b5c-b2fc-4f2a-b36d-90db3f3ae10e">3</ID>
		<Name ID="df05a434-d285-4608-b1c8-361ef4356773">$Enum_LoginTypes_Ldap</Name>
	</SchemeRecord>
</SchemeTable>