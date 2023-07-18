<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e9fd155c-b189-4a5d-b0b4-970c94a2fa0a" Name="FmTopicParticipantRolesUnsubscribed" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица в которой хранятся данные по одписакам пользователями в ролях</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e9fd155c-b189-005d-2000-070c94a2fa0a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e9fd155c-b189-015d-4000-070c94a2fa0a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="e9fd155c-b189-005d-3100-070c94a2fa0a" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f1f185c5-d562-4301-bc80-00c8b9af6309" Name="Topic" Type="Reference(Typified) Not Null" ReferencedTable="35b11a3c-f9ec-4fac-a3f1-def11bba44ae">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f1f185c5-d562-0001-4000-00c8b9af6309" Name="TopicRowID" Type="Guid Not Null" ReferencedColumn="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c3e53c4f-cafe-489d-98e9-31bd2107f342" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c3e53c4f-cafe-009d-4000-01bd2107f342" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="050993aa-4e5a-44c7-b122-8a6e13e07daf" Name="Subscribe" Type="Boolean Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="e9fd155c-b189-005d-5000-070c94a2fa0a" Name="pk_FmTopicParticipantRolesUnsubscribed">
		<SchemeIndexedColumn Column="e9fd155c-b189-005d-3100-070c94a2fa0a" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="e9fd155c-b189-005d-7000-070c94a2fa0a" Name="idx_FmTopicParticipantRolesUnsubscribed_ID" IsClustered="true">
		<SchemeIndexedColumn Column="e9fd155c-b189-015d-4000-070c94a2fa0a" />
	</SchemeIndex>
	<SchemeIndex ID="f094cf52-ef97-46b1-b281-02211d7759f1" Name="ndx_FmTopicParticipantRolesUnsubscribed_TopicRowIDUserID">
		<Description>Быстрое удаление топиков для FK. Быстрый поиск для отписок.</Description>
		<SchemeIndexedColumn Column="f1f185c5-d562-0001-4000-00c8b9af6309" />
		<SchemeIndexedColumn Column="c3e53c4f-cafe-009d-4000-01bd2107f342" />
	</SchemeIndex>
</SchemeTable>