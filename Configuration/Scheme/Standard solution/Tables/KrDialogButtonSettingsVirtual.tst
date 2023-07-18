<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="0d52e2ff-45ec-449d-bd49-e0b5f666ee65" Name="KrDialogButtonSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d52e2ff-45ec-009d-2000-00b5f666ee65" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0d52e2ff-45ec-019d-4000-00b5f666ee65" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d52e2ff-45ec-009d-3100-00b5f666ee65" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="a0277225-278b-4203-8b8a-bea725ec5c47" Name="Name" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="de83799e-8c83-4d56-a9a7-6131a2453b2c" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="e07bb4d3-1312-4638-9751-ddd8e3a127fc">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de83799e-8c83-0056-4000-0131a2453b2c" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="2c0addb0-cd0b-434f-b2f8-1ef4129e57d2" />
		<SchemeReferencingColumn ID="cc191b53-2fec-4224-be7e-43f7937a554c" Name="TypeName" Type="String(Max) Not Null" ReferencedColumn="2e5e1f17-469e-489e-9229-5592736dca6b" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a3b6da3f-072f-4cea-8a54-9e79bf173452" Name="Caption" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="90537da9-5f5b-4d25-8c43-0602b1f85052" Name="Icon" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="1e7826dc-4df6-4f8c-8d83-a328ff3875eb" Name="Cancel" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="b7a095ec-eb3b-4c3a-8e19-85107e374f62" Name="df_KrDialogButtonSettingsVirtual_Cancel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="920fa886-70a2-4937-9540-e19bff6a5fe0" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d52e2ff-45ec-009d-5000-00b5f666ee65" Name="pk_KrDialogButtonSettingsVirtual">
		<SchemeIndexedColumn Column="0d52e2ff-45ec-009d-3100-00b5f666ee65" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0d52e2ff-45ec-009d-7000-00b5f666ee65" Name="idx_KrDialogButtonSettingsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0d52e2ff-45ec-019d-4000-00b5f666ee65" />
	</SchemeIndex>
</SchemeTable>