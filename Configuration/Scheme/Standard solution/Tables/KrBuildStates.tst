<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="e12af590-efd5-4890-b1c7-5a7ce83195dd" Name="KrBuildStates" Group="Kr">
	<Description>Состояние компиляции объекта.</Description>
	<SchemePhysicalColumn ID="cc448df6-5d4f-4641-91ea-32dd7c455205" Name="ID" Type="Int16 Not Null" />
	<SchemePhysicalColumn ID="b0b81b41-7500-4290-8055-af8d5ee3d310" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="3e557e4c-748c-46b1-b201-123102ef7d62" Name="pk_KrBuildStates">
		<SchemeIndexedColumn Column="cc448df6-5d4f-4641-91ea-32dd7c455205" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="cc448df6-5d4f-4641-91ea-32dd7c455205">0</ID>
		<Name ID="b0b81b41-7500-4290-8055-af8d5ee3d310">$Enum_KrBuildStates_None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cc448df6-5d4f-4641-91ea-32dd7c455205">1</ID>
		<Name ID="b0b81b41-7500-4290-8055-af8d5ee3d310">$Enum_KrBuildStates_Error</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="cc448df6-5d4f-4641-91ea-32dd7c455205">2</ID>
		<Name ID="b0b81b41-7500-4290-8055-af8d5ee3d310">$Enum_KrBuildStates_Success</Name>
	</SchemeRecord>
</SchemeTable>