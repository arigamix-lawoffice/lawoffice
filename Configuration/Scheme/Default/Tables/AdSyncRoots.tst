<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="68543b32-9960-4b90-9c67-72a297f4feff" Name="AdSyncRoots" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="68543b32-9960-0090-2000-02a297f4feff" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="68543b32-9960-0190-4000-02a297f4feff" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="68543b32-9960-0090-3100-02a297f4feff" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="c02c101c-871a-4e65-a1b9-e5f0d2d90ef0" Name="RootName" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="86673210-8ce9-4161-8a07-42573f52551f" Name="SyncStaticRoles" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="35d30c77-3aa5-4de0-a864-f8c75f3ffcdc" Name="df_AdSyncRoots_SyncStaticRoles" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4835b141-cac6-49e5-bc03-fe934c13abb8" Name="SyncDepartments" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1e6fd154-c5fc-41eb-875b-f09e9d40ff36" Name="df_AdSyncRoots_SyncDepartments" Value="true" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="68543b32-9960-0090-5000-02a297f4feff" Name="pk_AdSyncRoots">
		<SchemeIndexedColumn Column="68543b32-9960-0090-3100-02a297f4feff" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="68543b32-9960-0090-7000-02a297f4feff" Name="idx_AdSyncRoots_ID" IsClustered="true">
		<SchemeIndexedColumn Column="68543b32-9960-0190-4000-02a297f4feff" />
	</SchemeIndex>
</SchemeTable>