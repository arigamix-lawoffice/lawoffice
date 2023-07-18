<?xml version="1.0" encoding="utf-8"?>
<SchemeTable Partition="d1b372f3-7565-4309-9037-5e5a0969d94e" ID="496c30f2-79d0-408a-8085-95b43d67a22b" Name="KrStageTemplateGroupPosition" Group="Kr">
	<Description>Позиции, куда необходимо подставлять этапы из шаблона этапа KrStageTemplate</Description>
	<SchemePhysicalColumn ID="2357fdc4-35e8-4200-b626-a21027473232" Name="ID" Type="Int32 Not Null" />
	<SchemePhysicalColumn ID="bf262a11-5e78-4752-9dc7-795ae179eca7" Name="Name" Type="String(50) Not Null" />
	<SchemePrimaryKey ID="f37753dd-66a3-4b9c-9080-025cd73a70f9" Name="pk_KrStageTemplateGroupPosition">
		<SchemeIndexedColumn Column="2357fdc4-35e8-4200-b626-a21027473232" />
	</SchemePrimaryKey>
	<SchemeRecord>
		<ID ID="2357fdc4-35e8-4200-b626-a21027473232">0</ID>
		<Name ID="bf262a11-5e78-4752-9dc7-795ae179eca7">$Views_KrStageTemplateGroupPosition_AtFirst</Name>
	</SchemeRecord>
	<SchemeRecord>
		<ID ID="2357fdc4-35e8-4200-b626-a21027473232">1</ID>
		<Name ID="bf262a11-5e78-4752-9dc7-795ae179eca7">$Views_KrStageTemplateGroupPosition_AtLast</Name>
	</SchemeRecord>
</SchemeTable>