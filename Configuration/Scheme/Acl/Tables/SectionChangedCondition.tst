<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="3b9d9643-ed47-4301-94b2-8eedcabc23bc" Name="SectionChangedCondition" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Entries">
	<Description>Секция для условий, проверяющих изменение секции.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b9d9643-ed47-0001-2000-0eedcabc23bc" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="3b9d9643-ed47-0101-4000-0eedcabc23bc" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="9c54d18e-5f42-49cd-aa95-8463351a60c6" Name="Section" Type="Reference(Abstract) Not Null">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="9c54d18e-5f42-00cd-4000-0463351a60c6" Name="SectionID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="5323a99a-a4b6-4c80-b484-e01ae7725ea5" Name="SectionName" Type="String(Max) Not Null" />
		<SchemePhysicalColumn ID="3ccaa512-1abf-4d04-8667-723551f83eb4" Name="SectionTypeID" Type="Int32 Not Null" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="3b9d9643-ed47-0001-5000-0eedcabc23bc" Name="pk_SectionChangedCondition" IsClustered="true">
		<SchemeIndexedColumn Column="3b9d9643-ed47-0101-4000-0eedcabc23bc" />
	</SchemePrimaryKey>
</SchemeTable>