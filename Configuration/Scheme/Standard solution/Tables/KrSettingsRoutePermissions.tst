<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="39e6d38f-4e35-45e9-8c71-42a932dce18c" Name="KrSettingsRoutePermissions" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Разрешения в маршрутах - родительская таблица, каждой строкой которой является пересечение остальных таблиц.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="39e6d38f-4e35-00e9-2000-02a932dce18c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="39e6d38f-4e35-01e9-4000-02a932dce18c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="39e6d38f-4e35-00e9-3100-02a932dce18c" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="18ad5d65-6158-4932-9b95-5fbe629fb882" Name="Name" Type="String(50) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="39e6d38f-4e35-00e9-5000-02a932dce18c" Name="pk_KrSettingsRoutePermissions">
		<SchemeIndexedColumn Column="39e6d38f-4e35-00e9-3100-02a932dce18c" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="39e6d38f-4e35-00e9-7000-02a932dce18c" Name="idx_KrSettingsRoutePermissions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="39e6d38f-4e35-01e9-4000-02a932dce18c" />
	</SchemeIndex>
</SchemeTable>