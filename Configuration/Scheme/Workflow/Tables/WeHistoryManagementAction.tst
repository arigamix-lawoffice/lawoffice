<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="bb018cba-ef03-4bb4-a7e6-8fb083fc44a4" Name="WeHistoryManagementAction" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="bb018cba-ef03-00b4-2000-0fb083fc44a4" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="bb018cba-ef03-01b4-4000-0fb083fc44a4" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="ba3ca71f-60b8-492c-bc61-b70f668e4bc5" Name="TaskHistoryGroupType" Type="Reference(Typified) Not Null" ReferencedTable="319be329-6cd3-457a-b792-41c26a266b95">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="ba3ca71f-60b8-002c-4000-070f668e4bc5" Name="TaskHistoryGroupTypeID" Type="Guid Not Null" ReferencedColumn="319be329-6cd3-017a-4000-01c26a266b95" />
		<SchemeReferencingColumn ID="38b01a03-93a8-4e64-b16d-effea2977ec8" Name="TaskHistoryGroupTypeCaption" Type="String(128) Not Null" ReferencedColumn="bf5a5121-9947-45f6-a8a0-2608885b4e19" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="82d96d29-33b3-42c8-b0d8-893958b52480" Name="ParentTaskHistoryGroupType" Type="Reference(Typified) Not Null" ReferencedTable="319be329-6cd3-457a-b792-41c26a266b95">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="82d96d29-33b3-00c8-4000-093958b52480" Name="ParentTaskHistoryGroupTypeID" Type="Guid Not Null" ReferencedColumn="319be329-6cd3-017a-4000-01c26a266b95" />
		<SchemeReferencingColumn ID="d9f8a854-6a0c-401e-9dd6-374b611c49bd" Name="ParentTaskHistoryGroupTypeCaption" Type="String(128) Not Null" ReferencedColumn="bf5a5121-9947-45f6-a8a0-2608885b4e19" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="050bc663-5876-4a06-bf7f-bc96a27f02a2" Name="NewIteration" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="aab94d49-a1b9-4b24-99cb-0b31cb50c967" Name="df_WeHistoryManagementAction_NewIteration" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="bb018cba-ef03-00b4-5000-0fb083fc44a4" Name="pk_WeHistoryManagementAction" IsClustered="true">
		<SchemeIndexedColumn Column="bb018cba-ef03-01b4-4000-0fb083fc44a4" />
	</SchemePrimaryKey>
</SchemeTable>