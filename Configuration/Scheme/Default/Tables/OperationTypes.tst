<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="b23fccd5-5ba1-45b6-a0ad-e9d0cf730da0" Name="OperationTypes" Group="System">
	<Description>Типы операций</Description>
	<SchemePhysicalColumn ID="6096f85d-06b5-433a-9219-d0ec5f045561" Name="ID" Type="Guid Not Null">
		<Description>Идентификатор типа операции.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="35985e42-276a-4ed3-84f4-aa154ac3a4df" Name="Name" Type="String(128) Not Null">
		<Description>Отображаемое имя типа операции.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="5b6fcc6c-34ae-43ae-bc5b-05e8d4720572" Name="pk_OperationTypes">
		<SchemeIndexedColumn Column="6096f85d-06b5-433a-9219-d0ec5f045561" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">033e01ef-5913-4588-80c0-61567323577d</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_SavingCard</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">00000000-0000-0000-0000-000000000000</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_Unnamed</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">f3bd681e-861d-4820-8fa5-b2443b20dbba</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_CalculatingCalendar</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">2a7364c5-d927-4b50-8c67-02d241765e5f</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_ImportingCard</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">730e24fd-52ee-4bfc-9ce0-9daf3283fabe</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_FileConvert</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">de7d7a67-fa4b-4c80-a7f2-b9e45095f6c8</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_CalculatingRoles</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">9d7d2fe7-6c05-42c9-a32d-63076c1b5cef</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_CalculatingAD</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">de6c8d23-53e2-4659-b43c-0eea4f0fec19</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_WorkflowEngineAsync</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">333ee6b8-6468-4e0e-9ac0-c73db83919dc</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_SendingForumsNotifications</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">6d10f621-d73b-449a-baf2-68be18c60689</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_AclCalculation</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">0b663cca-c724-404f-8300-40d2b60392c2</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_CalculatingSmartRoles</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">06e3df9e-0820-4ee4-b32c-fa6c3218e99b</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_DeferredDeletion</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="6096f85d-06b5-433a-9219-d0ec5f045561">b8f6298c-2d53-446f-9007-7849ef050b5e</ID>
		<Name ID="35985e42-276a-4ed3-84f4-aa154ac3a4df">$Enum_OperationTypes_TextRecognition</Name>
	</SchemeRecord>
</SchemeTable>