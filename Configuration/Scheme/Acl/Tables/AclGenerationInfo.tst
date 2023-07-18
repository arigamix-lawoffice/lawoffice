<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="5cca17a2-50e3-4b20-98c2-2f6ed9ce31fa" Name="AclGenerationInfo" Group="Acl">
	<Description>Таблица с информацией о генерации ACL.</Description>
	<SchemeComplexColumn ID="a831e6dd-5726-48b6-ac8d-0bb65c1eeb73" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="5518f35a-ea30-4968-983d-aec524aeb710" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a831e6dd-5726-00b6-4000-0bb65c1eeb73" Name="RuleID" Type="Guid Not Null" ReferencedColumn="5518f35a-ea30-0168-4000-0ec524aeb710" />
		<SchemeReferencingColumn ID="ab74b2ec-aa35-41d8-baf5-dfc859895a19" Name="RuleVersion" Type="Int32 Not Null" ReferencedColumn="0cf9c11d-cfea-4684-bda4-4c8f7e8384be" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="82e99394-ad5d-4911-a198-c65b951011d1" Name="NextRequest" Type="BinaryJson Null">
		<Description>Следующий запрос, используемый для перерасчёта ACL.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a34dc386-b7a6-4b58-ada6-9c0f2e0e6900" Name="NextRequestVersion" Type="Int32 Null">
		<Description>Номер версии правила расчёта ACL, для которой был добавлен запрос на продолжение расчёта.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="47283bee-b62c-4458-87f7-41fbc1325b1e" Name="pk_AclGenerationInfo">
		<SchemeIndexedColumn Column="a831e6dd-5726-00b6-4000-0bb65c1eeb73" />
	</SchemePrimaryKey>
</SchemeTable>