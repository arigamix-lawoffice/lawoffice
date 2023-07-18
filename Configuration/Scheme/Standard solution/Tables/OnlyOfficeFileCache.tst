<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="8c151402-bae9-41a6-864f-f7558bd88c86" Name="OnlyOfficeFileCache" Group="OnlyOffice">
	<SchemePhysicalColumn ID="5b339133-6202-418f-8db5-16d6ddc0e2de" Name="ID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="de77cc8f-e25d-4818-8de5-9f4e12edc926" Name="CreatedBy" Type="Reference(Typified) Not Null" ReferencedTable="6c977939-bbfc-456f-a133-f1c2244e3cc3" WithForeignKey="false">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="de77cc8f-e25d-0018-4000-0f4e12edc926" Name="CreatedByID" Type="Guid Not Null" ReferencedColumn="6c977939-bbfc-016f-4000-01c2244e3cc3" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="51019018-a2b1-4706-a2ff-0f58e9a27b93" Name="SourceFileVersionID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="d56e7145-dbbe-400a-9cc1-da22e1f9f581" Name="SourceFileName" Type="String(256) Not Null" />
	<SchemePhysicalColumn ID="7df8ad86-ce8b-4959-904a-ed9537baead0" Name="ModifiedFileUrl" Type="String(Max) Null">
		<Description>The URL to the edited file on the document server.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bd265ad1-f4c3-44fa-9959-ee0fceb1dd7f" Name="LastModifiedFileUrlTime" Type="DateTime Null">
		<Description>The time ModifiedFileUrl was last modified.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="1831cf9d-8a56-41f2-abba-383aade27b8e" Name="LastAccessTime" Type="DateTime Not Null">
		<Description>The time this row was created/modified/selected.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="11179ecc-2a2a-462b-bdf1-0823784035d2" Name="HasChangesAfterClose" Type="Boolean Null" />
	<SchemePhysicalColumn ID="c9a8aea7-3306-43f2-8670-1bf3bd26eb17" Name="EditorWasOpen" Type="Boolean Not Null">
		<SchemeDefaultConstraint IsPermanent="true" ID="39ff7a8a-bf96-4017-b2f6-2695a57d3838" Name="df_OnlyOfficeFileCache_EditorWasOpen" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="3f2b96af-f85e-4505-8aea-a07e02d47766" Name="CoeditKey" Type="String(128) Null" />
	<SchemePrimaryKey ID="a0e2331d-a7d8-4ccb-90f4-c784fc33174a" Name="pk_OnlyOfficeFileCache" IsClustered="true">
		<SchemeIndexedColumn Column="5b339133-6202-418f-8db5-16d6ddc0e2de" />
	</SchemePrimaryKey>
	<SchemeIndex ID="0838f15a-ad68-4a2e-849a-2052abcece2b" Name="ndx_OnlyOfficeFileCache_CoeditKeyCreatedByID">
		<SchemeIndexedColumn Column="3f2b96af-f85e-4505-8aea-a07e02d47766" />
		<SchemeIndexedColumn Column="de77cc8f-e25d-0018-4000-0f4e12edc926" />
	</SchemeIndex>
</SchemeTable>