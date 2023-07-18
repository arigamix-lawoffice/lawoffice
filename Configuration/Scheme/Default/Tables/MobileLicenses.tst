<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="457f5393-50a4-40ea-8637-37fb57330ae2" Name="MobileLicenses" Group="System" InstanceType="Cards" ContentType="Collections">
	<Description>Сотрудники, для которых указаны лицензии мобильного согласования.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="457f5393-50a4-00ea-2000-07fb57330ae2" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="457f5393-50a4-01ea-4000-07fb57330ae2" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="457f5393-50a4-00ea-3100-07fb57330ae2" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="971f609d-845a-4b2e-9772-f73ebad1a98c" Name="User" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Ссылка на сотрудника, для которого указана лицензия мобильного согласования.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="971f609d-845a-002e-4000-073ebad1a98c" Name="UserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6f2a1774-7bb8-46b6-bbb5-b6ed472a5bfd" Name="UserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="457f5393-50a4-00ea-5000-07fb57330ae2" Name="pk_MobileLicenses">
		<SchemeIndexedColumn Column="457f5393-50a4-00ea-3100-07fb57330ae2" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="457f5393-50a4-00ea-7000-07fb57330ae2" Name="idx_MobileLicenses_ID" IsClustered="true">
		<SchemeIndexedColumn Column="457f5393-50a4-01ea-4000-07fb57330ae2" />
	</SchemeIndex>
	<SchemeIndex ID="18aa8336-5b68-4a0d-9771-5cdbf817c761" Name="ndx_MobileLicenses_UserID" IsUnique="true">
		<SchemeIndexedColumn Column="971f609d-845a-002e-4000-073ebad1a98c" />
	</SchemeIndex>
</SchemeTable>