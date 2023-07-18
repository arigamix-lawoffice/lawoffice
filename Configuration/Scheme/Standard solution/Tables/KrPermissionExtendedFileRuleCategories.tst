<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="2a337def-1279-456a-a61a-0232aa082123" Name="KrPermissionExtendedFileRuleCategories" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Набор категорий, проверяемых в расширенных правилах доступа к файлам</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2a337def-1279-006a-2000-0232aa082123" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2a337def-1279-016a-4000-0232aa082123" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2a337def-1279-006a-3100-0232aa082123" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="2884b819-bb6a-4df0-acc0-0bab805f2604" Name="Rule" Type="Reference(Typified) Not Null" ReferencedTable="7ca15c10-9fd1-46e9-8769-b0acc0efe118" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2884b819-bb6a-00f0-4000-0bab805f2604" Name="RuleRowID" Type="Guid Not Null" ReferencedColumn="7ca15c10-9fd1-00e9-3100-00acc0efe118" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="8f500bb7-d1eb-495d-94d3-53c456a601c1" Name="Category" Type="Reference(Typified) Not Null" ReferencedTable="e1599715-02d4-4ca9-b63e-b4b1ce642c7a" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8f500bb7-d1eb-005d-4000-03c456a601c1" Name="CategoryID" Type="Guid Not Null" ReferencedColumn="e1599715-02d4-01a9-4000-04b1ce642c7a" />
		<SchemeReferencingColumn ID="2f018245-3e0e-4189-a801-4e88961f2d90" Name="CategoryName" Type="String(255) Not Null" ReferencedColumn="e2598c40-038d-4af4-9907-f2514170cc4d" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2a337def-1279-006a-5000-0232aa082123" Name="pk_KrPermissionExtendedFileRuleCategories">
		<SchemeIndexedColumn Column="2a337def-1279-006a-3100-0232aa082123" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2a337def-1279-006a-7000-0232aa082123" Name="idx_KrPermissionExtendedFileRuleCategories_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2a337def-1279-016a-4000-0232aa082123" />
	</SchemeIndex>
	<SchemeIndex ID="d0dffdf6-6d17-42a4-b871-59a814e893c6" Name="ndx_KrPermissionExtendedFileRuleCategories_RuleRowID">
		<Description>Быстрое удаление правил для FK.</Description>
		<SchemeIndexedColumn Column="2884b819-bb6a-00f0-4000-0bab805f2604" />
	</SchemeIndex>
</SchemeTable>