<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="219b245d-a909-4517-8be6-d22ef7a28dba" Name="KrSettingsRouteExtraTaskTypes" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="219b245d-a909-0017-2000-022ef7a28dba" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="219b245d-a909-0117-4000-022ef7a28dba" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="219b245d-a909-0017-3100-022ef7a28dba" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="f552ad5c-232c-412f-8115-8367965d7326" Name="TaskType" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f552ad5c-232c-002f-4000-0367965d7326" Name="TaskTypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="ccd0d4c0-f39d-468d-9546-d86eec6ff344" Name="TaskTypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="693a8ecb-13af-4518-99cb-42454a55393b" Name="TaskTypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="219b245d-a909-0017-5000-022ef7a28dba" Name="pk_KrSettingsRouteExtraTaskTypes">
		<SchemeIndexedColumn Column="219b245d-a909-0017-3100-022ef7a28dba" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="219b245d-a909-0017-7000-022ef7a28dba" Name="idx_KrSettingsRouteExtraTaskTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="219b245d-a909-0117-4000-022ef7a28dba" />
	</SchemeIndex>
</SchemeTable>