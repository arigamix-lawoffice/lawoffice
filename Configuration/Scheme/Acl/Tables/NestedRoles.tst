<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="312b1519-8079-44d7-a5b4-496db41da98c" Name="NestedRoles" Group="Acl" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция для вложенных ролей.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="312b1519-8079-00d7-2000-096db41da98c" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="312b1519-8079-01d7-4000-096db41da98c" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="687534e2-d49e-4b51-b941-c16bd42c2ba0" Name="Context" Type="Reference(Abstract) Null" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="687534e2-d49e-0051-4000-016bd42c2ba0" Name="ContextID" Type="Guid Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
		<SchemePhysicalColumn ID="8b60f42f-9213-4a36-9346-c3aebdc34fe9" Name="ContextName" Type="String(Max) Null" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b02b08b7-448d-4a81-9fca-dc697ca438b8" Name="Parent" Type="Reference(Typified) Not Null" ReferencedTable="81f6010b-9641-4aa5-8897-b8e8603fbf4b" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b02b08b7-448d-0081-4000-0c697ca438b8" Name="ParentID" Type="Guid Not Null" ReferencedColumn="81f6010b-9641-01a5-4000-08e8603fbf4b" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="312b1519-8079-00d7-5000-096db41da98c" Name="pk_NestedRoles" IsClustered="true">
		<SchemeIndexedColumn Column="312b1519-8079-01d7-4000-096db41da98c" />
	</SchemePrimaryKey>
	<SchemeIndex ID="2b006932-c89b-4a06-848d-64e88838e0ff" Name="ndx_NestedRoles_ParentIDContextID" IsUnique="true">
		<SchemeIndexedColumn Column="b02b08b7-448d-0081-4000-0c697ca438b8" />
		<SchemeIndexedColumn Column="687534e2-d49e-0051-4000-016bd42c2ba0" />
	</SchemeIndex>
</SchemeTable>