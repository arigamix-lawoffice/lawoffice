<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="2317e111-2d0e-42d9-94dd-973411ecadca" Name="BusinessProcessCardTypes" Group="WorkflowEngine" InstanceType="Cards" ContentType="Collections">
	<Description>Список типов карточек, для которых доступен данный процесс</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2317e111-2d0e-00d9-2000-073411ecadca" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2317e111-2d0e-01d9-4000-073411ecadca" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="2317e111-2d0e-00d9-3100-073411ecadca" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="27bbdebd-1eda-49be-bb90-f2851fd1d684" Name="CardType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="27bbdebd-1eda-00be-4000-02851fd1d684" Name="CardTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="8d864c71-0689-4113-8225-5ea84c7416f5" Name="CardTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="2317e111-2d0e-00d9-5000-073411ecadca" Name="pk_BusinessProcessCardTypes">
		<SchemeIndexedColumn Column="2317e111-2d0e-00d9-3100-073411ecadca" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="2317e111-2d0e-00d9-7000-073411ecadca" Name="idx_BusinessProcessCardTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="2317e111-2d0e-01d9-4000-073411ecadca" />
	</SchemeIndex>
	<SchemeIndex ID="ef02dc01-143e-4c72-b743-594d5ffdada4" Name="ndx_BusinessProcessCardTypes_CardTypeID">
		<SchemeIndexedColumn Column="27bbdebd-1eda-00be-4000-02851fd1d684" />
	</SchemeIndex>
</SchemeTable>