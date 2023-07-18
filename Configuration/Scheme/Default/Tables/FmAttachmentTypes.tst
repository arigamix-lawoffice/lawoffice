<?xml version="1.0" encoding="utf-8"?>
<SchemeTable ID="74caae68-ee60-4d36-b6af-b81bdd06d4a3" Name="FmAttachmentTypes" Group="Fm">
	<Description>Типы прикрепленных элементов (файлы, ссылки и пр)</Description>
	<SchemePhysicalColumn ID="9c3cfae0-6106-42dd-81f3-eb05fc71e011" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="1b5d3bfe-97ce-4449-9b23-7321a274c2be" Name="Name" Type="String(256) Not Null" />
	<SchemePrimaryKey ID="fa02a121-a5cf-4cb5-9954-f267d060302f" Name="pk_FmAttachmentTypes">
		<SchemeIndexedColumn Column="9c3cfae0-6106-42dd-81f3-eb05fc71e011" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="9c3cfae0-6106-42dd-81f3-eb05fc71e011">0</ID>
		<Name ID="1b5d3bfe-97ce-4449-9b23-7321a274c2be">File</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9c3cfae0-6106-42dd-81f3-eb05fc71e011">1</ID>
		<Name ID="1b5d3bfe-97ce-4449-9b23-7321a274c2be">Link</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9c3cfae0-6106-42dd-81f3-eb05fc71e011">2</ID>
		<Name ID="1b5d3bfe-97ce-4449-9b23-7321a274c2be">InnerItem</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="9c3cfae0-6106-42dd-81f3-eb05fc71e011">3</ID>
		<Name ID="1b5d3bfe-97ce-4449-9b23-7321a274c2be">ExternalInnerItem</Name>
	</SchemeRecord>
</SchemeTable>