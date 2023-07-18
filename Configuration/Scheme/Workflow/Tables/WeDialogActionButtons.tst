<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="a99b285f-80c3-442a-85a6-2a3bfd645d2b" Name="WeDialogActionButtons" Group="WorkflowEngine" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Секция с настройками кнопок для действия Диалог</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a99b285f-80c3-002a-2000-0a3bfd645d2b" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a99b285f-80c3-012a-4000-0a3bfd645d2b" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a99b285f-80c3-002a-3100-0a3bfd645d2b" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="2757d19d-581b-4b10-b0ae-fd2db00ab006" Name="Name" Type="String(Max) Not Null" />
	<SchemeComplexColumn ID="31d36967-512f-4b0f-b327-00e248352408" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="e07bb4d3-1312-4638-9751-ddd8e3a127fc">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="31d36967-512f-000f-4000-00e248352408" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="2c0addb0-cd0b-434f-b2f8-1ef4129e57d2" />
		<SchemeReferencingColumn ID="61b549f7-a9a7-46d1-b6a3-44bb120f4abd" Name="TypeName" Type="String(Max) Not Null" ReferencedColumn="2e5e1f17-469e-489e-9229-5592736dca6b" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="d8c38911-10ac-4381-8c9c-462d58735b9d" Name="Caption" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="93c1ff5f-7db0-4989-ae13-58397ee407fe" Name="Icon" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="52c7f2b2-2bea-43f2-95e5-234213144a4a" Name="Cancel" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="d1613e8d-109b-48af-b042-58894b716e86" Name="df_WeDialogActionButtons_Cancel" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="05b475ce-6ead-439b-b743-84481059e556" Name="Order" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="e9433b81-d52c-4cf9-8e04-032bab2b16e7" Name="Script" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="7c3fd619-8087-4614-897f-a6d46b0ed2a1" Name="NotEnd" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="9abab96b-dc70-4424-8acf-af9a07857937" Name="df_WeDialogActionButtons_NotEnd" Value="false" />
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="b8d1df47-cfe0-48f9-8836-28db656c3507" Name="TaskDialog" Type="Reference(Typified) Null" ReferencedTable="7c068441-e9e1-445a-a371-bf9436156428" IsReferenceToOwner="true" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b8d1df47-cfe0-00f9-4000-08db656c3507" Name="TaskDialogRowID" Type="Guid Null" ReferencedColumn="7c068441-e9e1-005a-3100-0f9436156428" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a99b285f-80c3-002a-5000-0a3bfd645d2b" Name="pk_WeDialogActionButtons">
		<SchemeIndexedColumn Column="a99b285f-80c3-002a-3100-0a3bfd645d2b" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a99b285f-80c3-002a-7000-0a3bfd645d2b" Name="idx_WeDialogActionButtons_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a99b285f-80c3-012a-4000-0a3bfd645d2b" />
	</SchemeIndex>
</SchemeTable>