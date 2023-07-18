<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="dd8eeaba-9042-4fb5-9e8e-f7544463464f" ID="9d29f065-3c4b-4209-af8d-10b699895231" Name="WorkflowEngineLogLevels" Group="WorkflowEngine">
	<Description>Уровни логирования в WorkflowEngine</Description>
	<SchemePhysicalColumn ID="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="13cfc11d-4b9b-429e-a7d6-0688c814d3fc" Name="Name" Type="String(128) Not Null" />
	<SchemePrimaryKey ID="1729aebe-756b-4d39-9404-90823c5c5510" Name="pk_WorkflowEngineLogLevels">
		<SchemeIndexedColumn Column="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f">0</ID>
		<Name ID="13cfc11d-4b9b-429e-a7d6-0688c814d3fc">None</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f">1</ID>
		<Name ID="13cfc11d-4b9b-429e-a7d6-0688c814d3fc">Error</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f">2</ID>
		<Name ID="13cfc11d-4b9b-429e-a7d6-0688c814d3fc">Info</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="8ebfe0c6-6728-4c78-8577-4d94d2d2c47f">3</ID>
		<Name ID="13cfc11d-4b9b-429e-a7d6-0688c814d3fc">Debug</Name>
	</SchemeRecord>
</SchemeTable>