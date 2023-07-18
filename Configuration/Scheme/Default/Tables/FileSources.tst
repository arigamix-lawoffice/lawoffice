<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="e8300fe5-3b24-4c27-a45a-6cd8575bfcd5" Name="FileSources" Group="System">
	<Description>Способы хранения файлов.</Description>
	<SchemePhysicalColumn ID="983cbcc4-c185-43fd-a57c-edef94a23551" Name="ID" Type="Int16 Not Null">
		<Description>Идентификатор способа хранения файлов.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e1be5ca6-9f7a-4ebf-991d-d6892bf52e62" Name="Name" Type="String(128) Not Null">
		<Description>Название способа хранения файлов.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="ff199ab4-5038-4534-bde4-06018dfd1b5b" Name="Path" Type="String(255) Not Null">
		<Description>Если IsDatabase=True, то это название строки подключения к базе данных из конфигурационного файла.
Если IsDatabase=False, то это полный путь к файловой папке.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="bc2a09f5-c721-4134-b87f-7f1757d548cd" Name="IsDatabase" Type="Boolean Not Null">
		<Description>True, если запись соответствует контенту в базе данных;
False, если запись соответствует контента на файловой системе.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="32d1ba75-5ced-486a-9f73-19c57a9b1b6d" Name="df_FileSources_IsDatabase" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="d36159ad-efd9-4d3a-a3a3-ca923b63cbb5" Name="Description" Type="String(Max) Null">
		<Description>Текстовое описание источника файлов. Необязательные комментарии.</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="799cb3e0-d9db-4e00-ad2c-efa0485ca643" Name="Size" Type="Int32 Not Null">
		<Description>Текущий размер занятого места в файловой папке или в базе данных. Не задаётся и не используется системой.
Рекомендуется для реализации логики в расширениях, связанной с выбором источника файлов в зависимости от свободного места.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="c65e5a90-d5e1-4b65-a19a-dd8bf1ff3ebf" Name="df_FileSources_Size" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="0f36e24f-4298-4bf6-a92b-4c0b658ea446" Name="MaxSize" Type="Int32 Not Null">
		<Description>Максимальный размер занятого места в файловой папке или в базе данных. Не задаётся и не используется системой.
Рекомендуется для реализации логики в расширениях, связанной с выбором источника файлов в зависимости от свободного места.</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="253f2775-20bf-41a5-a839-cf19f2f4e6e4" Name="df_FileSources_MaxSize" Value="0" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6d250973-ece2-40a8-a946-34b667ae99f4" Name="FileExtensions" Type="String(Max) Null">
		<Description>Расширения файлов, которые будут размещаться в этом местоположении независимо от местоположения по умолчанию.
Несколько расширений указывается без точки и разделяется пробелом.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="71d11f1d-1ce9-4375-8c69-abd5a5d05317" Name="pk_FileSources" IsClustered="true">
		<SchemeIndexedColumn Column="983cbcc4-c185-43fd-a57c-edef94a23551" />
	</SchemePrimaryKey>
	<SchemeIndex ID="490cc11c-779e-4ec0-8839-7d3a02f988e1" Name="ndx_FileSources_Name" IsUnique="true">
		<SchemeIndexedColumn Column="e1be5ca6-9f7a-4ebf-991d-d6892bf52e62">
			<Expression Dbms="PostgreSql">lower("Name")</Expression>
		</SchemeIndexedColumn>
	</SchemeIndex>
</SchemeTable>