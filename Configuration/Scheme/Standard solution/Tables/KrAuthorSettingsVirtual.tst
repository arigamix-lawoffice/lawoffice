<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="17931d48-fae6-415e-bb76-3ea3a457a2e9" Name="KrAuthorSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="17931d48-fae6-005e-2000-0ea3a457a2e9" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="17931d48-fae6-015e-4000-0ea3a457a2e9" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1ab8f9ce-59c6-4ff7-a74c-aadcd83e6f12" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1ab8f9ce-59c6-00f7-4000-0adcd83e6f12" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="50aec72e-ed2f-41b7-9f0b-c424e2cd6b35" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="17931d48-fae6-005e-5000-0ea3a457a2e9" Name="pk_KrAuthorSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="17931d48-fae6-015e-4000-0ea3a457a2e9" />
	</SchemePrimaryKey>
</SchemeTable>