<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="ae4e68f0-ff8e-4055-9386-f601f1f3c664" Name="AcquaintanceComments" Group="Common">
	<Description>Строки по данным для комментариев, отправленных на массовое ознакомление. По одной строке для каждой отправки на ознакомление с непустым комментарием, при этом в отправке может быть указано несколько ролей, в каждой из которых несколько сотрудников.</Description>
	<SchemePhysicalColumn ID="ab53d587-0dc1-4695-822a-ffc50cc472a2" Name="ID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="3dd031b5-2ff5-4f68-a1ab-61afbe4b736e" Name="Comment" Type="String(440) Null">
		<Description>Комментарий при отправке на ознакомление.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="b1d69b4c-7453-4859-96f1-ac48afb36965" Name="pk_AcquaintanceComments" IsClustered="true">
		<SchemeIndexedColumn Column="ab53d587-0dc1-4695-822a-ffc50cc472a2" />
	</SchemePrimaryKey>
	<SchemeIndex ID="a1c9d39f-39e5-46ba-8954-1d14b1820298" Name="ndx_AcquaintanceComments_Comment" SupportsPostgreSql="false">
		<SchemeIndexedColumn Column="3dd031b5-2ff5-4f68-a1ab-61afbe4b736e" />
	</SchemeIndex>
	<SchemeIndex ID="ac9e5180-9baa-425c-a167-7f612820b3c4" Name="ndx_AcquaintanceComments_Comment_ac9e5180" SupportsSqlServer="false" Type="GIN">
		<SchemeIndexedColumn Column="3dd031b5-2ff5-4f68-a1ab-61afbe4b736e">
			<Expression Dbms="PostgreSql">"Comment" gin_trgm_ops</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>