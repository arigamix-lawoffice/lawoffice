<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="dc9eb404-c42d-40ab-a4c0-3b8b6089b926" Name="WeTaskGroupActionOptionTypes" Group="WorkflowEngine">
	<Description>Список допустимых условий выполнения перехода</Description>
	<SchemePhysicalColumn ID="42e8f9cb-9637-47a7-84d5-368f9834e248" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="1f9a9050-e03e-4971-a244-c069f6e0ca19" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="9962196f-db77-424b-916c-a4052919c49b" Name="pk_WeTaskGroupActionOptionTypes" IsClustered="true">
		<SchemeIndexedColumn Column="42e8f9cb-9637-47a7-84d5-368f9834e248" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="42e8f9cb-9637-47a7-84d5-368f9834e248">0</ID>
		<Name ID="1f9a9050-e03e-4971-a244-c069f6e0ca19">$WorkflowEngine_TaskGroupOptionTypes_OneNow</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="42e8f9cb-9637-47a7-84d5-368f9834e248">1</ID>
		<Name ID="1f9a9050-e03e-4971-a244-c069f6e0ca19">$WorkflowEngine_TaskGroupOptionTypes_OneAfterAll</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="42e8f9cb-9637-47a7-84d5-368f9834e248">2</ID>
		<Name ID="1f9a9050-e03e-4971-a244-c069f6e0ca19">$WorkflowEngine_TaskGroupOptionTypes_All</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="42e8f9cb-9637-47a7-84d5-368f9834e248">3</ID>
		<Name ID="1f9a9050-e03e-4971-a244-c069f6e0ca19">$WorkflowEngine_TaskGroupOptionTypes_AfterFinish</Name>
	</SchemeRecord>
</SchemeTable>