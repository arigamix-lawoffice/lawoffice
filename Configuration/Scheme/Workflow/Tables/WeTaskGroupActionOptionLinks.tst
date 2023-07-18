<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="1e26efb8-a6ee-4582-9ac3-88da4ef74d24" Name="WeTaskGroupActionOptionLinks" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1e26efb8-a6ee-0082-2000-08da4ef74d24" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1e26efb8-a6ee-0182-4000-08da4ef74d24" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="1e26efb8-a6ee-0082-3100-08da4ef74d24" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="643f8435-5906-4e31-99e1-cad23e85a9cc" Name="Link" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="643f8435-5906-0031-4000-0ad23e85a9cc" Name="LinkID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="b7cfad98-da97-46bb-8966-525cf6f01705" Name="LinkName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="c1649e2b-56fb-44be-87d0-0604fe8393bc" Name="LinkCaption" Type="String(Max) Not Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="3a4d7f27-71a6-4aed-b866-90b2efcc231e" Name="Option" Type="Reference(Typified) Not Null" ReferencedTable="dee05376-8267-42b9-8cc9-1ff5bb58bb06" IsReferenceToOwner="true">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3a4d7f27-71a6-00ed-4000-00b2efcc231e" Name="OptionRowID" Type="Guid Not Null" ReferencedColumn="dee05376-8267-00b9-3100-0ff5bb58bb06" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="1e26efb8-a6ee-0082-5000-08da4ef74d24" Name="pk_WeTaskGroupActionOptionLinks">
		<SchemeIndexedColumn Column="1e26efb8-a6ee-0082-3100-08da4ef74d24" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="1e26efb8-a6ee-0082-7000-08da4ef74d24" Name="idx_WeTaskGroupActionOptionLinks_ID" IsClustered="true">
		<SchemeIndexedColumn Column="1e26efb8-a6ee-0182-4000-08da4ef74d24" />
	</SchemeIndex>
</SchemeTable>