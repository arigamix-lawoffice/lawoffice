<?xml version="1.0" encoding="utf-8"?>
<SchemeTable IsSystem="true" IsPermanent="true" ID="1074eadd-21d7-4925-98c8-40d1e5f0ca0e" Name="Instances" Group="System">
	<Description>Contains system info of cards</Description>
	<SchemePhysicalColumn IsPermanent="true" IsSealed="true" ID="9a58123b-b2e9-4137-9c6c-5dab0ec02747" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>Identity of an instance</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn IsPermanent="true" IsSealed="true" ID="60c8027d-9dfe-42a6-a06f-7672e561c7a2" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="60c8027d-9dfe-00a6-4000-0672e561c7a2" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4">
			<Description>ID of a type.</Description>
		</SchemeReferencingColumn>
		<SchemeReferencingColumn ID="92bcb2df-1c3a-4fd0-bced-d371b6661562" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa">
			<Description>Caption of a type.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="ca3866a0-02f0-4618-81fd-278fdc22a032" Name="Created" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="838214a4-4dbf-4ad5-bb10-26be0511da3c" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="838214a4-4dbf-00d5-4000-06be0511da3c" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="84113bd6-a52e-4226-86f6-7df11fb8aee4" Name="CreatedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="b7c70726-9456-43ea-a10d-2eb4c4ff9855" Name="Modified" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="d1c28a25-f8fc-4879-89f2-74c192b76ffb" Name="ModifiedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d1c28a25-f8fc-0079-4000-04c192b76ffb" Name="ModifiedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="25ddb036-f04d-4ce0-898e-bccbf6fd1113" Name="ModifiedByName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964">
			<Description>Отображаемое имя пользователя.</Description>
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="97ce640c-b0dd-4b03-9be8-4e0545426378" Name="Version" Type="Int32 Not Null" />
	<SchemePrimaryKey IsPermanent="true" IsSealed="true" ID="9b74ae19-8d78-4a3c-bfce-35bd0cbfdee1" Name="pk_Instances" IsClustered="true">
		<SchemeIndexedColumn Column="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemePrimaryKey>
	<SchemeIndex ID="688f487d-c1c8-4c11-8c84-72941a995036" Name="ndx_Instances_TypeID">
		<SchemeIndexedColumn Column="60c8027d-9dfe-00a6-4000-0672e561c7a2" />
	</SchemeIndex>
</SchemeTable>