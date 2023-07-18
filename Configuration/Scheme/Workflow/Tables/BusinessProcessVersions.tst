<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="dcd38c54-ed18-4503-b435-3dee1c6c2c62" Name="BusinessProcessVersions" Group="WorkflowEngine" InstanceType="Cards" ContentType="Hierarchies">
	<Description>Дерево версий бизнесс процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dcd38c54-ed18-0003-2000-0dee1c6c2c62" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dcd38c54-ed18-0103-4000-0dee1c6c2c62" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="dcd38c54-ed18-0003-3100-0dee1c6c2c62" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" ID="dcd38c54-ed18-0003-2200-0dee1c6c2c62" Name="Parent" Type="Reference(Typified) Null" ReferencedTable="dcd38c54-ed18-4503-b435-3dee1c6c2c62">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="dcd38c54-ed18-0103-4020-0dee1c6c2c62" Name="ParentRowID" Type="Guid Null" ReferencedColumn="dcd38c54-ed18-0003-3100-0dee1c6c2c62" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="f69da7c5-7010-408e-9163-4ff02d2c6658" Name="Version" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="97ec7948-e15f-4370-95d3-5798c420c4d0" Name="Created" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="6b8ac7e5-a91c-49a4-965a-aa45c9055c13" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6b8ac7e5-a91c-00a4-4000-0a45c9055c13" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="c69027c6-53e1-45a9-86a1-9c1d309245bc" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="7a270cb6-daae-4680-8bbd-9dd123a0e2f0" Name="Modified" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="439c3dc3-edcf-40c1-a40d-fc2102eb4a17" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="439c3dc3-edcf-00c1-4000-0c2102eb4a17" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="9a7ade6a-92d4-4f8e-a2ae-71270af561a9" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="342eae18-5da4-478c-8844-8d63787a4529" Name="LockedForEditing" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="7c90d53d-91ea-461d-b208-0688253db61d" Name="ScriptFileID" Type="Guid Null" />
	<SchemePhysicalColumn ID="39796979-a52c-4017-9be1-5f30e22a7c2f" Name="ProcessData" Type="BinaryJson Null" />
	<SchemePhysicalColumn ID="3b61fd69-f2b1-4e4a-8ad9-29affa2dd78c" Name="IsDefault" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="1254cb0b-043f-47ce-b08e-cca7ae76c7f6" Name="df_BusinessProcessVersions_IsDefault" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="4876d08e-3f2d-4c33-b75d-41f2f1eafbe9" Name="LockedBy" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="4876d08e-3f2d-0033-4000-01f2f1eafbe9" Name="LockedByID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="ab7295b2-7e96-447d-9d69-96a53cd3df5b" Name="LockedByName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="33398b95-01e3-4b0d-9287-446deb059410" Name="Locked" Type="DateTime Null">
		<Description>Дата блокировки версии процесса</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="dcd38c54-ed18-0003-5000-0dee1c6c2c62" Name="pk_BusinessProcessVersions">
		<SchemeIndexedColumn Column="dcd38c54-ed18-0003-3100-0dee1c6c2c62" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="66b21a31-9f66-46f5-97f3-abb47243e41b" Name="ndx_BusinessProcessVersions_ParentRowID">
		<Predicate Dbms="SqlServer">[ParentRowID] IS NOT NULL</Predicate>
		<Predicate Dbms="PostgreSql">"ParentRowID" IS NOT NULL</Predicate>
		<SchemeIndexedColumn Column="dcd38c54-ed18-0103-4020-0dee1c6c2c62" />
	</SchemeIndex>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="dcd38c54-ed18-0003-7000-0dee1c6c2c62" Name="idx_BusinessProcessVersions_ID" IsClustered="true">
		<SchemeIndexedColumn Column="dcd38c54-ed18-0103-4000-0dee1c6c2c62" />
	</SchemeIndex>
</SchemeTable>