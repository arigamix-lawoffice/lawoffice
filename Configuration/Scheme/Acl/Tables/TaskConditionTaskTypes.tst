<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="bc3e1376-a8e1-4256-bf84-1bfc7a49c95f" ID="f7cd6753-21d7-4095-8c3a-e7175f591ad3" Name="TaskConditionTaskTypes" Group="Acl" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Типы заданий для условий проверки заданий.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7cd6753-21d7-0095-2000-07175f591ad3" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="f7cd6753-21d7-0195-4000-07175f591ad3" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7cd6753-21d7-0095-3100-07175f591ad3" Name="RowID" Type="Guid Not Null" />
	<SchemeComplexColumn ID="fd15ceae-1b19-424e-be33-1e52b2ef13cb" Name="Type" Type="Reference(Typified) Not Null" ReferencedTable="b0538ece-8468-4d0b-8b4e-5a1d43e024db">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="fd15ceae-1b19-004e-4000-0e52b2ef13cb" Name="TypeID" Type="Guid Not Null" ReferencedColumn="a628a864-c858-4200-a6b7-da78c8e6e1f4" />
		<SchemeReferencingColumn ID="76784239-2e07-4a93-829d-cd6b700e056e" Name="TypeName" Type="String(128) Not Null" ReferencedColumn="71181642-0d62-45f9-8ad8-ccec4bd4ce22" />
		<SchemeReferencingColumn ID="41cf7feb-cc78-47b6-94ed-3f1cd769f7ff" Name="TypeCaption" Type="String(128) Not Null" ReferencedColumn="0a02451e-2e06-4001-9138-b4805e641afa" />
	</SchemeComplexColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7cd6753-21d7-0095-5000-07175f591ad3" Name="pk_TaskConditionTaskTypes">
		<SchemeIndexedColumn Column="f7cd6753-21d7-0095-3100-07175f591ad3" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="f7cd6753-21d7-0095-7000-07175f591ad3" Name="idx_TaskConditionTaskTypes_ID" IsClustered="true">
		<SchemeIndexedColumn Column="f7cd6753-21d7-0195-4000-07175f591ad3" />
	</SchemeIndex>
</SchemeTable>