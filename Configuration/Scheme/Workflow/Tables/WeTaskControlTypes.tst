<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="ab612473-e0a2-4dd7-b05e-d9bbdf06b62f" Name="WeTaskControlTypes" Group="WorkflowEngine">
	<Description>Список доступных манипуляций над заданием из действия Управление заданием</Description>
	<SchemePhysicalColumn ID="d10c759b-79b0-4a8f-ae83-fbf86debcf7a" Name="ID" Type="Int32 Not Null">
		<Description>Идентификатор</Description>
	</SchemePhysicalColumn>
	<SchemePhysicalColumn ID="6181923c-bd93-4e41-977c-427f083995e4" Name="Name" Type="String(128) Not Null">
		<Description>Имя типа</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey ID="16f3921b-1adc-42ec-ae39-7151b8f4a6ca" Name="pk_WeTaskControlTypes" IsClustered="true">
		<SchemeIndexedColumn Column="d10c759b-79b0-4a8f-ae83-fbf86debcf7a" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="d10c759b-79b0-4a8f-ae83-fbf86debcf7a">0</ID>
		<Name ID="6181923c-bd93-4e41-977c-427f083995e4">$WorkflowEngine_TaskControlTypes_DeleteTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="d10c759b-79b0-4a8f-ae83-fbf86debcf7a">1</ID>
		<Name ID="6181923c-bd93-4e41-977c-427f083995e4">$WorkflowEngine_TaskControlTypes_UpdateTask</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="d10c759b-79b0-4a8f-ae83-fbf86debcf7a">2</ID>
		<Name ID="6181923c-bd93-4e41-977c-427f083995e4">$WorkflowEngine_TaskControlTypes_CompleteTask</Name>
	</SchemeRecord>
</SchemeTable>