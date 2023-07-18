<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="c98ce2bb-a770-4e13-a1b6-314ba68f9bfc" Name="KrActiveTasks" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Активные задания процесса согласования</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c98ce2bb-a770-0013-2000-014ba68f9bfc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c98ce2bb-a770-0113-4000-014ba68f9bfc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="c98ce2bb-a770-0013-3100-014ba68f9bfc" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="5e760bf0-1aab-481a-b725-462ed609eb12" Name="TaskID" Type="Guid Not Null">
		<Description>Ссылка на активное задание процесса согласования</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="c98ce2bb-a770-0013-5000-014ba68f9bfc" Name="pk_KrActiveTasks">
		<SchemeIndexedColumn Column="c98ce2bb-a770-0013-3100-014ba68f9bfc" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="c98ce2bb-a770-0013-7000-014ba68f9bfc" Name="idx_KrActiveTasks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="c98ce2bb-a770-0113-4000-014ba68f9bfc" />
	</SchemeIndex>
</SchemeTable>