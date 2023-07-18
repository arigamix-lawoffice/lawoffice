<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="0958f50b-8fd2-4e65-9531-fd540f3150ab" Name="RoleDeputiesNestedManagementTypes" Group="Acl" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0958f50b-8fd2-0065-2000-0d540f3150ab" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="0958f50b-8fd2-0165-4000-0d540f3150ab" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="0958f50b-8fd2-0065-3100-0d540f3150ab" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="b3d43f3c-601f-4536-8c30-a18bb865b1ff" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="dd329f32-adf0-4336-bd9e-fa084c0fe494" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b3d43f3c-601f-0036-4000-018bb865b1ff" Name="ParentRowID" Type="Guid Not Null" ReferencedColumn="dd329f32-adf0-0036-3100-0a084c0fe494" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="e1207f82-93dc-4a66-822e-cc54bc3c5ecf" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="a90baecf-c9ce-4cba-8bb0-150a13666266" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="e1207f82-93dc-0066-4000-0c54bc3c5ecf" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a90baecf-c9ce-01ba-4000-050a13666266" />
		<SchemeReferencingColumn ID="0707610e-26b4-463d-804e-c05aa1ea8daf" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="447f7cb1-76ae-4703-b3bb-16a57d4e7ab1" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="0958f50b-8fd2-0065-5000-0d540f3150ab" Name="pk_RoleDeputiesNestedManagementTypes">
		<SchemeIndexedColumn Column="0958f50b-8fd2-0065-3100-0d540f3150ab" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="0958f50b-8fd2-0065-7000-0d540f3150ab" Name="idx_RoleDeputiesNestedManagementTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="0958f50b-8fd2-0165-4000-0d540f3150ab" />
	</SchemeIndex>
	<SchemeIndex ID="394287fd-80e7-48e4-a0ba-1a6355051a73" Name="ndx_RoleDeputiesNestedManagementTypes_TypeIDID">
		<SchemeIndexedColumn Column="e1207f82-93dc-0066-4000-0c54bc3c5ecf" />
		<SchemeIndexedColumn Column="0958f50b-8fd2-0165-4000-0d540f3150ab" />
	</SchemeIndex>
	<SchemeIndex ID="bd6375e2-1086-4fa1-b543-0b891e3ca30a" Name="ndx_RoleDeputiesNestedManagementTypes_ParentRowIDTypeIDRowID">
		<SchemeIndexedColumn Column="b3d43f3c-601f-0036-4000-018bb865b1ff" />
		<SchemeIndexedColumn Column="e1207f82-93dc-0066-4000-0c54bc3c5ecf" />
		<SchemeIndexedColumn Column="0958f50b-8fd2-0065-3100-0d540f3150ab" />
	</SchemeIndex>
</SchemeTable>