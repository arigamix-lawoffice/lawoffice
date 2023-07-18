<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="05394727-2b6f-4d59-9900-d95bc8effdc5" Name="WfSatellite" Group="Wf" InstanceType="Cards" ContentType="Entries">
	<Description>Основная секция карточки-сателлита для бизнес-процессов Workflow.</Description>
	<SchemeComplexColumn IsSystem="true" IsPermanent="true" IsSealed="true" ID="05394727-2b6f-0059-2000-095bc8effdc5" Name="ID" Type="Reference(Typified) Not Null" ReferencedTable="1074eadd-21d7-4925-98c8-40d1e5f0ca0e">
		<SchemeReferencingColumn IsSystem="true" IsPermanent="true" ID="05394727-2b6f-0159-4000-095bc8effdc5" Name="ID" Type="Guid Not Null" ReferencedColumn="9a58123b-b2e9-4137-9c6c-5dab0ec02747" />
	</SchemeComplexColumn>
	<SchemePhysicalColumn ID="c3f2c79b-79e1-4b40-8465-80a32a99505e" Name="Data" Type="BinaryJson Null">
		<Description>Неструктурированные данные по всем бизнес-процессам в сателлите, или Null, если такие данные отсутствуют.</Description>
	</SchemePhysicalColumn>
	<SchemePrimaryKey IsSystem="true" IsPermanent="true" IsSealed="true" ID="05394727-2b6f-0059-5000-095bc8effdc5" Name="pk_WfSatellite" IsClustered="true">
		<SchemeIndexedColumn Column="05394727-2b6f-0159-4000-095bc8effdc5" />
	</SchemePrimaryKey>
</SchemeTable>