<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="cefba5f8-8b2c-4be0-ba24-564f3a474240" Name="ViewsVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Представления, отображаемые как виртуальные карточки в клиенте.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="cefba5f8-8b2c-00e0-2000-064f3a474240" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="cefba5f8-8b2c-01e0-4000-064f3a474240" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="41103cff-efc2-4464-a183-cb1ef5fdb1e9" Name="Alias" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="8d05a3a6-8214-435c-b7cb-f586ef53c5e5" Name="Caption" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="95fc6282-c5fd-4018-aa73-95d1af0932d5" Name="GroupName" Type="String(128) Null" />
	<SchemePhysicalColumn ID="d2d669d0-86f0-4526-a000-2fdf3b6b778e" Name="Description" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="236c6e8e-ad53-4858-809b-950e47f2d286" Name="Modified" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="594bb4df-765d-463a-99c9-9ee0345c1e12" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="594bb4df-765d-003a-4000-0ee0345c1e12" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="0c53684c-5448-4438-820f-66e1cb255552" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="cefba5f8-8b2c-00e0-5000-064f3a474240" Name="pk_ViewsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="cefba5f8-8b2c-01e0-4000-064f3a474240" />
	</SchemePrimaryKey>
</SchemeTable>