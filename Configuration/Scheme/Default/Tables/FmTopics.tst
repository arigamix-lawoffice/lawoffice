<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="35b11a3c-f9ec-4fac-a3f1-def11bba44ae" Name="FmTopics" Group="Fm" InstanceType="Cards" ContentType="Collections">
	<Description>Таблица для хранения топиков</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="35b11a3c-f9ec-00ac-2000-0ef11bba44ae" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="35b11a3c-f9ec-01ac-4000-0ef11bba44ae" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="810e1033-46bc-43a8-9271-781b5c5e126e" Name="Description" Type="String(Max) Null" />
	<SchemePhysicalColumn ID="549154f1-9cb4-4f76-a11f-8ff855cf928d" Name="Title" Type="String(Max) Not Null" />
	<SchemePhysicalColumn ID="0cb9524b-c879-40ac-8e38-6a767e15e13b" Name="Created" Type="DateTime Not Null" />
	<SchemeComplexColumn ID="50c74c66-9fbd-41e7-a674-9899561e26ba" Name="Author" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="50c74c66-9fbd-00e7-4000-0899561e26ba" Name="AuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="2658e4ef-ed9c-4cf9-85b9-6dfe2a23a4a5" Name="AuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="2ea050ca-f9c1-40ad-ad23-efb535b63834" Name="IsArchived" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="de12e7df-5eac-4b0b-bae5-6ea57ebde094" Name="df_FmTopics_IsArchived" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="45d40159-87d7-4c77-8e8c-366713913454" Name="LastMessageTime" Type="DateTime Null">
		<Description>Время последнего сообщения</Description>
	</SchemePhysicalColumn>
	<SchemeComplexColumn ID="2e5674df-95d3-4ee3-991c-73d40d193fc7" Name="LastMessageAuthor" Type="Reference(Typified) Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3">
		<Description>Автор последнего сообщения</Description>
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="2e5674df-95d3-00e3-4000-03d40d193fc7" Name="LastMessageAuthorID" Type="Guid Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
		<SchemeReferencingColumn ID="fb00ba09-c138-4774-bbce-c05c5e43ef90" Name="LastMessageAuthorName" Type="String(128) Null" ReferencedColumn="1782f76a-4743-4aa4-920c-7edaee860964" />
	</SchemeComplexColumn>
	<SchemeComplexColumn ID="f0c40d36-32b8-4a07-a4f5-7f909bafb2da" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="c0645587-3584-4b23-867f-54071abfa5a1">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f0c40d36-32b8-0007-4000-0f909bafb2da" Name="TypeID" Type="Guid Not Null" ReferencedColumn="05279a02-2e6d-41de-a328-aa0077493870">
			<SchemeDefaultConstraint IsPermanent="true" ID="b87afe8f-8bd9-4a29-83a1-9d45dd8e7c83" Name="df_FmTopics_TypeID" Value="680d0d81-d8f3-485e-9058-e17ab9e186e0" />
		</SchemeReferencingColumn>
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="35b11a3c-f9ec-00ac-5000-0ef11bba44ae" Name="pk_FmTopics">
		<SchemeIndexedColumn Column="35b11a3c-f9ec-00ac-3100-0ef11bba44ae" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="35b11a3c-f9ec-00ac-7000-0ef11bba44ae" Name="idx_FmTopics_ID" IsClustered="true">
		<SchemeIndexedColumn Column="35b11a3c-f9ec-01ac-4000-0ef11bba44ae" />
	</SchemeIndex>
	<SchemeIndex ID="8c3e3cf0-9370-4c1d-9d9c-68af13121404" Name="ndx_FmTopics_IDTypeID">
		<SchemeIndexedColumn Column="35b11a3c-f9ec-01ac-4000-0ef11bba44ae" />
		<SchemeIndexedColumn Column="f0c40d36-32b8-0007-4000-0f909bafb2da" />
	</SchemeIndex>
</SchemeTable>