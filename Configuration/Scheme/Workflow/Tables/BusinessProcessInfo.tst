<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="5640ffb9-ef7c-4584-8793-57da90e82fa0" Name="BusinessProcessInfo" Group="WorkflowEngine" InstanceType="Cards" ContentType="Entries">
	<Description>Секция с основной информацией о бизнес-процессе.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="5640ffb9-ef7c-0084-2000-07da90e82fa0" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="5640ffb9-ef7c-0184-4000-07da90e82fa0" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="08d8a253-0ba9-416b-943b-1699364c7d53" Name="Name" Type="String(128) Not Null">
		<Description>Имя процесса</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="53f09e36-e4c8-4c7a-b8ea-65462169f483" Name="StartFromCard" Type="Boolean Not Null">
		<Description>Определяет, запускается ли процесс из карточки</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="f254990e-6cf0-40c0-91d8-7c9593c9eead" Name="df_BusinessProcessInfo_StartFromCard" Value="true" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="b4a9d87e-1776-4e20-b59a-de0bc01c88fb" Name="Multiple" Type="Boolean Not Null">
		<Description>Определяет, может ли процесс быть запущен в нескольких экземплярах</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="81fdd271-093e-4dba-8e66-bc785cd93f9e" Name="df_BusinessProcessInfo_Multiple" Value="false" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="5c26482c-0ab6-450c-9659-5f1ec341f2df" Name="ConditionModified" Type="DateTime Null">
		<Description>Дата последнего изменения скриптов-условий в тайлах</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="62578901-0a86-4974-a8f1-ebddae616e4f" Name="Group" Type="String(128) Not Null">
		<Description>Группа</Description>
		<SchemeDefaultConstraint IsPermanent="true" ID="394e74f1-865e-49e7-b8d4-3beb031c3570" Name="df_BusinessProcessInfo_Group" Value="" />
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="e17d4d16-74cd-4ec0-a41c-cd8773d6a5b7" Name="LockMessage" Type="String(Max) Null">
		<Description>Сообщение об блокировка процесса, выводимое пользователю</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="8b7e3737-eaab-4fb7-a2ea-1d2f2f9e597e" Name="ErrorMessage" Type="String(Max) Null">
		<Description>Сообщение об ошибке, выводимое пользователю</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="5640ffb9-ef7c-0084-5000-07da90e82fa0" Name="pk_BusinessProcessInfo" IsClustered="true">
		<SchemeIndexedColumn Column="5640ffb9-ef7c-0184-4000-07da90e82fa0" />
	</SchemePrimaryKey>
</SchemeTable>