<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ba745c18-badf-4d8c-a26c-46619ba56b6f" Name="KrSecondaryProcessGroupsVirtual" Group="Kr" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ba745c18-badf-008c-2000-06619ba56b6f" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ba745c18-badf-018c-4000-06619ba56b6f" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="ba745c18-badf-008c-3100-06619ba56b6f" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="488cc232-dd99-4d83-8b11-9d4178207c8c" Name="StageGroup" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="488cc232-dd99-0083-4000-0d4178207c8c" Name="StageGroupID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="91a93ed4-a774-4eb0-bef1-495cb8aabecd" Name="StageGroupName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="ba745c18-badf-008c-5000-06619ba56b6f" Name="pk_KrSecondaryProcessGroupsVirtual">
		<SchemeIndexedColumn Column="ba745c18-badf-008c-3100-06619ba56b6f" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="ba745c18-badf-008c-7000-06619ba56b6f" Name="idx_KrSecondaryProcessGroupsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="ba745c18-badf-018c-4000-06619ba56b6f" />
	</SchemeIndex>
</SchemeTable>