<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="f3f630df-d649-43ce-9d5b-75048184a749" ID="0b3ce213-cc34-469d-a0dd-19a4643b1a49" Name="LawUsers" Group="LawList" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0b3ce213-cc34-009d-2000-09a4643b1a49" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0b3ce213-cc34-019d-4000-09a4643b1a49" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0b3ce213-cc34-009d-3100-09a4643b1a49" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="43ea0c1d-aad5-44eb-a42f-f1283d810f14" Name="User" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="43ea0c1d-aad5-00eb-4000-01283d810f14" Name="UserID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="053d5536-0c22-4aa9-934a-0af536f20d5a" Name="UserName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="4f6a0967-4ba7-4909-bd30-a56c60b2a434" Name="UserWorkplace" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0b3ce213-cc34-009d-5000-09a4643b1a49" Name="pk_LawUsers">
		<SchemeIndexedColumn Column="0b3ce213-cc34-009d-3100-09a4643b1a49" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0b3ce213-cc34-009d-7000-09a4643b1a49" Name="idx_LawUsers_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0b3ce213-cc34-019d-4000-09a4643b1a49" />
	</SchemeIndex>
</SchemeTable>