<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="c44db46a-349f-45ec-b0ab-ec212c09b276" Name="SmartRoleGeneratorInfo" Group="Acl">
	<Description>Таблица с информацией о расчёте генераторов умных ролей.</Description>
	<SchemeComplexColumn ID="a7cbe940-d6bb-4ef3-bb6e-e65779da120e" Name="Generator" Type="Reference(Typified) Not Null" ReferencedTable="5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a7cbe940-d6bb-00f3-4000-065779da120e" Name="GeneratorID" Type="Guid Not Null" ReferencedColumn="5f3a0dbc-2fc4-0169-4000-0b95f39970ba" />
		<SchemeReferencingColumn ID="8683f9f3-3859-48bb-9e74-d8309eed4125" Name="GeneratorVersion" Type="Int32 Not Null" ReferencedColumn="7fb7cc34-0d1a-4dc9-bb5d-9269bfb4ddb1" />
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="635373c4-72b6-4b24-b6bc-840d48c42980" Name="pk_SmartRoleGeneratorInfo">
		<SchemeIndexedColumn Column="a7cbe940-d6bb-00f3-4000-065779da120e" />
	</SchemePrimaryKey>
</SchemeTable>