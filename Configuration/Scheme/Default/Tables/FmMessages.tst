<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="a03f6c5d-e719-43d6-bcc5-d2ea321765ab" Name="FmMessages" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица для хранения сообщений</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a03f6c5d-e719-00d6-2000-02ea321765ab" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="a03f6c5d-e719-01d6-4000-02ea321765ab" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="a03f6c5d-e719-00d6-3100-02ea321765ab" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="964b37bc-0ff9-4eb9-aafe-d9094b371751" Name="Body" Type="BinaryJson Not Null">
		<Description>Json с html тела сообщений и служебными полями "Users", "Roles"</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6cef9f61-7549-4d2c-9e60-809c9336388d" Name="Created" Type="DateTime2 Not Null">
		<Description>Дата создания сообщения</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="d317d42f-8bfa-41df-868c-a916b6357117" Name="Author" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="d317d42f-8bfa-00df-4000-0916b6357117" Name="AuthorID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="b97d2a05-bd20-4103-9a34-c1b66aab6820" Name="AuthorName" Type="String(128) Not Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="b13ab931-cbde-4c4e-8155-07fe794a96e2" Name="Topic" Type="Reference(Typified) Not Null" ReferencedTable="35b11a3c-f9ec-4fac-a3f1-def11bba44ae">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="b13ab931-cbde-004e-4000-07fe794a96e2" Name="TopicRowID" Type="Guid Not Null" ReferencedColumn="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="1af200a8-1852-4505-b634-1afea4b57adf" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="43f92881-c875-437a-bf1c-b7793c099d00" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="1af200a8-1852-0005-4000-0afea4b57adf" Name="TypeID" Type="Int32 Not Null" ReferencedColumn="70da9423-8064-4045-842f-69a3fe1f59e5" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="07039209-fc2f-4c39-8c58-f447b30c99ca" Name="ModifiedBy" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="07039209-fc2f-0039-4000-0447b30c99ca" Name="ModifiedByID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="a173f1d5-3b79-4d0a-ba16-80e8da10d8e8" Name="ModifiedByName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="1bf08245-a305-4cdd-81b7-23d548e8238b" Name="ModifiedAt" Type="DateTime Null" />
	<SchemePhysicalColumn ID="9a54ff5b-9248-48c4-9917-1708781a2f79" Name="PlainText" Type="String(Max) Null">
		<Description>В этой колонке хранятся сообщения без форматирования (без html)</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="ec80a438-29d4-46a0-899f-c106d9ad7be1" Name="df_FmMessages_PlainText" Value="" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="a03f6c5d-e719-00d6-5000-02ea321765ab" Name="pk_FmMessages">
		<SchemeIndexedColumn Column="a03f6c5d-e719-00d6-3100-02ea321765ab" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="a03f6c5d-e719-00d6-7000-02ea321765ab" Name="idx_FmMessages_ID" IsClustered="true">
		<SchemeIndexedColumn Column="a03f6c5d-e719-01d6-4000-02ea321765ab" />
	</SchemeIndex>
	<SchemeIndex ID="f3a35e95-7ed7-4ee5-8c01-2841f523367a" Name="ndx_FmMessages_TopicRowIDCreated">
		<SchemeIndexedColumn Column="b13ab931-cbde-004e-4000-07fe794a96e2" />
		<SchemeIndexedColumn Column="6cef9f61-7549-4d2c-9e60-809c9336388d" />
	</SchemeIndex>
</SchemeTable>