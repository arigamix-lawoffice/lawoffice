<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="df81680b-406f-4f50-9df2-c14dda232aea" Name="WorkflowActions" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция со списком действий</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df81680b-406f-0050-2000-014dda232aea" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="df81680b-406f-0150-4000-014dda232aea" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df81680b-406f-0050-3100-014dda232aea" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="f9331843-803f-40c2-9e47-c3875257d9c8" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="b8fb0d25-e123-41e4-a85f-79e62e1107bb" Name="Name" Type="String(128) Not Null" />
	<SchemePhysicalColumn ID="f829209d-78c0-4109-8c10-d04a1a7a2224" Name="Caption" Type="String(128) Null" />
	<SchemePhysicalColumn ID="7517cb0c-ea41-459f-a285-c457273057a8" Name="ActionType" Type="String(128) Not Null">
		<Description>Наименование типа действия</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="a18b5657-50e3-459f-883d-0cab4de0e526" Name="HasPreCondition" Type="Boolean Not Null">
		<Description>Флаг определяет, задано ли для данного действия предусловие</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d0f0e509-9ec3-4898-bb2d-02e40398fb78" Name="df_WorkflowActions_HasPreCondition" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3ee10770-9bae-4065-993b-aafebac6851f" Name="HasPreScript" Type="Boolean Not Null">
		<Description>Флаг определяет, задан ли для данного действия прескрипт</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="feca7eab-62ed-461d-9d1b-585fb4411257" Name="df_WorkflowActions_HasPreScript" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c3f7dcab-0d3a-407a-82b9-ebb36c50685c" Name="HasPostScript" Type="Boolean Not Null">
		<Description>Флаг определяет, задан ли для данного действия постскрипт</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="fb78bbd9-2f90-4302-a6cc-38090b723c15" Name="df_WorkflowActions_HasPostScript" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="df81680b-406f-0050-5000-014dda232aea" Name="pk_WorkflowActions">
		<SchemeIndexedColumn Column="df81680b-406f-0050-3100-014dda232aea" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="df81680b-406f-0050-7000-014dda232aea" Name="idx_WorkflowActions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="df81680b-406f-0150-4000-014dda232aea" />
	</SchemeIndex>
</SchemeTable>