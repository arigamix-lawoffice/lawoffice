<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="df1d09a4-5ef2-4f2b-885e-c4ad6df06555" Name="ActionHistoryDatabasesVirtual" Group="System" IsVirtual="true" InstanceType="Cards" ContentType="Collections">
	<Description>Базы данных для хранения истории действий. Виртуальная таблица, обеспечивающая редактирование таблицы ActionHistoryDatabases через карточку настроек.
Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка DatabaseID - идентификатору базы данных, т.е. аналог ActionHistoryDatabases.ID.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df1d09a4-5ef2-002b-2000-04ad6df06555" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="df1d09a4-5ef2-012b-4000-04ad6df06555" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="df1d09a4-5ef2-002b-3100-04ad6df06555" Name="RowID" Type="Guid Not Null" />
	<SchemePhysicalColumn ID="87950d6c-4f12-4c7c-a485-76ab9a66405b" Name="DatabaseID" Type="Int16 Not Null">
		<Description>Идентификатор базы данных, т.е. аналог ActionHistoryDatabases.ID.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="4c833e6f-85a6-4897-9d78-680954b5f7fc" Name="df_ActionHistoryDatabasesVirtual_DatabaseID" Value="1" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bdcc1a3b-e167-4a07-b908-6d18557d02fd" Name="DatabaseIDText" Type="String(32) Not Null">
		<Description>Текстовая информация по идентификатору DatabaseID и признаку IsDefault.
Используется для удобства отображения.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8acabc48-cdd3-4062-b555-e7130b7129db" Name="Name" Type="String(128) Not Null">
		<Description>Человекочитаемое название базы данных.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="00be1e37-18cc-419b-9f08-d0750fa0fb52" Name="Description" Type="String(Max) Null">
		<Description>Текстовое описание. Необязательное поле.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="fe8e1e65-46c0-49c2-b4b3-c6232ed39fe6" Name="ConfigurationString" Type="String(128) Null">
		<Description>Алиас строки подключения к базе данных из файла конфигурации.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0b22935d-e454-4d21-a728-bea3f468058b" Name="IsDefault" Type="Boolean Not Null">
		<Description>Признак того, что текущая запись является настройкой по умолчанию.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="d4138e89-01b7-4bc9-8880-c7a9d014f115" Name="df_ActionHistoryDatabasesVirtual_IsDefault" Value="false" />
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="df1d09a4-5ef2-002b-5000-04ad6df06555" Name="pk_ActionHistoryDatabasesVirtual">
		<SchemeIndexedColumn Column="df1d09a4-5ef2-002b-3100-04ad6df06555" />
	</SchemePrimaryKey>
	<SchemeIndex IsSystem="true" IsPermanent="true" IsSealed="true" ID="df1d09a4-5ef2-002b-7000-04ad6df06555" Name="idx_ActionHistoryDatabasesVirtual_ID" IsClustered="true">
		<SchemeIndexedColumn Column="df1d09a4-5ef2-012b-4000-04ad6df06555" />
	</SchemeIndex>
</SchemeTable>