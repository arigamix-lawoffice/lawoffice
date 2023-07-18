<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="608289ef-42c8-4f6e-8d4f-27ef725732b5" Name="Satellites" Group="System" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="32561dd5-a010-0057-2000-0c3e621034f4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="32561dd5-a010-0157-4000-0c3e621034f4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="c74c77cf-07ef-45f5-b700-d8d6bcfe53e3" Name="MainCard" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="c74c77cf-07ef-00f5-4000-08d6bcfe53e3" Name="MainCardID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="52278112-300d-4150-ad5c-2f796ec58235" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="52278112-300d-0050-4000-0f796ec58235" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e3538214-777f-4ed5-b787-b580b1a9b3fd" Name="Task" Type="Reference(Typified) Null" ReferencedTable="5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e3538214-777f-00d5-4000-0580b1a9b3fd" Name="TaskID" Type="Guid Null" ReferencedColumn="5bfa9936-bb5a-008f-3100-080bfd8f75f8" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="32561dd5-a010-0057-5000-0c3e621034f4" Name="pk_Satellites" IsClustered="true">
		<SchemeIndexedColumn Column="32561dd5-a010-0157-4000-0c3e621034f4" />
	</SchemePrimaryKey>
	<SchemeIndex ID="4568eb44-f43c-4ccb-98a8-ef7916fe1f2c" Name="ndx_Satellites_MainCardIDTypeID">
		<SchemeIndexedColumn Column="c74c77cf-07ef-00f5-4000-08d6bcfe53e3" />
		<SchemeIndexedColumn Column="52278112-300d-0050-4000-0f796ec58235" />
	</SchemeIndex>
</SchemeTable>