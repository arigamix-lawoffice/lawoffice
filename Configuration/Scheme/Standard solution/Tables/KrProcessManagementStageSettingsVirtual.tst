<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="65b430e7-42f5-44c0-9d36-d31756c9941a" Name="KrProcessManagementStageSettingsVirtual" Group="KrStageTypes" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="65b430e7-42f5-00c0-2000-031756c9941a" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="65b430e7-42f5-01c0-4000-031756c9941a" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="7d536fbf-a804-4667-a44b-f8e9decd0c07" Name="StageGroup" Type="Reference(Typified) Null" ReferencedTable="fde6b6e3-f7b6-467f-96e1-e2df41a22f05">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="7d536fbf-a804-0067-4000-08e9decd0c07" Name="StageGroupID" Type="Guid Null" ReferencedColumn="fde6b6e3-f7b6-017f-4000-02df41a22f05" />
		<SchemeReferencingColumn ID="de7fbaa0-496e-4c27-ade4-56a5e296f510" Name="StageGroupName" Type="String(255) Null" ReferencedColumn="fc8faabd-cc86-44b3-8430-1a0e816cea27" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="988b0607-e734-4fc2-891d-d1786aec59c9" Name="Stage" Type="Reference(Typified) Null" ReferencedTable="92caadca-2409-40ff-b7d8-1d4fd302b1e9">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="988b0607-e734-00c2-4000-01786aec59c9" Name="StageRowID" Type="Guid Null" ReferencedColumn="92caadca-2409-00ff-3100-0d4fd302b1e9" />
		<SchemeReferencingColumn ID="b546a741-d374-4199-a115-3cc75d01a281" Name="StageName" Type="String(128) Null" ReferencedColumn="95ac1ae3-f232-47d5-b7bf-2d012f6117db" />
		<SchemePhysicalColumn ID="3118e068-e314-456a-abaf-cb0ce776332b" Name="StageRowGroupName" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c3b9e900-9d2b-4920-8cf8-c10094fe385c" Name="ManagePrimaryProcess" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="93c0f558-39c2-4978-9aa5-cd133beee5f1" Name="df_KrProcessManagementStageSettingsVirtual_ManagePrimaryProcess" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="711d4232-22c6-4eeb-98ca-6a2642fc3acc" Name="Mode" Type="Reference(Typified) Not Null" ReferencedTable="778c5e62-6064-447e-92ac-68913d6a42cd">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="711d4232-22c6-00eb-4000-0a2642fc3acc" Name="ModeID" Type="Int16 Not Null" ReferencedColumn="99558c30-ca4e-42f3-952f-9c486b6d4c4c" />
		<SchemeReferencingColumn ID="ed364921-24ca-4f66-b233-257b35902352" Name="ModeName" Type="String(128) Not Null" ReferencedColumn="48aeec20-f020-42b5-87f3-30dcff12f057" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="31c4ad92-0e7b-4f64-a163-ac0987e3d854" Name="Signal" Type="String(128) Null" />
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="65b430e7-42f5-00c0-5000-031756c9941a" Name="pk_KrProcessManagementStageSettingsVirtual" IsClustered="true">
		<SchemeIndexedColumn Column="65b430e7-42f5-01c0-4000-031756c9941a" />
	</SchemePrimaryKey>
</SchemeTable>