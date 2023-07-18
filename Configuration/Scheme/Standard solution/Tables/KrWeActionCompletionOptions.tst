<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="6a24d3cd-ec83-4e7a-8815-77b054c69371" Name="KrWeActionCompletionOptions" Group="KrWe">
	<Description>Список возможных вариантов завершения действий.</Description>
	<SchemePhysicalColumn ID="e256f95b-bb48-4bd7-b626-ac3733f2c638" Name="ID" Type="Guid Not Null" IsRowGuidColumn="true">
		<Description>Идентификатор варианта завершения действия.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="72a16177-c566-43da-a251-bc6529f5fbe4" Name="Name" Type="String(128) Not Null">
		<Description>Имя варианта завершения действия.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="c475d8d0-f491-4235-a08d-cf1a8f41d72f" Name="Caption" Type="String(128) Not Null">
		<Description>Отображаемое имя варианта завершения действия.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="52f25f1a-6ac6-40f4-9f82-50bc8f77ae63" Name="pk_KrWeActionCompletionOptions" IsClustered="true">
		<SchemeIndexedColumn Column="e256f95b-bb48-4bd7-b626-ac3733f2c638" />
	</SchemePrimaryKey>
	<SchemeIndex ID="f1524690-8bbb-49cf-b5e5-8d18a1a8648e" Name="ndx_KrWeActionCompletionOptions_Name" IsUnique="true">
		<SchemeIndexedColumn Column="72a16177-c566-43da-a251-bc6529f5fbe4">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
	<SchemeRecord>
		<ID ID="e256f95b-bb48-4bd7-b626-ac3733f2c638">4339a03f-234d-4a9a-a6e4-58a88a5a03ce</ID>
		<Name ID="72a16177-c566-43da-a251-bc6529f5fbe4">Approved</Name>
		<Caption ID="c475d8d0-f491-4235-a08d-cf1a8f41d72f">$KrAction_ActionCompletionOption_Approved</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e256f95b-bb48-4bd7-b626-ac3733f2c638">6fbdd34b-be9a-40bf-90cb-1640d4abb9f5</ID>
		<Name ID="72a16177-c566-43da-a251-bc6529f5fbe4">Disapproved</Name>
		<Caption ID="c475d8d0-f491-4235-a08d-cf1a8f41d72f">$KrAction_ActionCompletionOption_Disapproved</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e256f95b-bb48-4bd7-b626-ac3733f2c638">fa94b7bf-7b99-46d6-9c65-b21a483ebc45</ID>
		<Name ID="72a16177-c566-43da-a251-bc6529f5fbe4">Signed</Name>
		<Caption ID="c475d8d0-f491-4235-a08d-cf1a8f41d72f">$KrAction_ActionCompletionOption_Signed</Caption>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="e256f95b-bb48-4bd7-b626-ac3733f2c638">4a1936c7-1f94-4897-9dae-934163e2fe1c</ID>
		<Name ID="72a16177-c566-43da-a251-bc6529f5fbe4">Declined</Name>
		<Caption ID="c475d8d0-f491-4235-a08d-cf1a8f41d72f">$KrAction_ActionCompletionOption_Declined</Caption>
	</SchemeRecord>
</SchemeTable>