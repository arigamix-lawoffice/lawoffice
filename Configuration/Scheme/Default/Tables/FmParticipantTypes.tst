<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="2b2a8e44-eecd-4afe-b017-20f8a00846ff" Name="FmParticipantTypes" Group="Fm">
	<Description>Типы участников форума</Description>
	<SchemePhysicalColumn ID="9a779c0c-c9b5-4a10-96f6-134a10783c05" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="4920fdb3-ad1c-47c9-9512-81877792c321" Name="Name" Type="String(512) Not Null" />
	<SchemePrimaryKey ID="14ca83e7-0fa2-4ea9-9b22-b3928ef29db4" Name="pk_FmParticipantTypes">
		<SchemeIndexedColumn Column="9a779c0c-c9b5-4a10-96f6-134a10783c05" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="9a779c0c-c9b5-4a10-96f6-134a10783c05">0</ID>
		<Name ID="4920fdb3-ad1c-47c9-9512-81877792c321">$FmParticipantTypes_Participant</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9a779c0c-c9b5-4a10-96f6-134a10783c05">1</ID>
		<Name ID="4920fdb3-ad1c-47c9-9512-81877792c321">$FmParticipantTypes_Moderator</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9a779c0c-c9b5-4a10-96f6-134a10783c05">2</ID>
		<Name ID="4920fdb3-ad1c-47c9-9512-81877792c321">SuperModerator</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9a779c0c-c9b5-4a10-96f6-134a10783c05">3</ID>
		<Name ID="4920fdb3-ad1c-47c9-9512-81877792c321">$FmParticipantTypes_ParticipantFromRole</Name>
	</SchemeRecord>
</SchemeTable>