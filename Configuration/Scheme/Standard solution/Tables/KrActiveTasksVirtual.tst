<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="21dbb01c-1510-4318-b47d-c2be3197cdfb" Name="KrActiveTasksVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Активные задания процесса согласования</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="21dbb01c-1510-0018-2000-02be3197cdfb" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="21dbb01c-1510-0118-4000-02be3197cdfb" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="21dbb01c-1510-0018-3100-02be3197cdfb" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="85d08c8a-3d63-4929-89f7-7b1789ef22b6" Name="TaskID" Type="Guid Not Null">
		<Description>Ссылка на активное задание процесса согласования</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="21dbb01c-1510-0018-5000-02be3197cdfb" Name="pk_KrActiveTasksVirtual">
		<SchemeIndexedColumn Column="21dbb01c-1510-0018-3100-02be3197cdfb" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="21dbb01c-1510-0018-7000-02be3197cdfb" Name="idx_KrActiveTasksVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="21dbb01c-1510-0118-4000-02be3197cdfb" />
	</SchemeIndex>
</SchemeTable>