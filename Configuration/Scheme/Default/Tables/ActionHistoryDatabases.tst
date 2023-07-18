<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="db0969a9-e71d-405d-bf86-15f263cf69c8" Name="ActionHistoryDatabases" Group="System">
	<Description>Базы данных для хранения истории действий.</Description>
	<SchemePhysicalColumn ID="c2197dfc-e396-4d0c-9543-09fb0aee2218" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор строки с информацией о базе данных.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="778e565f-fd4c-4787-b63e-c6920bb7fa27" Name="Name" Type="String(128) Not Null">
		<Description>Человекочитаемое название базы данных.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b0afa917-cc46-48a7-bd38-0a27ed751308" Name="Description" Type="String(Max) Null">
		<Description>Текстовое описание. Необязательное поле.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="4560f689-0b12-40cf-a803-321dc0f212f1" Name="ConfigurationString" Type="String(128) Null">
		<Description>Алиас строки подключения к базе данных из файла конфигурации.</Description>
	</SchemePhysicalColumn>
	<SchemeUniqueKey ID="257c1c98-d083-4f7b-989d-26a6d738cabe" Name="ndx_ActionHistoryDatabases_ConfigurationString">
		<SchemeIndexedColumn Column="4560f689-0b12-40cf-a803-321dc0f212f1" />
	</SchemeUniqueKey>
	<SchemePrimaryKey ID="f06a4e35-d72c-475c-bc49-fa6bd42b1bde" Name="pk_ActionHistoryDatabases" IsClustered="true">
		<SchemeIndexedColumn Column="c2197dfc-e396-4d0c-9543-09fb0aee2218" />
	</SchemePrimaryKey>
</SchemeTable>