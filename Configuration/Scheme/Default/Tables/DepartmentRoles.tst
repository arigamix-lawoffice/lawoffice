<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="d43dace1-536f-4c9f-af15-49a8892a7427" Name="DepartmentRoles" Group="Roles" InstanceType="Cards" ContentType="Entries">
	<Description>Роли департаментов.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="d43dace1-536f-009f-2000-09a8892a7427" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d43dace1-536f-019f-4000-09a8892a7427" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="6ec51960-a91e-4bc6-9da6-24203e20fa10" Name="HeadUser" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Персональная роль пользователя, являющегося главой департамента.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6ec51960-a91e-00c6-4000-04203e20fa10" Name="HeadUserID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c66e905d-1687-4fd2-911a-d1ca9f600fec" Name="HeadUserName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="d43dace1-536f-009f-5000-09a8892a7427" Name="pk_DepartmentRoles" IsClustered="true">
		<SchemeIndexedColumn Column="d43dace1-536f-019f-4000-09a8892a7427" />
	</SchemePrimaryKey>
	<SchemeIndex ID="14a4b460-614d-43e7-a5ee-c022fa16fadd" Name="ndx_DepartmentRoles_HeadUserID">
		<SchemeIndexedColumn Column="6ec51960-a91e-00c6-4000-04203e20fa10" />
	</SchemeIndex>
</SchemeTable>