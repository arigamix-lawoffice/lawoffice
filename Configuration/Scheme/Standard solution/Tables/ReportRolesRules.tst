<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="359edaf2-fdb7-4e24-afc8-f31281328a81" Name="ReportRolesRules" Group="Common" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="359edaf2-fdb7-0024-2000-031281328a81" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="359edaf2-fdb7-0124-4000-031281328a81" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="72d636e0-f1f6-43cc-bfda-9b3e94153955" Name="Caption" Type="String(128) Not Null">
		<Description>Название правила для просмотра отчётов</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="359edaf2-fdb7-0024-5000-031281328a81" Name="pk_ReportRolesRules" IsClustered="true">
		<SchemeIndexedColumn Column="359edaf2-fdb7-0124-4000-031281328a81" />
	</SchemePrimaryKey>
</SchemeTable>