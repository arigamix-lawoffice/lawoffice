<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="b7538557-04c6-43d4-9d7a-4412dc1ed103" Name="Acl" Group="Acl">
	<Description>Основная таблица со списком ролей доступа к карточке.</Description>
	<SchemePhysicalColumn ID="8bd11564-87d1-44de-a2a5-096e38c56177" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор карточки, к которой относяится запись.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c65bd543-8600-4920-ba63-2d3bc7ec6e10" Name="RowID" Type="Guid Not Null">
		<Description>Уникальный идентификатор строки.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="5ba986bc-c7a4-4786-8302-d40176a32010" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="5518f35a-ea30-4968-983d-aec524aeb710" WithForeignKey="false">
		<Description>Правило, которое добавило эту запись.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5ba986bc-c7a4-0086-4000-040176a32010" Name="RuleID" Type="Guid Not Null" ReferencedColumn="5518f35a-ea30-0168-4000-0ec524aeb710" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="97daae1f-9e1f-4748-8d5d-b656693bc31c" Name="Role" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<Description>Роль, для которой создана данная запись.</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="97daae1f-9e1f-0048-4000-0656693bc31c" Name="RoleID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeComplexColumn>
	<SchemePrimaryKey ID="501e53b1-eff8-4f2d-a08e-07f1d7d0b0d2" Name="pk_Acl">
		<SchemeIndexedColumn Column="c65bd543-8600-4920-ba63-2d3bc7ec6e10" />
	</SchemePrimaryKey>
	<SchemeIndex ID="db2dbae1-022b-4dfd-a4fb-33835b004125" Name="idx_Acl_ID" IsClustered="true">
		<SchemeIndexedColumn Column="8bd11564-87d1-44de-a2a5-096e38c56177" />
	</SchemeIndex>
	<SchemeIndex ID="7fbc0e7d-e700-4e3a-adbf-bd3bb2a9ea4e" Name="ndx_Acl_RuleID">
		<SchemeIndexedColumn Column="5ba986bc-c7a4-0086-4000-040176a32010" />
	</SchemeIndex>
	<SchemeIndex ID="a3dd3d95-5602-4b1e-a2ea-d8d9aeccd8f0" Name="ndx_Acl_RoleIDRuleIDID">
		<SchemeIndexedColumn Column="97daae1f-9e1f-0048-4000-0656693bc31c" />
		<SchemeIndexedColumn Column="5ba986bc-c7a4-0086-4000-040176a32010" />
		<SchemeIndexedColumn Column="8bd11564-87d1-44de-a2a5-096e38c56177" />
	</SchemeIndex>
</SchemeTable>