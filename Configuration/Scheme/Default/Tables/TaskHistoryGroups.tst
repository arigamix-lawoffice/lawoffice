<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="31644536-fba1-456c-881c-7dae73b7182c" Name="TaskHistoryGroups" Group="System" InstanceType="Cards" ContentType="Hierarchies">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="31644536-fba1-006c-2000-0dae73b7182c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31644536-fba1-016c-4000-0dae73b7182c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="31644536-fba1-006c-3100-0dae73b7182c" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" ID="31644536-fba1-006c-2200-0dae73b7182c" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="31644536-fba1-456c-881c-7dae73b7182c" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31644536-fba1-016c-4020-0dae73b7182c" Name="ParentRowID" Type="Guid Null" ReferencedColumn="31644536-fba1-006c-3100-0dae73b7182c" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="2cd43245-765d-4dcf-b313-a08c41d4b05f" Name="Type" Type="Reference(Typified) Null" ReferencedTable="319be329-6cd3-457a-b792-41c26a266b95" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2cd43245-765d-00cf-4000-008c41d4b05f" Name="TypeID" Type="Guid Null" ReferencedColumn="319be329-6cd3-017a-4000-01c26a266b95" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="e44f8ea2-a2cf-43c8-b08a-e67bbffc83b3" Name="Caption" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="c8934249-601e-4dda-a15e-1f808b6bc9aa" Name="Iteration" Type="Int32 Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="90079d73-1a53-49b8-9d00-916746134bdd" Name="df_TaskHistoryGroups_Iteration" Value="1" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="31644536-fba1-006c-5000-0dae73b7182c" Name="pk_TaskHistoryGroups">
		<SchemeIndexedColumn Column="31644536-fba1-006c-3100-0dae73b7182c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="31644536-fba1-006c-7000-0dae73b7182c" Name="idx_TaskHistoryGroups_ID" IsClustered="true">
		<SchemeIndexedColumn Column="31644536-fba1-016c-4000-0dae73b7182c" />
	</SchemeIndex>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3f9bd4a6-b315-47da-a807-aeb1b27c97f6" Name="ndx_TaskHistoryGroups_ParentRowID">
		<Description>Быстрое удаление строк без FK.</Description>
		<Predicate Dbms="SqlServer">[ParentRowID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"ParentRowID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="31644536-fba1-016c-4020-0dae73b7182c" />
	</SchemeIndex>
</SchemeTable>