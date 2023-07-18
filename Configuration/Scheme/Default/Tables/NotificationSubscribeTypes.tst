<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="287bfcf3-aa96-44ee-96a8-68fbc1f2d3ab" Name="NotificationSubscribeTypes" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="287bfcf3-aa96-00ee-2000-08fbc1f2d3ab" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="287bfcf3-aa96-01ee-4000-08fbc1f2d3ab" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="287bfcf3-aa96-00ee-3100-08fbc1f2d3ab" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b79e8705-75b2-48e4-b61d-6ec48eb00496" Name="NotificationType" Type="Reference(Typified) Not Null" ReferencedTable="bae37ba2-7a39-49a1-8cc8-64f032ba3f79">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b79e8705-75b2-00e4-4000-0ec48eb00496" Name="NotificationTypeID" Type="Guid Not Null" ReferencedColumn="bae37ba2-7a39-01a1-4000-04f032ba3f79" />
		<SchemeReferencingColumn ID="6dde1f0b-79f3-46a4-817f-5f2b41a9b916" Name="NotificationTypeName" Type="String(256) Not Null" ReferencedColumn="fe686962-4e72-4a67-8dc8-9afa19da3f6a" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="287bfcf3-aa96-00ee-5000-08fbc1f2d3ab" Name="pk_NotificationSubscribeTypes">
		<SchemeIndexedColumn Column="287bfcf3-aa96-00ee-3100-08fbc1f2d3ab" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="287bfcf3-aa96-00ee-7000-08fbc1f2d3ab" Name="idx_NotificationSubscribeTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="287bfcf3-aa96-01ee-4000-08fbc1f2d3ab" />
	</SchemeIndex>
</SchemeTable>