<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0209ddc9-6457-406e-8a09-ab8ac6916e26" Name="KrTaskTypeCondition" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Основная секция с тиами заданий для настройки условия "По типу заданий"</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0209ddc9-6457-006e-2000-0b8ac6916e26" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0209ddc9-6457-016e-4000-0b8ac6916e26" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0209ddc9-6457-006e-3100-0b8ac6916e26" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="bd4916ff-5aef-4566-88c4-0f82e1b2b153" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bd4916ff-5aef-0066-4000-0f82e1b2b153" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="1f059066-64bc-4369-b8b2-045c05ac63bb" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0209ddc9-6457-006e-5000-0b8ac6916e26" Name="pk_KrTaskTypeCondition">
		<SchemeIndexedColumn Column="0209ddc9-6457-006e-3100-0b8ac6916e26" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0209ddc9-6457-006e-7000-0b8ac6916e26" Name="idx_KrTaskTypeCondition_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0209ddc9-6457-016e-4000-0b8ac6916e26" />
	</SchemeIndex>
</SchemeTable>