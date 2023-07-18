<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="53a3ee37-b714-4503-9e0e-e2ed1ccd164f" Name="MetaRoleTypes" Group="Roles">
	<Description>Типы метаролей.</Description>
	<SchemePhysicalColumn ID="5302e6ab-b1ec-4d3f-881a-068d2553d25e" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор типа метароли.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4e7fe851-49a2-4fbd-b1dc-3359489275a0" Name="Name" Type="String(128) Not Null">
		<Description>Имя типа метароли.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="4b51ff82-7e46-488f-806d-8afc6ff306f6" Name="pk_MetaRoleTypes">
		<SchemeIndexedColumn Column="5302e6ab-b1ec-4d3f-881a-068d2553d25e" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="5302e6ab-b1ec-4d3f-881a-068d2553d25e">0</ID>
		<Name ID="4e7fe851-49a2-4fbd-b1dc-3359489275a0">Guid</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5302e6ab-b1ec-4d3f-881a-068d2553d25e">1</ID>
		<Name ID="4e7fe851-49a2-4fbd-b1dc-3359489275a0">Integer</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="5302e6ab-b1ec-4d3f-881a-068d2553d25e">2</ID>
		<Name ID="4e7fe851-49a2-4fbd-b1dc-3359489275a0">String</Name>
	</SchemeRecord>
</SchemeTable>