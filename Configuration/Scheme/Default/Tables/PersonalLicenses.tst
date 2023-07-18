<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="8a96f177-b2a7-4ffc-885b-2f36730d0d2e" Name="PersonalLicenses" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Сотрудники, для которых указаны персональные лицензии.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a96f177-b2a7-00fc-2000-0f36730d0d2e" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8a96f177-b2a7-01fc-4000-0f36730d0d2e" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a96f177-b2a7-00fc-3100-0f36730d0d2e" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f61e16a0-d07c-4f80-86fa-7afec89a5a67" Name="User" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Ссылка на сотрудника, для которого указана персональная лицензия.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f61e16a0-d07c-0080-4000-0afec89a5a67" Name="UserID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="38624648-fb2a-4909-af8a-87eed70d8a86" Name="UserName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a96f177-b2a7-00fc-5000-0f36730d0d2e" Name="pk_PersonalLicenses">
		<SchemeIndexedColumn Column="8a96f177-b2a7-00fc-3100-0f36730d0d2e" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="8a96f177-b2a7-00fc-7000-0f36730d0d2e" Name="idx_PersonalLicenses_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8a96f177-b2a7-01fc-4000-0f36730d0d2e" />
	</SchemeIndex>
	<SchemeIndex ID="296f6599-6c5e-43eb-b0e2-e85f403ca37a" Name="ndx_PersonalLicenses_UserID" IsUnique="true">
		<SchemeIndexedColumn Column="f61e16a0-d07c-0080-4000-0afec89a5a67" />
	</SchemeIndex>
</SchemeTable>