<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="6963c76f-5e8d-49b5-80a3-f2ec342de0bf" Name="ConditionUsePlaces" Group="System">
	<SchemePhysicalColumn ID="67485d1f-6b37-4438-9264-458bdf2ff2e7" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор типа карточки</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6aaafc76-c841-45a4-a733-4a94e572f5a5" Name="Name" Type="String(Max) Not Null" />
	<SchemePrimaryKey ID="a5c58e39-d5d9-420c-b29c-5d420bcc8d42" Name="pk_ConditionUsePlaces">
		<SchemeIndexedColumn Column="67485d1f-6b37-4438-9264-458bdf2ff2e7" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="67485d1f-6b37-4438-9264-458bdf2ff2e7">929ad23c-8a22-09aa-9000-398bf13979b2</ID>
		<Name ID="6aaafc76-c841-45a4-a733-4a94e572f5a5">$ConditionUsePlace_NotificationRules</Name>
	</SchemeRecord>
</SchemeTable>