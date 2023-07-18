<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="3dbb9a1f-ae27-4612-aec1-4f077494dfef" Name="LawAdministrators" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3dbb9a1f-ae27-0012-2000-0f077494dfef" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3dbb9a1f-ae27-0112-4000-0f077494dfef" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3dbb9a1f-ae27-0012-3100-0f077494dfef" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="ae249ecb-0f57-456e-be97-2d9f4dec4992" Name="User" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ae249ecb-0f57-006e-4000-0d9f4dec4992" Name="UserID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="e1b2ddc8-d68d-4324-8659-e9ef43b2823b" Name="UserName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3dbb9a1f-ae27-0012-5000-0f077494dfef" Name="pk_LawAdministrators">
		<SchemeIndexedColumn Column="3dbb9a1f-ae27-0012-3100-0f077494dfef" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="3dbb9a1f-ae27-0012-7000-0f077494dfef" Name="idx_LawAdministrators_ID" IsClustered="true">
		<SchemeIndexedColumn Column="3dbb9a1f-ae27-0112-4000-0f077494dfef" />
	</SchemeIndex>
</SchemeTable>