<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="a4b6af05-9147-4335-8bf4-0e7387f77455" Name="KrPermissionExtendedMandatoryRules" Group="Kr" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4b6af05-9147-0035-2000-0e7387f77455" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a4b6af05-9147-0135-4000-0e7387f77455" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4b6af05-9147-0035-3100-0e7387f77455" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="d6779b1a-6cdf-4670-afc3-1506aa48903e" Name="Section" Type="Reference(Abstract) Not Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d6779b1a-6cdf-0070-4000-0506aa48903e" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="6319c7e3-c6ff-4e7b-9b98-71dfbf03b728" Name="SectionName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="7a2b14db-c0d8-40c4-a6c5-57c7fed84020" Name="SectionTypeID" Type="Int32 Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e75237ff-be41-4998-915a-a07e4880db9b" Name="ValidationType" Type="Reference(Typified) Not Null" ReferencedTable="4439a1f6-c747-442b-b315-caae1c934058">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e75237ff-be41-0098-4000-007e4880db9b" Name="ValidationTypeID" Type="Int32 Not Null" ReferencedColumn="5d57253a-2583-479d-86df-e00c72b335a4" />
		<SchemeReferencingColumn ID="6494ab14-5b0e-41bb-9cce-b160c1a3f56d" Name="ValidationTypeName" Type="String(Max) Not Null" ReferencedColumn="1a620d37-9710-4302-8ab8-17c7d5f0b96d" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="29878a2e-956c-4a9f-aedb-b4ca94a493c4" Name="Text" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="f9cb14fe-d5c0-4806-a39d-dbe166f745de" Name="Order" Type="Int32 Not Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4b6af05-9147-0035-5000-0e7387f77455" Name="pk_KrPermissionExtendedMandatoryRules">
		<SchemeIndexedColumn Column="a4b6af05-9147-0035-3100-0e7387f77455" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a4b6af05-9147-0035-7000-0e7387f77455" Name="idx_KrPermissionExtendedMandatoryRules_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a4b6af05-9147-0135-4000-0e7387f77455" />
	</SchemeIndex>
</SchemeTable>