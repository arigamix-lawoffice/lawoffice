<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="5576e1f1-316a-4256-a136-c33eb871b7d5" Name="ProtocolReports" Group="Common" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5576e1f1-316a-0056-2000-033eb871b7d5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5576e1f1-316a-0156-4000-033eb871b7d5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5576e1f1-316a-0056-3100-033eb871b7d5" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="3ba3c7d2-70d1-4488-9e23-008b72421943" Name="Subject" Type="String(1024) Not Null">
		<Description>Тема доклада.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="2c5c1fa3-d659-4221-8d28-491ee900c4d2" Name="Order" Type="Int32 Not Null">
		<Description>Порядок докладов в списке.</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="8ae5993f-6fe9-429a-bb66-2c71e1725760" Name="Person" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<Description>Докладчик</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="8ae5993f-6fe9-009a-4000-0c71e1725760" Name="PersonID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c072538b-eac1-49aa-bd36-1b0d7e6c10e8" Name="PersonName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5576e1f1-316a-0056-5000-033eb871b7d5" Name="pk_ProtocolReports">
		<SchemeIndexedColumn Column="5576e1f1-316a-0056-3100-033eb871b7d5" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="5576e1f1-316a-0056-7000-033eb871b7d5" Name="idx_ProtocolReports_ID" IsClustered="true">
		<SchemeIndexedColumn Column="5576e1f1-316a-0156-4000-033eb871b7d5" />
	</SchemeIndex>
</SchemeTable>