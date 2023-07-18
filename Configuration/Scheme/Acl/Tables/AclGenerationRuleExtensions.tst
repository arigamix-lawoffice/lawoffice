<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="c2c3b955-ca13-4e63-83aa-cb033ebdce57" Name="AclGenerationRuleExtensions" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c2c3b955-ca13-0063-2000-0b033ebdce57" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c2c3b955-ca13-0163-4000-0b033ebdce57" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c2c3b955-ca13-0063-3100-0b033ebdce57" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="0398e98a-1ff2-44ea-89a6-b387b2d4a60e" Name="Extension" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<Description>Секция с расширениями правила расчёта ACL</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0398e98a-1ff2-00ea-4000-0387b2d4a60e" Name="ExtensionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="387239b6-7fb9-4572-95ef-83b02a6c73b6" Name="ExtensionName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c2c3b955-ca13-0063-5000-0b033ebdce57" Name="pk_AclGenerationRuleExtensions">
		<SchemeIndexedColumn Column="c2c3b955-ca13-0063-3100-0b033ebdce57" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c2c3b955-ca13-0063-7000-0b033ebdce57" Name="idx_AclGenerationRuleExtensions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c2c3b955-ca13-0163-4000-0b033ebdce57" />
	</SchemeIndex>
</SchemeTable>