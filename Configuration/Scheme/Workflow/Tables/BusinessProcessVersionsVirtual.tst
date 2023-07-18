<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="6999d0d3-a44f-43b5-8e79-a551697340e6" Name="BusinessProcessVersionsVirtual" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Список версий бизнес-процесса, который отображается в карточке шаблона бизнес-процесса</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6999d0d3-a44f-00b5-2000-0551697340e6" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="6999d0d3-a44f-01b5-4000-0551697340e6" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="6999d0d3-a44f-00b5-3100-0551697340e6" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="bc2464df-08ea-4909-b712-414c6ea2869f" Name="Version" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="35394860-fd0f-4aa3-8403-27482aa8f8df" Name="Created" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="15a6f077-6eef-4a31-835d-c8e6d957ed66" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="15a6f077-6eef-0031-4000-08e6d957ed66" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="6758baaf-2a3e-4617-920c-83f0c607b010" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="a53475cd-6351-4a07-bff6-d3817f4b8efa" Name="Modified" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="a6141350-67a6-47e4-b208-7e7e53da5347" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a6141350-67a6-00e4-4000-0e7e53da5347" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a48843fe-68be-4eb6-8e13-df14bc3ddcb0" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="fb866c3f-072e-4342-ad36-6bd3cad6ed05" Name="ParentVersion" Type="Int32 Null" />
	<SchemePhysicalColumn ID="5119b710-e65c-4620-84a1-9d271e1216ab" Name="IsDefault" Type="Boolean Not Null" />
	<SchemePhysicalColumn ID="7fc8d728-80ec-40a5-b203-09c5b709782a" Name="LockedForEditing" Type="Boolean Not Null" />
	<SchemeComplexColumn ID="378675ca-e3b3-4e4a-ac40-dd8a92a0365c" Name="LockedBy" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="378675ca-e3b3-004a-4000-0d8a92a0365c" Name="LockedByID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="d40ed089-fbc0-40ab-a6ec-b2ac574e83d3" Name="LockedByName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="75647825-8974-48cd-a1ad-a034317d40c4" Name="ActiveCount" Type="Int32 Not Null">
		<Description>Число активных процессов</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4668373a-3d13-4ec6-a1c4-2cb326813a27" Name="Locked" Type="DateTime Null">
		<Description>Дата блокировки версии процесса</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="6999d0d3-a44f-00b5-5000-0551697340e6" Name="pk_BusinessProcessVersionsVirtual">
		<SchemeIndexedColumn Column="6999d0d3-a44f-00b5-3100-0551697340e6" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="6999d0d3-a44f-00b5-7000-0551697340e6" Name="idx_BusinessProcessVersionsVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="6999d0d3-a44f-01b5-4000-0551697340e6" />
	</SchemeIndex>
</SchemeTable>