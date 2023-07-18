<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a6386cf3-fff8-401e-b8d2-222d0221951f" Name="KrVirtualFileStates" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<Description>Состояния для карточки виртуального файла</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6386cf3-fff8-001e-2000-022d0221951f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a6386cf3-fff8-011e-4000-022d0221951f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6386cf3-fff8-001e-3100-022d0221951f" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="7b230c5d-5e19-4b77-95e7-a3742319c426" Name="State" Type="Reference(Typified) Not Null" ReferencedTable="47107d7a-3a8c-47f0-b800-2a45da222ff4">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7b230c5d-5e19-0077-4000-03742319c426" Name="StateID" Type="Int16 Not Null" ReferencedColumn="502209b0-233f-4e1f-be01-35a50f53414c" />
		<SchemeReferencingColumn ID="8e0070e5-844b-41e1-b261-f41f50322b20" Name="StateName" Type="String(128) Not Null" ReferencedColumn="4c1a8dd7-72ed-4fc9-b559-b38ae30dccb9" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6386cf3-fff8-001e-5000-022d0221951f" Name="pk_KrVirtualFileStates">
		<SchemeIndexedColumn Column="a6386cf3-fff8-001e-3100-022d0221951f" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a6386cf3-fff8-001e-7000-022d0221951f" Name="idx_KrVirtualFileStates_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a6386cf3-fff8-011e-4000-022d0221951f" />
	</SchemeIndex>
	<SchemeIndex ID="b21cb481-e780-4cad-80a9-454fb9b62d84" Name="ndx_KrVirtualFileStates_StateIDID">
		<SchemeIndexedColumn Column="7b230c5d-5e19-0077-4000-03742319c426" />
		<SchemeIndexedColumn Column="a6386cf3-fff8-011e-4000-022d0221951f" />
	</SchemeIndex>
</SchemeTable>