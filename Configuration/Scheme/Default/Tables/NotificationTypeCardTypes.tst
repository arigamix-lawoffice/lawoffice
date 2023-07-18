<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b54be13b-72be-4b20-b090-b861efaf8585" Name="NotificationTypeCardTypes" Group="System" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b54be13b-72be-0020-2000-0861efaf8585" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b54be13b-72be-0120-4000-0861efaf8585" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="b54be13b-72be-0020-3100-0861efaf8585" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="5f0bd84a-17ab-4bf8-8f81-1dbe036a5765" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5f0bd84a-17ab-00f8-4000-0dbe036a5765" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="ab6186c4-3283-4e7b-9482-692afc962b72" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="b54be13b-72be-0020-5000-0861efaf8585" Name="pk_NotificationTypeCardTypes">
		<SchemeIndexedColumn Column="b54be13b-72be-0020-3100-0861efaf8585" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="b54be13b-72be-0020-7000-0861efaf8585" Name="idx_NotificationTypeCardTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="b54be13b-72be-0120-4000-0861efaf8585" />
	</SchemeIndex>
</SchemeTable>